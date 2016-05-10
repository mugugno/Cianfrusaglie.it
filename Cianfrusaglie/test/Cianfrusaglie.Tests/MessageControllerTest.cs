using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Cianfrusaglie.Controllers;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc;
using Moq;

namespace Cianfrusaglie.Tests
{
    public class MessageControllerTest: BaseTestSetup
    {
        protected MessagesController CreateMessageController(string userId, string userName)
        {
            //create the mockObject
            var mockHttpContext = new Mock<HttpContext>();
            //invoce this function to setup this properties
            mockHttpContext.SetupAllProperties();

            if (userId == null || userName == null)
                return new MessagesController(Context);
            var validPrincipal =
               new ClaimsPrincipal(new[] { new ClaimsIdentity(new[] { new Claim(ClaimTypes.NameIdentifier, userId) }) });
            mockHttpContext.Setup(h => h.User).Returns(validPrincipal);
            return new MessagesController(Context)
            {
                ActionContext = new ActionContext { HttpContext = mockHttpContext.Object },
                Url = new Mock<IUrlHelper>().Object
            };
        }

        /*io utente voglio vedere i miei messaggi
        ok se lo sono
        bad request se non sono l'utente
        */

        public void UserTryToVisualizeMessagesOfOther()
        {
            
        }
    }
}
