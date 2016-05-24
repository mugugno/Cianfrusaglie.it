﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cianfrusaglie.Controllers;
using Cianfrusaglie.Models;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;
using Xunit;

namespace Cianfrusaglie.Tests
{
    public class FeedbackControllerTest : BaseTestSetup
    {
        protected FeedbackController CreateFeedbackController( string userId ) {
            return new FeedbackController( Context ) {ActionContext = MockActionContextForLogin( userId )};
        }

        private static FeedBack CreateNewFeedback(Announce announce, User feedbackAuthor, User feedbackReceiver)
        {
            return new FeedBack
            {
                Announce = announce,
                Author = feedbackAuthor,
                DateTime = DateTime.Now,
                Receiver = feedbackReceiver,
                Vote = 2,
                Text = "buu"
            };
        }

        private void SetUserInterestedToAnnounce( Announce announce, User user ) {
            Context.Interested.Add( new Interested {Announce = announce, DateTime = DateTime.Now, User = user} );
            Context.SaveChanges();
        }

        private void SetUserInterestedToAnnounceAndChoosen( Announce announce, User user ) {
            SetUserInterestedToAnnounce( announce, user );
            var interestedUser = Context.Interested.Single( i => i.User.Equals( user ) );
            interestedUser.ChooseDate = DateTime.Now;
            Context.SaveChanges();
        }

        [Fact]
        public void InterestedUserGivesFeedbackToAuthorAndIsOk() {
            
            var announce = Context.Announces.Include( a => a.Interested ).First( a => a.Closed == false && a.DeadLine == null &&  a.Author.UserName.Equals( FirstUserName ) );
            var interestedUser = Context.Users.First( u => !u.UserName.Equals( FirstUserName ) );
            SetUserInterestedToAnnounce( announce, interestedUser );
            var feedbackController = CreateFeedbackController( interestedUser.Id );
            var actionResult = feedbackController.Create( CreateNewFeedback( announce, interestedUser, announce.Author ) );
            Assert.IsType< RedirectToActionResult >( actionResult );
        }

        [Fact]
        public void AuthorGivesFeedbackToChoosenUserAndIsOk() {
            var announce = Context.Announces.Include(a => a.Interested).First(a => a.Closed == false && a.DeadLine == null && a.Author.UserName.Equals(FirstUserName));
            var author = announce.Author;
            var interestedUser = Context.Users.First(u => !u.UserName.Equals(FirstUserName));
            SetUserInterestedToAnnounceAndChoosen( announce, interestedUser );
            var feedbackController = CreateFeedbackController( author.Id );
            var actionResult = feedbackController.Create( CreateNewFeedback( announce, author, interestedUser ) );
            Assert.IsType< RedirectToActionResult >( actionResult );
        }

        [Fact]
        public void UserTryToGiveAFeedbackForAnAnnounceThatIsNotInterestedInAndFails() {
            var announce = Context.Announces.Include(a => a.Interested).First(a => a.Closed == false && a.DeadLine == null && a.Author.UserName.Equals(FirstUserName));
            var user = Context.Users.Single( u => u.UserName.Equals( SecondUserName ) );
            var feedbackController = CreateFeedbackController( user.Id );
            var actionResult = feedbackController.Create( CreateNewFeedback( announce, user, announce.Author ) );
            Assert.IsType< BadRequestResult >( actionResult );
        }

        [Fact]
        public void AuthorGivesFeedbackToAnInterestedUserNotChoosenAndFails() {
            var announce = Context.Announces.Include(a => a.Interested).First(a => a.Closed == false && a.DeadLine == null && a.Author.UserName.Equals(FirstUserName));
            var author = announce.Author;
            var interestedUser = Context.Users.First(u => !u.UserName.Equals(FirstUserName));
            SetUserInterestedToAnnounce( announce, interestedUser );
            var feedbackController = CreateFeedbackController(author.Id);
            var actionResult = feedbackController.Create(CreateNewFeedback(announce, author, interestedUser));
            Assert.IsType<BadRequestResult>(actionResult);
        }
    }
}
