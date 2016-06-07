using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Claims;
using System.Threading.Tasks;
using Cianfrusaglie.Constants;
using Cianfrusaglie.GeoPosition;
using Cianfrusaglie.Models;
using Cianfrusaglie.Statics;
using Cianfrusaglie.Suggestions;
using Cianfrusaglie.ViewModels.Search;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Internal;
using Microsoft.Data.Entity.Metadata.Internal;
using static Cianfrusaglie.Constants.CommonFunctions;

namespace Cianfrusaglie.Controllers {

    public class SearchController : Controller {
        private readonly ApplicationDbContext _context;
        private readonly int resultsPerPage = 12;
        public RankAlgorithm RankAlgorithm;

        public SearchController( ApplicationDbContext context ) {
            _context = context;
            RankAlgorithm = new RankAlgorithm( context );
        }

        public IActionResult Advanced() {
            SetRootLayoutViewData( this, _context );
            return View();
        }

        /// <summary>
        ///     Restituisce la pagina con i risultati della ricerca.
        /// </summary>
        /// <param name="title">La stringa scritta nella barra di ricerca</param>
        /// <param name="categories">Le categorie selezionate</param>
        /// <param name="range">Il range in Kilometri. Di default è 0.</param>
        /// <param name="page">La pagina da visualizzare.</param>
        /// <returns>La View con i risultati della ricerca</returns>
        public IActionResult Index( string title, IEnumerable< int > categories, int range = 0, int page = 0 ) {
            User user = null;
            if( LoginChecker.HasLoggedUser( this ) )
                user = _context.Users.Single( u => User.GetUserId().Equals( u.Id ) );

            //TODO QUANDO SI FARANNO I BARATTI
            //ViewData["listExchange"] = _context.Announces.Where();
            SetRootLayoutViewData( this, _context );
            //ViewData[ "listImages" ] = _context.ImageUrls.ToList();

            ViewData[ "pageNumber" ] = page;

            List< Announce > result;
            List< Announce > pageResults;
            var ctxAnnounces = _context.Announces.Include( a => a.Author );
            if( string.IsNullOrEmpty( title ) && !categories.Any() ) {
                if( user == null ) {
                    result = ctxAnnounces.OrderByDescending( u => u.PublishDate ).ToList();
                    ViewData[ "numberOfPages" ] = result.Count % resultsPerPage == 0
                        ? result.Count / resultsPerPage : 1 + result.Count / resultsPerPage;
                    if( result.Count > resultsPerPage * ( page + 1 ) )
                        pageResults = result.GetRange( resultsPerPage * page, resultsPerPage );
                    else
                        pageResults =
                            result.GetRange(
                                Math.Min( resultsPerPage * page, result.Count - 1 < 0 ? 0 : result.Count - 1 ),
                                Math.Max( result.Count - resultsPerPage * page, 0 ) );
                } else {
                    result = ctxAnnounces.OrderByDescending( a => RankAlgorithm.CalculateRank( a, user ) ).ToList();
                    ViewData[ "numberOfPages" ] = result.Count % resultsPerPage == 0
                        ? result.Count / resultsPerPage : 1 + result.Count / resultsPerPage;
                    if( result.Count > resultsPerPage * ( page + 1 ) )
                        pageResults = result.GetRange( resultsPerPage * page, resultsPerPage );
                    else
                        pageResults =
                            result.GetRange(
                                Math.Min( resultsPerPage * page, result.Count - 1 < 0 ? 0 : result.Count - 1 ),
                                Math.Max( result.Count - resultsPerPage * page, 0 ) );
                }
            } else {
                if( categories == null )
                    categories = new List< int >();

                result = SearchAnnounces( title, categories ).ToList();

                if( user != null && range > 0 ) {
                    var loggedUser = _context.Users.Single( u => u.Id.Equals( User.GetUserId() ) );
                    if(loggedUser.Latitude!=null && loggedUser.Longitude!=null)
                    {
                        result =
                              DistanceSearch(result, loggedUser.Latitude.Value, loggedUser.Longitude.Value, range).OrderByDescending(
                                  a => RankAlgorithm.CalculateRank(a, user)).ToList();
                    }
                }

                ViewData[ "numberOfPages" ] = result.Count % resultsPerPage == 0
                    ? result.Count / resultsPerPage : 1 + result.Count / resultsPerPage;
                if( result.Count > resultsPerPage * ( page + 1 ) )
                    pageResults = result.GetRange( resultsPerPage * page, resultsPerPage );
                else
                    pageResults =
                        result.GetRange(
                            Math.Min( resultsPerPage * page, result.Count - 1 < 0 ? 0 : result.Count - 1 ),
                            Math.Max( result.Count - resultsPerPage * page, 0 ) );
            }

            ViewData[ "listImages" ] =
                _context.ImageUrls.Where(
                    iu => pageResults.SelectMany( p => p.Images.Select( i => i.Id ) ).Contains( iu.Id ) ).ToList();

            return View( pageResults );
        }

