using System.Linq;
using System.Security.Claims;
using Microsoft.AspNet.Mvc;
using Cianfrusaglie.Models;

namespace Cianfrusaglie.Controllers {
   public class MessagesController : Controller {
      private readonly ApplicationDbContext _context;

      public MessagesController( ApplicationDbContext context ) { _context = context; }

      //tutti gli utenti con cui l'utente loggato ha messaggiato
      // GET: Messages
      public IActionResult Index() { 
         var usr = _context.Users.Single( u => u.Id == User.GetUserId() );
         var userWitchIHaveMessaged = usr.SentMessages.Select( m => m.Receiver );
         var userThatSendedMeMessage = usr.ReceivedMessages.Select( m => m.Sender );

         var users = userWitchIHaveMessaged.Union( userThatSendedMeMessage ).Distinct();

         return View( users.ToList() );
      }

      // GET: Messages/Details/5
      public IActionResult Details( string id ) {
         if( id == null )
            return HttpNotFound();

         var messages =
            _context.Messages.Where(
               m =>
                  ( m.Sender.Id == User.GetUserId() && m.Receiver.Id == id ) ||
                  ( m.Receiver.Id == User.GetUserId() && m.Sender.Id == id ) );

         return View( messages.ToList() );
      }

      // GET: Messages/Create
      public IActionResult Create() { return View(); }

      // POST: Messages/Create
      [HttpPost, ValidateAntiForgeryToken]
      public IActionResult Create( Message message ) {
         if( ModelState.IsValid ) {
            _context.Messages.Add( message );
            _context.SaveChanges();
            return RedirectToAction( "Index" );
         }
         return View( message );
      }

      // GET: Messages/Edit/5
      public IActionResult Edit( int? id ) {
         if( id == null )
            return HttpNotFound();

         var message = _context.Messages.Single( m => m.Id == id );
         if( message == null )
            return HttpNotFound();
         return View( message );
      }

      // POST: Messages/Edit/5
      [HttpPost, ValidateAntiForgeryToken]
      public IActionResult Edit( Message message ) {
         if( ModelState.IsValid ) {
            _context.Update( message );
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