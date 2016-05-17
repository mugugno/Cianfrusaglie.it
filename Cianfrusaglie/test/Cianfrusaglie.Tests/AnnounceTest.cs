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
            var announceController = CreateAnnounceController( null );
            var res = announceController.Details( 20 );
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
    }
}