        public IActionResult SearchRedirect( string title, IEnumerable< int > categories ) {
            return RedirectToAction( "Index", new {title, categories} );
        }

        public IActionResult LastAnnounces( string f = "f", int page = 0 ) {
            //TODO QUANDO SI FARANNO I BARATTI
            //ViewData["listExchange"] = _context.Announces.Where();
            SetRootLayoutViewData( this, _context );
            ViewData[ "listImages" ] = _context.ImageUrls.ToList();

            ViewData[ "pageNumber" ] = page;

            var result =
                _context.Announces.Include( a => a.Author ).Where( a => !a.Closed ).OrderByDescending(
                    u => u.PublishDate ).ToList();
            List< Announce > pageResults;
            ViewData[ "numberOfPages" ] = result.Count % resultsPerPage == 0
                ? result.Count / resultsPerPage : 1 + result.Count / resultsPerPage;

            if( result.Count > resultsPerPage * ( page + 1 ) )
                pageResults = result.GetRange( resultsPerPage * page, resultsPerPage );
            else
                pageResults =
                    result.GetRange( Math.Min( resultsPerPage * page, result.Count - 1 < 0 ? 0 : result.Count - 1 ),
                        Math.Max( result.Count - resultsPerPage * page, 0 ) );

            return View( nameof( Index ), pageResults );
        }

        public IActionResult Suggestions( string f = "f", int page = 0 ) {
            //TODO QUANDO SI FARANNO I BARATTI
            //ViewData["listExchange"] = _context.Announces.Where();
            SetRootLayoutViewData( this, _context );
            ViewData[ "listImages" ] = _context.ImageUrls.ToList();

            ViewData[ "pageNumber" ] = page;

            var result = GetSuggestedAnnounces( _context, this ).ToList();
            List< Announce > pageResults;
            ViewData[ "numberOfPages" ] = result.Count % resultsPerPage == 0
                ? result.Count / resultsPerPage : 1 + result.Count / resultsPerPage;

            if( result.Count > resultsPerPage * ( page + 1 ) )
                pageResults = result.GetRange( resultsPerPage * page, resultsPerPage );
            else
                pageResults =
                    result.GetRange( Math.Min( resultsPerPage * page, result.Count - 1 < 0 ? 0 : result.Count - 1 ),
                        Math.Max( result.Count - resultsPerPage * page, 0 ) );

            return View( nameof( Index ), pageResults );
        }

        /// <summary>
        ///     Data una stringa titolo e una lista di Id di categorie, ritorna i risultati della ricerca.
        ///     Se titolo == null allora la ricerca è effettuata solo per categoria (e viceversa).
        /// </summary>
        /// <param name="title">Titolo che si vuole cercare</param>
        /// <param name="categories">Lista di Id di categorie da utilizzare nella ricerca</param>
        /// <returns>Un IEnumerable di Annunci contenenti i risultati della ricerca.</returns>
        public IEnumerable< Announce > SearchAnnounces( string title, IEnumerable< int > categories ) {
            Task< IQueryable< Announce > > categoryTask = null;
            var catEnum = categories as IList< int > ?? categories.ToList();
            if( catEnum.Any() )
                categoryTask = Task.Run( () => CategoryBySearch( catEnum ) );
            Task< IEnumerable< Announce > > textTask = null;
            if( title != null ) {
                title = title.Trim();
                if( title.Length > 0 )
                    textTask = Task.Run( () => TitleBasedSearch( title ) );
            }

            var catResults = categoryTask?.Result.ToList() ?? new List< Announce >();

            foreach( var ann in catResults )
                ann.Author = _context.Users.Single( u => u.Id == ann.AuthorId );

            var textResults = textTask == null ? new List< Announce >() : textTask.Result;

            if( textTask == null || categoryTask == null )
                return catResults.Union( textResults );
            return catResults.Intersect( textResults );
        }

        /// <summary>
        ///     Dato un IEnumerable di Id di Annunci, ritorna i risultati della ricerca per sole categorie.
        ///     In caso una delle categorie sia una Macro, allora la ricerca sarà eseguita anche rispetto alle sue sottocategorie.
        /// </summary>
        /// <param name="categories">IEnumerable di Id di categorie</param>
        /// <returns>IEnumerable di Annunci contenente il risultato della ricerca</returns>
        public IQueryable< Announce > CategoryBySearch( IEnumerable< int > categories ) {
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
                    announce => !announce.Closed && ( announce.DeadLine == null || announce.DeadLine > DateTime.Now ) );
        }

