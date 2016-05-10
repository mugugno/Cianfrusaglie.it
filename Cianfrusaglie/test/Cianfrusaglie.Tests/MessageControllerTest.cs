using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Cianfrusaglie.Controllers;
using Cianfrusaglie.ViewModels.Account;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc;
using Moq;
using Xunit;

namespace Cianfrusaglie.Tests
{
    public class MessageControllerTest : BaseTestSetup
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
                new ClaimsPrincipal(new[] {new ClaimsIdentity(new[] {new Claim(ClaimTypes.NameIdentifier, userId)})});
            mockHttpContext.Setup(h => h.User).Returns(validPrincipal);
            return new MessagesController(Context)
            {
                ActionContext = new ActionContext {HttpContext = mockHttpContext.Object},
                Url = new Mock<IUrlHelper>().Object
            };
        }

        /*io utente voglio vedere i miei messaggi
        bad request se non sono l'utente
        */

        [Fact]
        public async void UserLoggedTryToVisualizeHisMessage()
        {

            //tiro su l'utente dal database cone quello username
            var usr = Context.Users.Single(u => u.UserName.Equals(FirstUserName));

            //query per prendere id del secondo utente
            var secondUsr = Context.Users.Single(u => u.UserName.Equals(SecondUserName));

            //create the messageController
            var messageController = new MessagesController(Context);

            //dato l'utente, visualizzo tutti i suoi messaggi
            var result = messageController.Index();
            
            //test 
            Assert.IsNotType<BadRequestResult>(result);

        }


        //utente non loggato cerca di vedere i messaggi di un altro
        [Fact]
        public void UserNotLoggedTryToVisualizeMessagesOfOther()
        {
            //tiro su l'utente dal database cone quello username
            var usr = Context.Users.Single(u => u.UserName.Equals(FirstUserName));

            //query per prendere id del secondo utente
            //var secondUsr = Context.Users.Single(u => u.UserName.Equals(SecondUserName));

            //create the messageController
            var messageController = CreateMessageController(null, null);

            //dato l'utente, visualizzo tutti i suoi messaggi
            var result = messageController.Index();
            //test 
            Assert.IsType<BadRequestResult>(result);

        }

        //TODO DETAILS, visualizzazione della conversazione tra due user
        //la view è andata a buon fine
        [Fact]
        public void CorrectViewOfConversationBetweenTwoUsers()
        {
            //tiro su l'utente dal database cone quello username
            var usr = Context.Users.Single(u => u.UserName.Equals(FirstUserName));

            //query per prendere id del secondo utente
            var secondUsr = Context.Users.Single(u => u.UserName.Equals(SecondUserName));

            //create the messageController
            var messageController = CreateMessageController( usr.Id, usr.UserName );

            //dato l'utente, visualizzo tutti i suoi messaggi
            var result = messageController.Details(usr.Id);

            //test 
            Assert.IsType<ViewResult>(result);
            //Assert.IsNotType<BadRequestResult>(result);

        }


        //la view non è andata a buon fine
        [Fact]
        public void NotCorrectViewOfConversationBetweenTwoUsers()
        {
            //tiro su l'utente dal database cone quello username
            var usr = Context.Users.Single(u => u.UserName.Equals(FirstUserName));

            //query per prendere id del secondo utente
            var secondUsr = Context.Users.Single(u => u.UserName.Equals(SecondUserName));

            //create the messageController
            var messageController = CreateMessageController(usr.Id, usr.UserName);

            //dato l'utente, visualizzo tutti i suoi messaggi
            var result = messageController.Details(usr.Id);

            //test 
            Assert.IsType<BadRequestResult>(result);
        }

        //TODO INVIO MESSAGGIO(creazione)
        //io lo invio A un utente che non esiste

        //io lo invio all'utente giusto
        //TODO DELETE MESSAGGIO
        //io lo elimino e la cosa è andata a buon fine
        //io lo elimino e la cosa NON è andata a buon fine

        }
    }
        
