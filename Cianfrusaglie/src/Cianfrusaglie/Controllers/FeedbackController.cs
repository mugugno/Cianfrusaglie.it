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
            var announce = _context.Announces.SingleOrDefault( a => a.Id == announceId );
            return announce != null && announce.Interested.Any( interested => interested.UserId == userId );
        }

        private bool IsUserTheLastChoosenForTheAnnounce( int announceId, string userId ) {
            var announce = _context.Announces.Include( a => a.ChosenUsers ).SingleOrDefault(a => a.Id == announceId);
            if( announce == null )
                return false;
            {
                var lastChoosenUser = announce.ChosenUsers.OrderByDescending( a => a.ChosenDateTime ).FirstOrDefault();
                return lastChoosenUser != null && lastChoosenUser.ChosenUser.Id == userId;
            }
        }

        // GET: Feedback/Create
        public IActionResult Create(int announceId, string receiverId) {
            CommonFunctions.SetRootLayoutViewData(this, _context);
            var announce = _context.Announces.Single( a => a.Id.Equals( announceId ) );
            var user = _context.Users.Single( u => u.Id.Equals( receiverId ) );
            ViewData["announce"] = announce;
            ViewData["receiver"] = user;
            ViewData["authorId"] = User.GetUserId();
            return View();
        }

        // POST: Feedback/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(FeedBack feedBack) {
            feedBack.Announce = _context.Announces.Single( a => a.Id.Equals( feedBack.AnnounceId ) );
            feedBack.Receiver = _context.Users.Single(a => a.Id.Equals(feedBack.ReceiverId));
            feedBack.Author = _context.Users.Single(a => a.Id.Equals(feedBack.AuthorId));
            if (ModelState.IsValid)
            {
                if (!LoginChecker.HasLoggedUser(this))
                    return HttpBadRequest();
                var announce = feedBack.Announce;
                if( announce != null ) {
                    if( feedBack.Author.Id == announce.AuthorId ) {
                        //autore da feedback a prescelto
                        if( IsUserTheLastChoosenForTheAnnounce( announce.Id, feedBack.Receiver.Id ) ) {
                            _context.FeedBacks.Add(feedBack);
                            _context.SaveChanges();
                            return RedirectToAction("Index");
                        }
                    }
                    if( IsUserInterestedToAnnounce( announce.Id, feedBack.Author.Id ) ) {
                        //interessato da feedback ad annuncio (autore dell'annuncio)
                        if( announce.AuthorId == feedBack.Receiver.Id ) {
                            _context.FeedBacks.Add(feedBack);
                            _context.SaveChanges();
                            return RedirectToAction("Index");
                        }
                    }
                    return HttpBadRequest();
                }
                return HttpBadRequest();
            }
            CommonFunctions.SetRootLayoutViewData(this, _context);
            return RedirectToAction( "Create", new {announceId = feedBack.AnnounceId, receiverId = feedBack.ReceiverId} );
        }
    }
}
