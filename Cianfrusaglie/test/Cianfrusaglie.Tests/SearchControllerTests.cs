using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Cianfrusaglie.Controllers;
using Cianfrusaglie.Models;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc;
using Moq;
using Xunit;

namespace Cianfrusaglie.Tests
{
    public class SearchControllerTests : BaseTestSetup
    {

        protected SearchController CreateResearchController(string id, string userName)
        {
            var mockHttpContext = new Mock<HttpContext>();
            mockHttpContext.SetupAllProperties();
            if (id == null || userName == null) return new SearchController(Context);
            var validPrincipal = new ClaimsPrincipal(
               new[]
               {
                    new ClaimsIdentity(
                        new[] {new Claim(ClaimTypes.NameIdentifier, id)})
               });
            mockHttpContext.Setup(h => h.User).Returns(validPrincipal);
            return new SearchController(Context)
            {
                ActionContext = new ActionContext
                {
                    HttpContext = mockHttpContext.Object
                },
                Url = new Mock<IUrlHelper>().Object
            };
        }

        [Fact]
        public void SimpleSearchCategoryBased()
        {
            var researchController = CreateResearchController(null,null);
            var choosedOnes = new[] {"Musica", "Libri", "Videogiochi"};
            var categories = Context.Categories.Where(c => choosedOnes.Contains(c.Name));
            var result = researchController.CategoryBasedSearch(categories);

            var announce = Context.Announces.Single(a => a.Title == "Libro di OST i Videogiochi");
            Assert.Contains(announce,result);

        }

        [Theory]
        [InlineData("Libro di Videogiochi","Libro di OST di Videogiochi")]
        [InlineData("Halo", "Halo 5 Usato")]
        public void SimpleSearchTitleBased(string searchExample, string realTitle)
        {
            var researchController = CreateResearchController(null, null);
            var result = researchController.TitleBasedSearch(searchExample);
            var announce = Context.Announces.Single(a => a.Title.Equals(realTitle));
            Assert.Contains(announce,result);
        }

        [Fact]
        public void PerformSearchTextAndCategories()
        {
            var researchController = CreateResearchController(null, null);
        }

    }
}
