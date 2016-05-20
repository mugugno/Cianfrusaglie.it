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

         ctx.ImageUrls.Add( new ImageUrl() { Announce = announce2, Url = @"/images/kitten.jpg" } );

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
      }
   }
}