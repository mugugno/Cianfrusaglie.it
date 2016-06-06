using System;
using System.Collections.Generic;
using System.Linq;
using Cianfrusaglie.Controllers;
using Cianfrusaglie.Models;
using Cianfrusaglie.ViewModels.Search;
using Microsoft.AspNet.Mvc;
using Moq;
using Xunit;
using Microsoft.Data.Entity;

namespace Cianfrusaglie.Tests {
    public class SearchControllerTests : BaseTestSetup {
        protected SearchController CreateResearchController( string id ) {
            return new SearchController( Context ) {
                ActionContext = MockActionContextForLogin( id ),
                Url = new Mock< IUrlHelper >().Object
            };
        }

        [Theory, InlineData( "Libro di Videogiochi", "Libro di OST di Videogiochi" ),
         InlineData( "Halo", "Halo 5 Usato" )]
        public void SimpleSearchTitleBased( string searchExample, string realTitle ) {
            var researchController = CreateResearchController( null );
            var result = researchController.TitleBasedSearch( searchExample );
            var announce = Context.Announces.Single( a => a.Title.Equals( realTitle ) );
            Assert.Contains( announce, result );
        }

        [Theory, InlineData( "C for Dummies" ), InlineData( "Libro di Mariangiongiangela" )]
        public void SearchOnTitleDoesntContainClosedOrExpired( string title ) {
            var researchController = CreateResearchController( null );

            var result = researchController.TitleBasedSearch( title ).ToList();
            Assert.True( !result.Any( p => p.Closed ) );
            Assert.True( !result.Any( p => p.DeadLine != null && p.DeadLine < DateTime.Now ) );
        }

        [Theory, InlineData( "Libro" )]
        public void SearchByTitleReturnsAnnouncesWithTheSubWord( string title ) {
            var researchController = CreateResearchController( null );
            var result = researchController.TitleBasedSearch( title );

            Assert.True( result.All( r => SearchController.AreSimilar( r.Title, title ) ) );
        }

        [Theory, InlineData( "libr", "libro" )]
        public void AreSimilarSubWord( string a, string b ) {
            Assert.True( SearchController.AreSimilar( b, a ) );
        }

        [Fact]
        public void PerformSearchTextAndCategories() {
            var researchController = CreateResearchController( null );
            string title = "Usato";
            var categoriesString = new[] {"Videogiochi"};
            var categories = Context.Categories.Where( c => categoriesString.Contains( c.Name ) ).Select( c => c.Id );

            var result = researchController.SearchAnnounces( title, categories );
            Assert.Contains( result, p => p.Title == "Halo 5 Usato" );
        }

        [Fact]
        public void PerformSearchWithEmptyTitleAndEmptyCategoriesReturnEmptyResult() {
            var researchController = CreateResearchController( null );
            var result = researchController.SearchAnnounces( "", new List< int >() );
            Assert.Empty( result );
        }

        [Fact]
        public void SearchOnCategoriesDoesntContainClosedOrExpired() {
            var researchController = CreateResearchController( null );
            var category =
                Context.Categories.Where( c => c.Name.Equals( "Cucina" ) || c.Name.Equals( "Libri" ) ).Select( c => c.Id );

            var result = researchController.CategoryBySearch( category ).ToList();
            Assert.True( !result.Any( p => p.Closed ) );
            Assert.True( !result.Any( p => p.DeadLine != null && p.DeadLine < DateTime.Now ) );
        }

        [Fact]
        public void SearchOnlyForCategory() {
            var researchController = CreateResearchController( null );
            var category = Context.Categories.Single( c => c.Name.Equals( "Videogiochi" ) );
            var allVideogamesAnnounces = Context.AnnounceCategories.Where( ac => ac.CategoryId.Equals( category.Id ) );
            var announces = allVideogamesAnnounces.Select( ac => ac.Announce ).Distinct();
            //tutti gli annunnci delle categorie category

            var result = researchController.SearchAnnounces( "", new[] {category.Id} );
            var enumerable = result as Announce[] ?? result.ToArray();
            Assert.Empty( enumerable.Except( announces ) );
            Assert.Empty( announces.ToList().Except( enumerable ) );
        }

        [Fact]
        public void SearchOnlyForTitle() {
            var researchController = CreateResearchController( null );
            string title = "Usato";
            var result = researchController.SearchAnnounces( title, new List< int >() );
            Assert.Contains( result, p => p.Title == "Halo 5 Usato" );
        }

