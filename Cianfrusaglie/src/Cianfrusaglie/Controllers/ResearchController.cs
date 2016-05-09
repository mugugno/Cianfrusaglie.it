using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;
using Cianfrusaglie.Models;
using Microsoft.Data.Entity.Internal;

namespace Cianfrusaglie.Controllers
{
    public class ResearchController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ResearchController(ApplicationDbContext context)
        {
            _context = context;
        }

        //public IActionResult Index()
        //{
            
        //}


        public IEnumerable<Announce> SearchAnnounces(string title, IEnumerable<Category> categories )
        {
            return CategoryBasedSearch(categories);
        }

        public IEnumerable<Announce> CategoryBasedSearch(IEnumerable<Category> categories)
        {
            var announces = _context.Announces;
            foreach (var announce in announces)
            {
                var announcesCategories = _context.AnnounceCategories.Where(a => a.AnnounceId.Equals(announce.Id));
                var result = ! categories.Except(announcesCategories.Select( ac => ac.Category)).Any();
                if (result)
                {
                    yield return announce;
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}