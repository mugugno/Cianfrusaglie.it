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
        /*private IEnumerable< Announce > GetUserAnnounces( string userId ) {
            return _context.Announces.Where( announce => announce.Author.Id.Equals( userId ) );
        }

        private IEnumerable< Announce > GetUserInterestedAnnounces( string userId ) {
            return from interested in _context.Interested
                where interested.UserId == userId
                select interested.Announce;
        }*/

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
        public IActionResult Create()
        {
            ViewData["AnnounceId"] = new SelectList(_context.Announces, "Id", "Announce");
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
                if( announce != null ) {
                    if( feedBack.Author.Id == announce.AuthorId ) {
                        //TODO autore da feedback a prescelto
                    }
                    if( IsUserInterestedToAnnounce( announce.Id, feedBack.Author.Id ) ) {
                        //caso interessato da feedback ad annuncio (autore dell'annuncio)
                        if( announce.AuthorId == feedBack.Receiver.Id ) {
                            _context.FeedBacks.Add(feedBack);
                            _context.SaveChanges();
                            return RedirectToAction("Index");
                        }
                    }
                    return new BadRequestResult();
                }
                return new BadRequestResult();
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
