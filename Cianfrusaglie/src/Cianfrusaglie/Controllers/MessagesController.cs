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

        // Dizionario di tutti messaggi, con relativo ricevente, con un dato utente
        public Dictionary< Message, User > GetConversationWithUser( string id ) {
            var dictionary =
                _context.Messages.Where(
                    m =>
                        ( m.Sender.Id.Equals( User.GetUserId() ) && m.Receiver.Id.Equals( id ) ) ||
                        ( m.Receiver.Id.Equals( User.GetUserId() ) && m.Sender.Id.Equals( id ) ) ).OrderBy(
                            m => m.DateTime ).Select( m => new {m, m.Receiver} );
            return dictionary.ToDictionary( x => x.m, x => x.m.Receiver );
        }

        // Dizionario di tutte le conversazioni dell'utente loggato
        public Dictionary< User, Dictionary< Message, User > > GetAllConversations() {
            var dictionary =
                _context.Messages.Where(
                    m => m.Sender.Id.Equals( User.GetUserId() ) || m.Receiver.Id.Equals( User.GetUserId() ) )
                    .OrderByDescending( m => m.DateTime ).Select(
                        m => m.Sender.Id.Equals( User.GetUserId() ) ? m.Receiver : m.Sender ).ToList().Distinct().Select
                    ( u => new {u, u.Id} );
            return dictionary.ToDictionary( x => x.u, x => GetConversationWithUser( x.u.Id ) );
        }

        public void SetMessagesToReadStatus() {
            var unreadMessages =_context.Messages.Where( m => m.Receiver.Id.Equals( User.GetUserId() ) && !m.Read ).ToList() ;
            foreach( var message in unreadMessages ) {
                message.Read = true;
                _context.SaveChanges();
            }
            _context.SaveChanges();

        }

        // Pagina della chat, con tutte le conversazioni e relativi messaggi
        // GET: Messages
        public IActionResult Index( string id = "" ) {
            if( !LoginChecker.HasLoggedUser( this ) )
                return HttpBadRequest();
            ViewData[ "formCategories" ] = _context.Categories.ToList();
            ViewData[ "numberOfCategories" ] = _context.Categories.ToList().Count;
            ViewData[ "allConversations" ] = GetAllConversations();
            ViewData[ "idAfterRefresh" ] = id;
            SetMessagesToReadStatus();
            return View();
        }

        // Redirect dei link "Contatta" negli annunci
        public IActionResult Details( string id = "" ) { return RedirectToAction( "Create", new {id} ); }

        // Pagina di invio di un messaggio a un dato utente
        // GET: Messages/Create
        public IActionResult Create( string id = "" ) {
            if( !LoginChecker.HasLoggedUser( this ) )
                return HttpBadRequest();
            // non si pu� scrivere a se stessi
            if( id == User.GetUserId() )
                return HttpNotFound();
            // non si pu� vedere conversazioni di utenti che non esistono.
            if (!_context.Users.Any(u => u.Id == id))
                return HttpNotFound();
            if ( !_context.Users.Any( u => u.Id == User.GetUserId() ) )
                return HttpNotFound();
            ViewData[ "formCategories" ] = _context.Categories.ToList();
            ViewData[ "numberOfCategories" ] = _context.Categories.ToList().Count;
            ViewData[ "receiver" ] = _context.Users.First( u => u.Id.Equals( id ) );
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
                var loggedUsr = _context.Users.Single( u => u.Id == User.GetUserId() );
                var receiverUsr = _context.Users.SingleOrDefault( u => u.Id == messageCreate.ReceiverId );

                if( receiverUsr == null )
                    return HttpBadRequest();

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
            //TODO: Bad Request da trattare
            if( !LoginChecker.HasLoggedUser( this ) )
                return HttpBadRequest();
            var message = _context.Messages.SingleOrDefault( m => m.Id == id );
            if( message == null )
                return HttpNotFound();
            ViewData[ "formCategories" ] = _context.Categories.ToList();
            ViewData[ "numberOfCategories" ] = _context.Categories.ToList().Count;

            return View( message );
        }

        // POST: Messages/Delete/5
        [HttpPost, ActionName( "Delete" ), ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed( int id ) {
            //TODO: Bad Request da trattare
            if(! LoginChecker.HasLoggedUser( this ) )
                return HttpBadRequest();
            var message = _context.Messages.SingleOrDefault( m => m.Id == id );
            //TODO: Gestire qui la HTTP not found
            if( message == null )
                return HttpNotFound();
            _context.Messages.Remove( message );
            _context.SaveChanges();
            return RedirectToAction( "Index" );
        }
    }
}