using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Cianfrusaglie.Controllers;
using Cianfrusaglie.Models;
using Cianfrusaglie.Services;
using Cianfrusaglie.ViewModels.Account;
using Microsoft.AspNet.Hosting;
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

namespace Cianfrusaglie.Tests {
    public class BaseTestSetup {
        protected const string CommonUserPassword = "Cane1!";
        protected const string FirstUserName = "pippo1";
        protected const string SecondUserName = "pippo2";
        public const string ThirdUserName = "pippo3";
        private readonly IEmailSender _emailSender;

        private readonly Mock< SignInManager< User > > _mockSignInManager;
        private readonly ISmsSender _smsSender;
        protected readonly ApplicationDbContext Context;
        protected readonly IHostingEnvironment HostingEnvironment;
        protected readonly UserManager< User > UserManager;
        protected SignInManager< User > SignInManager;

        protected BaseTestSetup() {
            var services = new ServiceCollection();
            services.AddEntityFramework().AddInMemoryDatabase().AddDbContext< ApplicationDbContext >(
                o => o.UseInMemoryDatabase() );

            services.AddIdentity< User, IdentityRole >().AddEntityFrameworkStores< ApplicationDbContext >()
                .AddDefaultTokenProviders();
            services.AddCaching();
            services.AddSession( options => {
                options.IdleTimeout = TimeSpan.FromMinutes( 30 );
                options.CookieName = ".MyApplication";
            } );

            var defaultHttpContext = new DefaultHttpContext();
            defaultHttpContext.Features.Set( new HttpAuthenticationFeature() );
            services.AddSingleton< IHttpContextAccessor >(
                h => new HttpContextAccessor {HttpContext = defaultHttpContext} );
            var serviceProvider = services.BuildServiceProvider();
            Context = serviceProvider.GetRequiredService< ApplicationDbContext >();

            var mockHostingEnvironment = new Mock< IHostingEnvironment >();
            mockHostingEnvironment.Setup( h => h.WebRootPath ).Returns( "" );

            HostingEnvironment = mockHostingEnvironment.Object;

            UserManager = serviceProvider.GetRequiredService< UserManager< User > >();

            SignInManager = serviceProvider.GetRequiredService< SignInManager< User > >();
            _mockSignInManager = MockSignInManager< User >( UserManager );
            _emailSender = new Mock< IEmailSender >().Object;
            _smsSender = new Mock< ISmsSender >().Object;

            var mockHttpContext = new Mock< HttpContext >();
            mockHttpContext.SetupAllProperties();


            _mockSignInManager.Setup(
                s =>
                    s.PasswordSignInAsync( It.IsAny< string >(), It.IsAny< string >(), It.IsAny< bool >(),
                        It.IsAny< bool >() ) ).Returns( Task.FromResult( SignInResult.Success ) );
            _mockSignInManager.Setup( s => s.SignInAsync( It.IsAny< User >(), It.IsAny< bool >(), It.IsAny< string >() ) )
                .Returns( Task.FromResult( SignInResult.Success ) );

            CreateUsers();
            CreateCategories();
            CreateAnnounces();
            CreateMessages();
        }

        protected ActionContext MockActionContextForLogin( string id ) {
            var mockHttpContext = new Mock< HttpContext >();
            var principal = new Mock< ClaimsPrincipal >();
            principal.Setup( p => p.Identity.IsAuthenticated ).Returns( id != null );
            if( id == null )
                mockHttpContext.SetupGet( x => x.User ).Returns( principal.Object );
            else {
                var c = new Claim( ClaimTypes.NameIdentifier, id );
                principal.Setup( p => p.FindFirst( It.IsAny< string >() ) ).Returns( c );
                mockHttpContext.SetupGet( x => x.User ).Returns( principal.Object );
            }
            return new ActionContext {HttpContext = mockHttpContext.Object};
        }

        protected Mock< SignInManager< TUser > > MockSignInManager< TUser >( UserManager< User > userManager )
            where TUser: class {
            var context = new Mock< HttpContext >();
            return new Mock< SignInManager< TUser > >( userManager,
                new HttpContextAccessor {HttpContext = context.Object},
                new Mock< IUserClaimsPrincipalFactory< TUser > >().Object, null, null ) {CallBase = true};
        }

