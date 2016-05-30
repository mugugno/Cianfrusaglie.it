using System;
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

      protected SearchController CreateResearchController( string id ) {
         return new SearchController( Context ) {
             ActionContext = MockActionContextForLogin(id),
             Url = new Mock<IUrlHelper>().Object
         };
      }

      [Fact]
      public void SimpleSearchCategoryBased() {
         var researchController = CreateResearchController( null );
         var choosedOnes = new[] {"Musica", "Libri", "Videogiochi"};
         var categories = Context.Categories.Where( c => choosedOnes.Contains( c.Name ) ).Select(c=>c.Id);
         var result = researchController.CategoryBySearch( categories );

         var announce = Context.Announces.Single( a => a.Title == "Libro di OST di Videogiochi" );
         Assert.Contains( announce, result );
      }

      [Theory, 
       InlineData( "Libro di Videogiochi", "Libro di OST di Videogiochi" ),
       InlineData( "Halo", "Halo 5 Usato" )]
      public void SimpleSearchTitleBased( string searchExample, string realTitle ) {
         var researchController = CreateResearchController( null );
         var result = researchController.TitleBasedSearch( searchExample );
         var announce = Context.Announces.Single( a => a.Title.Equals( realTitle ) );
         Assert.Contains( announce, result );
      }

      [Fact]
      public void PerformSearchTextAndCategories() {
         var researchController = CreateResearchController( null );
         string title = "Usato";
         var categoriesString = new[] {"Videogiochi"};
         var categories = Context.Categories.Where( c => categoriesString.Contains( c.Name ) ).Select(c=>c.Id);

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
      public void SearchOnlyForCategory() {
         var researchController = CreateResearchController( null );
         var category = Context.Categories.Single( c => c.Name.Equals( "Videogiochi" ) );
         var allVideogamesAnnounces = Context.AnnounceCategories.Where( ac => ac.CategoryId.Equals( category.Id ) );
         var announces = allVideogamesAnnounces.Select( ac => ac.Announce ).Distinct(); //tutti gli annunnci delle categorie category

         var result = researchController.SearchAnnounces( "", new[] { category.Id } );
          var enumerable = result as Announce[] ?? result.ToArray();
          Assert.Empty( enumerable.Except( announces ) );
         Assert.Empty( announces.ToList().Except( enumerable ) );
      }

      [Fact]
      public void SearchOnlyForTitle() {
         var researchController = CreateResearchController( null );
         var title = "Usato";
         var result = researchController.SearchAnnounces( title, new List< int >() );
         Assert.Contains( result, p => p.Title == "Halo 5 Usato" );
      }

      [Fact]
      public void SearchOnCategoriesDoesntContainClosedOrExpired() {
         var researchController = CreateResearchController( null );
         var category = Context.Categories.Where( c => c.Name.Equals( "Cucina" ) || c.Name.Equals("Libri") ).Select( c => c.Id );

         var result = researchController.CategoryBySearch( category ).ToList();
         Assert.True( !result.Any( p => p.Closed ) );
         Assert.True( !result.Any( p => p.DeadLine != null && p.DeadLine < DateTime.Now ) );
      }

      [Theory, InlineData( "C for Dummies" ), InlineData( "Libro di Mariangiongiangela" )]
      public void SearchOnTitleDoesntContainClosedOrExpired( string title ) {
         var researchController = CreateResearchController( null );

         var result = researchController.TitleBasedSearch( title ).ToList();
         Assert.True( !result.Any( p => p.Closed ) );
         Assert.True( !result.Any( p => p.DeadLine != null && p.DeadLine < DateTime.Now ) );
      }

      [Theory, InlineData("Libro")]
      public void SearchByTitleReturnsAnnouncesWithTheSubWord(string title) {
         var researchController = CreateResearchController( null );
         var result = researchController.TitleBasedSearch( title );

         Assert.True( result.All( r => SearchController.AreSimilar( r.Title, title ) ) );
      }

      [Theory, InlineData( "libr", "libro" )]
      public void AreSimilarSubWord( string a, string b ) {
         Assert.True( SearchController.AreSimilar( a, b ) );
      }
   }
}