using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;

namespace Cianfrusaglie.Models {
   public class CianfrusaglieDbContext : DbContext {
      public DbSet< User > Users { get; set; }
      public DbSet< Category > Categories { get; set; }
      public DbSet< Message > Messages { get; set; }

      protected override void OnModelCreating( ModelBuilder modelBuilder ) {
         modelBuilder.Entity< User >().HasIndex( u => u.Email ).IsUnique( true );
         modelBuilder.Entity< User >().HasIndex( u => u.NickName ).IsUnique( true );

         modelBuilder.Entity< Category >().HasIndex( c => c.Name ).IsUnique( true );
         modelBuilder.Entity< Category >().HasOne( c => c.OverCategory ).WithMany( c => c.SubCategories );

         modelBuilder.Entity< Message >().HasOne( m => m.Sender ).WithMany( u => u.SendedMessages ).OnDelete( DeleteBehavior.Restrict );
         modelBuilder.Entity< Message >().HasOne( m => m.Receiver ).WithMany( u => u.ReceivedMessages ).OnDelete( DeleteBehavior.Restrict );
      }
   }
}