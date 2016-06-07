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
         /// Ritorna un IEnumerable contenente gli annunci aperti dell'utente loggato.
         /// </summary>
         /// <returns>Restituisce annunci, ancora aperti, pubblicati dall'utente loggato</returns>
        public IEnumerable< Announce > GetLoggedUserOpenPublishedAnnounces() {
            return _context.Announces.Include( a => a.Images ).Include(a => a.Interested).Include( a=> a.ChosenUsers ).Where( a => a.AuthorId == User.GetUserId() && !a.Closed );
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
            var loggedUserWonClosedAnnounces = _context.Announces.Include( a => a.ChosenUsers ).Include( a=> a.Images ).Include( a => a.FeedBacks ).Where(
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
            foreach( var entity in _context.Announces.Include( a => a.Interested ).Include(a => a.Images).Include(a => a.FeedBacks)) {
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
        public IEnumerable<Announce> GetLoggedUserOpenInterestedAnnounces()
        {
           return _context.Announces.Include(p => p.Images).Where(a => a.Interested.Any(u => u.UserId.Equals(User.GetUserId())) && !a.Closed);
        }

        /// <summary>
        /// Ritorna gli annunci aperti a cui l'utente è interessato ed è attualmente l'ultimo chosen (cioè è il vincitore temporaneo)
        /// </summary>
        /// <returns> la lista degli annunci aperti a cui l'utente è interessato ed è attualmente l'ultimo chosen</returns>
        public IEnumerable< Announce > GetLoggedUserOpenAnnouncesHeIsWinning() {
            var tmp = _context.Announces.Where( a => a.ChosenUsers.Any( c=> c.ChosenUserId.Equals( User.GetUserId() )));
            return tmp.Where(
                a =>
                    a.ChosenUsers.OrderByDescending(c => c.ChosenDateTime).First().ChosenUserId.Equals(
                        User.GetUserId())).ToList();
        }

        /// <summary>
        /// Ritorna gli annunci aperti a cui l'utente è interessato, ed è stato scelto (ora potrebbe anche essere stato rimosso), per cui deve dare il feedback (o lo ha già fatto)
        /// </summary>
        /// <returns>gli annunci aperti in cui l'utente è nella chosen list</returns>
        public IEnumerable< Announce > GetLoggedUserOpenAnnouncesHeHasToGiveFeedback() {
           return _context.AnnounceChosenUsers.Where(u => u.ChosenUserId.Equals(User.GetUserId())).Select(u => u.Announce);
       }

        /// <summary>
        /// Ritorna gli annunci aperti di cui l'utente è proprietario o interessato, in ogni caso ha già dato il suo feedback
        /// </summary>
        /// <returns>gli annunci di cui l'utente ha già dato il feedback</returns>
       public IEnumerable< Announce > GetLoggedUserOpenAnnouncesHeAlreadyGaveFeedback() {
           return _context.FeedBacks.Where( f => f.AuthorId.Equals( User.GetUserId() ) ).Select( f => f.Announce );
       } 
        
        /* AZIONI DEL CONTROLLER */
        

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
            //ViewData["announcesImWinning"] = _context.Announces.Where(a => a.ChosenUsers.OrderBy(c => c.ChosenDateTime).Last().ChosenUser.Id.Equals(User.GetUserId())).Select(a => a.Id).ToList();
            //ViewData["announceIWasChosenFor"] = _context.AnnounceChosenUsers.Where(u => u.ChosenUserId.Equals(User.GetUserId())).Select(u => u.Announce)
            //ViewData["announceIAlreadyGiveFeedback"] = _context.FeedBacks.Where(f => f.AuthorId.Equals(User.GetUserId())).Select(f => f.AnnounceId).ToList();
            var result = new List< IEnumerable< Announce > > {
                GetLoggedUserOpenPublishedAnnounces(),
                GetLoggedUserOpenInterestedAnnounces(),
                GetLoggedUserOpenAnnouncesHeIsWinning(),
                GetLoggedUserOpenAnnouncesHeHasToGiveFeedback(),
                GetLoggedUserOpenAnnouncesHeAlreadyGaveFeedback()
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
            SetCloseAnnounceToRead(User.GetUserId());
            SetRootLayoutViewData(this, _context);
            var model = new MyHistoryViewModel
            {
                LostClosedAnnounces = GetLoggedUserLostAnnounces().ToList(),
                PublishedClosedAnnounces = GetLoggedUserClosedAnnounces().ToList(),
                WonClosedAnnounces = GetLoggedUserWonClosedAnnounces().ToList()
            };
            return View(model);
        }

        /* NOTIFICHE */


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


        //TODO summary
        public void SetCloseAnnounceToRead(string id)
        {
            var notifications = _context.NotificationCenter.Where(u=>u.UserId.Equals(id) && !u.Read && u.TypeNotification==MessageTypeNotification.NewAnotherChoosed);
            foreach(var notification in notifications)
            {
                notification.Read = true;
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
            (i.TypeNotification == MessageTypeNotification.NewChoosed));
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