        [Fact]
        public void SimpleSearchCategoryBased() {
            var researchController = CreateResearchController( null );
            var choosedOnes = new[] {"Musica", "Libri", "Videogiochi"};
            var categories = Context.Categories.Where( c => choosedOnes.Contains( c.Name ) ).Select( c => c.Id );
            var result = researchController.CategoryBySearch( categories );

            var announce = Context.Announces.Single( a => a.Title == "Libro di OST di Videogiochi" );
            Assert.Contains( announce, result );
        }

        [Fact]
        public void TitleSearchWhichContainsGatReturnsCorrectResult() {
            var user = Context.Users.Single( u => u.UserName.Equals( FirstUserName ) );
            var searchController = CreateResearchController( user.Id );
            var gat = new Gat() {Text = "Ciao"};
            var announce = Context.Announces.Single( a => a.Title == "Libro di OST di Videogiochi" );
            var announceGat = new AnnounceGat() {Announce = announce, Gat = gat};
            Context.Gats.Add( gat );
            Context.AnnounceGats.Add( announceGat );
            Context.SaveChanges();

            var result = searchController.TitleBasedSearch( gat.Text );
            Assert.Contains( announce, result );
        }

        [Fact]
        public void TitleSearchWhichNotContainsGatReturnsCorrectResultWithoutAnnounce() {
            var user = Context.Users.Single( u => u.UserName.Equals( FirstUserName ) );
            var searchController = CreateResearchController( user.Id );
            var gat = new Gat() {Text = "Ciao"};
            var announce = Context.Announces.Single( a => a.Title == "Libro di OST di Videogiochi" );
            var announceGat = new AnnounceGat() {Announce = announce, Gat = gat};
            Context.Gats.Add( gat );
            Context.AnnounceGats.Add( announceGat );
            Context.SaveChanges();

            var result = searchController.TitleBasedSearch( "Altra cosa" );
            Assert.DoesNotContain( announce, result );
        }

        [Fact]
        public void UserPerformsAdvancedSearchUsingTitleAndIsOk()
        {
            var user = Context.Users.Single(u => u.UserName.Equals(FirstUserName));
            var searchController = CreateResearchController(user.Id);
            var advSearchByTitle = new AdvancedSearchViewModel() {Title = "Halo"};
            var announce =
                Context.Announces.Single(ann => ann.Author.UserName == FirstUserName && ann.Title == "Halo 5 Usato");
            var result = searchController.PerformAdvancedSearch(advSearchByTitle);
            Assert.Contains(announce, result);
        }

        [Fact]
        public void UserPerformsAdvancedSearchUsingPriceRange()
        {
            var user = Context.Users.Single(u => u.UserName.Equals(FirstUserName));
            var searchController = CreateResearchController(user.Id);
            var advSearchByPriceRange = new AdvancedSearchViewModel()
            {
                PriceRange = new Tuple<int, int>(500, 1500)
            };
            var announce =
                Context.Announces.Single(
                    ann => ann.Price == 1000);
            var result = searchController.PerformAdvancedSearch(advSearchByPriceRange);
            Assert.Contains(announce, result);

            var wrongAnnounce = Context.Announces.Single(ann => ann.Price == 15);
            Assert.DoesNotContain(wrongAnnounce, result);
        }

        [Fact]
        public void UserPerformsAdvancedSearchUsingKmRange()
        {
            var user = Context.Users.Single(u => u.UserName.Equals(FirstUserName));
            user.Latitude = 0.0;
            user.Longitude = 0.0;
            Context.SaveChanges();
            var searchController = CreateResearchController(user.Id);
            var advSearchByKmRange = new AdvancedSearchViewModel()
            {
                KmRange = new Tuple<int, int>(0, 10)
            };
            var announce = Context.Announces.Single(ann => ann.Title == "Regalo bicicletta arrugginita");
            var result = searchController.PerformAdvancedSearch(advSearchByKmRange);

            Assert.Contains(announce, result);

            var wrongAnnounce = Context.Announces.Single(ann => ann.Title == "Halo 5 Usato");
            Assert.DoesNotContain(wrongAnnounce, result);
        }

        [Fact]
        public void UserPerformsAdvancedSearchUsingFeedback()
        {
            var user = Context.Users.Single(u => u.UserName.Equals(FirstUserName));
            var user2 = Context.Users.Single(u => u.UserName.Equals(SecondUserName));
            user.FeedbacksMean = 4.0;
            user2.FeedbacksMean = 2.0;
            Context.SaveChanges();
            var searchController = CreateResearchController(user.Id);
            var advSearchByFeedback = new AdvancedSearchViewModel() {FeedbackRatingRange = new Tuple<int, int>(3,5)};
            var announce = Context.Announces.First(ann => ann.AuthorId.Equals(user.Id));
            var result = searchController.PerformAdvancedSearch(advSearchByFeedback);
            var wrongAnnounce = Context.Announces.First(ann => ann.AuthorId.Equals(user2.Id));

            Assert.Contains(announce, result);
            Assert.DoesNotContain(wrongAnnounce, result);
        }

