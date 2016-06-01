using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Cianfrusaglie.Models;
using Cianfrusaglie.Statics;
using Cianfrusaglie.ViewModels.History;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;
using static Cianfrusaglie.Constants.CommonFunctions;

namespace Cianfrusaglie.Controllers {
    public class HistoryController : Controller {
        private readonly ApplicationDbContext _context;

        public HistoryController( ApplicationDbContext context ) { _context = context; }

        /// <summary>
        ///     Ritorna un IEnumerable contenente gli annunci dell'utente loggato.
        /// </summary>
        /// <returns>Restituisce annunci pubblicati dall'utente loggato</returns>
        public IEnumerable< Announce > GetLoggedUserPublishedAnnounces() {
            var myAnnounces =
                _context.Announces.Include( p => p.Images ).Include( p => p.Interested ).Where(
                    a => a.AuthorId == User.GetUserId() );
            return myAnnounces;
        }

        /// <summary>
        /// Ritorna gli annunci chiusi pubblicati dall'utente.
        /// </summary>
        /// <returns>Gli annunci chiusi pubblicati dall'utente</returns>
        public IEnumerable< Announce > GetLoggedUserClosedAnnounces() {
            return _context.Announces.Where( announce => announce.Closed && announce.AuthorId.Equals( User.GetUserId() ) );
        }

        /// <summary>
        /// Ritorna gli annunci chiusi vinti dall'utente.
        /// </summary>
        /// <returns>Gli annunci chiusi vinti dall'utente</returns>
        public IEnumerable< Announce > GetLoggedUserWonClosedAnnounces() {
            string userId = User.GetUserId();
            var loggedUserWonClosedAnnounces = _context.Announces.Include( a => a.ChosenUsers ).Where(
                announce =>
                    announce.Closed && userId.Equals(announce.ChosenUsers.OrderByDescending(a => a.ChosenDateTime).FirstOrDefault().ChosenUserId) );
            return
                loggedUserWonClosedAnnounces;
        }

        /// <summary>
        /// Ritorna gli annunci chiusi interessati all'utente
        /// </summary>
        /// <returns>Gli annunci chiusi interessati all'utente</returns>
        private IEnumerable< Announce > GetLoggedUserInterestedClosedAnnounce() {
            foreach( var entity in _context.Announces.Include( a => a.Interested ) ) {
                if( entity.Closed  ) {
                    foreach( var interested in entity.Interested ) {
                        if( interested.UserId.Equals( User.GetUserId() ) ) {
                            yield return entity;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Ritorna gli annunci chiusi persi dell'utente (ovvero gli annunci per cui l'utente ha avuto interesse ma non è stato scelto come assegnatario)
        /// </summary>
        /// <returns>gli annunci chiusi persi dell'utente</returns>
        public IEnumerable< Announce > GetLoggedUserLostAnnounces() {
            return GetLoggedUserInterestedClosedAnnounce().Except( GetLoggedUserWonClosedAnnounces() );
        }

        /// <summary>
        ///     Questo metodo carica la pagina con tutti gli annunci pubblicati dall'utente loggato (membro)
        /// </summary>
        /// <returns>La View di tutti i tuoi annunci</returns>
        // GET: History
        public IActionResult Index() {
            if( !LoginChecker.HasLoggedUser( this ) )
                return HttpBadRequest();
            SetRootLayoutViewData( this, _context );

            return View( GetLoggedUserPublishedAnnounces().ToList() );
        }

        public IActionResult MyHistory() {
            if(!LoginChecker.HasLoggedUser(this)) 
                return HttpBadRequest();
            SetRootLayoutViewData(this, _context);
            var model = new MyHistoryViewModel {
                LostClosedAnnounces = GetLoggedUserLostAnnounces().ToList(),
                PublishedClosedAnnounces = GetLoggedUserClosedAnnounces().ToList(),
                WonClosedAnnounces = GetLoggedUserWonClosedAnnounces().ToList()
            };
            return View( model );
        }
    }
}