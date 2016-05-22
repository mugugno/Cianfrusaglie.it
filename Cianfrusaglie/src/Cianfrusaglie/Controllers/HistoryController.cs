using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
         var myAnnounces = _context.Announces.Include( p => p.Images ).Where( a => a.AuthorId == User.GetUserId() );
         return myAnnounces;
      } 

      /// <summary>
      /// Ritorna una dizionario contenente gli utenti interessati ad un mio annuncio.
      /// </summary>
      /// <returns>Dizionario la cui chiave è l'id dell'annuncio e il valore è la lista degli id degli utenti interessati</returns>
      public Dictionary<int,List<int>> GetInterestedToAnnounces(){
            var announces = GetLoggedUserPublishedAnnounces();
            var dictionary = new Dictionary<int, List<int>>();
            foreach(var announce in announces)
            {
                var interessati = new List<int>();
                interessati.Add(_context.Interested.Where(i => i.AnnounceId.Equals(announce.Id)).ToList().Count);
                interessati.Add(_context.Interested.Where(i => i.AnnounceId.Equals(announce.Id) && !i.Read).ToList().Count);
                dictionary.Add(announce.Id, interessati);
            }
            return dictionary;
        
      }

      /// <summary>
      /// Questo metodo carica la pagina con tutti gli annunci pubblicati dall'utente loggato (membro)
      /// </summary>
      /// <returns>La View di tutti i tuoi annunci</returns>
      // GET: History
      public IActionResult Index() {
         if( !LoginChecker.HasLoggedUser( this ) )
            return HttpBadRequest();
        ViewData["formCategories"] = _context.Categories.ToList();
        ViewData["numberOfCategories"] = _context.Categories.ToList().Count;
        ViewData["IsThereNewMessage"] = IsThereNewMessage(User.GetUserId(), _context);
        ViewData[" IsThereNewInterested"] = IsThereNewInterested(User.GetUserId(), _context);
        ViewData["IsThereAnyNotification"] = IsThereAnyNotification(User.GetUserId(), _context);
        ViewData["Interested"] = GetInterestedToAnnounces();
        return View( GetLoggedUserPublishedAnnounces().ToList() );
      }
      
   }
}