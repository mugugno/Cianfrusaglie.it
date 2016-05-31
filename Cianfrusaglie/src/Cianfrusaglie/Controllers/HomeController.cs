using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Cianfrusaglie.Models;
using Cianfrusaglie.Statics;
using Cianfrusaglie.Suggestions;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;
using static Cianfrusaglie.Constants.CommonFunctions;

namespace Cianfrusaglie.Controllers {
   public class HomeController : Controller {
      private readonly ApplicationDbContext _context;
      public RankAlgorithm RankAlgorithm;
      public UserManager< User > UserManager;

      public HomeController( ApplicationDbContext context ) {
         _context = context;
         RankAlgorithm = new RankAlgorithm( context );
      }

      public IActionResult Index() {
         //Include(u=>u.AnnounceCategories).
         var ann= _context.Announces.Include( a => a.Author ).OrderByDescending( u => u.PublishDate ).Take( 3 ).ToList();
         ViewData[ "listAnnounces" ] = ann;

         IEnumerable< Announce > suggestions;
         if( LoginChecker.HasLoggedUser( this ) ) {
            //var user = _context.Users.Single( u => User.GetUserId().Equals( u.Id ) );
            suggestions = GetSuggestedAnnounces( _context, this ).Take( 3 ).ToList();
         } else
            suggestions = new List< Announce >();

         ViewData[ "listSuggestedAnnounces" ] = suggestions;

         SetRootLayoutViewData( this, _context );
         ViewData[ "listCategory" ] = ViewData[ "formCategories" ];
         ViewData[ "listImages" ] = _context.ImageUrls.Where( i => ann.Select( a => a.Id ).Contains( i.AnnounceId ) || suggestions.Select( a => a.Id ).Contains( i.AnnounceId ) ).ToList();

         return View();
      }

      public IActionResult About() {
         ViewData[ "Message" ] = "Your application description page.";

         return View();
      }

      public IActionResult Contact() {
         ViewData[ "Message" ] = "Your contact page.";

         return View();
      }

      public IActionResult Error() { return View(); }
   }
}