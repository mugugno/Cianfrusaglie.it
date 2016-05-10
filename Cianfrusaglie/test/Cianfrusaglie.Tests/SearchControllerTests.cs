using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Cianfrusaglie.Controllers;
using Cianfrusaglie.Models;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc;
using Moq;
using Xunit;

namespace Cianfrusaglie.Tests {
   public class SearchControllerTests : BaseTestSetup {

      protected SearchController CreateResearchController( string id, string userName ) {
         var mockHttpContext = new Mock< HttpContext >();
         mockHttpContext.SetupAllProperties();
         if( id == null || userName == null )
            return new SearchController( Context );
         var validPrincipal =
            new ClaimsPrincipal( new[] {new ClaimsIdentity( new[] {new Claim( ClaimTypes.NameIdentifier, id )} )} );
         mockHttpContext.Setup( h => h.User ).Returns( validPrincipal );
         return new SearchController( Context ) {
            ActionContext = new ActionContext {HttpContext = mockHttpContext.Object},
            Url = new Mock< IUrlHelper >().Object
         };
      }

      [Fact]
      public void SimpleSearchCategoryBased() {
         var researchController = CreateResearchController( null, null );
         var choosedOnes = new[] {"Musica", "Libri", "Videogiochi"};
         var categories = Context.Categories.Where( c => choosedOnes.Contains( c.Name ) );
         var result = researchController.CategoryBasedSearch( categories );

         var announce = Context.Announces.Single( a => a.Title == "Libro di OST di Videogiochi" );
         Assert.Contains( announce, result );
      }

      [Theory, InlineData( "Libro di Videogiochi", "Libro di OST di Videogiochi" ), InlineData( "Halo", "Halo 5 Usato" )
      ]
      public void SimpleSearchTitleBased( string searchExample, string realTitle ) {
         var researchController = CreateResearchController( null, null );
         var result = researchController.TitleBasedSearch( searchExample );
         var announce = Context.Announces.Single( a => a.Title.Equals( realTitle ) );
         Assert.Contains( announce, result );
      }

      [Fact]
      public void PerformSearchTextAndCategories() {
         var researchController = CreateResearchController( null, null );
         string title = "Usato";
         var categoriesString = new[] {"Videogiochi"};
         var categories = Context.Categories.Where( c => categoriesString.Contains( c.Name ) );

         var result = researchController.SearchAnnounces( title, categories );
         Assert.Contains( result, p => p.Title == "Halo 5 Usato" );
      }

      [Fact]
      public void PerformSearchWithEmptyTitleAndEmptyCategoriesReturnEmptyResult() {
         var researchController = CreateResearchController( null, null );
         var result = researchController.SearchAnnounces( "", new List< Category >() );
         Assert.Empty( result );
      }

      [Fact]
      public void SearchOnlyForCategory() {
         var researchController = CreateResearchController( null, null );
         var category = Context.Categories.Single( c => c.Name.Equals( "Videogiochi" ) );
         var allVideogamesAnnounces = Context.AnnounceCategories.Where( ac => ac.CategoryId.Equals( category.Id ) );
         var announces = allVideogamesAnnounces.Select( ac => ac.Announce ).Distinct(); //tutti gli annunnci delle categorie category

         var result = researchController.SearchAnnounces( "", new[] { category } );
         Assert.Empty( result.Except( announces ) );
         Assert.Empty( announces.ToList().Except( result ) );
      }

      [Fact]
      public void SearchOnlyForTitle() {
         var researchController = CreateResearchController( null, null );
         var title = "Usato";
         var result = researchController.SearchAnnounces( title, new List< Category >() );
         Assert.Contains( result, p => p.Title == "Halo 5 Usato" );
      }
   }
}