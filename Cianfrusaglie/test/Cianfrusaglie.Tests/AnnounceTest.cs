using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Castle.Core.Logging;
using Cianfrusaglie.Controllers;
using Cianfrusaglie.Models;
using Cianfrusaglie.Services;
using Microsoft.AspNet.Identity;
using Moq;
using Xunit;


namespace Cianfrusaglie.Tests
{
    public class AnnounceTest : BaseTestSetup
    {

        private AnnouncesController donation;
        private AccountController _accountController;
        private Mock<AnnouncesController> _donationMockCreator;
        public AnnounceTest()
        {
            donation = new AnnouncesController(_context);
            var mockUserManagerCreator = new Mock<UserManager<User>>();
            var mockSigninManagerCreator = new Mock<SignInManager<User>>();
            var mockEmailSenderCreator = new Mock<IEmailSender>();
            var mockLoggerCreator = new Mock<ILoggerFactory>();
            var mockSmsSenderCreator = new Mock<ISmsSender>();

            mockSigninManagerCreator.Setup(
                s => s.PasswordSignInAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()))
                .Returns(new Task<SignInResult>( () => SignInResult.Success ));

            _accountController = new AccountController(mockUserManagerCreator.Object, mockSigninManagerCreator.Object,mockEmailSenderCreator.Object, mockSmsSenderCreator.Object, mockLoggerCreator.Object);
            _donationMockCreator = new Mock<AnnouncesController>();
            
        }

        [Fact]
        public void UserEditHisAnnounce()
        {
            //var usr = _context.Users.First();
            //_donationMockCreator.Setup(u => u.User).Returns( usr );
            //var d = _donationMockCreator.Object;
        }
    }
}
