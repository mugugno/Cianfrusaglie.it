using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using Cianfrusaglie.Controllers;
using Cianfrusaglie.Models;
using Cianfrusaglie.ViewModels.Account;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Http.Internal;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Cianfrusaglie.Tests {
    public class AccountControllerTests : BaseTestSetup {
        [Fact]
        public void LoggedUserTriesToLoginAndFail() {
            var user = Context.Users.Single( u => u.UserName.Equals( FirstUserName ) );
            var accountController = CreateAccountController( user.Id );
            var result = accountController.Login();
            Assert.IsType< BadRequestResult >( result );
        }

        [Fact]
        public void LoggedUserTriesToRegisterAndFail() {
            var user = Context.Users.Single( u => u.UserName.Equals( FirstUserName ) );
            var accountController = CreateAccountController( user.Id );
            var result = accountController.Register();
            Assert.IsType< BadRequestResult >( result );
        }

        [Fact]
        public void UnloggedUserTriesToRegisterWithPostRequestAndFail() {
            var accountViewModel = new RegisterViewModel() {
                UserName = "pippo",
                Email = "pippo@gmail.com",
                Password = "Gggggggg111!",
                ConfirmPassword = "Gggggggg111!"
            };
            var accountController = CreateAccountController( null );
            var result = accountController.Register( accountViewModel );

            Assert.IsType< BadRequestResult >( result );
        }
    }
}