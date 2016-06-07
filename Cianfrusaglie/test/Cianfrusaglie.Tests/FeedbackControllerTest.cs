using System;
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

        [Fact]
        public void InterestedUserGivesFeedbackToAuthorAndFails()
        {
            var announce = Context.Announces.Include(a => a.Interested).First(a => a.Closed == false && a.DeadLine == null && a.Author.UserName.Equals(FirstUserName));
            var interestedUser = Context.Users.First(u => !u.UserName.Equals(FirstUserName));
            SetUserInterestedToAnnounce(announce, interestedUser);
            var feedbackController = CreateFeedbackController(interestedUser.Id);
            var actionResult = feedbackController.Create(CreateNewFeedback(announce, interestedUser, announce.Author));
            Assert.IsType<BadRequestResult>(actionResult);
        }

        [Fact]
        public void AuthorGivesFeedbackToLastChoosenUserAndIsOk() {
            var announce = Context.Announces.Include(a => a.Interested).First(a => a.Closed == false && a.DeadLine == null && a.Author.UserName.Equals(FirstUserName));
            var author = announce.Author;
            var choosenUser = Context.Users.First(u => !u.UserName.Equals(FirstUserName));
            SetUserInterestedToAnnounceAndChoosen( announce, choosenUser, 0 );
            var feedbackController = CreateFeedbackController( author.Id );
            var actionResult = feedbackController.Create( CreateNewFeedback( announce, author, choosenUser ) );
            Assert.IsType< RedirectToActionResult >( actionResult );
        }

        [Fact]
        public void AuthorGivesFeedbackToNotLastChoosenUserAndIsOk()
        {
            var announce = Context.Announces.Include(a => a.Interested).First(a => a.Closed == false && a.DeadLine == null && a.Author.UserName.Equals(FirstUserName));
            var author = announce.Author;
            var choosenUser = Context.Users.First(u => !u.UserName.Equals(FirstUserName));
            SetUserInterestedToAnnounceAndChoosen(announce, choosenUser, 0);
            var notChoosenUser = Context.Users.First(u => !u.UserName.Equals(FirstUserName) && !u.UserName.Equals( choosenUser.UserName ));
            SetUserInterestedToAnnounceAndChoosen(announce, notChoosenUser, 1);
            var feedbackController = CreateFeedbackController(author.Id);
            var actionResult = feedbackController.Create(CreateNewFeedback(announce, author, notChoosenUser));
            Assert.IsType< RedirectToActionResult >(actionResult);
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

        [Fact]
        public void ChoosenUserGivesFeedbackToClosedAnnounceAnIsOk() {
            var announce = Context.Announces.Include(a => a.Interested).First(a => a.Closed == false && a.DeadLine == null && a.Author.UserName.Equals(FirstUserName));
            var author = announce.Author;
            var choosenUser = Context.Users.First(u => !u.UserName.Equals(FirstUserName));
            SetUserInterestedToAnnounceAndChoosen( announce, choosenUser, 0 );
            CloseAnnounce( announce.Id );
            var feedbackController = CreateFeedbackController( choosenUser.Id );
            var actionResult = feedbackController.Create(CreateNewFeedback(announce, choosenUser, author));
            Assert.IsType< RedirectToActionResult >(actionResult);
        }
    }
}
