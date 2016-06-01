using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Cianfrusaglie.Constants;
using Cianfrusaglie.Models;
using Cianfrusaglie.Statics;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using static Cianfrusaglie.Constants.CommonFunctions;

namespace Cianfrusaglie.Controllers {
   public class HistoryController : Controller {
      private readonly ApplicationDbContext _context;

      public HistoryController( ApplicationDbContext context ) {
         _context = context;
      }

      /// <summary>
      /// Ritorna un IEnumerable contenente gli annunci dell'utente loggato.
      /// </summary>
      /// <returns>Restituisce annunci pubblicati dall'utente loggato</returns>
      public IEnumerable< Announce > GetLoggedUserPublishedAnnounces() {
         var myAnnounces = _context.Announces.Include( p => p.Images ).Include( p => p.Interested ).Where( a => a.AuthorId == User.GetUserId() );
         SetInterestedToReadStatus(User.GetUserId());
         return myAnnounces;
      } 

      /// <summary>
      /// Questo metodo carica la pagina con tutti gli annunci pubblicati dall'utente loggato (membro)
      /// </summary>
      /// <returns>La View di tutti i tuoi annunci</returns>
      // GET: History
      public IActionResult Index() {
         if( !LoginChecker.HasLoggedUser( this ) )
            return HttpBadRequest();
        CommonFunctions.SetRootLayoutViewData( this, _context );

        return View( GetLoggedUserPublishedAnnounces().ToList() );
      }

        //TODO SUMMARY
        public void SetInterestedToReadStatus(string id)
        {
            var newInterested = _context.NotificationCenter.Where(i => i.UserId.Equals(id) && !i.Read && i.TypeNotification == MessageTypeNotification.NewInterested);
            foreach (var interested in newInterested)
            {
                interested.Read = true;
                //_context.SaveChanges();
            }
            _context.SaveChanges();
        }

    }
}