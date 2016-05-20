using System.Linq;
using System.Security.Claims;
using Cianfrusaglie.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc;
using static Cianfrusaglie.Constants.CommonFunctions;

namespace Cianfrusaglie.Controllers {
    public class HomeController : Controller {
        private readonly ApplicationDbContext _context;
        public UserManager< User > UserManager;

        public HomeController( ApplicationDbContext context ) { _context = context; }

        public IActionResult Index() {
            ViewData[ "listImages" ] = _context.ImageUrls.ToList();
            ViewData[ "listUsers" ] = _context.Users.ToList();
            ViewData[ "listAnnounces" ] = _context.Announces.OrderByDescending( u => u.PublishDate ).Take( 4 ).ToList();
            ViewData[ "formCategories" ] = _context.Categories.ToList();
            ViewData[ "numberOfCategories" ] = _context.Categories.ToList().Count;
            ViewData["IsThereNewMessage"] = IsThereNewMessage(User.GetUserId(), _context);
            ViewData[" IsThereNewInterested"] = IsThereNewInterested(User.GetUserId(), _context);
            ViewData["IsThereAnyNotification"] = IsThereAnyNotification(User.GetUserId(), _context);
            ViewData["listCategory"] = _context.Categories.ToList();
            //CreateUsers();
            return View();
        }

        private void CreateUsers() {
            // _context.Users.Add(new User() { UserName = "pippopaolo", Email = "pippopaolo@gmail.com", PasswordHash = "fuewvuw4y75w94ywif" });
            //_context.Users.Add(new User() { UserName = "pippopaolo2", Email = "pippopaolo2@gmail.com", PasswordHash = "fuewvuw4y75w94ywif" });
            //Context.Users.Add(new User() { UserName = "pippopaolo3", Email = "pippopaolo3@gmail.com", PasswordHash = "fuewvuw4y75w94ywif" });
            //Context.Users.Add(new User() { UserName = "pippopaolo4", Email = "pippopaolo4@gmail.com", PasswordHash = "fuewvuw4y75w94ywif" });
            //_context.SaveChanges();
            //CreateAnnounces();
        }

        /*private void CreateAnnounces()
        {

            var announce = new Announce();
            var context = _context;
            var usr = context.Users.SingleOrDefault(u => u.UserName.Equals("pippopaolo"));
            announce.Author = usr;
            announce.Title = "Libro di OST di Videogiochi";
            announce.Description = "Tutti i compositori da Uematsu in giù";
            context.Announces.Add(announce);

            var announceCategory1 = new AnnounceCategory
            {
                Announce = announce,
                Category = context.Categories.Single(a => a.Name.Equals("Libri"))
            };
            var announceCategory11 = new AnnounceCategory
            {
                Announce = announce,
                Category = context.Categories.Single(a => a.Name.Equals("Musica"))
            };
            var announceCategory12 = new AnnounceCategory
            {
                Announce = announce,
                Category = context.Categories.Single(a => a.Name.Equals("Videogiochi"))
            };
            context.AnnounceCategories.Add(announceCategory1);
            context.AnnounceCategories.Add(announceCategory11);
            context.AnnounceCategories.Add(announceCategory12);
            context.SaveChanges();

            var announce2 = new Announce();
            var usr2 = context.Users.Single(u => u.UserName.Equals("pippopaolo2"));
            announce2.Author = usr2;
            announce2.Title = "Halo 5 Usato";
            announce2.Description = "Guardiani ovunque";
            
            context.Announces.Add(announce2);

            var announceCategory2 = new AnnounceCategory
            {
                Announce = announce2,
                Category = context.Categories.Single(a => a.Name.Equals("Videogiochi"))
            };
            context.AnnounceCategories.Add(announceCategory2);
            context.SaveChanges();

            var announce3 = new Announce();
            usr2 = context.Users.Single(u => u.UserName.Equals("pippopaolo2"));
            announce3.Author = usr2;
            announce3.Title = "Halo 5 Usato";
            announce3.Description = "Guardiani ovunque";

            context.Announces.Add(announce3);

            var announceCategory3 = new AnnounceCategory
            {
                Announce = announce3,
                Category = context.Categories.Single(a => a.Name.Equals("Videogiochi"))
            };
            context.AnnounceCategories.Add(announceCategory3);
            context.SaveChanges();

            var announce4 = new Announce();
            usr2 = context.Users.Single(u => u.UserName.Equals("pippopaolo2"));
            announce4.Author = usr2;
            announce4.Title = "Halo 5 Usato";
            announce4.Description = "Guardiani ovunque";

            context.Announces.Add(announce4);

            var announceCategory4 = new AnnounceCategory
            {
                Announce = announce4,
                Category = context.Categories.Single(a => a.Name.Equals("Videogiochi"))
            };
            context.AnnounceCategories.Add(announceCategory4);
            context.SaveChanges();

            var announce5 = new Announce();
            usr2 = context.Users.Single(u => u.UserName.Equals("pippopaolo2"));
            announce5.Author = usr2;
            announce5.Title = "Halo 5 Usato";
            announce5.Description = "Guardiani ovunque";

            context.Announces.Add(announce5);

            var announceCategory5 = new AnnounceCategory
            {
                Announce = announce5,
                Category = context.Categories.Single(a => a.Name.Equals("Videogiochi"))
            };
            context.AnnounceCategories.Add(announceCategory5);
            context.SaveChanges();
        }*/

        public IActionResult About() {
            ViewData[ "Message" ] = "Your application description page.";

            return View();
        }

        public IActionResult Contact() {
            ViewData[ "Message" ] = "Your contact page.";

            return View();
        }

        public IActionResult Error() { return View(); }
    }
}