using System;
using System.Linq;
using Castle.Core.Internal;
using Cianfrusaglie.Controllers;
using Cianfrusaglie.Models;
using Cianfrusaglie.ViewModels;
using Microsoft.AspNet.Mvc;
using Moq;
using Xunit;

namespace Cianfrusaglie.Tests {
    public class MessageControllerTest : BaseTestSetup {
        protected MessagesController CreateMessageController( string userId ) {
            //create the mockObject
            return new MessagesController( Context ) {
                ActionContext = MockActionContextForLogin( userId ),
                Url = new Mock< IUrlHelper >().Object
            };
        }

        //Test dove elimino un messaggio e non va a buon fine perchè l'id è null oppure non esiste.
        [Theory, InlineData( null, typeof( HttpNotFoundResult ) ), InlineData( 2222, typeof( BadRequestResult ) )]
        public void DeleteUserButHisNotExist( int? falseId, Type errorType ) {
            //creo un messaggio di un utente fittizio e vedo che non riesce a eliminarlo con httpNotFound

            //tiro su l'utente dal database cone quello username
            var usr = Context.Users.Single( u => u.UserName.Equals( FirstUserName ) );

            //create the messageController
            var messageController = CreateMessageController( usr.Id );

            //risultato
            var result = messageController.Delete( falseId );

            //test
            Assert.IsType< HttpNotFoundResult >( result );
        }

        //TODO DETAILS, visualizzazione della conversazione tra due user
        //estraggo dal database i messaggi corretti dei due utenti
        [Fact]
        public void CorrectViewOfConversationBetweenTwoUsers() {
            //tiro su l'utente dal database cone quello username
            var usr = Context.Users.Single( u => u.UserName.Equals( FirstUserName ) );

            //query per prendere id del secondo utente
            var secondUsr = Context.Users.Single( u => u.UserName.Equals( SecondUserName ) );

            //create the messageController
            var messageController = CreateMessageController( usr.Id );

            //dato l'utente, estraggo tutti i loro messaggi
            var result = messageController.Details( secondUsr.Id );

            //test 
            Assert.IsType< RedirectToActionResult >( result );
        }

        //io lo elimino e la cosa NON è andata a buon fine

        [Fact]
        public void SendMessageToUserThatExistsIsOk() {
            var user = Context.Users.Single( u => u.UserName.Equals( FirstUserName ) );
            var receiver = Context.Users.Single( u => u.UserName.Equals( SecondUserName ) );
            var messagesController = CreateMessageController( user.Id );
            var messageViewModel = new MessageCreateViewModel {
                ReceiverId = receiver.Id,
                Text = "Ah ciao sono Sio e ti regalo un drago."
            };
            var result = messagesController.Create( messageViewModel );
            Assert.IsNotType< BadRequestResult >( result );
            var messages = Context.Messages.Where( m => m.Receiver.Id.Equals( receiver.Id ) );
            Assert.Single( messages,
                m =>
                    m.Sender.Id.Equals( user.Id ) && m.Receiver.Id.Equals( receiver.Id ) &&
                    m.Text.Equals( messageViewModel.Text ) );
        }


        //TODO INVIO MESSAGGIO(creazione)
        //io lo invio A un utente che non esiste
        [Fact]
        public void SendMessageToUserThatNotExist() {
            //creo un utente che non esiste e gli mando un messaggio

            //tiro su l'utente dal database cone quello username
            var usr = Context.Users.Single( u => u.UserName.Equals( FirstUserName ) );

            //query per prendere id del secondo utente
            //var secondUsr = Context.Users.Single(u => u.UserName.Equals(SecondUserName));


            //creo il ViewModel del messaggio
            var messageViewModel = new MessageCreateViewModel {
                ReceiverId = "5098",
                Text = "IO TE LO STO INVIANDO MA INTANTO NON ESISTI"
            };

            //create the messageController
            var messageController = CreateMessageController( usr.Id );

            //dato l'utente, invio il suo messaggio a un utente che non esiste
            var result = messageController.Create( messageViewModel );

            //controllo che dia una badRequest 
            Assert.IsType< BadRequestResult >( result );
        }


        //Test dove si verifica che i dati estratti sono quelli corretti
        [Fact]
        public void TestGetConversationBetweenUser() {
            //creo delle conversazioni tra 2 utenti e controllo che ci siano
            var userTest1 = Context.Users.Single( u => u.UserName == FirstUserName );
            var userTest2 = Context.Users.Single( u => u.UserName == SecondUserName );
            var userTest3 = Context.Users.Single( u => u.UserName == ThirdUserName );
            ;

            //creo messaggio tra user 1 e user 2
            var messageTest1 = new Message {Receiver = userTest2, Sender = userTest1, Text = "Sono bellissimo e tu no"};

            //creo messaggio tra user 1 e user 2
            var messageTest2 = new Message {Receiver = userTest3, Sender = userTest1, Text = "Sei più bello te"};

            Context.Messages.AddRange( messageTest1, messageTest2 );
            Context.SaveChanges();

            var result = Context.Messages.Where( m => m.Sender == userTest1 );

            //create the messageController
            var messageController = CreateMessageController( userTest1.Id );

            //funzione da testare
            var userTest = messageController.GetConversationWithUser( userTest2.Id ).Keys;
            Assert.Contains( messageTest1, userTest );
            Assert.DoesNotContain( messageTest2, userTest );
        }

