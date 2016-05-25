using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Cianfrusaglie.Constants;
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
            var announce = _context.Announces.Include( a=>a.ChosenUsers ).SingleOrDefault( a => a.Id == id );
            if( announce == null )
                return HttpNotFound();
            if( !LoginChecker.HasLoggedUser( this ) )
                return HttpBadRequest();
            if( !announce.AuthorId.Equals( User.GetUserId() ) )
                return HttpBadRequest();
            var interested =
                _context.Interested.Include( i => i.User ).Where( i => i.AnnounceId.Equals( id ) ).Select( u => u.User ).ToList();
            var interestedViewModel = new InterestedAnnounceViewModel() {Announce = announce, InterestedUsers = interested};

            SetInterestedToReadStatus(id);
            CommonFunctions.SetRootLayoutViewData( this,_context );

            var chosen = announce.ChosenUsers.OrderByDescending( u => u.ChosenDateTime ).FirstOrDefault();
            if( chosen != null ) {
                ViewData["chosenUserId"] = chosen.ChosenUserId;
                ViewData["allOthersChosenUserId"] = announce.ChosenUsers.Where(u => !u.ChosenUserId.Equals(chosen.ChosenUserId)).Select(u => u.Id);
            }
               
           
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
