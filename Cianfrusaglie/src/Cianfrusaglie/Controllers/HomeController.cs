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
         ViewData[ "listImages" ] = _context.ImageUrls.ToList();
         //Include(u=>u.AnnounceCategories).
         ViewData[ "listAnnounces" ] = _context.Announces.Include( a => a.Author ).OrderByDescending( u => u.PublishDate ).Take( 3 ).ToList();

         if( LoginChecker.HasLoggedUser( this ) ) {
            var user = _context.Users.Single( u => User.GetUserId().Equals( u.Id ) );
            ViewData[ "listSuggestedAnnounces" ] = GetSuggestedAnnounces( _context, this ).Take( 3 ).ToList();
         } else
            ViewData[ "listSuggestedAnnounces" ] = new List< Announce >();
         SetRootLayoutViewData( this, _context );
         ViewData[ "listCategory" ] = ViewData[ "formCategories" ];
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