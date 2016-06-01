using System;
using System.Linq;
using System.Security.Claims;
using Cianfrusaglie.Constants;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;
using Cianfrusaglie.Models;
using Cianfrusaglie.Statics;
using Cianfrusaglie.ViewModels.InterestedAnnounce;


namespace Cianfrusaglie.Controllers {
    public class InterestedAnnounceController : Controller {
        private readonly ApplicationDbContext _context;

        public InterestedAnnounceController(ApplicationDbContext context) {
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


            CommonFunctions.SetRootLayoutViewData( this,_context );

            var chosen = announce.ChosenUsers.OrderByDescending( u => u.ChosenDateTime ).FirstOrDefault();
            if( chosen != null ) {
                ViewData["chosenUserId"] = chosen.ChosenUserId;
                ViewData["allOthersChosenUserId"] = announce.ChosenUsers.Where(u => !u.ChosenUserId.Equals(chosen.ChosenUserId)).Select(u => u.ChosenUserId).ToList();
                ViewData["feedbackGivenUsers"] = _context.FeedBacks.Where(f => f.AnnounceId.Equals(announce.Id) && f.ReceiverId.Equals(chosen.ChosenUserId) && f.AuthorId.Equals(User.GetUserId())).Select(f => f.ReceiverId).ToList();
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
            var IChooseYou = new AnnounceChosen() {
                ChosenUserId = userId,
                AnnounceId = announceId,
                ChosenDateTime = DateTime.Now,
            };
            _context.AnnounceChosenUsers.Add( IChooseYou );

            _context.NotificationCenter.Add(new Notification
            {
                User = _context.Users.Single(u => u.Id.Equals(userId)),
                TypeNotification = MessageTypeNotification.NewChoosed

            });

            _context.NotificationCenter.Add(new Notification
            {
                User = _context.Users.Single(u => u.Id.Equals(userId)),
                TypeNotification = MessageTypeNotification.NewFeedback
            });

            var notChooseds = _context.Interested.Where(u => u.AnnounceId.Equals(announceId) && !u.UserId.Equals(userId));
            foreach (var notChoosed in notChooseds)
            {
                var userNotChoosed = _context.Users.Single(u => u.Id.Equals(notChoosed.UserId));
                _context.NotificationCenter.Add(new Notification
                {
                    User = userNotChoosed,
                    TypeNotification = MessageTypeNotification.NewAnotherChoosed
                });
            }

            _context.SaveChanges();
            return RedirectToAction( "Index" , new { id=announceId } );
        }

       public IActionResult Close( int id ) {
         if( !LoginChecker.HasLoggedUser( this ) )
            return HttpBadRequest();

         var announce = _context.Announces.SingleOrDefault( a => a.Id == id );
          if( announce != null && announce.AuthorId == User.GetUserId() ) {
             announce.Closed = true;
             _context.SaveChanges();
          }

          return RedirectToAction( nameof( Index ), new { id = id } );
       }



    }
}
