using System;
using System.Collections.Generic;
using System.Linq;
using Cianfrusaglie.Controllers;
using Cianfrusaglie.Models;
using Cianfrusaglie.ViewModels.Announce;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc;
using Moq;
using Xunit;

namespace Cianfrusaglie.Tests {
    public class AnnounceTest : BaseTestSetup {
        protected AnnouncesController CreateAnnounceController( string id ) {
            return new AnnouncesController( Context, HostingEnvironment ) {
                ActionContext = MockActionContextForLogin( id ),
                Url = new Mock< IUrlHelper >().Object
            };
        }

        [Fact]
        public void CorrectInsertionIsOk() {
            var usr = Context.Users.Single( u => u.UserName.Equals( FirstUserName ) );
            var announceController = CreateAnnounceController( usr.Id );
            var announce = new Announce {Author = usr, Title = "Un annuncio bello bello", Description = "Sono bello"};
            var announceViewModel = new CreateAnnounceViewModel {
                Title = announce.Title,
                Description = announce.Description,
                Photos = new List< IFormFile >()
            };
            var res = announceController.Create( announceViewModel ).Result;
            Assert.True(
                Context.Announces.Any(
                    a =>
                        a.Author.Equals( usr ) && a.Title.Equals( announce.Title ) &&
                        a.Description.Equals( announce.Description ) ) );
            Assert.IsNotType< BadRequestResult >( res );
        }

        [Fact]
        public void RequestingExistingAnnounceIsCorrectlyVisualized() {
            var announce = Context.Announces.First();
            var usr = Context.Users.Single( u => u.UserName.Equals( FirstUserName ) );
            var announceController = CreateAnnounceController( usr.Id );
            var res = announceController.Details( announce.Id );
            Assert.IsNotType< BadRequestResult >( res );
        }

        [Fact]
        public void RequestingExistingAnnounceIsCorrectlyVisualizedForDelete() {
            var announce = Context.Announces.First();
            var usr = Context.Users.Single( u => u.UserName.Equals( FirstUserName ) );
            var announceController = CreateAnnounceController( usr.Id );
            var res = announceController.Delete( announce.Id );
            Assert.IsNotType< BadRequestResult >( res );
        }

        // Per il momento non vogliamo consentire l'edit di un annuncio
        //[Fact]
        //public void RequestingExistingAnnounceIsCorrectlyVisualizedForEdit() {
        //    var announce = Context.Announces.First();
        //    var usr = Context.Users.Single( u => u.UserName.Equals( FirstUserName ) );
        //    var announceController = CreateAnnounceController( usr.Id );
        //    var res = announceController.Edit( announce.Id );
        //    Assert.IsNotType< BadRequestResult >( res );
        //}

        [Fact]
        public void RequestNotExistingAnnounceForDelete() {
            var usr = Context.Users.Single( u => u.UserName.Equals( FirstUserName ) );
            var announceController = CreateAnnounceController( usr.Id );
            var res = announceController.Delete( 20 );
            Assert.IsType< HttpNotFoundResult >( res );
        }

        [Fact]
        public void RequestNotExistingAnnounceForEdit() {
            var usr = Context.Users.Single( u => u.UserName.Equals( FirstUserName ) );
            var announceController = CreateAnnounceController( usr.Id );
            var res = announceController.Edit( 20 );
            Assert.IsType< HttpNotFoundResult >( res );
        }

        [Fact]
        public void RequestNotExistingAnnounceForView() {
            var announceController = CreateAnnounceController( Context.Users.Single(u=>u.UserName.Equals(FirstUserName)).Id );
            var res = announceController.Details( 900 );
            Assert.IsType< HttpNotFoundResult >( res );
        }

        [Fact]
        public void UserDeletesHisAnnounces() {
            var usr = Context.Users.Single( u => u.UserName.Equals( FirstUserName ) );
            var announceController = CreateAnnounceController( usr.Id );
            var announce = Context.Announces.First( a => a.Author.Equals( usr ) );
            var res = announceController.DeleteConfirmed( announce.Id );
            Assert.DoesNotContain( announce, Context.Announces );
            Assert.IsNotType< BadRequestResult >( res );
        }

