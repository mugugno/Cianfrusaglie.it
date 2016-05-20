using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using Cianfrusaglie.Models;
using Cianfrusaglie.Statics;
using Cianfrusaglie.ViewModels.InterestedAnnounce;

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

            //TODO: aggiungere i ViewData necessari!

            return View(interestedViewModel);
        }

    }
}
