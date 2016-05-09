using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using Cianfrusaglie.Models;
using Microsoft.AspNet.Identity;
using System.Security.Claims;
namespace Cianfrusaglie.Controllers
{
    public class AnnouncesController : Controller
    {
        private ApplicationDbContext _context;

        public AnnouncesController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Announces
        public IActionResult Index()
        {
            ViewData["listAnnounces"] = _context.Announces.ToList();
            ViewData["numberOfAnnounces"] = _context.Announces.ToList().Count;
            ViewData["formCategories"] = _context.Categories.ToList();
            ViewData["numberOfCategories"] = _context.Categories.ToList().Count;
            return View();
        }

        // GET: Announces/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Announce announce = _context.Announces.SingleOrDefault(m => m.Id == id);
            if (announce == null)
            {
                return HttpNotFound();
            }

            return View(announce);
        }

        // GET: Announces/Create
        public IActionResult Create()
        {
            //TODO scrivere in maniera pi� furba ma ora va benissimo cos�!
            ViewData["formFields"] = _context.FormFields.ToList();
            ViewData["formMacroCategories"] = _context.Categories.ToList();
            ViewData["numberOfMacroCategories"] = _context.Categories.ToList().Count;
            return View();
        }

        // POST: Announces/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Announce announce)
        {
            //TODO: Aggiungere i campi della risposta di errore.
            if (User == null)
                return HttpBadRequest();
            var idlogged = User.GetUserId();
            announce.Author = _context.Users.First(u => u.Id.Equals(idlogged));
            if (ModelState.IsValid)
            {
                
                _context.Announces.Add(announce);
                _context.SaveChanges();
                return Redirect("Index");
            }
            return View(announce);
        }

        // GET: Announces/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Announce announce = _context.Announces.SingleOrDefault(m => m.Id == id);
            if (announce == null)
            {
                return HttpNotFound();
            }
            return View(announce);
        }

        // POST: Announces/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Announce announce)
        {
            //TODO: Aggiungere i campi della risposta di errore.
            if (User == null)
                return HttpBadRequest();
            if (!User.GetUserId().Equals(announce.Author.Id))
            {
                return HttpBadRequest();
            }
            if (ModelState.IsValid)
            {
                _context.Update(announce);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(announce);
        }

        // GET: Announces/Delete/5
        [ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Announce announce = _context.Announces.SingleOrDefault(m => m.Id == id);
            if (announce == null)
            {
                return HttpNotFound();
            }

            return View(announce);
        }

        // POST: Announces/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Announce announce = _context.Announces.SingleOrDefault(m => m.Id == id);
            if(announce == null)
                return HttpBadRequest();
            //TODO: Aggiungere i campi della risposta di errore.
            if (User == null)
                return HttpBadRequest();
            if (!User.GetUserId().Equals(announce.Author.Id))
                return HttpBadRequest();
            _context.Announces.Remove(announce);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult SubmitAnnounce()
        {
            return View();
        }
    }
}
