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

        private bool IsUserChoosenForTheAnnounce( int announceId, string userId ) {
            var announce = _context.Announces.Include( a => a.ChosenUsers ).SingleOrDefault(a => a.Id == announceId);
            return announce != null && announce.ChosenUsers.Any( chosen => chosen.ChosenUserId == userId );
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
            var announce = _context.Announces.SingleOrDefault( a => a.Id.Equals( feedBack.AnnounceId ) );
            if( announce == null )
                return HttpBadRequest();
            feedBack.Announce = announce;
            var receiver = _context.Users.SingleOrDefault(a => a.Id.Equals(feedBack.ReceiverId));
            if(receiver == null)
                return HttpBadRequest();
            feedBack.Receiver = receiver;
            var author = _context.Users.SingleOrDefault(a => a.Id.Equals(feedBack.AuthorId));
            if( author == null )
                return HttpBadRequest();
            feedBack.Author = author;

            if (ModelState.IsValid)
            {
                if (!LoginChecker.HasLoggedUser(this))
                    return HttpBadRequest();
                
                if (feedBack.Author.Id == announce.AuthorId)
                {
                    //autore da feedback a prescelto
                    if (IsUserChoosenForTheAnnounce(announce.Id, feedBack.Receiver.Id))
                    {
                        _context.FeedBacks.Add(feedBack);
                        _context.SaveChanges();
                        return RedirectToAction("InterestedAnnounce",new {id=announce.Id});
                    }
                }
                if (!IsUserInterestedToAnnounce(announce.Id, feedBack.Author.Id))
                    return new BadRequestResult();
                //interessato da feedback ad annuncio (autore dell'annuncio)
                if (announce.AuthorId != feedBack.Receiver.Id)
                    return new BadRequestResult();
                _context.FeedBacks.Add(feedBack);
                _context.SaveChanges();
                return RedirectToAction("InterestedAnnounce", new { id = announce.Id });
            }
            CommonFunctions.SetRootLayoutViewData(this, _context);
            return RedirectToAction("Create", new { announceId = feedBack.AnnounceId, receiverId = feedBack.ReceiverId });
            
        }
    }
}
