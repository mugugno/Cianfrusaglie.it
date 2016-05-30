using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Cianfrusaglie.Constants;
using Cianfrusaglie.GeoPosition;
using Cianfrusaglie.Models;
using Cianfrusaglie.Statics;
using Cianfrusaglie.Suggestions;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;
using static Cianfrusaglie.Constants.CommonFunctions;

namespace Cianfrusaglie.Controllers {
   public class SearchController : Controller {
        private readonly ApplicationDbContext _context;
        public RankAlgorithm RankAlgorithm;
        private int resultsPerPage = 12;

        public SearchController( ApplicationDbContext context ) {
            _context = context;
            RankAlgorithm = new RankAlgorithm(context);
        }

        public IActionResult Advanced()
        {
            CommonFunctions.SetRootLayoutViewData( this,_context );
            return View();
        }

        /// <summary>
        /// Restituisce la pagina con i risultati della ricerca.
        /// </summary>
        /// <param name="title">La stringa scritta nella barra di ricerca</param>
        /// <param name="categories">Le categorie selezionate</param>
        /// <returns>La View con i risultati della ricerca</returns>
        public IActionResult Index(string title, IEnumerable<int> categories, int range = 0, int page = 0) {
            ViewData["listUsers"] = _context.Users.ToList();
            User user = null;
            if (LoginChecker.HasLoggedUser(this))
                user = _context.Users.Single(u => User.GetUserId().Equals(u.Id));

            //TODO QUANDO SI FARANNO I BARATTI
            //ViewData["listExchange"] = _context.Announces.Where();
            CommonFunctions.SetRootLayoutViewData( this, _context );
            ViewData["listUsers"] = _context.Users.ToList();
            ViewData["listImages"] = _context.ImageUrls.ToList();
            
            ViewData[ "pageNumber" ] = page;

            List< Announce > result;
            List< Announce > pageResults;
            if (string.IsNullOrEmpty(title) && !categories.Any())
                if (user == null) {
                    result = _context.Announces.OrderByDescending(u => u.PublishDate).ToList();
                    ViewData["numberOfPages"] = (result.Count % resultsPerPage) == 0 ? (result.Count / resultsPerPage) : (1+ result.Count / resultsPerPage);
                    if( result.Count > resultsPerPage * ( page + 1 ) ) {
                         pageResults = result.GetRange( resultsPerPage * page, resultsPerPage );
                    } else {
                         pageResults = result.GetRange( Math.Min( resultsPerPage * page, (result.Count - 1) < 0 ? 0 : result.Count - 1), Math.Max(result.Count - resultsPerPage * page, 0));
                    }
                    return View(pageResults);
                } else {
                    result = _context.Announces.OrderByDescending(a => RankAlgorithm.CalculateRank(a, user)).ToList();
                    ViewData["numberOfPages"] = (result.Count % resultsPerPage) == 0 ? (result.Count / resultsPerPage) : (1 + result.Count / resultsPerPage);
                    if (result.Count > resultsPerPage * (page + 1)) {
                        pageResults = result.GetRange(resultsPerPage * page, resultsPerPage);
                    } else {
                        pageResults = result.GetRange(Math.Min(resultsPerPage * page, (result.Count - 1) < 0 ? 0 : result.Count - 1), Math.Max(result.Count - resultsPerPage * page, 0));
                    }
                    return View(pageResults);

                }
            if (categories == null)
                categories = new List<int>();

            result = SearchAnnounces(title, categories).ToList();

            if (user != null && range > 0) {
                var loggedUser = _context.Users.Single(u => u.Id.Equals(User.GetUserId()));
                result = DistanceSearch(result, loggedUser.Latitude, loggedUser.Longitude, range).OrderByDescending(a => RankAlgorithm.CalculateRank(a, user)).ToList();
            }

            ViewData["numberOfPages"] = (result.Count % resultsPerPage) == 0 ? (result.Count / resultsPerPage) : (1 + result.Count / resultsPerPage);
            if (result.Count > resultsPerPage * (page + 1)) {
                pageResults = result.GetRange(resultsPerPage * page, resultsPerPage);
            } else {
                pageResults = result.GetRange(Math.Min(resultsPerPage * page, (result.Count - 1) <0 ? 0 : result.Count -1), Math.Max(result.Count - resultsPerPage * page, 0));
            }
            return View(pageResults);
        }

        public IActionResult SearchRedirect(string title, IEnumerable<int> categories) {
            return RedirectToAction("Index", new { title, categories });
        }

