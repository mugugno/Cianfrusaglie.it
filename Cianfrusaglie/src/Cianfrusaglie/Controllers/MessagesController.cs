using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Cianfrusaglie.Models;
using Cianfrusaglie.Statics;
using Cianfrusaglie.ViewModels;
using Microsoft.AspNet.Mvc;
using static Cianfrusaglie.Constants.CommonFunctions;

namespace Cianfrusaglie.Controllers {
    public class MessagesController : Controller {
        private readonly ApplicationDbContext _context;

        public MessagesController( ApplicationDbContext context ) { _context = context; }

        /// <summary>
        /// Dato un Id di un Utente, restituisce la la conversazione che ha effettuato con l'utente loggato.
        /// </summary>
        /// <param name="id">Id dell'utente per cui voglio le conversazioni</param>
        /// <returns>Dizionario di tutti messaggi, con relativo ricevente, con un dato utente</returns>
        public Dictionary< Message, User > GetConversationWithUser( string id ) {
            var dictionary =
                _context.Messages.Where(
                    m =>
                        ( m.Sender.Id.Equals( User.GetUserId() ) && m.Receiver.Id.Equals( id ) ) ||
                        ( m.Receiver.Id.Equals( User.GetUserId() ) && m.Sender.Id.Equals( id ) ) ).OrderBy(
                            m => m.DateTime ).Select( m => new {m, m.Receiver} );
            return dictionary.ToDictionary( x => x.m, x => x.m.Receiver );
        }

        /// <summary>
		/// Ritorna un Dizionario con chiave Utente e valore Dizionario di Messaggio, Utente contenente tutte le conversazioni
		/// che l'utente loggato ha effettuato.
        /// </summary>
        /// <returns>Dizionario di tutte le conversazioni dell'utente loggato</returns>
        public Dictionary< User, Dictionary< Message, User > > GetAllConversations() {
            var dictionary =
                _context.Messages.Where(
                    m => m.Sender.Id.Equals( User.GetUserId() ) || m.Receiver.Id.Equals( User.GetUserId() ) )
                    .OrderByDescending( m => m.DateTime ).Select(
                        m => m.Sender.Id.Equals( User.GetUserId() ) ? m.Receiver : m.Sender ).ToList().Distinct().Select
                    ( u => new {u, u.Id} );
            return dictionary.ToDictionary( x => x.u, x => GetConversationWithUser( x.u.Id ) );
        }

		/// <summary>
		/// Quando l'utente apre le sue conversazioni tutti i messaggi risultano letti
		/// </summary>
        public void SetMessagesToReadStatus() {
            var unreadMessages =_context.Messages.Where( m => m.Receiver.Id.Equals( User.GetUserId() ) && !m.Read ).ToList() ;
            foreach( var message in unreadMessages ) {
                message.Read = true;
                _context.SaveChanges();
            }
            _context.SaveChanges();

        }
		
		/// <summary>
		/// Dato l'Id di un Utente, ritorna la pagina della chat, con tutte le conversazioni 
		/// e relativi messaggi con quell'utente
		/// In caso l'utente non sia loggato, allora ritorna una BadRequest
		/// </summary>
		/// <param name="id">Id dell'utente per cui voglio le conversazioni</param>
        // GET: Messages
        public IActionResult Index( string id = "" ) {
            if( !LoginChecker.HasLoggedUser( this ) )
                return HttpBadRequest();
            ViewData[ "formCategories" ] = _context.Categories.ToList();
            ViewData[ "numberOfCategories" ] = _context.Categories.ToList().Count;
            ViewData[ "allConversations" ] = GetAllConversations();
            ViewData[ "idAfterRefresh" ] = id;
            SetMessagesToReadStatus();
            ViewData[ "IsThereNewMessage" ] = IsThereNewMessage( User.GetUserId(), _context );
            ViewData[" IsThereNewInterested"] = IsThereNewInterested(User.GetUserId(), _context);
            ViewData["IsThereAnyNotification"] = IsThereAnyNotification(User.GetUserId(), _context);
		    ViewData[ "MyAvatarUrl" ] = _context.Users.First( u => u.Id.Equals( User.GetUserId() ) ).ProfileImageUrl;
            return View();
        }

