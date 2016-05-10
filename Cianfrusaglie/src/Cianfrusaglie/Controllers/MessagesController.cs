using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNet.Mvc;
using Cianfrusaglie.Models;
using Cianfrusaglie.Statics;
using Cianfrusaglie.ViewModels;

namespace Cianfrusaglie.Controllers {
   public class MessagesController : Controller {
      private readonly ApplicationDbContext _context;

      public MessagesController( ApplicationDbContext context ) { _context = context; }

      protected IEnumerable< Message > GetLoggedUsersMessagesWithUser( string id ) {
         if( id == null )
            throw new ArgumentNullException();

         var messages =
            _context.Messages.Where(
               m =>
                  ( m.Sender.Id == User.GetUserId() && m.Receiver.Id == id ) ||
                  ( m.Receiver.Id == User.GetUserId() && m.Sender.Id == id ) );

         return messages;
      }

      protected IEnumerable< User > GetLoggedUsersConversationsUsers() {
         var usr = _context.Users.Single( u => u.Id == User.GetUserId() );
         var userWitchIHaveMessaged = usr.SentMessages?.Select( m => m.Receiver ) ?? new List<User>();
         var userThatSendedMeMessage = usr.ReceivedMessages?.Select( m => m.Sender ) ?? new List<User>();

         var users = userWitchIHaveMessaged.Union( userThatSendedMeMessage ).Distinct();
         return users;
      }

      //tutti gli utenti con cui l'utente loggato ha messaggiato
      // GET: Messages
      public IActionResult Index() {
         if(!LoginChecker.HasLoggedUser(this))
            return HttpBadRequest();

         return View( GetLoggedUsersConversationsUsers().ToList() );
      }

      // GET: Messages/Details/5
      public IActionResult Details( string id ) {
         if( !LoginChecker.HasLoggedUser( this ) )
            return HttpBadRequest();

         if( id == null )
            return HttpNotFound();

         return View( GetLoggedUsersMessagesWithUser( id ).ToList() );
      }

      // inviare un messaggio all'utente con id = id
      // GET: Messages/Create
      public IActionResult Create( string id ) {
         if( !LoginChecker.HasLoggedUser( this ) )
            return HttpBadRequest();

         if( id == null )
            return HttpNotFound();

         ViewData[ "receiverId" ] = id;
         return View();
      }

      // POST: Messages/Create
      [HttpPost, ValidateAntiForgeryToken]
      public IActionResult Create( MessageViewModel message ) {
         if( !LoginChecker.HasLoggedUser( this ) )
            return HttpBadRequest();

         if( message == null )
            return HttpBadRequest();

         if( ModelState.IsValid ) {
            var loggedUsr = _context.Users.Single( u => u.Id == User.GetUserId() );
            var receiverUsr = _context.Users.SingleOrDefault( u => u.Id == message.ReceiverId );

            if( receiverUsr == null )
               return HttpBadRequest(); //id utente non valido

            _context.Messages.Add( new Message() { Sender = loggedUsr, Receiver = receiverUsr, Text = message.Text, DateTime = DateTime.Now} );
            _context.SaveChanges();
            return RedirectToAction( "Index" );
         }
         return View( message );
      }

      // GET: Messages/Delete/5
      [ActionName( "Delete" )]
      public IActionResult Delete( int? id ) {
         if( id == null )
            return HttpNotFound();

         var message = _context.Messages.Single( m => m.Id == id );
         if( message == null )
            return HttpNotFound();

         return View( message );
      }

      // POST: Messages/Delete/5
      [HttpPost, ActionName( "Delete" ), ValidateAntiForgeryToken]
      public IActionResult DeleteConfirmed( int id ) {
         var message = _context.Messages.Single( m => m.Id == id );
         _context.Messages.Remove( message );
         _context.SaveChanges();
         return RedirectToAction( "Index" );
      }
   }
}