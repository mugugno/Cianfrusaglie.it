using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Cianfrusaglie.GeoPosition;
using Cianfrusaglie.Models;
using Cianfrusaglie.Statics;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;
using static Cianfrusaglie.Constants.CommonFunctions;

namespace Cianfrusaglie.Controllers {
   public class SearchController : Controller {
      private readonly ApplicationDbContext _context;

      public SearchController( ApplicationDbContext context ) { _context = context; }

      /// <summary>
      /// Restituisce la pagina con i risultati della ricerca.
      /// </summary>
      /// <param name="title">La stringa scritta nella barra di ricerca</param>
      /// <param name="categories">Le categorie selezionate</param>
      /// <returns>La View con i risultati della ricerca</returns>
      public IActionResult Index( string title, IEnumerable< int > categories, int range = 0 ) {
         ViewData[ "listUsers" ] = _context.Users.ToList();
         ViewData[ "lastAnnounces" ] = _context.Announces.OrderBy( u => u.PublishDate ).Take( 4 ).ToList();
         ViewData[ "listAnnounce" ] = _context.Announces.OrderByDescending( u => u.PublishDate ).ToList();
         //TODO QUANDO SI FARANNO I BARATTI
         //ViewData["listExchange"] = _context.Announces.Where();
         ViewData[ "" + "for" + "" + "mCategories" ] = _context.Categories.ToList();
         ViewData[ "numberOfCategories" ] = _context.Categories.ToList().Count;
         if( title == null )
            title = "";
         if( categories == null )
            categories = new List< int >();

         var result = SearchAnnounces( title, categories ).ToList();

         if( LoginChecker.HasLoggedUser( this ) && range > 0 ) {
            var loggedUser = _context.Users.Single( u => u.Id.Equals( User.GetUserId() ) );
            result = DistanceSearch( result, loggedUser.Latitude, loggedUser.Longitude, range ).ToList();
         }

         ViewData[ "listUsers" ] = _context.Users.ToList();
         ViewData[ "listImages" ] = _context.ImageUrls.ToList();
         ViewData[ "IsThereNewMessage" ] = IsThereNewMessage( User.GetUserId(), _context );
         ViewData[ " IsThereNewInterested" ] = IsThereNewInterested( User.GetUserId(), _context );
         ViewData[ "IsThereAnyNotification" ] = IsThereAnyNotification( User.GetUserId(), _context );
         return View( result );
      }

      public IActionResult SearchRedirect( string title, IEnumerable< int > categories ) {
         return RedirectToAction( "Index", new {title, categories} );
      }


      /// <summary>
      /// ricerca per titolo e categoria (compone le varie ricerche)
      /// se titolo == null allora solo ricerca per categoria e viceversa
      /// </summary>
      /// <param name="title">titolo annuncio</param>
      /// <param name="categories">lista di categorie</param>
      /// <returns></returns>
      public IEnumerable< Announce > SearchAnnounces( string title, IEnumerable< int > categories ) {
         Task< IEnumerable< Announce > > categoryTask = null;
         var catEnum = categories as IList< int > ?? categories.ToList();
         if( catEnum.Any() )
            categoryTask = Task.Run( () => CategoryBySearch( catEnum ) );
         Task< IEnumerable< Announce > > textTask = null;
         if( title != "" )
            textTask = Task.Run( () => TitleBasedSearch( title ) );

         var catResults = categoryTask == null ? new List< Announce >() : categoryTask.Result;
         var textResults = textTask == null ? new List< Announce >() : textTask.Result;

         if( textTask == null || categoryTask == null )
            return catResults.Union( textResults );
         return catResults.Intersect( textResults );
      }

      /// <summary>
      /// ricerca per annunci le cui categorie sono contenute in categories
      /// se c'è una macro categoria in categories viene espansa con le sue sotto categorie
      /// </summary>
      /// <param name="categories">lista id delle categorie</param>
      /// <returns></returns>
      public IEnumerable< Announce > CategoryBySearch( IEnumerable< int > categories ) {
         var catLeafs = new List< int >();
         foreach( int ci in categories ) {
            var cat = _context.Categories.Include( p => p.SubCategories ).Single( c => c.Id == ci );
            if( !cat.SubCategories.Any() )
               catLeafs.Add( cat.Id );
            else
               catLeafs.AddRange( cat.SubCategories.Select( c => c.Id ).ToList() );
         }

         var announcesCategories = _context.AnnounceCategories.Where( a => catLeafs.Contains( a.CategoryId ) );
         return
            announcesCategories.Select( ac => ac.Announce ).Where(
               announce => !announce.Closed && ( announce.DeadLine == null || announce.DeadLine > DateTime.Now ) )
               .ToList();
      }

      /// <summary>
      /// ricerca per titolo non ancora "furba"
      /// </summary>
      /// <param name="title"></param>
      /// <returns></returns>
      public IEnumerable< Announce > TitleBasedSearch( string title ) {
         return
            _context.Announces.Where(
               announce =>
                  !announce.Closed && ( announce.DeadLine == null || announce.DeadLine > DateTime.Now ) &&
                  AreSimilar( announce.Title, title ) );
      }

      public IEnumerable< Announce > DistanceSearch( IEnumerable< Announce > announces, double latitude,
         double longitude, int range ) {
         return announces.Where( a => GeoCoordinate.Distance( a.Latitude, a.Longitude, latitude, longitude ) <= range );
      }

      /// <summary>
      /// confronta 2 stringhe, sono simili se hanno in comune almeno una parola
      /// TODO da migliorare
      /// </summary>
      /// <param name="firstString">stringa sul db</param>
      /// <param name="secondString">stringa che si stà cercando</param>
      /// <returns></returns>
      protected bool AreSimilar( string firstString, string secondString ) {
         var first = firstString.ToLower().Split( ' ' );
         var second = secondString.ToLower().Split( ' ' );

         // meglio StartWith ?
         return first.Any( f => second.Any( s => f.Contains( s ) ) );
      }

      protected override void Dispose( bool disposing ) {
         if( disposing )
            _context.Dispose();
         base.Dispose( disposing );
      }
   }
}