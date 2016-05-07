using Cianfrusaglie.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Http.Features;
using Microsoft.AspNet.Http.Features.Authentication;
using Microsoft.AspNet.Http.Features.Authentication.Internal;
using Microsoft.AspNet.Http.Internal;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Internal;
using Microsoft.Data.Entity.Storage.Internal;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;

namespace Cianfrusaglie.Tests
{
    public class BaseTestSetup
    {
        protected readonly ApplicationDbContext Context;
        protected UserManager<User> UserManager;
        protected SignInManager<User> SignInManager; 

        public BaseTestSetup()
        {

            var services = new ServiceCollection();
            services.AddEntityFramework()
                .AddInMemoryDatabase()
                .AddDbContext<ApplicationDbContext>(o => o.UseInMemoryDatabase());
            services.AddIdentity<User, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

            var context = new DefaultHttpContext();
            context.Features.Set( new HttpAuthenticationFeature() );
            services.AddSingleton<IHttpContextAccessor>(h => new HttpContextAccessor { HttpContext = context });
            var serviceProvider = services.BuildServiceProvider();
            Context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            UserManager = serviceProvider.GetRequiredService<UserManager<User>>();
            SignInManager = serviceProvider.GetRequiredService<SignInManager<User>>();


            CreateUsers();

            Context.SaveChanges();
        }

        private void CreateUsers()
        {
            Context.Users.Add(new User() { UserName = "pippopaolo", Email = "pippopaolo@gmail.com", PasswordHash = "fuewvuw4y75w94ywif" });
            Context.Users.Add(new User() { UserName = "pippopaolo2", Email = "pippopaolo2@gmail.com", PasswordHash = "fuewvuw4y75w94ywif" });
            Context.Users.Add(new User() { UserName = "pippopaolo3", Email = "pippopaolo3@gmail.com", PasswordHash = "fuewvuw4y75w94ywif" });
            Context.Users.Add(new User() { UserName = "pippopaolo4", Email = "pippopaolo4@gmail.com", PasswordHash = "fuewvuw4y75w94ywif" });
        }

      
    }
}
