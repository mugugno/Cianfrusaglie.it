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
    public class ResearchControllerTests : BaseTestSetup
    {

        protected ResearchController CreateResearchController(string id, string userName)
        {
            var mockHttpContext = new Mock<HttpContext>();
            mockHttpContext.SetupAllProperties();
            if (id == null || userName == null) return new ResearchController(Context);
            var validPrincipal = new ClaimsPrincipal(
               new[]
               {
                    new ClaimsIdentity(
                        new[] {new Claim(ClaimTypes.NameIdentifier, id)})
               });
            mockHttpContext.Setup(h => h.User).Returns(validPrincipal);
            return new ResearchController(Context)
            {
                ActionContext = new ActionContext
                {
                    HttpContext = mockHttpContext.Object
                },
                Url = new Mock<IUrlHelper>().Object
            };
        }

        [Fact]
        public void SimpleSearchReturnsCorrectValues()
        {
            var usr = Context.Users.Single(u => u.UserName.Equals(FirstUserName));
            var researchController = CreateResearchController(usr.Id,usr.UserName);
            var choosedOnes = new[] {"Musica", "Libri", "Videogiochi"};
            var categories = Context.Categories.Where(c => choosedOnes.Contains(c.Name));
            var result = researchController.CategoryBasedSearch(categories);

            Assert.True(result.Equals(Context.Announces));

        }

    }
}
