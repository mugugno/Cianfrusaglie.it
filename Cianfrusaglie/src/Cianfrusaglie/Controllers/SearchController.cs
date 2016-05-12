using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cianfrusaglie.Models;
using Microsoft.AspNet.Mvc;

namespace Cianfrusaglie.Controllers {
   public class SearchController : Controller {
      private readonly ApplicationDbContext _context;

      public SearchController( ApplicationDbContext context ) { _context = context; }

      public IActionResult Index(string title, IEnumerable< int > categories) {
         if( title == null )
            title = "";
         if( categories == null )
            categories = new List< int >();

         var result = SearchAnnounces( title, categories).ToList();
         return View(result);
      }

       public IActionResult SearchRedirect(string title, IEnumerable<int> categories) {

            return RedirectToAction( "Index", new { title = title, categories = categories});

        }
        


       public IActionResult RedirectToChat( string id ) {
           return RedirectToAction( nameof(MessagesController.Details), id );
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
         var announces = _context.Announces;
         foreach( var announce in announces )
         {

             var ids = _context.Categories.Where(c => categories.Contains(c.OverCategory.Id)).Select(u => u.Id).ToList();
             var announcesCategories = _context.AnnounceCategories.Where(a => a.AnnounceId.Equals(announce.Id) && (categories.Contains(a.CategoryId) || ids.Contains(a.CategoryId)));
            
             if (announcesCategories.Any())
             {
                 foreach (var tmp in announcesCategories)
                 {
                     var annuncio = _context.Announces.SingleOrDefault(u => u.Id == tmp.AnnounceId);
                     yield return annuncio;
                    }
             }
                    
            }
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