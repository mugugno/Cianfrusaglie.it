using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using Cianfrusaglie.Models;
using Cianfrusaglie.Statics;
using Cianfrusaglie.ViewModels.InterestedAnnounce;
using static Cianfrusaglie.Constants.CommonFunctions;


namespace Cianfrusaglie.Controllers
{
    public class InterestedAnnounceController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InterestedAnnounceController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: InterestedAnnounce
        public IActionResult Index(int id) {
            var announce = _context.Announces.SingleOrDefault( a => a.Id == id );
            if( announce == null )
                return HttpNotFound();
            if( !LoginChecker.HasLoggedUser( this ) )
                return HttpBadRequest();
            if( !announce.AuthorId.Equals( User.GetUserId() ) )
                return HttpBadRequest();
            var interested =
                _context.Interested.Include( i => i.User ).Where( i => i.AnnounceId.Equals( id ) ).Select( u => u.User ).ToList();
            var interestedViewModel = new InterestedAnnounceViewModel() {Announce = announce, InterestedUsers = interested};

            ViewData["formCategories"] = _context.Categories.ToList();
            ViewData["numberOfCategories"] = _context.Categories.ToList().Count;
            ViewData["IsThereNewMessage"] = IsThereNewMessage(User.GetUserId(), _context);
            SetInterestedToReadStatus(id);
            ViewData["IsThereNewInterested"] = IsThereNewInterested(User.GetUserId(), _context);
            ViewData["IsThereAnyNotification"] = IsThereAnyNotification(User.GetUserId(), _context);



            return View(interestedViewModel);
        }
        //  GET: InterestedAnnounce/?userId,announceId
        public IActionResult ChooseUserAsReceiverForAnnounce(string userId, int announceId) {
            var announce = _context.Announces.SingleOrDefault(a => a.Id == announceId);
            if (announce == null)
                return HttpNotFound();
            if ( string.IsNullOrEmpty( userId )) {
                return HttpBadRequest();
            }
            if (!LoginChecker.HasLoggedUser(this))
                return HttpBadRequest();
            if (!announce.AuthorId.Equals(User.GetUserId()))
                return HttpBadRequest();
            AnnounceChosen IChooseYou = new AnnounceChosen() {
                ChosenUserId = userId,
                AnnounceId = announceId,
                ChosenDateTime = DateTime.Now,
            };
            _context.AnnounceChosenUsers.Add( IChooseYou );
            _context.SaveChanges();
            return RedirectToAction( "Index" , new { id=announceId } );
        }

        public void SetInterestedToReadStatus(int id)
        {
            var newInterested = _context.Interested.Where(i => !i.Read && i.AnnounceId.Equals(id));
            foreach (var interested in newInterested)
            {
                interested.Read = true;
                //_context.SaveChanges();
            }
            _context.SaveChanges();

        }

    }
}
