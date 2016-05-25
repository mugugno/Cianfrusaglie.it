using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Cianfrusaglie.Models;
using Cianfrusaglie.Statics;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc;
using static Cianfrusaglie.Constants.CommonFunctions;
using Cianfrusaglie.Suggestions;

namespace Cianfrusaglie.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        public UserManager<User> UserManager;
        public RankAlgorithm rankAlgorithm;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
            rankAlgorithm = new RankAlgorithm(context);
        }

        public IActionResult Index()
        {
            ViewData["listImages"] = _context.ImageUrls.ToList();
            ViewData["listUsers"] = _context.Users.ToList();
            ViewData["listAnnounces"] = _context.Announces.OrderByDescending(u => u.PublishDate).Take(3).ToList();

            if( LoginChecker.HasLoggedUser( this ) ) {
                var user = _context.Users.Single( u => User.GetUserId().Equals( u.Id ) );
                ViewData[ "listSuggestedAnnounces" ] =
                    _context.Announces.Where(
                        a => !a.AuthorId.Equals( User.GetUserId() ) &&
                            GeoPosition.GeoCoordinate.Distance( a.Latitude, a.Longitude, user.Latitude, user.Longitude ) <=
                            100 ).OrderByDescending( a => rankAlgorithm.calculateRank( a, user ) ).Take( 3 ).ToList();
            } else {
                ViewData[ "listSuggestedAnnounces" ] = new List< Announce >();
            }
            ViewData["formCategories"] = _context.Categories.ToList();
            ViewData["numberOfCategories"] = _context.Categories.ToList().Count;
            ViewData["IsThereNewMessage"] = IsThereNewMessage(User.GetUserId(), _context);
            ViewData[" IsThereNewInterested"] = IsThereNewInterested(User.GetUserId(), _context);
            ViewData["IsThereAnyNotification"] = IsThereAnyNotification(User.GetUserId(), _context);
            ViewData["listCategory"] = _context.Categories.ToList();
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error() { return View(); }
    }
}