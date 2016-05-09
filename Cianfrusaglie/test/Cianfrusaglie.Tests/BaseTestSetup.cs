using Cianfrusaglie.Models;
using System;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using Cianfrusaglie.Controllers;
using Cianfrusaglie.Services;
using Cianfrusaglie.ViewModels.Account;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Http.Features;
using Microsoft.AspNet.Http.Features.Authentication.Internal;
using Microsoft.AspNet.Http.Internal;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;

namespace Cianfrusaglie.Tests
{
    public class BaseTestSetup
    {
        protected const string CommonUserPassword = "Cane1!";
        protected const string FirstUserName = "pippo1";
        protected const string SecondUserName = "pippo2";
        protected readonly ApplicationDbContext Context;
        protected readonly UserManager<User> UserManager;
        protected SignInManager<User> SignInManager;

        private readonly Mock<SignInManager<User>> _mockSignInManager;
        private readonly IEmailSender _emailSender;
        private readonly ISmsSender _smsSender;
        private readonly Mock<HttpContext> _mockHttpContext;

        private Mock<SignInManager<TUser>> MockSignInManager<TUser>(UserManager<User> userManager ) where TUser : class
        {
            var context = new Mock<HttpContext>();
            return new Mock<SignInManager<TUser>>(userManager,
                new HttpContextAccessor { HttpContext = context.Object },
                new Mock<IUserClaimsPrincipalFactory<TUser>>().Object,
                null, null)
            { CallBase = true };
        }

        protected BaseTestSetup()
        {
            var services = new ServiceCollection();
            services.AddEntityFramework()
                .AddInMemoryDatabase()
                .AddDbContext<ApplicationDbContext>(o => o.UseInMemoryDatabase());

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

            var defaultHttpContext = new DefaultHttpContext();
            defaultHttpContext.Features.Set( new HttpAuthenticationFeature() );
            services.AddSingleton<IHttpContextAccessor>(h => new HttpContextAccessor { HttpContext = defaultHttpContext });
            var serviceProvider = services.BuildServiceProvider();
            Context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            UserManager = serviceProvider.GetRequiredService<UserManager<User>>();

            SignInManager = serviceProvider.GetRequiredService<SignInManager<User>>();
            _mockSignInManager = MockSignInManager<User>(UserManager);
            _emailSender = new Mock<IEmailSender>().Object;
            _smsSender = new Mock<ISmsSender>().Object;

            _mockHttpContext = new Mock<HttpContext>();
            _mockHttpContext.SetupAllProperties();
            

            _mockSignInManager.Setup(
                s => s.PasswordSignInAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()))
                 .Returns(Task.FromResult<SignInResult>(SignInResult.Success));

            CreateUsers();
            CreateCategories();
            CreateAnnounces();

        }

        protected AccountController CreateAccountController()
        {
            return new AccountController(UserManager, _mockSignInManager.Object, _emailSender, _smsSender, new LoggerFactory())
            {
                Url = new Mock<IUrlHelper>().Object
            };
        }

        protected AnnouncesController CreateAnnounceController(string id, string userName)
        {
            if(id == null || userName==null) return new AnnouncesController(Context);
            var validPrincipal = new ClaimsPrincipal(
               new[]
               {
                    new ClaimsIdentity(
                        new[] {new Claim(ClaimTypes.NameIdentifier, id)})
               });
            _mockHttpContext.Setup(h => h.User).Returns(validPrincipal);
            return new AnnouncesController(Context)
            {
                ActionContext = new ActionContext
                {
                    HttpContext = _mockHttpContext.Object
                },
                Url = new Mock<IUrlHelper>().Object
            };
        }

private void CreateUsers()
        {
            var registerViewModel = new RegisterViewModel()
            {
                ConfirmPassword = CommonUserPassword,
                Password = CommonUserPassword,
                Email = "pippo1@gmail.com",
                UserName = FirstUserName
            };
            var user = new User { UserName = registerViewModel.UserName, Email = registerViewModel.Email };
            var identityResult = UserManager.CreateAsync(user, registerViewModel.Password).Result;
            var result = UserManager.CreateAsync(new User() { UserName = SecondUserName, Email = "pippo2@gmail.com" }, registerViewModel.Password).Result;

            //Context.Users.Add(new User() { UserName = "pippopaolo", Email = "pippopaolo@gmail.com", PasswordHash = "fuewvuw4y75w94ywif" });
            //Context.Users.Add(new User() { UserName = "pippopaolo2", Email = "pippopaolo2@gmail.com", PasswordHash = "fuewvuw4y75w94ywif" });
            //Context.Users.Add(new User() { UserName = "pippopaolo3", Email = "pippopaolo3@gmail.com", PasswordHash = "fuewvuw4y75w94ywif" });
            //Context.Users.Add(new User() { UserName = "pippopaolo4", Email = "pippopaolo4@gmail.com", PasswordHash = "fuewvuw4y75w94ywif" });
        }

        private void CreateCategories()
        {
            Context.EnsureSeedData();
        }

        private void CreateAnnounces()
        {
            var announce = new Announce();
            var usr = Context.Users.Single(u => u.UserName.Equals(SecondUserName));
            announce.Author = usr;
            announce.Title = "Libro di OST di Videogiochi";
            announce.Description = "Tutti i compositori da Uematsu in giù";
            announce.GeoCoordinate = new GeoCoordinateEntity();
            Context.Announces.Add(announce);

            var announceCategory1 = new AnnounceCategory()
            {
                Announce = announce,
                Category = Context.Categories.Single(a => a.Name.Equals("Libri"))
            };
            var announceCategory11 = new AnnounceCategory()
            {
                Announce = announce,
                Category = Context.Categories.Single(a => a.Name.Equals("Musica"))
            };
            var announceCategory12 = new AnnounceCategory()
            {
                Announce = announce,
                Category = Context.Categories.Single(a => a.Name.Equals("Videogiochi"))
            };
            Context.AnnounceCategories.Add(announceCategory1);
            Context.AnnounceCategories.Add(announceCategory11);
            Context.AnnounceCategories.Add(announceCategory12);
            Context.SaveChanges();

            var announce2 = new Announce();
            var usr2 = Context.Users.Single(u => u.UserName.Equals(FirstUserName));
            announce2.Author = usr2;
            announce2.Title = "Halo 5 Usato";
            announce2.Description = "Guardiani ovunque";
            announce2.GeoCoordinate = new GeoCoordinateEntity();
            Context.Announces.Add(announce2);

            var announceCategory2 = new AnnounceCategory()
            {
                Announce = announce2,
                Category = Context.Categories.Single(a => a.Name.Equals("Videogiochi"))
            };
            Context.AnnounceCategories.Add(announceCategory2);
            Context.SaveChanges();
        }
      
    }
}