        protected AccountController CreateAccountController( string userId ) {
            return new AccountController( UserManager, _mockSignInManager.Object, _emailSender, _smsSender,
                new LoggerFactory() ) {
                    Url = new Mock< IUrlHelper >().Object,
                    ActionContext = MockActionContextForLogin( userId )
                };
        }

        private void CreateUsers() {
            var registerViewModel = new RegisterViewModel {
                ConfirmPassword = CommonUserPassword,
                Password = CommonUserPassword,
                Email = "pippo1@gmail.com",
                UserName = FirstUserName
            };
            var user = new User {UserName = registerViewModel.UserName, Email = registerViewModel.Email};
            var identityResult = UserManager.CreateAsync( user, registerViewModel.Password ).Result;
            var result =
                UserManager.CreateAsync( new User {UserName = SecondUserName, Email = "pippo2@gmail.com"},
                    registerViewModel.Password ).Result;
            var result2ForTest =
                UserManager.CreateAsync( new User {UserName = ThirdUserName, Email = "pippo3@gmail.com"},
                    registerViewModel.Password ).Result;
        }

        private void CreateCategories() { Context.EnsureSeedData(); }

        private void CreateAnnounces() {
         var firstUser = Context.Users.Single( u => u.UserName.Equals( FirstUserName ) );
         var secondUser = Context.Users.Single( u => u.UserName.Equals( SecondUserName ) );
           var announce = new Announce {
              Author = secondUser,
              Title = "Libro di OST di Videogiochi",
              Description = "Tutti i compositori da Uematsu in giù"
           };
           Context.Announces.Add( announce );

            var announceCategory1 = new AnnounceCategory {
                Announce = announce,
                Category = Context.Categories.Single( a => a.Name.Equals( "Libri" ) )
            };
            var announceCategory11 = new AnnounceCategory {
                Announce = announce,
                Category = Context.Categories.Single( a => a.Name.Equals( "Musica" ) )
            };
            var announceCategory12 = new AnnounceCategory {
                Announce = announce,
                Category = Context.Categories.Single( a => a.Name.Equals( "Videogiochi" ) )
            };
            Context.AnnounceCategories.Add( announceCategory1 );
            Context.AnnounceCategories.Add( announceCategory11 );
            Context.AnnounceCategories.Add( announceCategory12 );
            Context.SaveChanges();

            
           var announce2 = new Announce {Author = firstUser, Title = "Halo 5 Usato", Description = "Guardiani ovunque"};
           Context.Announces.Add( announce2 );

            var announceCategory2 = new AnnounceCategory {
                Announce = announce2,
                Category = Context.Categories.Single( a => a.Name.Equals( "Videogiochi" ) )
            };
            Context.AnnounceCategories.Add( announceCategory2 );
            Context.SaveChanges();

           var announce3 = new Announce {Author = firstUser, Title = "C for Dummies", Description = "Impara il C", DeadLine = DateTime.Now.AddDays(-1)};
           Context.Announces.Add( announce3 );

            var announceCategory3 = new AnnounceCategory {
               Announce = announce3,
               Category = Context.Categories.Single( a => a.Name.Equals( "Libri" ) )
            };
            Context.AnnounceCategories.Add( announceCategory3 );
            Context.SaveChanges();

         var announce4 = new Announce { Author = firstUser, Title = "Libro di Mariangiongiangela", Description = "non gettate il forno dalla finestra", Closed= true };
         Context.Announces.Add( announce4 );

         var announceCategory4 = new AnnounceCategory {
            Announce = announce4,
            Category = Context.Categories.Single( a => a.Name.Equals( "Cucina" ) )
         };
         Context.AnnounceCategories.Add( announceCategory4 );
         Context.SaveChanges();
      }

        private void CreateMessages() {
            var firstUser = Context.Users.Single( u => u.UserName.Equals( FirstUserName ) );
            var secondUser = Context.Users.Single(u => u.UserName.Equals(SecondUserName));

            var msg1 = new Message() {
                DateTime = DateTime.Now.AddSeconds( -1 ),
                Receiver = secondUser,
                Sender = firstUser,
                Text = "Ciao, come stai?"
            };

            Context.Messages.Add( msg1 );
            Context.SaveChanges();
        }
    }
}