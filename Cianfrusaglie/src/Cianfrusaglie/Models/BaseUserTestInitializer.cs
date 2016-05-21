using System;
using System.Linq;

namespace Cianfrusaglie.Models {
   public static class BaseUserTestInitializer {
      public static void SeedBaseUserTest( this ApplicationDbContext ctx ) {
         if( ctx.Users.Any() )
            return;

         var user1 = new User() {
            Email = "mario.rossi@email.it",
            Name = "Mario",
            UserName = "MarioRed",
            BirthDate = new DateTime(1970,1,1),
            PhoneNumber = "123456789",
            Genre = 2,
            PasswordHash = "cipolla!",
            Latitude = 44.40678,
            Longitude = 8.93391
         };

         var user2 = new User() {
            Email = "giovanna.bianchi@email.it",
            Name = "Giovanna",
            UserName = "Giogio",
            BirthDate = new DateTime( 1980, 7, 7 ),
            PhoneNumber = "123456789",
            Genre = 1,
            PasswordHash = "cipolla!",
            Latitude = 44.40678,
            Longitude = 8.93391
         };

         ctx.Users.AddRange( user1, user2 );
         ctx.SaveChanges();

         //TODO popolamente gat e tabelle per l'algoritmo dei suggerimenti (chiamando le funzioni apposite)
         var announce1 = new Announce() {
            Author = user1,
            Title = "Telefonino Samsung con Android",
            Description = "Regalo telefonino Samsung con Android\ndisponibile lunedì pomeriggio 14-16",
            PublishDate = DateTime.Now.AddDays(-3),
            Latitude = 44.40678,
            Longitude = 8.93391,
            MeterRange = 7
         };

         ctx.Announces.Add( announce1 );
         ctx.SaveChanges();

         ctx.AnnounceCategories.Add( 
            new AnnounceCategory() { Announce = announce1, Category = ctx.Categories.Single( p => p.Name == "Cellulari" ) } );
         ctx.SaveChanges();

         ctx.AnnounceFormFieldsValues.AddRange(
            new AnnounceFormFieldsValues() { Announce = announce1, FormField =  ctx.FormFields.Single( p => p.Name == "Marca" ), Value = "galaxy s5570" },
            new AnnounceFormFieldsValues() { Announce = announce1, FormField = ctx.FormFields.Single( p => p.Name == "Stato" ), Value = "buono" }
         );
         ctx.SaveChanges();

         ctx.ImageUrls.Add( new ImageUrl() { Announce = announce1, Url = @"/images/cell1.jpg"} );

         ctx.SaveChanges();

         var announce2 = new Announce() {
            Author = user2,
            Title = "Gattino appena nato di pochi mesi",
            Description = "Regalo gattino appena nato perchè non lo posso tenere.",
            PublishDate = DateTime.Now.AddDays( -3 ),
            Latitude = 44.40678,
            Longitude = 8.93391,
            MeterRange = 7
         };

         ctx.Announces.Add( announce2 );
         ctx.SaveChanges();

         ctx.AnnounceCategories.Add(
            new AnnounceCategory() { Announce = announce2, Category = ctx.Categories.Single( p => p.Name == "Animali" && p.OverCategory != null ) } );
         ctx.SaveChanges();

         ctx.AnnounceFormFieldsValues.AddRange(
            new AnnounceFormFieldsValues() { Announce = announce2, FormField = ctx.FormFields.Single( p => p.Name == "Specie" ), Value = "gatto" },
            new AnnounceFormFieldsValues() { Announce = announce2, FormField = ctx.FormFields.Single( p => p.Name == "Taglia Animale" ), Value = "mini" }
         );
         ctx.SaveChanges();

         ctx.ImageUrls.Add( new ImageUrl() { Announce = announce2, Url = @"/images/i2.jpg" } );

         ctx.SaveChanges();

         var announce3 = new Announce() {
            Author = user1,
            Title = "Scarpe all star",
            Description = "Regalo scarpe all star come nuove, mai usate",
            PublishDate = DateTime.Now.AddDays( -3 ),
            Latitude = 44.40678,
            Longitude = 8.93391,
            MeterRange = 7
         };

         ctx.Announces.Add( announce3 );
         ctx.SaveChanges();

         ctx.AnnounceCategories.Add(
            new AnnounceCategory() { Announce = announce3, Category = ctx.Categories.Single( p => p.Name == "Scarpe" ) } );
         ctx.SaveChanges();

         ctx.AnnounceFormFieldsValues.AddRange(
            new AnnounceFormFieldsValues() { Announce = announce3, FormField = ctx.FormFields.Single( p => p.Name == "Marca" ), Value = "all star" },
            new AnnounceFormFieldsValues() { Announce = announce3, FormField = ctx.FormFields.Single( p => p.Name == "Colore" ), Value = "grigio" },
            new AnnounceFormFieldsValues() { Announce = announce3, FormField = ctx.FormFields.Single( p => p.Name == "Taglia Scarpa" ), Value = "43" }
         );
         ctx.SaveChanges();

         ctx.ImageUrls.Add( new ImageUrl() { Announce = announce3, Url = @"/images/all-star.jpg" } );

         ctx.SaveChanges();


         var announce4 = new Announce() {
            Author = user2,
            Title = "Tavolo da giardino con sedie",
            Description = "Regalo tavolo da giardino con 8 sedie. Venite a prenderlo se lo volete",
            PublishDate = DateTime.Now.AddDays( -3 ),
            Latitude = 44.40678,
            Longitude = 8.93391,
            MeterRange = 0
         };

         ctx.Announces.Add( announce4 );
         ctx.SaveChanges();

         ctx.AnnounceCategories.Add(
            new AnnounceCategory() { Announce = announce4, Category = ctx.Categories.Single( p => p.Name == "Mobili" ) } );
         ctx.SaveChanges();

         ctx.AnnounceFormFieldsValues.AddRange(
            new AnnounceFormFieldsValues() { Announce = announce4, FormField = ctx.FormFields.Single( p => p.Name == "Stato" ), Value = "buono" },
            new AnnounceFormFieldsValues() { Announce = announce4, FormField = ctx.FormFields.Single( p => p.Name == "Tipo Mobile" ), Value = "Tavolo" },
            new AnnounceFormFieldsValues() { Announce = announce4, FormField = ctx.FormFields.Single( p => p.Name == "Materiale" ), Value = "plastica" }
         );
         ctx.SaveChanges();

         ctx.ImageUrls.Add( new ImageUrl() { Announce = announce4, Url = @"/images/tavolo-da-giardino.jpg" } );

         ctx.SaveChanges();


         var announce6 = new Announce() {
            Author = user2,
            Title = "Registratore/Lettore VHS",
            Description = "Regalo lettore e registratore VHS Grunding per disuso.",
            PublishDate = DateTime.Now.AddDays( -7 ),
            Latitude = 44.40678,
            Longitude = 8.93391,
            MeterRange = 7
         };

         ctx.Announces.Add( announce6 );
         ctx.SaveChanges();

         ctx.AnnounceCategories.Add(
            new AnnounceCategory() { Announce = announce6, Category = ctx.Categories.Single( p => p.Name == "Periferiche" ) } );
         ctx.SaveChanges();

         ctx.AnnounceFormFieldsValues.AddRange(
            new AnnounceFormFieldsValues() { Announce = announce4, FormField = ctx.FormFields.Single( p => p.Name == "Marca" ), Value = "Grundig" },
            new AnnounceFormFieldsValues() { Announce = announce4, FormField = ctx.FormFields.Single( p => p.Name == "Tipo" ), Value = "VHS" },
            new AnnounceFormFieldsValues() { Announce = announce4, FormField = ctx.FormFields.Single( p => p.Name == "Compatibile Con" ), Value = "tv scart" }
         );
         ctx.SaveChanges();

         ctx.ImageUrls.Add( new ImageUrl() { Announce = announce6, Url = @"/images/lettorevhs.jpg" } );

         ctx.SaveChanges();


         var announce5 = new Announce() {
            Author = user1,
            Title = "VHS assortite",
            Description = "Regalo VHS. Non sò cosa contentano\nma io non le guardo più, ho tolto il video registratore.\nse nessuno le vuole le butto.",
            PublishDate = DateTime.Now.AddDays( -6 ),
            Latitude = 44.40678,
            Longitude = 8.93391,
            MeterRange = 6
         };

         ctx.Announces.Add( announce5 );
         ctx.SaveChanges();

         ctx.AnnounceCategories.Add(
            new AnnounceCategory() { Announce = announce5, Category = ctx.Categories.Single( p => p.Name == "MisteryBox" && p.OverCategory != null ) } );
         ctx.SaveChanges();

         ctx.ImageUrls.Add( new ImageUrl() { Announce = announce5, Url = @"/images/vhs.jpg" } );

         ctx.SaveChanges();


         var announce7 = new Announce() {
            Author = user2,
            Title = "LP Trololo",
            Description = "Regalo LP Trololo.\nnon mi piace più\noramai è abusato.",
            PublishDate = DateTime.Now.AddDays( -6 ),
            Latitude = 44.40678,
            Longitude = 8.93391,
            MeterRange = 30
         };

         ctx.Announces.Add( announce7 );
         ctx.SaveChanges();

         ctx.AnnounceCategories.Add(
            new AnnounceCategory() { Announce = announce7, Category = ctx.Categories.Single( p => p.Name == "Musica" ) } );
         ctx.SaveChanges();

         ctx.AnnounceFormFieldsValues.AddRange(
            new AnnounceFormFieldsValues() { Announce = announce4, FormField = ctx.FormFields.Single( p => p.Name == "Titolo" ), Value = "Trololo" },
            new AnnounceFormFieldsValues() { Announce = announce4, FormField = ctx.FormFields.Single( p => p.Name == "Autore" ), Value = "Edward Khil" },
            new AnnounceFormFieldsValues() { Announce = announce4, FormField = ctx.FormFields.Single( p => p.Name == "Supporto Musica" ), Value = "Vinile" }
         );
         ctx.SaveChanges();

         ctx.ImageUrls.Add( new ImageUrl() { Announce = announce7, Url = @"/images/tr.jpg" } );

         ctx.SaveChanges();


         var announce8 = new Announce() {
            Author = user1,
            Title = "Xbox 360 usata con Hardisk e 2 controller",
            Description = "Regalo Xbox 360 arcade usata con 2 controller e hardisk da 60gb perchè ora ho una PS4!!!",
            PublishDate = DateTime.Now.AddMonths( -7 ),
            Latitude = 44.3228049,
            Longitude = 8.466561899999988,
            MeterRange = 25
         };

         ctx.Announces.Add( announce8 );
         ctx.SaveChanges();

         ctx.AnnounceCategories.Add(
            new AnnounceCategory() { Announce = announce8, Category = ctx.Categories.Single( p => p.Name == "Console" ) } );
         ctx.SaveChanges();

         ctx.AnnounceFormFieldsValues.AddRange(
            new AnnounceFormFieldsValues() { Announce = announce8, FormField = ctx.FormFields.Single( p => p.Name == "Stato" ), Value = "scarso" },
            new AnnounceFormFieldsValues() { Announce = announce8, FormField = ctx.FormFields.Single( p => p.Name == "Modello" ), Value = "Xbox 360 Arcade" },
            new AnnounceFormFieldsValues() { Announce = announce8, FormField = ctx.FormFields.Single( p => p.Name == "Numero Joypad" ), Value = "2" }
         );
         ctx.SaveChanges();

         ctx.ImageUrls.Add( new ImageUrl() { Announce = announce8, Url = @"/images/xbox360.jpg" } );

         ctx.SaveChanges();


         var announce9 = new Announce() {
            Author = user1,
            Title = "Aspirapolvere Kirby con acessori",
            Description = "Regalo aspirapolvere kirby con acessori perchè troppo complesso e rumoroso",
            PublishDate = DateTime.Now.AddMonths( -1 ),
            Latitude = 44.3228049,
            Longitude = 8.466561899999988,
            MeterRange = 0
         };

         ctx.Announces.Add( announce9 );
         ctx.SaveChanges();

         ctx.AnnounceCategories.Add(
            new AnnounceCategory() { Announce = announce9, Category = ctx.Categories.Single( p => p.Name == "Elettrodomestici" ) } );
         ctx.SaveChanges();

         ctx.AnnounceFormFieldsValues.AddRange(
            new AnnounceFormFieldsValues() { Announce = announce9, FormField = ctx.FormFields.Single( p => p.Name == "Stato" ), Value = "Nuovo" },
            new AnnounceFormFieldsValues() { Announce = announce9, FormField = ctx.FormFields.Single( p => p.Name == "Marca" ), Value = "Kirby" },
            new AnnounceFormFieldsValues() { Announce = announce9, FormField = ctx.FormFields.Single( p => p.Name == "Tipo Elettrodomestico" ), Value = "Aspirapolvere" }
         );
         ctx.SaveChanges();

         ctx.ImageUrls.Add( new ImageUrl() { Announce = announce9, Url = @"/images/kirby.jpg" } );

         ctx.SaveChanges();


         var announce10 = new Announce() {
            Author = user2,
            Title = "Triciclo usato",
            Description = "Regalo triciclo usato di mio figlio\nora è grande e non lo usa più",
            PublishDate = DateTime.Now.AddDays( -3 ),
            Latitude = 44.40678,
            Longitude = 8.93391,
            MeterRange = 15
         };

         ctx.Announces.Add( announce10 );
         ctx.SaveChanges();

         ctx.AnnounceCategories.Add(
            new AnnounceCategory() { Announce = announce10, Category = ctx.Categories.Single( p => p.Name == "Giocattoli" ) } );
         ctx.SaveChanges();

         ctx.AnnounceFormFieldsValues.Add(
            new AnnounceFormFieldsValues() { Announce = announce10, FormField = ctx.FormFields.Single( p => p.Name == "Tipo Giocattolo" ), Value = "Veicolo" }
         );
         ctx.SaveChanges();

         ctx.ImageUrls.Add( new ImageUrl() { Announce = announce10, Url = @"/images/triciclo.jpg" } );

         ctx.SaveChanges();


         var announce11 = new Announce() {
            Author = user1,
            Title = "Apecar piaggio",
            Description = "Regalo apecar piaggio 3 ruote",
            PublishDate = DateTime.Now.AddDays( -3 ),
            Latitude = 44.40678,
            Longitude = 8.93391,
            MeterRange = 3
         };

         ctx.Announces.Add( announce11 );
         ctx.SaveChanges();

         ctx.AnnounceCategories.Add(
            new AnnounceCategory() { Announce = announce11, Category = ctx.Categories.Single( p => p.Name == "Auto e Moto" && p.OverCategory != null ) } );
         ctx.SaveChanges();

         ctx.AnnounceFormFieldsValues.AddRange(
            new AnnounceFormFieldsValues() { Announce = announce11, FormField = ctx.FormFields.Single( p => p.Name == "Anno" ), Value = "1977" },
            new AnnounceFormFieldsValues() { Announce = announce11, FormField = ctx.FormFields.Single( p => p.Name == "Marca" ), Value = "piaggio" },
            new AnnounceFormFieldsValues() { Announce = announce11, FormField = ctx.FormFields.Single( p => p.Name == "Cilindrata" ), Value = "218cc" },
            new AnnounceFormFieldsValues() { Announce = announce11, FormField = ctx.FormFields.Single( p => p.Name == "Cavalli" ), Value = "10" }
         );
         ctx.SaveChanges();

         ctx.ImageUrls.Add( new ImageUrl() { Announce = announce11, Url = @"/images/ape.jpg" } );

         ctx.SaveChanges();


         var announce12 = new Announce() {
            Author = user1,
            Title = "Modellino Olandese Volante",
            Description = "Non ho più spazio in casa... qualcuno lo vuole?",
            PublishDate = DateTime.Now.AddDays( -5 ),
            Latitude = 44.40678,
            Longitude = 8.93391,
            MeterRange = 11
         };

         ctx.Announces.Add( announce12 );
         ctx.SaveChanges();

         ctx.AnnounceCategories.Add(
            new AnnounceCategory() { Announce = announce12, Category = ctx.Categories.Single( p => p.Name == "Modellismo e collezionismo" ) } );
         ctx.SaveChanges();

         ctx.ImageUrls.Add( new ImageUrl() { Announce = announce12, Url = @"/images/olandese.jpg" } );

         ctx.SaveChanges();


         var announce13 = new Announce() {
            Author = user1,
            Title = "Trenino Lego",
            Description = "Regalo treno lego con qualche pezzo mancante",
            PublishDate = DateTime.Now.AddDays( -1 ),
            Latitude = 44.40678,
            Longitude = 8.93391,
            MeterRange = 11
         };

         ctx.Announces.Add( announce13 );
         ctx.SaveChanges();

         ctx.AnnounceCategories.AddRange(
            new AnnounceCategory() { Announce = announce13, Category = ctx.Categories.Single( p => p.Name == "Giocattoli" ) },
            new AnnounceCategory() { Announce = announce13, Category = ctx.Categories.Single( p => p.Name == "Modellismo e collezionismo" ) }
         );
         ctx.SaveChanges();

         ctx.AnnounceFormFieldsValues.AddRange(
            new AnnounceFormFieldsValues() { Announce = announce13, FormField = ctx.FormFields.Single( p => p.Name == "Marca" ), Value = "Lego" },
            new AnnounceFormFieldsValues() { Announce = announce13, FormField = ctx.FormFields.Single( p => p.Name == "Tipo Giocattolo" ), Value = "Piste" },
            new AnnounceFormFieldsValues() { Announce = announce13, FormField = ctx.FormFields.Single( p => p.Name == "Tipo Modellino/Collezione" ), Value = "treno" }
         );
         ctx.SaveChanges();

         ctx.ImageUrls.Add( new ImageUrl() { Announce = announce13, Url = @"/images/trenolego.jpg" } );

         ctx.SaveChanges();


         var announce14 = new Announce() {
            Author = user1,
            Title = "Racchetta da tennis shark",
            Description = "Regalo racchetta da tennis\nperchè mi è venuto il gomito del tennista",
            PublishDate = DateTime.Now.AddDays( -9 ),
            Latitude = 44.40678,
            Longitude = 8.93391,
            MeterRange = 1
         };

         ctx.Announces.Add( announce14 );
         ctx.SaveChanges();

         ctx.AnnounceCategories.Add(
            new AnnounceCategory() { Announce = announce14, Category = ctx.Categories.Single( p => p.Name == "Sport" ) }
         );
         ctx.SaveChanges();

         ctx.AnnounceFormFieldsValues.AddRange(
            new AnnounceFormFieldsValues() { Announce = announce14, FormField = ctx.FormFields.Single( p => p.Name == "Marca" ), Value = "Shark" },
            new AnnounceFormFieldsValues() { Announce = announce14, FormField = ctx.FormFields.Single( p => p.Name == "Sport" ), Value = "Tennis" }
         );
         ctx.SaveChanges();

         ctx.ImageUrls.Add( new ImageUrl() { Announce = announce14, Url = @"/images/tennis.jpg" } );

         ctx.SaveChanges();
      }
   }
}