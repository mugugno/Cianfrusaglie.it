using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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

        // GET: Feedback
        public IActionResult Index()
        {
            var applicationDbContext = _context.FeedBacks.Include(f => f.Announce);
            return View(applicationDbContext.ToList());
        }

        // GET: Feedback/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            FeedBack feedBack = _context.FeedBacks.Single(m => m.Id == id);
            if (feedBack == null)
            {
                return HttpNotFound();
            }

            return View(feedBack);
        }

        // GET: Feedback/Create
        public IActionResult Create(int announceId, string receiverId) {
            var announce = _context.Announces.Single( a => a.Id.Equals( announceId ) );
            var user = _context.Users.Single( u => u.Id.Equals( receiverId ) );
            ViewData["announce"] = announce;
            ViewData["receiver"] = user;
            return View();
        }

        // POST: Feedback/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(FeedBack feedBack)
        {
            if (ModelState.IsValid)
            {
                if (!LoginChecker.HasLoggedUser(this))
                    return HttpBadRequest();
                var announce = feedBack.Announce;
                if( announce == null )
                    return new BadRequestResult();
                if( feedBack.Author.Id == announce.AuthorId ) {
                    //autore da feedback a prescelto
                    if( IsUserChoosenForTheAnnounce( announce.Id, feedBack.Receiver.Id ) ) {
                        _context.FeedBacks.Add(feedBack);
                        _context.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }
                if( !IsUserInterestedToAnnounce( announce.Id, feedBack.Author.Id ) )
                    return new BadRequestResult();
                //interessato da feedback ad annuncio (autore dell'annuncio)
                if( announce.AuthorId != feedBack.Receiver.Id )
                    return new BadRequestResult();
                _context.FeedBacks.Add(feedBack);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewData["AnnounceId"] = new SelectList(_context.Announces, "Id", "Announce", feedBack.AnnounceId);
            return View(feedBack);
        }

        // GET: Feedback/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            FeedBack feedBack = _context.FeedBacks.Single(m => m.Id == id);
            if (feedBack == null)
            {
                return HttpNotFound();
            }
            ViewData["AnnounceId"] = new SelectList(_context.Announces, "Id", "Announce", feedBack.AnnounceId);
            return View(feedBack);
        }

        // POST: Feedback/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(FeedBack feedBack)
        {
            if (ModelState.IsValid)
            {
                _context.Update(feedBack);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewData["AnnounceId"] = new SelectList(_context.Announces, "Id", "Announce", feedBack.AnnounceId);
            return View(feedBack);
        }

        // GET: Feedback/Delete/5
        [ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            FeedBack feedBack = _context.FeedBacks.Single(m => m.Id == id);
            if (feedBack == null)
            {
                return HttpNotFound();
            }

            return View(feedBack);
        }

        // POST: Feedback/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            FeedBack feedBack = _context.FeedBacks.Single(m => m.Id == id);
            _context.FeedBacks.Remove(feedBack);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
