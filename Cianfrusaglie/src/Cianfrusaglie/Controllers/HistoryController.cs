using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Cianfrusaglie.Models;
using Cianfrusaglie.Statics;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;

namespace Cianfrusaglie.Controllers {
   public class HistoryController : Controller {
      private readonly ApplicationDbContext _context;

      public HistoryController( ApplicationDbContext context ) {
         _context = context;
      }

      public IEnumerable< Announce > GetLoggedUserPublishedAnnounces() {
         var myAnnounces = _context.Announces.Where( a => a.AuthorId == User.GetUserId() );
         return myAnnounces;
      } 

      // GET: History
      public IActionResult Index() {
         if( !LoginChecker.HasLoggedUser( this ) )
            return HttpBadRequest();
        ViewData["formCategories"] = _context.Categories.ToList();
        ViewData["numberOfCategories"] = _context.Categories.ToList().Count;
        return View( GetLoggedUserPublishedAnnounces().ToList() );
      }
      
   }
}