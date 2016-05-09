using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cianfrusaglie.Models;
using Microsoft.AspNet.Mvc;

namespace Cianfrusaglie.Controllers {
   public class SearchController : Controller {
      private readonly ApplicationDbContext _context;

      public SearchController( ApplicationDbContext context ) { _context = context; }

      public IActionResult Index(string title, IEnumerable< Category > categories ) {
         if( title == null )
            title = "";
         if( categories == null )
            categories = new List< Category >();

         var result = SearchAnnounces( title, categories ).ToList();
         return View( result );
      }

      public IEnumerable< Announce > SearchAnnounces( string title, IEnumerable< Category > categories ) {
         Task< IEnumerable< Announce > > categoryTask = null;
         if( categories.Any() )
            categoryTask = Task.Run( () => CategoryBasedSearch( categories ) );
         Task< IEnumerable< Announce > > textTask = null;
         if( title != "" )
            textTask = Task.Run( () => TitleBasedSearch( title ) );

         var catResults = categoryTask == null ? new List< Announce >() : categoryTask.Result;
         var textResults = textTask == null ? new List< Announce >() : textTask.Result;

         if( textTask == null || categoryTask == null )
            return catResults.Union( textResults );
         return catResults.Intersect( textResults );
      }

      public IEnumerable< Announce > CategoryBasedSearch( IEnumerable< Category > categories ) {
         var announces = _context.Announces;
         foreach( var announce in announces ) {
            var announcesCategories = _context.AnnounceCategories.Where( a => a.AnnounceId.Equals( announce.Id ) );
            bool result = !categories.Except( announcesCategories.Select( ac => ac.Category ) ).Any();
            if( result )
               yield return announce;
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