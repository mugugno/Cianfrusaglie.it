using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Cianfrusaglie.Models;
using Cianfrusaglie.Statics;
using Cianfrusaglie.ViewModels;
using Microsoft.AspNet.Mvc;

namespace Cianfrusaglie.Controllers {
    public class MessagesController : Controller {
        private readonly ApplicationDbContext _context;

        public MessagesController( ApplicationDbContext context ) { _context = context; }

        public IEnumerable< Message > GetLoggedUsersMessagesWithUser( string id ) {
            if( id == null )
                throw new ArgumentNullException();

            IQueryable< Message > messages =
                _context.Messages.Where(
                    m =>
                        ( m.Sender.Id == User.GetUserId() && m.Receiver.Id == id ) ||
                        ( m.Receiver.Id == User.GetUserId() && m.Sender.Id == id ) );

            return messages;
        }

        // FIX ABORTO DA RISCRIVERE
        public string GetReceiver( Message message ) {
            if( message == null )
                throw new ArgumentNullException();

            string receiver =
                _context.Messages.Where( m => m.Equals( message ) ).Select( u => u.Receiver ).Select( x => x.Id ).First();

            return receiver;
        }

        public User GetReceiverNew( Message message ) {
            if( message == null )
                throw new ArgumentNullException();

            User receiver = _context.Messages.Where( m => m.Equals( message ) ).Select( u => u.Receiver ).First();

            return receiver;
        }


        protected IEnumerable< User > GetLoggedUsersConversationsUsers() {
            List< User > userThatSendedMeAMessage =
                _context.Messages.Where( u => u.Sender.Id.Equals( User.GetUserId() ) ).Select( u => u.Receiver ).ToList();
            List< User > userThatISentAMessage =
                _context.Messages.Where( u => u.Receiver.Id.Equals( User.GetUserId() ) ).Select( u => u.Sender ).ToList();


            userThatSendedMeAMessage.AddRange( userThatISentAMessage );
            return userThatSendedMeAMessage.Distinct();
        }

        //tutti gli utenti con cui l'utente loggato ha messaggiato
        // GET: Messages
        public IActionResult Index( string id ) {
            if( !LoginChecker.HasLoggedUser( this ) )
                return HttpBadRequest();
            List< User > users = GetLoggedUsersConversationsUsers().ToList();
            var userAndMessagesDictionary = new Dictionary< User, Dictionary< Message, User > >();
            foreach( User user in users ) {
                var receiverDictionary = new Dictionary< Message, User >();
                foreach( Message m in GetLoggedUsersMessagesWithUser( user.Id ).ToList() ) {
                    receiverDictionary[ m ] = GetReceiverNew( m );
                }
                userAndMessagesDictionary[ user ] = receiverDictionary;
            }

            ViewData[ "userAndMessagesDictionary" ] = userAndMessagesDictionary;
            if( id != null )
                ViewData[ "idAfterRefresh" ] = id;
            else
                ViewData[ "idAfterRefresh" ] = "";
            return View();
        }

        // inviare un messaggio all'utente con id = id
        // GET: Messages/Create
        public IActionResult Create( string id ) {
            if( !LoginChecker.HasLoggedUser( this ) )
                return HttpBadRequest();
            ViewData[ "formCategories" ] = _context.Categories.ToList();
            ViewData[ "numberOfCategories" ] = _context.Categories.ToList().Count;
            if( id == null )
                return HttpNotFound();

            if( !_context.Users.Any( u => u.Id == User.GetUserId() ) )
                return HttpNotFound();

            ViewData[ "receiverId" ] = id;
            return View();
        }

        // POST: Messages/Create
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Create( MessageCreateViewModel messageCreate ) {
            if( !LoginChecker.HasLoggedUser( this ) )
                return HttpBadRequest();

            if( messageCreate == null )
                return HttpBadRequest();

            if( ModelState.IsValid ) {
                User loggedUsr = _context.Users.Single( u => u.Id == User.GetUserId() );
                User receiverUsr = _context.Users.SingleOrDefault( u => u.Id == messageCreate.ReceiverId );

                if( receiverUsr == null )
                    return HttpBadRequest(); //id utente non valido

                _context.Messages.Add( new Message {
                    Sender = loggedUsr,
                    Receiver = receiverUsr,
                    Text = messageCreate.Text,
                    DateTime = DateTime.Now
                } );
                _context.SaveChanges();
                return RedirectToAction( "Index", new {id = receiverUsr.Id} );
            }
            return View( messageCreate );
        }

        // GET: Messages/Delete/5
        [ActionName( "Delete" )]
        public IActionResult Delete( int? id ) {
            if( id == null )
                return HttpNotFound();

            Message message = _context.Messages.SingleOrDefault( m => m.Id == id );
            if( message == null )
                return HttpNotFound();

            return View( message );
        }

        // POST: Messages/Delete/5
        [HttpPost, ActionName( "Delete" ), ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed( int id ) {
            Message message = _context.Messages.Single( m => m.Id == id );
            _context.Messages.Remove( message );
            _context.SaveChanges();
            return RedirectToAction( "Index" );
        }
    }
}