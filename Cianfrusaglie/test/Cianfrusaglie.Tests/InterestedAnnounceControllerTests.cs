using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cianfrusaglie.Controllers;
using Cianfrusaglie.Models;
using Microsoft.AspNet.Mvc;
using Xunit;

namespace Cianfrusaglie.Tests
{
    public class InterestedAnnounceControllerTests : BaseTestSetup
    {
        protected InterestedAnnounceController CreateInterestedAnnounceController( string id ) {
            return new InterestedAnnounceController( Context ) {ActionContext = MockActionContextForLogin( id )};
        }

        [Fact]
        public void UserVisualizeInterestedToAnnounceIsOk() {
            string id = Context.Users.Single( u => u.UserName.Equals( FirstUserName ) ).Id;
            var interestedController = CreateInterestedAnnounceController( id );
            int announceId = Context.Announces.First( a => a.AuthorId.Equals( id ) ).Id;
            var result = interestedController.Index( announceId );
            Assert.IsType<ViewResult>( result );
        }

        [Fact]
        public void UserTriesToViewInterestedOfAnnounceOfOtherUserAndFails() {
            string id = Context.Users.Single(u => u.UserName.Equals(FirstUserName)).Id;
            var interestedController = CreateInterestedAnnounceController(id);
            int announceId = Context.Announces.First(a => a.Author.UserName.Equals(SecondUserName)).Id;
            var result = interestedController.Index(announceId);
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void UserTriesToViewInterestedToNonExistingAnnounceAndFails() {
            string id = Context.Users.Single(u => u.UserName.Equals(FirstUserName)).Id;
            var interestedController = CreateInterestedAnnounceController(id);
            var result = interestedController.Index(909090);
            Assert.IsType<HttpNotFoundResult>(result);
        }

        [Fact]
        public void VisitorTriesToViewInterestedToAnnounceAndFails() {
            var interestedController = CreateInterestedAnnounceController(null);
            int announceId = Context.Announces.First(a => a.Author.UserName.Equals( FirstUserName )).Id;
            var result = interestedController.Index(announceId);
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void ChoseUserNotExistingOrNullIdFails() {
            var author = Context.Users.Single(u => u.UserName.Equals(SecondUserName));
            var interestedController = CreateInterestedAnnounceController(author.Id);
            var result = interestedController.ChooseUserAsReceiverForAnnounce( null, 0 );
            Assert.IsType< HttpNotFoundResult >( result );
        }

        [Fact]
        public void ChoseUserWithUnexistingAnnounceIdFails()
        {
            var user = Context.Users.Single(u => u.UserName.Equals(FirstUserName));
            var author = Context.Users.Single(u => u.UserName.Equals(SecondUserName));
            var interestedController = CreateInterestedAnnounceController(author.Id);
            var announce = Context.Announces.First(a => a.Author.UserName.Equals(SecondUserName));
            Context.Interested.Add(new Interested()
            {
                Announce = announce,
                User = user
            });
            Context.SaveChanges();
            var result = interestedController.ChooseUserAsReceiverForAnnounce(user.Id, 9090);
            Assert.IsType<HttpNotFoundResult>(result);
        }

        [Fact]
        public void UserChoseInterestedAsChosenOneAndItsOk() {
            var user = Context.Users.Single(u => u.UserName.Equals(FirstUserName));
            var author = Context.Users.Single( u => u.UserName.Equals( SecondUserName ) );
            var interestedController = CreateInterestedAnnounceController(author.Id);
            var announce = Context.Announces.First( a => a.Author.UserName.Equals( SecondUserName ) );
            Context.Interested.Add( new Interested() {
                Announce = announce,
                User = user
            } );
            Context.SaveChanges();
            var result = interestedController.ChooseUserAsReceiverForAnnounce( user.Id, announce.Id );
            Assert.IsType< RedirectToActionResult >( result );
        }

        [Fact]
        public void VisitorTriestoChoseAndFails() {
            var interestedController = CreateInterestedAnnounceController(null);
            var user = Context.Users.Single(u => u.UserName.Equals(FirstUserName));
            var announce = Context.Announces.First(a => a.Author.UserName.Equals(SecondUserName));
            Context.Interested.Add(new Interested()
            {
                Announce = announce,
                User = user
            });
            var result = interestedController.ChooseUserAsReceiverForAnnounce(user.Id,announce.Id);
            Assert.IsType< BadRequestResult >( result );
        }
    }
}
