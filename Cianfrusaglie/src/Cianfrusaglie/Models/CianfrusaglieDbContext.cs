using Microsoft.Data.Entity;

namespace Cianfrusaglie.Models {
   public class CianfrusaglieDbContext : DbContext {
      public DbSet<User> Users { get; set; }

      protected override void OnModelCreating( ModelBuilder modelBuilder ) {
         modelBuilder.Entity<User>().HasIndex( u => u.Email );
         modelBuilder.Entity<User>().HasIndex( u => u.NickName );
      }
   }
}