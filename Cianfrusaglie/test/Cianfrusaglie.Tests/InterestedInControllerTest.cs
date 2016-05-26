using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cianfrusaglie.Controllers;
using Microsoft.AspNet.Mvc;
using Moq;
using Xunit;

namespace Cianfrusaglie.Tests
{
    public class InterestedInControllerTest : BaseTestSetup {

        protected InterestedInController CreateInterestedInController( string id ) {
            return new InterestedInController( Context ) {
                ActionContext = MockActionContextForLogin( id ),
                Url = new Mock< IUrlHelper >().Object
            };
        }

        // L'utente loggato (membro) visualizza correttamente la pagina degli annunci che gli interessano
        [Fact]
        public void UserTryToViewHisPublishedAnnouncesOk()
        {
            var usr = Context.Users.Single(u => u.UserName.Equals(FirstUserName));
            var interestedIn = CreateInterestedInController(usr.Id);
            var result = interestedIn.Index();
            Assert.IsType<ViewResult>(result);
        }

        // Visualizzare la pagina dell'utente null restituisce una bad request
        [Fact]
        public void VisitorTryToViewHistoryBadRequest() {
            var interestedIn = CreateInterestedInController( null );
            Assert.IsType<BadRequestResult>(interestedIn.Index());
        }

    }
}
