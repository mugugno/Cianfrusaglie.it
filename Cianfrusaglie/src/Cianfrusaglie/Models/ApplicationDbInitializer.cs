using System.Linq;

namespace Cianfrusaglie.Models {
   public static class ApplicationDbInitializer {
      public static void EnsureSeedData( this ApplicationDbContext ctx ) {
         if( !ctx.Categories.Any() ) {
            var c = new Category() { Name = "Libri Musica Film" };
            ctx.Categories.Add( c );
            ctx.SaveChanges();

            ctx.Categories.AddRange(
               new Category { OverCategory = c, Name = "Libri" },
               new Category { OverCategory = c, Name = "Musica" },
               new Category { OverCategory = c, Name = "Film" }
            );
            ctx.SaveChanges();

            c = new Category() { Name = "Videogiochi e Console" };
            ctx.Categories.Add( c );
            ctx.SaveChanges();

            ctx.Categories.AddRange(
               new Category { OverCategory = c, Name = "Videogiochi" },
               new Category { OverCategory = c, Name = "Console" }
            );
            ctx.SaveChanges();

            c = new Category() { Name = "Arredamento e accessori per la casa" };
            ctx.Categories.Add( c );
            ctx.SaveChanges();

            ctx.Categories.AddRange(
               new Category { OverCategory = c, Name = "Mobili" },
               new Category { OverCategory = c, Name = "Elettrodomestici" },
               new Category { OverCategory = c, Name = "Illuminazione" },
               new Category { OverCategory = c, Name = "Cucina" },
               new Category { OverCategory = c, Name = "Arredamento" },
               new Category { OverCategory = c, Name = "Accessori per la casa" }
            );
            ctx.SaveChanges();

            c = new Category() { Name = "Oggettistica" };
            ctx.Categories.Add( c );
            ctx.SaveChanges();

            ctx.Categories.AddRange(
               new Category { OverCategory = c, Name = "Giocattoli" },
               new Category { OverCategory = c, Name = "Modellismo e collezionismo" },
               new Category { OverCategory = c, Name = "Cianfrusaglie" }
            );
            ctx.SaveChanges();

            c = new Category() { Name = "Sport Tempo libero Fai da te" };
            ctx.Categories.Add( c );
            ctx.SaveChanges();

            ctx.Categories.AddRange(
               new Category { OverCategory = c, Name = "Sport" },
               new Category { OverCategory = c, Name = "Tempo libero" },
               new Category { OverCategory = c, Name = "Fai da te" }
            );
            ctx.SaveChanges();

            c = new Category() { Name = "Abbigliamento" };
            ctx.Categories.Add( c );
            ctx.SaveChanges();

            ctx.Categories.AddRange(
               new Category { OverCategory = c, Name = "Abbigliamento" },
               new Category { OverCategory = c, Name = "Accessori" },
               new Category { OverCategory = c, Name = "Scarpe" }
            );
            ctx.SaveChanges();

            c = new Category() { Name = "Auto e Moto" };
            ctx.Categories.Add( c );
            ctx.SaveChanges();

            ctx.Categories.AddRange(
               new Category { OverCategory = c, Name = "Auto e Moto" },
               new Category { OverCategory = c, Name = "Accessori" }
            );
            ctx.SaveChanges();

            c = new Category() { Name = "Elettronica" };
            ctx.Categories.Add( c );
            ctx.SaveChanges();

            ctx.Categories.AddRange(
               new Category { OverCategory = c, Name = "Computer" },
               new Category { OverCategory = c, Name = "Cellulari" },
               new Category { OverCategory = c, Name = "Tv e schermi" },
               new Category { OverCategory = c, Name = "Audio" },
               new Category { OverCategory = c, Name = "Accessori" },
               new Category { OverCategory = c, Name = "Periferiche" },
               new Category { OverCategory = c, Name = "Foto e videocamera" }
            );
            ctx.SaveChanges();

            c = new Category() { Name = "MisteryBox" };
            ctx.Categories.Add( c );
            ctx.SaveChanges();

            c = new Category() { Name = "Bambini 0-3 anni" };
            ctx.Categories.Add( c );
            ctx.SaveChanges();

            ctx.Categories.AddRange(
               new Category { OverCategory = c, Name = "Abbigliamento" },
               new Category { OverCategory = c, Name = "Mobilio" },
               new Category { OverCategory = c, Name = "Accessori" }
            );
            ctx.SaveChanges();

            c = new Category() { Name = "Animali" };
            ctx.Categories.Add( c );
            ctx.SaveChanges();

            ctx.Categories.AddRange(
               new Category { OverCategory = c, Name = "Animali" },
               new Category { OverCategory = c, Name = "Accessori" }
            );
            ctx.SaveChanges();
         }
      }
   }
}