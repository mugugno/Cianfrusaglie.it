using System.Linq;

namespace Cianfrusaglie.Models {
   public static class ApplicationDbInitializer {
      public static void EnsureSeedData( this ApplicationDbContext ctx ) {
         if( !ctx.Categories.Any() ) {
            var c = new Category() {Name = "Libri Musica Film"};
            ctx.Categories.Add( c );
         }

         ctx.SaveChanges();
      }
   }
}