      public IActionResult LastAnnounces(string f = "f", int page = 0) {
         //TODO QUANDO SI FARANNO I BARATTI
         //ViewData["listExchange"] = _context.Announces.Where();
         ViewData[ "formCategories" ] = _context.Categories.ToList();
         ViewData[ "numberOfCategories" ] = _context.Categories.ToList().Count;
         ViewData[ "listUsers" ] = _context.Users.ToList();
         ViewData[ "listImages" ] = _context.ImageUrls.ToList();
         ViewData[ "IsThereNewMessage" ] = IsThereNewMessage( User.GetUserId(), _context );
         ViewData[ " IsThereNewInterested" ] = IsThereNewInterested( User.GetUserId(), _context );
         ViewData[ "IsThereAnyNotification" ] = IsThereAnyNotification( User.GetUserId(), _context );
         ViewData[ "pageNumber" ] = page;

         var result = _context.Announces.Where(a => !a.Closed).OrderByDescending( u => u.PublishDate ).ToList();
         List<Announce> pageResults;
         ViewData[ "numberOfPages" ] = ( result.Count % resultsPerPage ) == 0 ? ( result.Count / resultsPerPage ) : ( 1 + result.Count / resultsPerPage );

         if( result.Count > resultsPerPage * ( page + 1 ) )
            pageResults = result.GetRange( resultsPerPage * page, resultsPerPage );
         else
            pageResults = result.GetRange( Math.Min( resultsPerPage * page, ( result.Count - 1 ) < 0 ? 0 : result.Count - 1 ), Math.Max( result.Count - resultsPerPage * page, 0 ) );

         return View( nameof( Index ), pageResults );
      }

      public IActionResult Suggestions( string f = "f", int page = 0 ) {
         //TODO QUANDO SI FARANNO I BARATTI
         //ViewData["listExchange"] = _context.Announces.Where();
         CommonFunctions.SetRootLayoutViewData( this, _context );
         ViewData[ "listUsers" ] = _context.Users.ToList();
         ViewData[ "listImages" ] = _context.ImageUrls.ToList();
        
         ViewData[ "pageNumber" ] = page;

         var result = CommonFunctions.GetSuggestedAnnounces( _context, this ).ToList();
         List<Announce> pageResults;
         ViewData[ "numberOfPages" ] = ( result.Count % resultsPerPage ) == 0 ? ( result.Count / resultsPerPage ) : ( 1 + result.Count / resultsPerPage );

         if( result.Count > resultsPerPage * ( page + 1 ) )
            pageResults = result.GetRange( resultsPerPage * page, resultsPerPage );
         else
            pageResults = result.GetRange( Math.Min( resultsPerPage * page, ( result.Count - 1 ) < 0 ? 0 : result.Count - 1 ), Math.Max( result.Count - resultsPerPage * page, 0 ) );

         return View( nameof( Index ), pageResults );
      }

        /// <summary>
        /// Data una stringa titolo e una lista di Id di categorie, ritorna i risultati della ricerca.
        /// Se titolo == null allora la ricerca � effettuata solo per categoria (e viceversa).
        /// </summary>
        /// <param name="title">Titolo che si vuole cercare</param>
        /// <param name="categories">Lista di Id di categorie da utilizzare nella ricerca</param>
        /// <returns>Un IEnumerable di Annunci contenenti i risultati della ricerca.</returns>
        public IEnumerable< Announce > SearchAnnounces( string title, IEnumerable< int > categories ) {
         Task< IEnumerable< Announce > > categoryTask = null;
         var catEnum = categories as IList< int > ?? categories.ToList();
         if( catEnum.Any() )
            categoryTask = Task.Run( () => CategoryBySearch( catEnum ) );
         Task< IEnumerable< Announce > > textTask = null;
         if( title != null ) {
            title = title.Trim();
            if( title.Length > 0 )
               textTask = Task.Run( () => TitleBasedSearch( title ) );
         }

         var catResults = categoryTask == null ? new List< Announce >() : categoryTask.Result;
         var textResults = textTask == null ? new List< Announce >() : textTask.Result;

         if( textTask == null || categoryTask == null )
            return catResults.Union( textResults );
         return catResults.Intersect( textResults );
      }

      /// <summary>
      /// Dato un IEnumerable di Id di Annunci, ritorna i risultati della ricerca per sole categorie.
      /// In caso una delle categorie sia una Macro, allora la ricerca sar� eseguita anche rispetto alle sue sottocategorie.
      /// </summary>
      /// <param name="categories">IEnumerable di Id di categorie</param>
      /// <returns>IEnumerable di Annunci contenente il risultato della ricerca</returns>
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
      /// Data una stringa, ritorna i risultati della ricerca basati esclusivamente su tale campo.
      /// </summary>
      /// <param name="title">La stringa da utilizzare come confronto</param>
      /// <returns>IEnumerable di Annunci contenente i risultati della ricerca</returns>
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
      /// Date due stringa, esegue dei confronti di tipo inclusivo e ritorna un booleano per indicare se siano o meno simili.
      /// </summary>
      /// <param name="firstString">Prima stringa da passare per il confronto (sul db)</param>
      /// <param name="secondString">Seconda stringa da passare per il confronto</param>
      /// <returns>Ritorna vero se le stringhe sono simili, falso al contrario.</returns>
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