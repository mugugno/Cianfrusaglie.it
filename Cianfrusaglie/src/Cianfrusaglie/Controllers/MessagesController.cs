using System.Linq;
using System.Security.Claims;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using Cianfrusaglie.Models;

namespace Cianfrusaglie.Controllers
{
    public class MessagesController : Controller
    {
        private ApplicationDbContext _context;

        public MessagesController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Messages
        public IActionResult Index(){
            return View( _context.Messages.Where( u => u.Receiver.Id == User.GetUserId() ).ToList() );
        }

        // GET: Messages/Details/5
        public IActionResult Details(string id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Message message = _context.Messages.Single(m => m.Id == id);
            if (message == null)
            {
                return HttpNotFound();
            }

            return View(message);
        }

        // GET: Messages/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Messages/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Message message)
        {
            if (ModelState.IsValid)
            {
                _context.Messages.Add(message);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(message);
        }

        // GET: Messages/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Message message = _context.Messages.Single(m => m.Id == id);
            if (message == null)
            {
                return HttpNotFound();
            }
            return View(message);
        }

        // POST: Messages/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Message message)
        {
            if (ModelState.IsValid)
            {
                _context.Update(message);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(message);
        }

        // GET: Messages/Delete/5
        [ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Message message = _context.Messages.Single(m => m.Id == id);
            if (message == null)
            {
                return HttpNotFound();
            }

            return View(message);
        }

        // POST: Messages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Message message = _context.Messages.Single(m => m.Id == id);
            _context.Messages.Remove(message);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