        [Fact]
        public void UserRequestSearchResultOrderedByDateAndIsOk() {
            var user = Context.Users.Single(u => u.UserName.Equals(FirstUserName));
            var searchController = CreateResearchController(user.Id);
            user.Latitude = 0.0;
            user.Longitude = 0.0;
            var advOrderDate = new AdvancedSearchViewModel()
            {
                OrderByDate = true,
                ShowGifts = true,
                ShowOnSale = true
            };
            var result = searchController.PerformAdvancedSearch(advOrderDate);
            var announces = Context.Announces
                .OrderBy( a=>a.PublishDate ).ToList();
            Assert.Equal( announces, result );
        }

        [Fact]
        public void UserRequestSearchResultNotOrderedByDateAndIsOk()
        {
            var user = Context.Users.Single(u => u.UserName.Equals(FirstUserName));
            var searchController = CreateResearchController(user.Id);
            user.Latitude = 0.0;
            user.Longitude = 0.0;
            var advOrderDate = new AdvancedSearchViewModel()
            {
                OrderByDate = false,
                ShowGifts = true,
                ShowOnSale = true
            };
            
            var result = searchController.PerformAdvancedSearch(advOrderDate).Select(a=>a.Id);
            var announces = Context.Announces.OrderBy(a=>a.PublishDate).Select(a => a.Id).ToList();
            Assert.NotEqual(announces, result);
        }

        [Fact]
        public void UserRequestSearchResultOrderdByPriceAscendingAndIsOk() {
            var user = Context.Users.Single(u => u.UserName.Equals(FirstUserName));
            var searchController = CreateResearchController(user.Id);
            var advOrderDate = new AdvancedSearchViewModel()
            {
                OrderByPrice = true,
                ShowGifts = true,
                ShowOnSale = true
            };
            var result = searchController.PerformAdvancedSearch(advOrderDate).Select(a => a.Id).ToList();
            var announces = Context.Announces
                .OrderBy(a => a.Price).Select(a => a.Id).ToList();
            Assert.Equal(announces, result);
        }

        [Fact]
        public void UserRequestSearchResultOrderdDescendingByPriceAndIsOk()
        {
            var user = Context.Users.Single(u => u.UserName.Equals(FirstUserName));
            var searchController = CreateResearchController(user.Id);
            var advOrderDate = new AdvancedSearchViewModel()
            {
                OrderByPrice = false,
                ShowGifts = true,
                ShowOnSale = true
            };
            var result = searchController.PerformAdvancedSearch(advOrderDate).Select(a=>a.Id).ToList();
            var announces = Context.Announces
                .OrderByDescending(a => a.Price).Select(a => a.Id).ToList();
            Assert.Equal(announces, result);

        }

        [Fact]
        public void UserRequestSearchResultOrderdByRangeAscendingAndIsOk()
        {

        }

        [Fact]
        public void UserRequestSearchResultOrderdDescendingByDistanceAndIsOk()
        {

        }

        [Fact]
        public void UserRequestOnlyGiftAndIsOk() {
            var user = Context.Users.Single(u => u.UserName.Equals(FirstUserName));
            var searchController = CreateResearchController(user.Id);
            var adv = new AdvancedSearchViewModel()
            {
                ShowGifts = true
            };
            var result = searchController.PerformAdvancedSearch(adv).Select(a => a.Id).ToList();
            var announces = Context.Announces.Where( a => a.Price == 0 ).Select(a=>a.Id).ToList();
            Assert.Equal(announces, result);
        }

        [Fact]
        public void UserRequestOnlySalesAndIsOk()
        {
            var user = Context.Users.Single(u => u.UserName.Equals(FirstUserName));
            var searchController = CreateResearchController(user.Id);
            var adv = new AdvancedSearchViewModel()
            {
                ShowOnSale = true
            };
            var result = searchController.PerformAdvancedSearch(adv).Select(a => a.Id).ToList();
            var announces = Context.Announces.Where(a => a.Price != 0).Select(a => a.Id).ToList();
            Assert.Equal(announces, result);
        }
    }

}