using System.Linq;
using Cianfrusaglie.Controllers;
using Microsoft.AspNet.Mvc;
using Moq;
using Xunit;

namespace Cianfrusaglie.Tests {
   public class HistoryTest : BaseTestSetup {
      protected HistoryController CreateHistoryController( string id, string userName ) {
         return new HistoryController( Context ) {
            ActionContext = MockActionContextForLogin( id ),
            Url = new Mock<IUrlHelper>().Object
         };
      }

      [Fact]
      public void UserTryToViewHisPublishedAnnouncesOk() {
         var history = CreateHistoryController( null, null );
         
      }

      [Fact]
      public void NotLoggedUserTryToViewHistoryBadRequest() {
         var history = CreateHistoryController( null, null );
         Assert.IsType< BadRequestResult >( history.Index() );
      }
   }
}