        [Fact]
        public void UserEditsHisAnnounce() {
            var usr = Context.Users.Single( u => u.UserName.Equals( FirstUserName ) );
            var announceController = CreateAnnounceController( usr.Id );
            var announce = Context.Announces.First( a => a.Author.Equals( usr ) );
            string old = announce.Description;
            announce.Description += "Ho cambiato la descrizione ahahah";
            announceController.Edit( announce.Id );

            var updatedAnnounce = Context.Announces.Single( a => a.Id.Equals( announce.Id ) );
            Assert.NotEqual( updatedAnnounce.Description, old );
        }

        [Fact]
        public void UserTriesToDeleteAlreadyDeletedAnnounce() {
            var usr = Context.Users.Single( u => u.UserName.Equals( FirstUserName ) );
            var announceController = CreateAnnounceController( usr.Id );
            var announce = Context.Announces.First( a => a.Author.Equals( usr ) );
            announceController.DeleteConfirmed( announce.Id );

            var res = announceController.DeleteConfirmed( announce.Id );
            Assert.IsType< BadRequestResult >( res );
        }

        [Fact]
        public void UserTriesToDeleteAnnounceOfOthers() {
            var announce = Context.Announces.First( a => a.Author.UserName.Equals( SecondUserName ) );
            string id = Context.Users.Single( u => u.UserName.Equals( FirstUserName ) ).Id;
            var announceController = CreateAnnounceController( id );
            announce.Description = "Ho cambiato qualcosa";
            var res = announceController.DeleteConfirmed( announce.Id );
            Assert.IsType< BadRequestResult >( res );
        }

        [Fact]
        public void UserTriesToViewPageDeletionAnnounceOfOthersAndFail()
        {
            var announce = Context.Announces.First(a => a.Author.UserName.Equals(SecondUserName));
            string id = Context.Users.Single(u => u.UserName.Equals(FirstUserName)).Id;
            var announceController = CreateAnnounceController(id);
            announce.Description = "Ho cambiato qualcosa";
            var res = announceController.Delete(announce.Id);
            Assert.IsType<BadRequestResult>(res);
        }

        [Fact]
        public void UserTriesToEditAnnounceOfOthers() {
            var announce = Context.Announces.First( a => a.Author.UserName.Equals( SecondUserName ) );
            string id = Context.Users.Single( u => u.UserName.Equals( FirstUserName ) ).Id;
            var announceController = CreateAnnounceController( id );
            announce.Description = "Ho cambiato qualcosa";
            var res = announceController.Edit( announce.Id );
            Assert.IsType< BadRequestResult >( res );
        }

        [Fact]
        public void VisitorTriesToCreateAnnounceAndFail() {
            var announceController = CreateAnnounceController( null );
            var announce = new Announce {Title = "Un annuncio bello bello", Description = "Sono bello"};
            var announceViewModel = new CreateAnnounceViewModel {
                Title = announce.Title,
                Description = announce.Description
            };
            var res = announceController.Create( announceViewModel ).Result;
            Assert.IsType< BadRequestResult >( res );
        }

        [Fact]
        public void VisitorTriesToDeleteAnnounceAndFail() {
            var announceController = CreateAnnounceController( null );
            var announce = Context.Announces.First( a => a.Author.UserName.Equals( FirstUserName ) );
            var res = announceController.DeleteConfirmed( announce.Id );
            Assert.IsType< BadRequestResult >( res );
        }

        [Fact]
        public void VisitorTriesToEditAnnounceAndFail() {
            var announceController = CreateAnnounceController( null );
            var announce = Context.Announces.First( a => a.Author.UserName.Equals( FirstUserName ) );
            var res = announceController.Edit( announce.Id );
            Assert.IsType< BadRequestResult >( res );
        }

        [Fact]
        public void VisitorTriesToOpenDeletePageAndFail() {
            var announceController = CreateAnnounceController( null );
            var announce = Context.Announces.First( a => a.Author.UserName.Equals( FirstUserName ) );
            var res = announceController.Delete( announce.Id );
            Assert.IsType< BadRequestResult >( res );
        }

        [Fact]
        public void VisitorTriesToViewAnnounceCreationPageAndFail() {
            var announceController = CreateAnnounceController( null );
            var result = announceController.Create();
            Assert.IsType< BadRequestResult >( result );
        }