        // Redirect dei link "Contatta" negli annunci
        public IActionResult Details( string id = "" ) { return RedirectToAction( "Create", new {id} ); }

        /// <summary>
        /// Dato l'Id di un utente, ritorna la View per la creazione di un nuovo messaggio che come destinatario
        /// ha quell'utente.
        /// Se non c'è alcun utente loggato, ritorna una BadRequest. Se l'Id non corrisponde ad alcun utente, ritorna
        /// una Http not Found.
        /// </summary>
        /// <param name="id">L'id dell'utente al quale mandare un messaggio</param>
        /// <returns>La view della creazione</returns>
        // GET: Messages/Create
        public IActionResult Create( string id = "" ) {
            if( !LoginChecker.HasLoggedUser( this ) )
                return HttpBadRequest();
            // non si può scrivere a se stessi
            if( id == User.GetUserId() )
                return HttpNotFound();
            // non si può vedere conversazioni di utenti che non esistono.
            if (!_context.Users.Any(u => u.Id == id))
                return HttpNotFound();
            if ( !_context.Users.Any( u => u.Id == User.GetUserId() ) )
                return HttpNotFound();
            ViewData[ "formCategories" ] = _context.Categories.ToList();
            ViewData[ "numberOfCategories" ] = _context.Categories.ToList().Count;
            ViewData[ "receiver" ] = _context.Users.First( u => u.Id.Equals( id ) );
            ViewData["IsThereNewMessage"] = IsThereNewMessage(User.GetUserId(), _context);
            ViewData[" IsThereNewInterested"] = IsThereNewInterested(User.GetUserId(), _context);
            ViewData["IsThereAnyNotification"] = IsThereAnyNotification(User.GetUserId(), _context);
            return View();
        }

		/// <summary>
		/// Dato un MessageCreateViewModel, crea il messaggio compilando tutti i campi opportuni.
		/// Se non c'è alcun utente loggato, ritorna una BadRequest. Se il modello non è valido, ritorna la View di creazione.
		/// </summary>
		/// <param name="messageCreate">Il modello con il quale compilare il messaggio</param>
		/// <returns>Un redirect alla conversazione con l'utente destinatario'</returns>
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

		/// <summary>
		/// Dato l'Id di un messaggio, ritorna la View per la cancellazione di un messaggio.
		/// Se non c'è alcun utente loggato, ritorna una BadRequest. Se l'Id non corrisponde ad alcun messaggio, ritorna
		/// una Http not Found. Se il messaggio da cancellare appartiene ad un altro utente, ritorna una BadRequest.
		/// </summary>
		/// <param name="id">L'id del messaggio da cancellare</param>
		/// <returns>La view della cancellazione</returns>
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
		    if( !message.Sender.Id.Equals( User.GetUserId() ) )
		        return HttpBadRequest();
            ViewData[ "formCategories" ] = _context.Categories.ToList();
            ViewData[ "numberOfCategories" ] = _context.Categories.ToList().Count;
            ViewData["IsThereNewMessage"] = IsThereNewMessage(User.GetUserId(), _context);
            ViewData[" IsThereNewInterested"] = IsThereNewInterested(User.GetUserId(), _context);
            ViewData["IsThereAnyNotification"] = IsThereAnyNotification(User.GetUserId(), _context);
            return View( message );
        }

		/// <summary>
		/// Dato l'Id di un messaggio, ritorna la View con la conversazione dopo che il messaggio in questione è stato cancellato.
		/// Se non c'è alcun utente loggato, ritorna una BadRequest. Se l'Id non corrisponde ad alcun messaggio, ritorna
		/// una Http not Found.Se il messaggio da cancellare appartiene ad un altro utente, ritorna una BadRequest.
		/// </summary>
		/// <param name="id">L'id del messaggio da cancellare</param>
		/// <returns>La view con la conversazione</returns>
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
            if (!message.Sender.Id.Equals(User.GetUserId()))
                return HttpBadRequest();
            _context.Messages.Remove( message );
            _context.SaveChanges();
            return RedirectToAction( "Index" );
        }
    }
}