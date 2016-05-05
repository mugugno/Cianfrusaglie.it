using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using Cianfrusaglie.Models;

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
            return View(_context.Announces.ToList());
        }

        // GET: Announces/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Announce announce = _context.Announces.Single(m => m.Id == id);
            if (announce == null)
            {
                return HttpNotFound();
            }

            return View(announce);
        }

        // GET: Announces/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Announces/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Announce announce)
        {
            if (ModelState.IsValid)
            {
                _context.Announces.Add(announce);
                _context.SaveChanges();
                return RedirectToAction("Index");
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

            Announce announce = _context.Announces.Single(m => m.Id == id);
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

            Announce announce = _context.Announces.Single(m => m.Id == id);
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
            Announce announce = _context.Announces.Single(m => m.Id == id);
            _context.Announces.Remove(announce);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