        [Fact]
        public void GatFromEmptyAnnounceAreEmpty() {
            var announceController = CreateAnnounceController( null );                     
            var result = announceController.GenerateGats(Context.Announces.First(a=>a.Author.UserName.Equals(FirstUserName)));
            Assert.Empty( result );
        }

        [Fact]
        public void GatAreGeneratedCorrectly()
        {
            var announceController = CreateAnnounceController(null);
            string s = "Ciao";
            var gat = new Gat() { Text = s };
            var announce = Context.Announces.Single( a => a.Title == "Libro di OST di Videogiochi" );
            var announceGat = new AnnounceGat() { Announce = announce, Gat = gat };
            Context.Gats.Add(gat);
            Context.AnnounceGats.Add( announceGat );
            Context.SaveChanges();
            var result = announceController.GenerateGats(announce);
            Assert.True(result.Any( g => g.Text.Equals( s ) ));
        }

        [Fact]
        public void UserInterestedInHisOwnAnnounceAndFail()
        {
            string id = Context.Users.Single(u => u.UserName.Equals(SecondUserName)).Id;
            var announceController = CreateAnnounceController(id);
            var ann = Context.Announces.First(u => u.AuthorId.Equals(id) && u.Closed == false);
            Assert.False(announceController.Interested(ann.Id));
        }
        [Fact]
        public void UserInterestedInAnnounceAndNotNull()
        {
            string id = Context.Users.Single(u => u.UserName.Equals(FirstUserName)).Id;
            var announceController = CreateAnnounceController(id);
            var ann = Context.Announces.First(u => !u.AuthorId.Equals(id) && u.Closed==false);
            announceController.Interested(ann.Id);
            var intrstd = Context.Interested.SingleOrDefault(u => u.AnnounceId == ann.Id && u.UserId == id);
            Assert.NotNull(intrstd);
        }
        [Fact]
        public void UserUnInterestedInAnnounceAndNull()
        {
            string id = Context.Users.Single(u => u.UserName.Equals(FirstUserName)).Id;
            var announceController = CreateAnnounceController(id);
            var ann = Context.Announces.First(u => !u.AuthorId.Equals(id) && u.Closed == false);
            announceController.Interested(ann.Id);
            announceController.Interested(ann.Id);
            var intrstd = Context.Interested.SingleOrDefault(u => u.AnnounceId == ann.Id && u.UserId == id);
            Assert.Null(intrstd);
        }
        [Fact]
        public void UserInterestedInClosedAnnounceAndFail()
        {
            string id = Context.Users.Single(u => u.UserName.Equals(SecondUserName)).Id;
            var announceController = CreateAnnounceController(id);
            var ann = Context.Announces.First(u => u.Closed==true && !u.AuthorId.Equals(id));       
            Assert.False(announceController.Interested(ann.Id));
        }

        [Fact]
        public void ChoosenUserUnInterestedInAnnounceAndFail() {
            string id = Context.Users.Single(u => u.UserName.Equals(FirstUserName)).Id;
            var announceController = CreateAnnounceController(id);
            var ann = Context.Announces.First(u => !u.AuthorId.Equals(id) && u.Closed == false);
            announceController.Interested(ann.Id);
            SetUserChoosenForTheAnnounce( ann.Id, id );
            Assert.False(announceController.Interested(ann.Id));
        }

        private void SetUserChoosenForTheAnnounce( int announceId, string userId ) {
            var announce = Context.Announces.SingleOrDefault( a => a.Id.Equals( announceId ) );
            var user = Context.Users.SingleOrDefault( u => u.Id.Equals( userId ) );
            Context.AnnounceChosenUsers.Add( new AnnounceChosen {
                Announce = announce,
                ChosenDateTime = DateTime.Now,
                ChosenUser = user
            } );
        }

        public void UserTriesToRemoveInterestFromClosedAnnounceAndFails() {
            var closedAnnounce = Context.Announces.First( a=> a.Author.UserName.Equals( FirstUserName ) && !a.Closed );
            var user = Context.Users.Single( u => u.UserName.Equals( SecondUserName ) );
            var announcesController = CreateAnnounceController( user.Id );
            announcesController.Interested( closedAnnounce.Id );
            closedAnnounce.Closed = true;
            Context.SaveChanges();
            bool result = announcesController.Interested(closedAnnounce.Id);
            Assert.False( result );
        }
    }
}