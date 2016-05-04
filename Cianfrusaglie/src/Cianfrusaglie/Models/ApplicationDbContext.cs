using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;

namespace Cianfrusaglie.Models
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Message> Messages { get; set; }
      public DbSet<Announce> Announces { get; set; } 

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            builder.Entity<Category>().HasIndex(c => c.Name).IsUnique(true);
            builder.Entity<Category>().HasOne(c => c.OverCategory).WithMany(c => c.SubCategories);

         builder.Entity<Announce>().HasOne( u => u.Author ).WithMany( u => u.PublishedAnnounces ).OnDelete( DeleteBehavior.Restrict );

         builder.Entity< Message >().HasOne( m => m.Sender ).WithMany( u => u.SentMessages ).OnDelete( DeleteBehavior.Restrict );
            builder.Entity< Message >().HasOne( m => m.Receiver ).WithMany( u => u.ReceivedMessages ).OnDelete( DeleteBehavior.Restrict );
        }
    }
}
