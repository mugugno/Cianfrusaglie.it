using System.Linq;
using Cianfrusaglie.Controllers;
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

        [Fact]
        public void HistoryFunctionIsOk() {
            var usr = Context.Users.Single( u => u.UserName.Equals( FirstUserName ) );
            var history = CreateHistoryController( usr.Id );
            var announces = Context.Announces.Where( a => a.AuthorId.Equals( usr.Id ) );
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
    }
}