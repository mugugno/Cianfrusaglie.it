using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Cianfrusaglie.Models;
using Cianfrusaglie.Statics;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using static Cianfrusaglie.Constants.CommonFunctions;

namespace Cianfrusaglie.Controllers
{
    public class InterestedInController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InterestedInController(ApplicationDbContext context)
        {
            _context = context;
        }

        public List< Announce > GetLoggedUserInterestedAnnounces()
        {
            var myAnnounces = _context.Announces.Include(p => p.Images).Where(a => a.Interested.Any(u => u.UserId.Equals( User.GetUserId() )));

            return myAnnounces.ToList();
        }
        /// <summary>
        /// Questo metodo carica la pagina con tutti gli annunci a cui è interessato l'utente loggato (membro)
        /// </summary>
        /// <returns>La View degli annunci a cui sei interessato</returns>
        // GET: History
        public IActionResult Index()
        {
            if (!LoginChecker.HasLoggedUser(this))
                return HttpBadRequest();
            ViewData["formCategories"] = _context.Categories.ToList();
            ViewData["numberOfCategories"] = _context.Categories.ToList().Count;
            ViewData["IsThereNewMessage"] = IsThereNewMessage(User.GetUserId(), _context);
            ViewData[" IsThereNewInterested"] = IsThereNewInterested(User.GetUserId(), _context);
            ViewData["IsThereAnyNotification"] = IsThereAnyNotification(User.GetUserId(), _context);
            return View(GetLoggedUserInterestedAnnounces());
        }




    }
}