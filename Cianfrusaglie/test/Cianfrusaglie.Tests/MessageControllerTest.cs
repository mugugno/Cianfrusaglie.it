﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Cianfrusaglie.Controllers;
using Cianfrusaglie.Models;
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
            return new MessagesController(Context)
            {
                ActionContext = MockActionContextForLogin(userId),
                Url = new Mock<IUrlHelper>().Object
            };
        }

        /*io utente voglio vedere i miei messaggi
        bad request se non sono l'utente
        */

        [Fact]
        public void UserLoggedTryToVisualizeHisMessage()
        {

            //tiro su l'utente dal database cone quello username
            var usr = Context.Users.Single(u => u.UserName.Equals(FirstUserName));

            //query per prendere id del secondo utente
            var secondUsr = Context.Users.Single(u => u.UserName.Equals(SecondUserName));

            //create the messageController
            var messageController = CreateMessageController(usr.Id, usr.UserName);

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
            //TODO DA CAMBIARE I PARAMETRI
            var messageController = CreateMessageController(null, null);

            //dato l'utente, visualizzo tutti i suoi messaggi
            var result = messageController.Index();
            //test 
            Assert.IsType<BadRequestResult>(result);

        }

        //TODO DETAILS, visualizzazione della conversazione tra due user
        //estraggo dal database i messaggi corretti dei due utenti
        [Fact]
        public void CorrectViewOfConversationBetweenTwoUsers()
        {
            //tiro su l'utente dal database cone quello username
            var usr = Context.Users.Single(u => u.UserName.Equals(FirstUserName));

            //query per prendere id del secondo utente
            var secondUsr = Context.Users.Single(u => u.UserName.Equals(SecondUserName));

            //create the messageController
            var messageController = CreateMessageController( usr.Id, usr.UserName );

            //dato l'utente, estraggo tutti i loro messaggi
            var result = messageController.Details(secondUsr.Id);

            //test 
            Assert.IsType<ViewResult>(result);

        }


        //i messaggi estratti non sono vuoti
        [Fact]
        public void CorrectViewOfConversationBetweenTwoUsers()
        {
            //tiro su l'utente dal database cone quello username
            var usr = Context.Users.Single(u => u.UserName.Equals(FirstUserName));

            //query per prendere id del secondo utente
            var secondUsr = Context.Users.Single(u => u.UserName.Equals(SecondUserName));

            //create the messageController
            var messageController = CreateMessageController(usr.Id, usr.UserName);

            //dato l'utente, visualizzo tutti i suoi messaggi
            var result = messageController.Details(secondUsr.Id);

            //test 
            Assert.IsType<ViewResult>(result);
        }


        //Test dove si verifica che i dati estratti sono quelli corretti
        [Fact]
        public void TestGetConversationBetweenUser()
        {
            //creo delle conversazioni tra 2 utenti e controllo che ci siano
            var userTest1 = Context.Users.Single(u => u.UserName == FirstUserName);
            var userTest2 = Context.Users.Single(u => u.UserName == SecondUserName); 
            var userTest3 = Context.Users.Single(u => u.UserName == ThirdUserName); ;

            //creo messaggio tra user 1 e user 2
            var messageTest1 = new Message
            {
                Receiver = userTest2,
                Sender = userTest1   
            };

            //creo messaggio tra user 1 e user 2
            var messageTest2 = new Message
            {
                Receiver = userTest3,
                Sender = userTest1
            };
            // var result = Context.Messages.Where( m => m.Sender == userTest1);
            //create the messageController
            var messageController = CreateMessageController(userTest1.Id, userTest1.UserName);


            //test 
            //inserisco in un hash set una lista dei messaggi che ho creato
            HashSet< Message > testResult = new HashSet<Message>
            {
                messageTest1,
                messageTest2,
            };
            //funzione da testare
            var userTest = messageController.GetLoggedUsersConversationsUsers();
            Assert.();

            Assert.IsType<ViewResult>(result);
        }


        //TODO INVIO MESSAGGIO(creazione)
        //io lo invio A un utente che non esiste
        public void SendMessageToUserThatNotExist()
        {

            //tiro su l'utente dal database cone quello username
            var usr = Context.Users.Single(u => u.UserName.Equals(FirstUserName));

            //query per prendere id del secondo utente
            var secondUsr = Context.Users.Single(u => u.UserName.Equals(SecondUserName));

            //create the messageController
            var messageController = CreateMessageController(usr.Id, usr.UserName);

            //dato l'utente, invio il suo messaggio a un utente che non esiste
            //var result = messageController.Create();

            //controllo che 
            Assert.IsType<BadRequestResult>(result);
        }

        //io lo invio all'utente giusto
        //TODO DELETE MESSAGGIO
        //io lo elimino e la cosa è andata a buon fine
        //io lo elimino e la cosa NON è andata a buon fine

        }
    }
        
