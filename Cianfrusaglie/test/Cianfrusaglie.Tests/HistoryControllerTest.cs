using System;
using System.Collections.Generic;
using System.Linq;
using Cianfrusaglie.Controllers;
using Cianfrusaglie.Models;
using Cianfrusaglie.ViewModels.Account;
using Microsoft.AspNet.Mvc;
using Moq;
using Xunit;

namespace Cianfrusaglie.Tests {
    public class HistoryControllerTest : BaseTestSetup {
        protected HistoryController CreateHistoryController( string id ) {
            return new HistoryController( Context ) {
                ActionContext = MockActionContextForLogin( id ),
                Url = new Mock< IUrlHelper >().Object
            };
        }

        private IEnumerable< IEnumerable< Announce > > PartitionAnnounces( IEnumerable< Announce > announces, int size ) {
            for (int i = 0; i < Math.Ceiling(announces.Count() / (Double)size); i++)
                yield return new List<Announce>(announces.Skip(size * i).Take(size));
        }

        [Fact]
        public void HistoryFunctionIsOk() {
            var usr = Context.Users.Single( u => u.UserName.Equals( FirstUserName ) );
            var history = CreateHistoryController( usr.Id );
            var announces = Context.Announces.Where( a => a.AuthorId.Equals( usr.Id ) && !a.Closed);
            var result = history.GetLoggedUserPublishedAnnounces();
            Assert.Equal( result, announces );
        }

        [Fact]
        public void UserTryToViewHisPublishedAnnouncesOk() {
            var usr = Context.Users.Single( u => u.UserName.Equals( FirstUserName ) );
            var history = CreateHistoryController( usr.Id );
            var result = history.Index();
            Assert.IsType< ViewResult >( result );
        }

        [Fact]
        public void VisitorTryToViewHistoryBadRequest() {
            var history = CreateHistoryController( null );
            Assert.IsType< BadRequestResult >( history.Index() );
        }

        [Fact]
        public void GetLoggedUserClosedAnnouncesFunctionIsOk() {
            var user = Context.Users.SingleOrDefault( u => u.UserName.Equals( FirstUserName ) );
            var history = CreateHistoryController(user.Id);
            var expectedAnnounces = Context.Announces.Where(a => a.AuthorId.Equals(user.Id));
            foreach( var userAnnounce in expectedAnnounces ) {
                CloseAnnounce( userAnnounce.Id );
            }
            var actualAnnounces = history.GetLoggedUserClosedAnnounces();
            Assert.Equal( expectedAnnounces, actualAnnounces );
        }

        [Fact]
        public void GetLoggedUserWonClosedAnnouncesFunctionIsOk() {
            var targetUser = CreateUser( "franco", "franco@gmail.com" );
            var otherUser = CreateUser( "ciccio", "ciccio@gmail.com" );
            const int partitionSize = 1;
            var partitionAnnounces = PartitionAnnounces( Context.Announces, partitionSize );
            var interestedButNotChoosenAnnounces = partitionAnnounces.First();
            var notLastChoosenAnnounces = PartitionAnnounces( Context.Announces, partitionSize ).Skip( 1 ).First();
            var lastChoosenAnnounces = PartitionAnnounces(Context.Announces, partitionSize).Skip(2).First();
            foreach( var announce in interestedButNotChoosenAnnounces ) {
                SetUserInterestedToAnnounce( announce, targetUser );
                CloseAnnounce( announce.Id );
            }
            foreach( var announce in notLastChoosenAnnounces ) {
                SetUserInterestedToAnnounceAndChoosen( announce, targetUser, 1 );
                SetUserInterestedToAnnounceAndChoosen( announce, otherUser, 0 );
                CloseAnnounce(announce.Id);
            }
            foreach( var announce in lastChoosenAnnounces ) {
                SetUserInterestedToAnnounceAndChoosen(announce, targetUser, 0);
                SetUserInterestedToAnnounceAndChoosen(announce, otherUser, 1);
                CloseAnnounce(announce.Id);
            }
            var history = CreateHistoryController(targetUser.Id);
            var actualChoosenAnnounces = history.GetLoggedUserWonClosedAnnounces();
            Assert.Equal( lastChoosenAnnounces, actualChoosenAnnounces );
        }

        [Fact]
        public void GetLoggedUserLostAnnouncesFunctionIsOk() {
            var targetUser = CreateUser("franco", "franco@gmail.com");
            var otherUser = CreateUser("ciccio", "ciccio@gmail.com");
            const int partitionSize = 1;
            var partitionAnnounces = PartitionAnnounces(Context.Announces, partitionSize);
            var interestedButNotChoosenAnnounces = partitionAnnounces.First();
            var notLastChoosenAnnounces = PartitionAnnounces(Context.Announces, partitionSize).Skip(1).First();
            var lastChoosenAnnounces = PartitionAnnounces(Context.Announces, partitionSize).Skip(2).First();
            foreach (var announce in interestedButNotChoosenAnnounces)
            {
                SetUserInterestedToAnnounce(announce, targetUser);
                CloseAnnounce(announce.Id);
            }
            foreach (var announce in notLastChoosenAnnounces)
            {
                SetUserInterestedToAnnounceAndChoosen(announce, targetUser, 1);
                SetUserInterestedToAnnounceAndChoosen(announce, otherUser, 0);
                CloseAnnounce(announce.Id);
            }
            foreach (var announce in lastChoosenAnnounces)
            {
                SetUserInterestedToAnnounceAndChoosen(announce, targetUser, 0);
                SetUserInterestedToAnnounceAndChoosen(announce, otherUser, 1);
                CloseAnnounce(announce.Id);
            }
            var history = CreateHistoryController(targetUser.Id);
            var actualLostAnnounces = history.GetLoggedUserLostAnnounces();
            Assert.Equal(interestedButNotChoosenAnnounces.Concat( notLastChoosenAnnounces ), actualLostAnnounces);
        }

        private User CreateUser(string userName, string email) {
            var registerViewModel = new RegisterViewModel
            {
                ConfirmPassword = CommonUserPassword,
                Password = CommonUserPassword,
                Email = email,
                UserName = userName
            };
            var user = new User { UserName = registerViewModel.UserName, Email = registerViewModel.Email };
            var result = UserManager.CreateAsync(user, registerViewModel.Password).Result;
            return Context.Users.Single( u => u.UserName.Equals( userName ));
        }
    }
}