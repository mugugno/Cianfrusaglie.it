using System.Linq;
using Cianfrusaglie.Models;
using Cianfrusaglie.ViewModels.Account;
using Microsoft.AspNet.Mvc;
using Xunit;

namespace Cianfrusaglie.Tests {
   public class AnnounceTest : BaseTestSetup {
      [Fact]
      public async void UserTriesToDeleteAnnounceOfOthers() {
         var announce = Context.Announces.Single( a => a.Author.UserName.Equals( SecondUserName ) );
         var loginViewModel = new LoginViewModel {
            Password = CommonUserPassword,
            RememberMe = true,
            UserName = FirstUserName
         };
         string id = Context.Users.Single( u => u.UserName.Equals( loginViewModel.UserName ) ).Id;
         var accountController = CreateAccountController();
         await accountController.Login( loginViewModel );
         var announceController = CreateAnnounceController( id, loginViewModel.UserName );
         announce.Description = "Ho cambiato qualcosa";
         var res = announceController.DeleteConfirmed( announce.Id );
         Assert.IsType< BadRequestResult >( res );
      }

      [Fact]
      public async void UserTriesToEditAnnounceOfOthers() {
         var announce = Context.Announces.Single( a => a.Author.UserName.Equals( SecondUserName ) );
         var loginViewModel = new LoginViewModel {
            Password = CommonUserPassword,
            RememberMe = true,
            UserName = FirstUserName
         };
         string id = Context.Users.Single( u => u.UserName.Equals( loginViewModel.UserName ) ).Id;
         var accountController = CreateAccountController();
         await accountController.Login( loginViewModel );
         var announceController = CreateAnnounceController( id, loginViewModel.UserName );
         announce.Description = "Ho cambiato qualcosa";
         var res = announceController.Edit( announce );
         Assert.IsType< BadRequestResult >( res );
      }

      [Fact]
      public async void UserDeletesHisAnnounces() {
         var loginViewModel = new LoginViewModel {
            Password = CommonUserPassword,
            RememberMe = true,
            UserName = FirstUserName
         };
         var usr = Context.Users.Single( u => u.UserName.Equals( loginViewModel.UserName ) );
         var accountController = CreateAccountController();
         await accountController.Login( loginViewModel );
         var announceController = CreateAnnounceController( usr.Id, loginViewModel.UserName );
         var announce = Context.Announces.Single( a => a.Author.Equals( usr ) );
         var res = announceController.DeleteConfirmed( announce.Id );
         Assert.DoesNotContain( announce, Context.Announces );
         Assert.IsNotType< BadRequestResult >( res );
      }

      [Fact]
      public async void CorrectInsertionIsOk() {
         var loginViewModel = new LoginViewModel {
            Password = CommonUserPassword,
            RememberMe = true,
            UserName = FirstUserName
         };
         var usr = Context.Users.Single( u => u.UserName.Equals( loginViewModel.UserName ) );
         var accountController = CreateAccountController();
         await accountController.Login( loginViewModel );
         var announceController = CreateAnnounceController( usr.Id, loginViewModel.UserName );

         var announce = new Announce {
            Author = usr,
            Title = "Un annuncio bello bello",
            Description = "Sono bello",
            GeoCoordinate = new GeoCoordinateEntity()
         };
         var res = announceController.Create( announce );


         Assert.Contains( announce, Context.Announces );
         Assert.IsNotType< BadRequestResult >( res );
      }

      [Fact]
      public async void UserEditsHisAnnounce() {
         var loginViewModel = new LoginViewModel {
            Password = CommonUserPassword,
            RememberMe = true,
            UserName = FirstUserName
         };
         var usr = Context.Users.Single( u => u.UserName.Equals( loginViewModel.UserName ) );
         var accountController = CreateAccountController();
         await accountController.Login( loginViewModel );
         var announceController = CreateAnnounceController( usr.Id, loginViewModel.UserName );
         var announce = Context.Announces.Single( a => a.Author.Equals( usr ) );
         string old = announce.Description;
         announce.Description += "Ho cambiato la descriozne ahahah";
         announceController.Edit( announce );

         var updatedAnnounce = Context.Announces.Single( a => a.Id.Equals( announce.Id ) );
         Assert.NotEqual( updatedAnnounce.Description, old );
      }

      [Fact]
      public void VisitorTriesToCreateAnnounceAndFail() {
         var announceController = CreateAnnounceController( null, null );
         var announce = new Announce {
            Title = "Un annuncio bello bello",
            Description = "Sono bello",
            GeoCoordinate = new GeoCoordinateEntity()
         };
         var res = announceController.Create( announce );
         Assert.IsType< BadRequestResult >( res );
      }

