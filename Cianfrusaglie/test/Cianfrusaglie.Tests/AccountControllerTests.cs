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
using Microsoft.Data.Entity;
using System.Globalization;
using Cianfrusaglie.Constants;

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
        public void UserTriesToRegisterWithOptionalFIelds()
        {
            var Name = "Bob";
            var email = "bob@gmail.com";
            var Lat = "1.1";
            var Lon = "2.1";
            var Surname = "BobSurname";
            var data = DateTime.Now;

            var categoryDictionary = new Dictionary<int, bool>();
            var two = Context.Categories.Take(2);
            categoryDictionary.Add(two.Take(1).SingleOrDefault().Id, true);
            categoryDictionary.Add(two.Skip(1).SingleOrDefault().Id, true);
 
            var registerViewModel = new RegisterViewModel
            {
                ConfirmPassword = CommonUserPassword,
                Password = CommonUserPassword,
                Email = email,
                UserName = Name,
                BirthDate = data,
                Genre = 0,
                Latitude = Lat,
                Longitude = Lon,
                Name = Name,
                Surname = Surname,
                CategoryDictionary = categoryDictionary     
            };

            var ac = CreateAccountController(null);
            var reg = ac.Register(registerViewModel);
            var dbuser = Context.Users.Include(p => p.CategoryPreferenceses).SingleOrDefault(u => u.UserName.Equals(Name));
            var dbcat = dbuser.CategoryPreferenceses.Select(u=>u.CategoryId);
            
            Assert.True(dbuser.BirthDate.Equals(data));
            Assert.True(dbuser.Name.Equals(Name));
            //Assert.True(dbuser.Longitude.ToString(CultureInfo.InvariantCulture).Equals(Lon));
            //Assert.True(dbuser.Latitude.ToString(CultureInfo.InvariantCulture).Equals(Lat));
            Assert.True(dbuser.Genre.Equals(Genre.Unspecified));
            Assert.True(dbuser.Surname.Equals(Surname));
            foreach(var tmp in two)
            {
                Assert.True(dbcat.Contains(tmp.Id));
            }
            
            
        }
    }
}