        /// <summary>
        ///     Data una stringa, ritorna i risultati della ricerca basati esclusivamente su tale campo.
        /// </summary>
        /// <param name="title">La stringa da utilizzare come confronto</param>
        /// <returns>IEnumerable di Annunci contenente i risultati della ricerca</returns>
        public IEnumerable< Announce > TitleBasedSearch( string title ) {
            var titleBasedSearch =
                _context.Announces.Include( a => a.Author ).Include( a => a.AnnouncesGats ).Where(
                    announce =>
                        ( !announce.Closed && ( announce.DeadLine == null || announce.DeadLine > DateTime.Now ) &&
                          AreSimilar( announce.Title, title ) ) ||
                        announce.AnnouncesGats.Any( ag => AreSimilar( ag.Gat.Text, title ) ) );

            return titleBasedSearch;
        }   

        public IEnumerable< Announce > DistanceSearch( IEnumerable< Announce > announces, double latitude,
            double longitude, int range ) {
            return announces.Where( a => GeoCoordinate.Distance( a.Latitude, a.Longitude, latitude, longitude ) <= range );
        }

        /// <summary>
        ///     Date due stringa, esegue dei confronti di tipo inclusivo e ritorna un booleano per indicare se siano o meno simili.
        /// </summary>
        /// <param name="firstString">Prima stringa da passare per il confronto (sul db)</param>
        /// <param name="secondString">Seconda stringa da passare per il confronto</param>
        /// <returns>Ritorna vero se le stringhe sono simili, falso al contrario.</returns>
        public static bool AreSimilar( string firstString, string secondString ) {
            var first = firstString.ToLower().Split( ' ' );
            var second = secondString.ToLower().Split( ' ' );

            // meglio StartWith ?
            return first.Any( f => second.Any( f.Contains ) );
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult AdvancedSearch(AdvancedSearchViewModel model) {
            var result = PerformAdvancedSearch( model );
            return View();
        }

        public IEnumerable< Announce > PerformAdvancedSearch(AdvancedSearchViewModel advancedSearchViewModel) {
            Predicate< Announce > truePredicate = (Announce a) => true;

            var rangeKmPredicate = truePredicate;

            if ( LoginChecker.HasLoggedUser( this ) ) {
                var user = _context.Users.Single( u => u.Id.Equals( User.GetUserId() ) );
                double min = advancedSearchViewModel.KmRangeMin;
                double? max = advancedSearchViewModel.KmRangeMax;

                if ( user.Latitude != null && user.Longitude != null ) {
                    double ul1 = user.Latitude.Value;
                    double ul2 = user.Longitude.Value;
                    double upper = max ?? double.MaxValue;
                    rangeKmPredicate =
                        (Announce a) =>
                        GeoCoordinate.Distance(a.Latitude, a.Longitude, ul1, ul2) >= min &&
                        GeoCoordinate.Distance(a.Latitude, a.Longitude, ul1, ul2) <= upper;
                }
                
            }

            int upperPrice = advancedSearchViewModel.PriceRangeMax ?? int.MaxValue;
            Predicate<Announce> pricePredicate =  a =>
                    a.Price >= advancedSearchViewModel.PriceRangeMin &&
                    a.Price <= upperPrice;
            int upperFeedback = advancedSearchViewModel.FeedbackRangeMax ?? 5;

            Predicate<Announce> feedbackPredicate = a =>
                    a.Author.FeedbacksMean >= advancedSearchViewModel.FeedbackRangeMin &&
                    a.Author.FeedbacksMean <= upperFeedback;

            var announces =
                _context.Announces.Include( a => a.Author ).Where(
                    a => feedbackPredicate( a ) && pricePredicate( a ) && rangeKmPredicate( a ) );

            if (!advancedSearchViewModel.ShowGifts)
                announces = announces.Where(a => a.Price != 0);

            if (!advancedSearchViewModel.ShowOnSale)
                announces = announces.Where(a => a.Price == 0);


            if ( advancedSearchViewModel.Title != null ) {
                var titleSearch = TitleBasedSearch(advancedSearchViewModel.Title);
                announces = announces.Where( a => titleSearch.Select( i => i.Id ).Contains( a.Id ) );
            }

            if( advancedSearchViewModel.OrderByDate)
                announces = announces.OrderByDescending( a => a.PublishDate );
            else if( advancedSearchViewModel.OrderByDistance ) {
                if( !LoginChecker.HasLoggedUser( this ) )
                    return announces.ToList();
                var user = _context.Users.Single( i => i.Id.Equals( User.GetUserId() ) );
                if( !user.Latitude.HasValue || !user.Longitude.HasValue )
                    return announces.ToList();
                double lat = user.Latitude.Value;
                double lon = user.Longitude.Value;
                announces =
                    announces.OrderBy(a => GeoCoordinate.Distance(lat, lon, a.Latitude, a.Longitude));
            }
            else {
                announces = advancedSearchViewModel.OrderByPrice ? announces.OrderByDescending(a => a.Price) : announces.OrderBy(a => a.Price);
            }   

            return announces.ToList();
        }

        protected override void Dispose( bool disposing ) {
            if( disposing )
                _context.Dispose();
            base.Dispose( disposing );
        }

        
    }


}