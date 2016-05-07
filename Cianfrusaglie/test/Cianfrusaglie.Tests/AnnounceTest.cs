using System.Linq;
using Cianfrusaglie.Controllers;
using Cianfrusaglie.Models;
using Cianfrusaglie.Services;
using Cianfrusaglie.ViewModels.Account;
using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;


namespace Cianfrusaglie.Tests
{
    public class AnnounceTest : BaseTestSetup
    {
        public AnnounceTest()
        {

        }

        [Fact]
        public async void UserTriesToDeleteAnnounceOfOthers()
        {
            var announce = new Announce();
            var usr = Context.Users.Single(u => u.UserName.Equals(SecondUserName));
            announce.Author = usr;
            announce.Title = "Un annuncio bello bello";
            announce.Description = "Sono bello";
            announce.GeoCoordinate = new GeoCoordinateEntity();
            Context.Announces.Add(announce);
            Context.SaveChanges();
            var idAnnounce = Context.Announces.Single(a => a.Author.Equals(usr) && a.Title.Equals(announce.Title) &&a.GeoCoordinate.Equals(announce.GeoCoordinate)).Id;

            var loginViewModel = new LoginViewModel() { Password = CommonUserPassword, RememberMe = true, UserName = "pippopippo1" };
            var id = Context.Users.Single(u => u.UserName.Equals(loginViewModel.UserName)).Id;
            var accountController = CreateAccountController();
            await accountController.Login(loginViewModel);

            var announceController = CreateAnnounceController(id, loginViewModel.UserName);
            var res = announceController.Delete(idAnnounce);
            Assert.IsType<BadRequestResult>(res);
        }

        [Fact]
        public async void UserTriesToEditAnnounceOfOthers()
        {
            var announce = Context.Announces.Single(a => a.Author.UserName.Equals(SecondUserName));
            var loginViewModel = new LoginViewModel() { Password = CommonUserPassword, RememberMe = true, UserName = FirstUserName };
            var id = Context.Users.Single(u => u.UserName.Equals(loginViewModel.UserName)).Id;
            var accountController = CreateAccountController();
            await accountController.Login(loginViewModel);
            var announceController = CreateAnnounceController(id, loginViewModel.UserName);
            announce.Description = "Ho cambiato qualcosa";
            var res = announceController.Edit(announce);
            Assert.IsType<BadRequestResult>(res);
        }

        [Fact]
        public async void CorrectInsertionIsOk()
        {
            var loginViewModel = new LoginViewModel() { Password = CommonUserPassword, RememberMe = true, UserName = FirstUserName };
            var usr = Context.Users.Single(u => u.UserName.Equals(loginViewModel.UserName));
            var accountController = CreateAccountController();
            await accountController.Login(loginViewModel);
            var announce = new Announce();
            
            announce.Author = usr;
            announce.Title = "Un annuncio bello bello";
            announce.Description = "Sono bello";
            announce.GeoCoordinate = new GeoCoordinateEntity();
            Context.Announces.Add(announce);
            Context.SaveChanges();

        }
    }
}
