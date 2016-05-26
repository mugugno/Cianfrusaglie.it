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
            var myAnnounces = _context.Announces.Include(p => p.Images).Where(a => a.Interested.Any(u => u.UserId.Equals( User.GetUserId() )) && !a.Closed);

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
            CommonFunctions.SetRootLayoutViewData( this, _context );
            ViewData["announceIWasChosenFor"] = _context.AnnounceChosenUsers.Where( u=> u.ChosenUserId.Equals( User.GetUserId() ) ).Select(u => u.AnnounceId).ToList();
            ViewData["announceIAlreadyGiveFeedback"] = _context.FeedBacks.Where( f => f.AuthorId.Equals( User.GetUserId() ) ).Select( f => f.AnnounceId ).ToList();
            //    _context.Announces.Include( a => a.ChosenUsers ).Select( a => new { a.Id, a.ChosenUsers }).ToDictionary( k=> k.Id, v=> v.ChosenUsers.ToList() );
            //ViewData["feedbackGivenAuthorsId"] = _context.FeedBacks.Where(  )

            return View(GetLoggedUserInterestedAnnounces());
        }




    }
}