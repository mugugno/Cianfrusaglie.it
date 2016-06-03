using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Cianfrusaglie.Constants;
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
      /// Ritorna un IEnumerable contenente gli annunci aperi dell'utente loggato.
      /// </summary>
      /// <returns>Restituisce annunci, ancora aperti, pubblicati dall'utente loggato</returns>
      public IEnumerable< Announce > GetLoggedUserPublishedAnnounces() {
         var myAnnounces = _context.Announces.Include( p => p.Images ).Include( p => p.Interested ).Where( a => a.AuthorId == User.GetUserId() && !a.Closed );
         return myAnnounces;
      } 

        /// <summary>
        /// Ritorna gli annunci chiusi pubblicati dall'utente.
        /// </summary>
        /// <returns>Gli annunci chiusi pubblicati dall'utente</returns>
        public IEnumerable< Announce > GetLoggedUserClosedAnnounces() {
            return _context.Announces.Include( a => a.Images ).Where( announce => announce.Closed && announce.AuthorId.Equals( User.GetUserId() ) );
        }

        /// <summary>
        /// Ritorna gli annunci chiusi vinti dall'utente.
        /// </summary>
        /// <returns>Gli annunci chiusi vinti dall'utente</returns>
        public IEnumerable< Announce > GetLoggedUserWonClosedAnnounces() {
            string userId = User.GetUserId();
            var loggedUserWonClosedAnnounces = _context.Announces.Include( a => a.ChosenUsers ).Include( a=> a.Images ).Where(
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
            foreach( var entity in _context.Announces.Include( a => a.Interested ).Include(a => a.Images)) {
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
        /// Ritorna gli annunci aperti a cui l'utente è interessato
        /// </summary>
        /// <returns>gli annunci aperti a cui l'utente è interessato</returns>
        public List<Announce> GetLoggedUserInterestedAnnounces()
        {
            return _context.Announces.Include(p => p.Images).Where(a => a.Interested.Any(u => u.UserId.Equals(User.GetUserId())) && !a.Closed).ToList();
        }


        /// <summary>
        /// Questo metodo carica la pagina con tutti gli annunci pubblicati dall'utente loggato (membro) e con tutti gli annunci a cui è interessato
        /// </summary>
        /// <returns>La View di tutti i tuoi annunci</returns>
        // GET: History
        public IActionResult Index() {
         if( !LoginChecker.HasLoggedUser( this ) )
            return HttpBadRequest();
            SetInterestedToReadStatus(User.GetUserId());
            SetChoosenToReadStatus(User.GetUserId());
            SetRootLayoutViewData( this, _context );
            ViewData["announceIWasChosenFor"] = _context.AnnounceChosenUsers.Where(u => u.ChosenUserId.Equals(User.GetUserId())).Select(u => u.AnnounceId).ToList();
            ViewData["announceIAlreadyGiveFeedback"] = _context.FeedBacks.Where(f => f.AuthorId.Equals(User.GetUserId())).Select(f => f.AnnounceId).ToList();
            var result = new List< IEnumerable< Announce > > {
                GetLoggedUserPublishedAnnounces(),
                GetLoggedUserInterestedAnnounces()
            };
            return View(result);
        }

        /// <summary>
        /// Questo metodo carica la pagina con tutti gli annunci, ormai chiusi, dell'utente loggato (membro) che ha vinto, perso, o inserito lui stesso
        /// </summary>
        /// <returns>La View degli annunci ormai chiusi</returns>
        public IActionResult MyHistory()
        {
            if (!LoginChecker.HasLoggedUser(this))
                return HttpBadRequest();
            SetRootLayoutViewData(this, _context);
            var model = new MyHistoryViewModel
            {
                LostClosedAnnounces = GetLoggedUserLostAnnounces().ToList(),
                PublishedClosedAnnounces = GetLoggedUserClosedAnnounces().ToList(),
                WonClosedAnnounces = GetLoggedUserWonClosedAnnounces().ToList()
            };
            return View(model);
        }


        /// <summary>
        /// L'utente ha visualizzato la notifica (che c'è un nuovo interessato ad un annuncio che ho pubblicato), il db viene aggiornato
        /// </summary>
        /// <param name="id">id dell'utente</param>
        public void SetInterestedToReadStatus(string id)
        {
            var newInterested = _context.NotificationCenter.Where(i => i.UserId.Equals(id) && !i.Read && i.TypeNotification == MessageTypeNotification.NewInterested);
            foreach (var interested in newInterested)
            {
                interested.Read = true;
                //_context.SaveChanges();
            }

            var feedbacks = _context.NotificationCenter.Where(i => i.UserId.Equals(id) && !i.Read &&
            i.TypeNotification == MessageTypeNotification.NewFeedback);

            foreach (var feedback in feedbacks)
            {
                feedback.Read = true;
            }

            _context.SaveChanges();
        }

        /// <summary>
        /// L'utente ha visualizzato la notifica (che sei stato scelto per un annuncio che ti interessa), il db viene aggiornato
        /// </summary>
        /// <param name="id">id dell'utente</param>
        public void SetChoosenToReadStatus(string id)
        {
            var newChoosed = _context.NotificationCenter.Where(i => i.UserId.Equals(id) && !i.Read &&
            (i.TypeNotification == MessageTypeNotification.NewChoosed ||
            i.TypeNotification == MessageTypeNotification.NewAnotherChoosed));
            foreach (var choosed in newChoosed)
            {
                choosed.Read = true;
                //_context.SaveChanges();
            }

            var feedbacks = _context.NotificationCenter.Where(i => i.UserId.Equals(id) && !i.Read &&
            i.TypeNotification == MessageTypeNotification.NewFeedback);

            foreach (var feedback in feedbacks)
            {
                feedback.Read = true;
            }

            _context.SaveChanges();
        }

    }
}