        [Fact]
        public void UserDeletesMessageFromConversationIsOk() {
            var sender = Context.Users.Single( u => u.UserName.Equals( FirstUserName ) );
            var receiver = Context.Users.Single( u => u.UserName.Equals( SecondUserName ) );
            var messagesController = CreateMessageController( sender.Id );
            messagesController.Create( new MessageCreateViewModel {ReceiverId = receiver.Id, Text = "Dudududadada"} );
            var message =
                Context.Messages.First( m => m.Receiver.Id.Equals( receiver.Id ) && m.Sender.Id.Equals( sender.Id ) );
            var result = messagesController.DeleteConfirmed( message.Id );
            Assert.IsNotType< BadRequestResult >( result );

            Assert.True( Context.Messages.Where( m => m.Id.Equals( message.Id ) ).IsNullOrEmpty() );
        }

        /*io utente voglio vedere i miei messaggi
        bad request se non sono l'utente
        */

        [Fact]
        public void UserLoggedTryToVisualizeHisMessage() {
            //tiro su l'utente dal database cone quello username
            var usr = Context.Users.Single( u => u.UserName.Equals( FirstUserName ) );

            //query per prendere id del secondo utente
            var secondUsr = Context.Users.Single( u => u.UserName.Equals( SecondUserName ) );

            //create the messageController
            var messageController = CreateMessageController( usr.Id );

            //dato l'utente, visualizzo tutti i suoi messaggi
            var result = messageController.Index();

            //test 
            Assert.IsNotType< BadRequestResult >( result );
        }

        [Fact]
        public void VisitorTriesToOpenCreationPageAndFail() {
            var messageController = CreateMessageController(null);
            var result = messageController.Create();
            Assert.IsType<BadRequestResult>(result);
        }

        //utente non loggato cerca di mandare messaggio
        [Fact]
        public void UserNotLoggedTryToCreateMessage() {
            //tiro su l'utente dal database cone quello username
            var usr = Context.Users.Single( u => u.UserName.Equals( FirstUserName ) );

            //nuovo messaggio
            var message = new MessageCreateViewModel {
                ReceiverId = usr.Id,
                Text = "Io ci provo, ma intanto non sono loggato"
            };
            var messageController = CreateMessageController( null );

            //dato l'utente, visualizzo tutti i suoi messaggi
            var result = messageController.Create( message );

            //test 
            Assert.IsType< BadRequestResult >( result );
        }

        //utente non loggato cerca di eliminare un messaggio
        [Fact]
        public void UserNotLoggedTryToDeleteMessage() {
            var message = Context.Messages.First();

            //creo il messageController
            var messageController = CreateMessageController( null );

            //risultato
            var result = messageController.Delete( message.Id );
            //test
            Assert.IsType< BadRequestResult >( result );
        }

        [Fact]
        public void VisitorTriesToOpenMessageCreationOrConversationMessageAndFails() {
            var messageController = CreateMessageController( null );
            var result = messageController.Create( );
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void UserTriesToReadConversationOfNonExistingUser() {
            var usr = Context.Users.Single(u => u.UserName.Equals(FirstUserName));
            var messageController = CreateMessageController(usr.Id);
            var result = messageController.Create( "piripicchio non esisto" );
            Assert.IsType<HttpNotFoundResult>(result);
        }

        [Fact]
        public void UserTriesToConfirmDeletionOfNonExistingMessageAndFails() {
            var usr = Context.Users.Single(u => u.UserName.Equals(FirstUserName));
            var messageController = CreateMessageController(usr.Id);
            var result = messageController.DeleteConfirmed( 1090 );
            Assert.IsType<HttpNotFoundResult>(result);
        }

        [Fact]
        public void VisitorTriesToConfirmDeletionOfMessageAndFails()
        {
            var messageController = CreateMessageController(null);
            var result = messageController.DeleteConfirmed(Context.Messages.First().Id);
            Assert.IsType<BadRequestResult>(result);
        }

        //utente non loggato cerca di vedere i messaggi di un altro
        [Fact]
        public void UserNotLoggedTryToVisualizeMessagesOfOther() {
            //create the messageController
            //TODO DA CAMBIARE I PARAMETRI
            var messageController = CreateMessageController( null );

            //dato l'utente, visualizzo tutti i suoi messaggi
            var result = messageController.Index();
            //test 
            Assert.IsType< BadRequestResult >( result );
        }
    }
}