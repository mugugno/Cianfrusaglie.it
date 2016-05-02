using Microsoft.Data.Entity;

namespace Cianfrusaglie.Models {
   public class CianfrusaglieDbContext : DbContext {
      public DbSet< User > Users { get; set; }
      public DbSet< Category > Categories { get; set; }
      public DbSet< Message > Messages { get; set; }

      protected override void OnModelCreating( ModelBuilder modelBuilder ) {
         modelBuilder.Entity< User >().HasIndex( u => u.Email );
         modelBuilder.Entity< User >().HasIndex( u => u.NickName );

         modelBuilder.Entity< Category >().HasIndex( u => u.Name );
         modelBuilder.Entity< Category >().HasOne( c => c.OverCategory ).WithMany( c => c.SubCategories );

         modelBuilder.Entity< Message >().HasOne( u => u.Sender );
         modelBuilder.Entity< Message >().HasOne( u => u.Receiver );
      }
   }
}