      [Fact]
      public void VisitorTriesToEditAnnounceAndFail() {
         var announceController = CreateAnnounceController( null, null );
         var announce = Context.Announces.Single( a => a.Author.UserName.Equals( FirstUserName ) );
         var res = announceController.Edit( announce );
         Assert.IsType< BadRequestResult >( res );
      }

      [Fact]
      public void VisitorTriesToDeleteAnnounceAndFail() {
         var announceController = CreateAnnounceController( null, null );
         var announce = Context.Announces.Single( a => a.Author.UserName.Equals( FirstUserName ) );
         var res = announceController.DeleteConfirmed( announce.Id );
         Assert.IsType< BadRequestResult >( res );
      }

      [Fact]
      public async void UserTriesToDeleteAlreadyDeletedAnnounce() {
         var loginViewModel = new LoginViewModel {
            Password = CommonUserPassword,
            RememberMe = true,
            UserName = FirstUserName
         };
         var usr = Context.Users.Single( u => u.UserName.Equals( loginViewModel.UserName ) );
         var accountController = CreateAccountController();
         await accountController.Login( loginViewModel );
         var announceController = CreateAnnounceController( usr.Id, loginViewModel.UserName );
         var announce = Context.Announces.Single( a => a.Author.Equals( usr ) );
         announceController.DeleteConfirmed( announce.Id );

         var res = announceController.DeleteConfirmed( announce.Id );
         Assert.IsType< BadRequestResult >( res );
      }

      [Fact]
      public async void RequestingExistingAnnounceIsCorrectlyVisualized() {
         var announce = Context.Announces.First();
         var loginViewModel = new LoginViewModel {
            Password = CommonUserPassword,
            RememberMe = true,
            UserName = FirstUserName
         };
         var usr = Context.Users.Single( u => u.UserName.Equals( loginViewModel.UserName ) );
         var accountController = CreateAccountController();
         await accountController.Login( loginViewModel );
         var announceController = CreateAnnounceController( usr.Id, loginViewModel.UserName );
         var res = announceController.Details( announce.Id );
         Assert.IsNotType< BadRequestResult >( res );
      }

      [Fact]
      public async void RequestingExistingAnnounceIsCorrectlyVisualizedForEdit() {
         var announce = Context.Announces.First();
         var loginViewModel = new LoginViewModel {
            Password = CommonUserPassword,
            RememberMe = true,
            UserName = FirstUserName
         };
         var usr = Context.Users.Single( u => u.UserName.Equals( loginViewModel.UserName ) );
         var accountController = CreateAccountController();
         await accountController.Login( loginViewModel );
         var announceController = CreateAnnounceController( usr.Id, loginViewModel.UserName );
         var res = announceController.Edit( announce.Id );
         Assert.IsNotType< BadRequestResult >( res );
      }

      [Fact]
      public async void RequestingExistingAnnounceIsCorrectlyVisualizedForDelete() {
         var announce = Context.Announces.First();
         var loginViewModel = new LoginViewModel {
            Password = CommonUserPassword,
            RememberMe = true,
            UserName = FirstUserName
         };
         var usr = Context.Users.Single( u => u.UserName.Equals( loginViewModel.UserName ) );
         var accountController = CreateAccountController();
         await accountController.Login( loginViewModel );
         var announceController = CreateAnnounceController( usr.Id, loginViewModel.UserName );
         var res = announceController.Delete( announce.Id );
         Assert.IsNotType< BadRequestResult >( res );
      }

      [Fact]
      public void RequestNotExistingAnnounceForView() {
         var announceController = CreateAnnounceController( null, null );
         var res = announceController.Details( 20 );
         Assert.IsType< HttpNotFoundResult >( res );
      }

      [Fact]
      public async void RequestNotExistingAnnounceForEdit() {
         var loginViewModel = new LoginViewModel {
            Password = CommonUserPassword,
            RememberMe = true,
            UserName = FirstUserName
         };
         var usr = Context.Users.Single( u => u.UserName.Equals( loginViewModel.UserName ) );
         var accountController = CreateAccountController();
         await accountController.Login( loginViewModel );
         var announceController = CreateAnnounceController( usr.Id, loginViewModel.UserName );
         var res = announceController.Edit( 20 );
         Assert.IsType< HttpNotFoundResult >( res );
      }

      [Fact]
      public async void RequestNotExistingAnnounceForDelete() {
         var loginViewModel = new LoginViewModel {
            Password = CommonUserPassword,
            RememberMe = true,
            UserName = FirstUserName
         };
         var usr = Context.Users.Single( u => u.UserName.Equals( loginViewModel.UserName ) );
         var accountController = CreateAccountController();
         await accountController.Login( loginViewModel );
         var announceController = CreateAnnounceController( usr.Id, loginViewModel.UserName );
         var res = announceController.Delete( 20 );
         Assert.IsType< HttpNotFoundResult >( res );
      }
   }
}