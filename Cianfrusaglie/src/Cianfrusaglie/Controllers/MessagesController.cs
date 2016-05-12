using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNet.Mvc;
using Cianfrusaglie.Models;
using Cianfrusaglie.Statics;
using Cianfrusaglie.ViewModels;
using Microsoft.Data.Entity;

namespace Cianfrusaglie.Controllers {
   public class MessagesController : Controller {
      private readonly ApplicationDbContext _context;

      public MessagesController( ApplicationDbContext context ) { _context = context; }

      public IEnumerable< Message > GetLoggedUsersMessagesWithUser( string id ) {
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
         var userThatSendedMeAMessage =
            _context.Messages.Where( u => u.Sender.Id.Equals( User.GetUserId() ) ).Select( u => u.Receiver ).ToList();
         var userThatISentAMessage =
            _context.Messages.Where( u => u.Receiver.Id.Equals( User.GetUserId() ) ).Select( u => u.Sender ).ToList();


         userThatSendedMeAMessage.AddRange( userThatISentAMessage );
         return userThatSendedMeAMessage.Distinct();
      }

      //tutti gli utenti con cui l'utente loggato ha messaggiato
      // GET: Messages
      public IActionResult Index() {
        ViewData["formCategories"] = _context.Categories.ToList();
        ViewData["numberOfCategories"] = _context.Categories.ToList().Count;
        if (!LoginChecker.HasLoggedUser(this))
        return HttpBadRequest();

         var users = GetLoggedUsersConversationsUsers().ToList();
         return View( users );
      }

      // GET: Messages/Details/5
      public IActionResult Details( string id ) {

        ViewData["formCategories"] = _context.Categories.ToList();
        ViewData["numberOfCategories"] = _context.Categories.ToList().Count;
        if ( !LoginChecker.HasLoggedUser( this ) )
        return HttpBadRequest();

         if( id == null )
            return HttpNotFound();

         var otherUser = _context.Users.SingleOrDefault( u => u.Id == id );
         if( otherUser == null )
            return HttpNotFound();

         ViewData[ "otherUser" ] = otherUser;
         ViewData[ "messages" ] = GetLoggedUsersMessagesWithUser( id ).ToList();
         ViewData[ "receiverId" ] = id;

         return View();
      }

      // inviare un messaggio all'utente con id = id
      // GET: Messages/Create
      public IActionResult Create( string id ) {
         if( !LoginChecker.HasLoggedUser( this ) )
            return HttpBadRequest();

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
            var loggedUsr = _context.Users.Single( u => u.Id == User.GetUserId() );
            var receiverUsr = _context.Users.SingleOrDefault( u => u.Id == messageCreate.ReceiverId );

            if( receiverUsr == null )
               return HttpBadRequest(); //id utente non valido

            _context.Messages.Add( new Message() { Sender = loggedUsr, Receiver = receiverUsr, Text = messageCreate.Text, DateTime = DateTime.Now} );
            _context.SaveChanges();
             return RedirectToAction( "Details", new {id = receiverUsr.Id} );
         }
         return View( messageCreate );
      }

      // GET: Messages/Delete/5
      [ActionName( "Delete" )]
      public IActionResult Delete( int? id ) {
         if( id == null )
            return HttpNotFound();

         var message = _context.Messages.SingleOrDefault( m => m.Id == id );
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