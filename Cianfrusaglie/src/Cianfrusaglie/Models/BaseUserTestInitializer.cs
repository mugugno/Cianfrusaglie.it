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
            PasswordHash = "cipolla!"
         };

         var user2 = new User() {};

         ctx.Users.AddRange( user1, user2 );
         ctx.SaveChanges();

         var announce1 = new Announce() {
            Author = user1,
            Title = "Telefonino Samsung con Android",
            Description = "Regalo telefonino Samsung con Android\ndisponibile lunedì pomeriggio 14-16",
            PublishDate = DateTime.Now.AddDays(-3)
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
      }
   }
}