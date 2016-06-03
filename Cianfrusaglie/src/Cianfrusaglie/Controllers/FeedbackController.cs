using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Cianfrusaglie.Constants;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using Cianfrusaglie.Models;
using Cianfrusaglie.Statics;


namespace Cianfrusaglie.Controllers
{
    public class FeedbackController : Controller
    {
        private ApplicationDbContext _context;

        public FeedbackController(ApplicationDbContext context)
        {
            _context = context;    
        }

        private bool IsUserInterestedToAnnounce( int announceId, string userId ) {
            var announce = _context.Announces.Include( a => a.Interested ).SingleOrDefault( a => a.Id == announceId );
            return announce != null && announce.Interested.Any( interested => interested.UserId == userId );
        }

        private bool IsUserChoosenForTheAnnounce( int announceId, string userId ) {
            var announce = _context.Announces.Include( a => a.ChosenUsers ).SingleOrDefault(a => a.Id == announceId);
            return announce != null && announce.ChosenUsers.Any( chosen => chosen.ChosenUserId == userId );
            }

        // GET: Feedback/Create
        public IActionResult Create(int announceId, string receiverId) {
            if (!LoginChecker.HasLoggedUser(this))
                return HttpBadRequest();
            CommonFunctions.SetRootLayoutViewData(this, _context);
            var receiver = _context.Users.SingleOrDefault( u => u.Id.Equals( receiverId ) );
            if(receiver==null)
                return HttpNotFound();
            ViewData["announce"] = announceId;
            ViewData["receiver"] = receiver;
            if(_context.FeedBacks.Any(f=> f.AnnounceId.Equals( announceId ) && f.ReceiverId.Equals( receiverId ) && f.AuthorId.Equals( User.GetUserId())))
            {
                return HttpBadRequest();
            }
            return View();
        }

        // POST: Feedback/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(FeedBack feedBack) {
            if (!LoginChecker.HasLoggedUser(this))
                return HttpBadRequest();
            if (_context.FeedBacks.Any(f => f.AnnounceId.Equals(feedBack.AnnounceId) && f.ReceiverId.Equals(feedBack.ReceiverId) && f.AuthorId.Equals(User.GetUserId())))
            {
                return HttpBadRequest();
            }
            var announce = _context.Announces.SingleOrDefault( a => a.Id.Equals( feedBack.AnnounceId ) );
            if (ModelState.IsValid) {
                feedBack.DateTime = DateTime.Now;
                User receiver;
                // o sei l'autore dell'annuncio che lascia il feedback a un prescelto
                if (feedBack.AuthorId.Equals(announce.AuthorId))
                {
                    //autore da feedback a prescelto
                    if (IsUserChoosenForTheAnnounce(announce.Id, feedBack.ReceiverId))
                    {
                        _context.FeedBacks.Add(feedBack);
                        _context.SaveChanges();
                        receiver = _context.Users.First( u => u.Id.Equals( feedBack.ReceiverId ) );
                        receiver.FeedbacksCount++;
                        receiver.FeedbacksSum += feedBack.Vote;
                        receiver.FeedbacksMean = (double)receiver.FeedbacksSum / receiver.FeedbacksCount;
                        _context.SaveChanges();
                        _context.NotificationCenter.Add(new Notification
                        {
                            User = receiver,
                            TypeNotification = MessageTypeNotification.NewReceivedFeedback
                        });
                        _context.SaveChanges();
                        return RedirectToAction( nameof(InterestedAnnounceController.Index), "InterestedAnnounce",new {id=announce.Id});
                    }
                }
                // oppure sei un interessato, che e' stato scelto, che da feedback all'autore dell'annuncio
                if (!IsUserChoosenForTheAnnounce(announce.Id, feedBack.AuthorId))
                    return new BadRequestResult();
                if (announce.AuthorId != feedBack.ReceiverId)
                    return new BadRequestResult();
                _context.FeedBacks.Add(feedBack);
                _context.SaveChanges();
                receiver = _context.Users.First(u => u.Id.Equals(feedBack.ReceiverId));
                receiver.FeedbacksCount++;
                receiver.FeedbacksSum += feedBack.Vote;
                receiver.FeedbacksMean = (double)receiver.FeedbacksSum / receiver.FeedbacksCount;
                _context.SaveChanges();
                var userReceiver = _context.Users.Single(u => u.Id.Equals(announce.AuthorId));
                _context.NotificationCenter.Add(new Notification
                {
                    User = userReceiver,
                    TypeNotification = MessageTypeNotification.NewReceivedFeedback
                });
                _context.SaveChanges();
                return RedirectToAction(nameof(InterestedInController.Index), "InterestedIn");
            }
            ViewData["announce"] = feedBack.AnnounceId;
            ViewData["receiver"] = feedBack.ReceiverId;
            CommonFunctions.SetRootLayoutViewData(this, _context);
            return View( feedBack );
            
        }
    }
}
