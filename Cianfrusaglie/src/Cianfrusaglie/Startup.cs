using System;
using Cianfrusaglie.Models;
using Cianfrusaglie.Services;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Cianfrusaglie {
    public class Startup {
        public Startup( IHostingEnvironment env ) {
            // Set up configuration sources.

            var builder =
                new ConfigurationBuilder().AddJsonFile( "appsettings.json" ).AddJsonFile(
                    $"appsettings.{env.EnvironmentName}.json", true );

            if( env.IsDevelopment() ) {
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets();

                // This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
                builder.AddApplicationInsightsSettings( true );
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices( IServiceCollection services ) {
            // Add framework services.
            services.AddApplicationInsightsTelemetry( Configuration );

            services.AddEntityFramework().AddSqlServer().AddDbContext< ApplicationDbContext >(
                options => options.UseSqlServer( Configuration[ "Data:DefaultConnection:ConnectionString" ] ) );

            // Opzioni sui ruoli
            // Modificare qui le impostazioni sulle password accettate e rifiutate
            services.AddIdentity< User, IdentityRole >( options => {
                options.User.RequireUniqueEmail = true ;
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 8;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonLetterOrDigit = false;
                options.Password.RequireUppercase = false;
            })
            .AddEntityFrameworkStores< ApplicationDbContext >().AddDefaultTokenProviders();

            services.AddMvc();

            services.AddCaching();
            services.AddSession( options => {
                options.IdleTimeout = TimeSpan.FromMinutes( 30 );
                options.CookieName = ".MyApplication";
            } );

            // Add application services.
            services.AddTransient< IEmailSender, AuthMessageSender >();
            services.AddTransient< ISmsSender, AuthMessageSender >();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure( IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory ) {
            loggerFactory.AddConsole( Configuration.GetSection( "Logging" ) );
            loggerFactory.AddDebug();

            app.UseApplicationInsightsRequestTelemetry();
            app.UseSession();

            if( env.IsDevelopment() ) {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            } else {
                app.UseExceptionHandler( "/Home/Error" );
                
                // For more details on creating database during deployment see http://go.microsoft.com/fwlink/?LinkID=615859
            }

            try {
                using( var serviceScope =
                        app.ApplicationServices.GetRequiredService< IServiceScopeFactory >().CreateScope() ) {
                    serviceScope.ServiceProvider.GetService< ApplicationDbContext >().Database.Migrate();
                    serviceScope.ServiceProvider.GetService< ApplicationDbContext >().EnsureSeedData();
                    //TODO togliere in release!!!
                    serviceScope.ServiceProvider.GetService<ApplicationDbContext>().SeedBaseUserTest();
               }
            } catch {}

            app.UseIISPlatformHandler( options => options.AuthenticationDescriptions.Clear() );

            app.UseApplicationInsightsExceptionTelemetry();

            app.UseStaticFiles();

            app.UseIdentity();

            // To configure external authentication please see http://go.microsoft.com/fwlink/?LinkID=532715

            app.UseMvc( routes => { routes.MapRoute( "default", "{controller=Home}/{action=Index}/{id?}" ); } );
        }

        // Entry point for the application.
        public static void Main( string[] args ) => WebApplication.Run< Startup >( args );
    }
}