using System.Linq;
using Cianfrusaglie.Controllers;
using Microsoft.AspNet.Mvc;
using Moq;
using Xunit;

namespace Cianfrusaglie.Tests
{
    public class ProfileControllerTest : BaseTestSetup
    {
        protected ProfileController CreateProfileController(string userId)
        {
            return new ProfileController(Context)
            {
                ActionContext = MockActionContextForLogin(userId),
                Url = new Mock<IUrlHelper>().Object
            };
        }

        [Fact]
        public void UserTryToViewHisProfileOk()
        {
            var usr = Context.Users.Single(u => u.UserName.Equals(FirstUserName));
            var profile = CreateProfileController(usr.Id);
            var result = profile.Index(usr.Id);
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void VisitorTryToViewHisProfileBadRequest()
        {
            var history = CreateProfileController(null);
            Assert.IsType<BadRequestResult>(history.Index(null));
        }
    }
}