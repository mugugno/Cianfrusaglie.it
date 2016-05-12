using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cianfrusaglie.Models;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;

namespace Cianfrusaglie.Controllers {
   public class SearchController : Controller {
      private readonly ApplicationDbContext _context;

      public SearchController( ApplicationDbContext context ) { _context = context; }

      public IActionResult Index(string title, IEnumerable< int > categories) {
        ViewData["listUsers"] = _context.Users.ToList();
        ViewData["listAnnounces"] = _context.Announces.OrderBy(u => u.PublishDate).Take(4).ToList();
        ViewData["formCategories"] = _context.Categories.ToList();
        ViewData["numberOfCategories"] = _context.Categories.ToList().Count;
        if ( title == null )
        title = "";
         if( categories == null )
            categories = new List< int >();

         var result = SearchAnnounces( title, categories).ToList();
            ViewData["listUsers"] = _context.Users.ToList();
            ViewData["listImages"] = _context.ImageUrls.ToList();
            return View( result );
      }

       public IActionResult SearchRedirect( string title, IEnumerable<int> categories) {
           return RedirectToAction("Index", new {title = title, categories = categories});
       }
      

        public IEnumerable< Announce > SearchAnnounces( string title, IEnumerable< int > categories) {
         Task< IEnumerable< Announce > > categoryTask = null;
         if( categories.Any() )
            categoryTask = Task.Run( () => CategoryBySearch( categories ) );
            Task< IEnumerable< Announce > > textTask = null;
         if( title != "" )
            textTask = Task.Run( () => TitleBasedSearch( title ) );

         var catResults = categoryTask == null ? new List< Announce >() : categoryTask.Result;
         var textResults = textTask == null ? new List< Announce >() : textTask.Result;

         if( textTask == null || categoryTask == null )
            return catResults.Union( textResults );
         return catResults.Intersect( textResults );
      }

      public IEnumerable< Announce > CategoryBySearch( IEnumerable< int > categories ) {
         var catLeafs= new List< int >();
         foreach( var ci in categories ) {
            var cat = _context.Categories.Include( p => p.SubCategories ).Single( c => c.Id == ci );
            if( !cat.SubCategories.Any() )
               catLeafs.Add( cat.Id );
            else
               catLeafs.AddRange( cat.SubCategories.Select( c => c.Id ).ToList() );
         }

         var announcesCategories = _context.AnnounceCategories.Where( a => catLeafs.Contains( a.CategoryId ) );
         return announcesCategories.Select( ac => ac.Announce ).ToList();
      }

       

        public IEnumerable< Announce > TitleBasedSearch( string title ) {
         foreach( var announce in _context.Announces )
            if( AreSimilar( announce.Title, title ) )
               yield return announce;
      }

      protected bool AreSimilar( string firstString, string secondString ) {
         var first = firstString.ToLower().Split( ' ' );
         var second = secondString.ToLower().Split( ' ' );
         var common = first.Where( s => second.Contains( s ) );
         return common.Any();
      }

      protected override void Dispose( bool disposing ) {
         if( disposing )
            _context.Dispose();
         base.Dispose( disposing );
      }
   }
}