using System.Linq;

namespace Cianfrusaglie.Models {
   public static class ApplicationDbInitializer {
      public static void EnsureSeedData( this ApplicationDbContext ctx ) {
         if( !ctx.Categories.Any() ) {
            var c = new Category() { Name = "Libri Musica Film" };
            ctx.Categories.Add( c );
            ctx.SaveChanges();

            var lmfLibri = new Category() { OverCategory = c, Name = "Libri" };
            ctx.Categories.Add( lmfLibri );
            ctx.SaveChanges();

            var lmfMusica = new Category { OverCategory = c, Name = "Musica" };
            ctx.Categories.Add( lmfMusica );
            ctx.SaveChanges();

            var lmfFilm = new Category {OverCategory = c, Name = "Film"};
            ctx.Categories.Add( lmfFilm );
            ctx.SaveChanges();


            c = new Category() { Name = "Videogiochi e Console" };
            ctx.Categories.Add( c );
            ctx.SaveChanges();

            var vcVideogiochi = new Category {OverCategory = c, Name = "Videogiochi"};
            ctx.Categories.Add( vcVideogiochi );
            ctx.SaveChanges();

            var vcConsole = new Category {OverCategory = c, Name = "Console"};
            ctx.Categories.Add( vcConsole );
            ctx.SaveChanges();


            c = new Category() { Name = "Arredamento e accessori per la casa" };
            ctx.Categories.Add( c );
            ctx.SaveChanges();

            var aacMobili = new Category {OverCategory = c, Name = "Mobili"};
            ctx.Categories.Add( aacMobili );
            ctx.SaveChanges();

            var aacElettrodomestici = new Category {OverCategory = c, Name = "Elettrodomestici"};
            ctx.Categories.Add( aacElettrodomestici );
            ctx.SaveChanges();

            var aacIlluminazione = new Category {OverCategory = c, Name = "Illuminazione"};
            ctx.Categories.Add( aacIlluminazione );
            ctx.SaveChanges();

            var aacCucina = new Category {OverCategory = c, Name = "Cucina"};
            ctx.Categories.Add( aacCucina );
            ctx.SaveChanges();

            var aacArredamento = new Category {OverCategory = c, Name = "Arredamento"};
            ctx.Categories.Add( aacArredamento );
            ctx.SaveChanges();

            var aacAcplc = new Category {OverCategory = c, Name = "Accessori per la casa"};
            ctx.Categories.Add( aacAcplc );
            ctx.SaveChanges();


            c = new Category() { Name = "Oggettistica" };
            ctx.Categories.Add( c );
            ctx.SaveChanges();


            var oGiocattoli = new Category {OverCategory = c, Name = "Giocattoli"};
            ctx.Categories.Add( oGiocattoli );
            ctx.SaveChanges();

            var oModCol = new Category {OverCategory = c, Name = "Modellismo e collezionismo"};
            ctx.Categories.Add( oModCol );
            ctx.SaveChanges();

            var oCianfrusaglie = new Category {OverCategory = c, Name = "Cianfrusaglie"};
            ctx.Categories.Add( oCianfrusaglie );
            ctx.SaveChanges();



            c = new Category() { Name = "Sport Tempo libero Fai da te" };
            ctx.Categories.Add( c );
            ctx.SaveChanges();


            var stlftSport = new Category {OverCategory = c, Name = "Sport"};
            ctx.Categories.Add( stlftSport );
            ctx.SaveChanges();

            var stlfTempoLibero = new Category {OverCategory = c, Name = "Tempo libero"};
            ctx.Categories.Add( stlfTempoLibero );
            ctx.SaveChanges();

            var stlfFaiDaTe = new Category {OverCategory = c, Name = "Fai da te"};
            ctx.Categories.Add( stlfFaiDaTe );
            ctx.SaveChanges();


            c = new Category() { Name = "Abbigliamento" };
            ctx.Categories.Add( c );
            ctx.SaveChanges();


            var aAbbigliamento = new Category {OverCategory = c, Name = "Abbigliamento"};
            ctx.Categories.Add( aAbbigliamento );
            ctx.SaveChanges();

            var aAccessori = new Category {OverCategory = c, Name = "Accessori"};
            ctx.Categories.Add( aAccessori );
            ctx.SaveChanges();

            var aScarpe = new Category() { OverCategory = c, Name = "Scarpe" };
            ctx.Categories.Add( aScarpe );
            ctx.SaveChanges();
            

            c = new Category() { Name = "Auto e Moto" };
            ctx.Categories.Add( c );
            ctx.SaveChanges();

            var amAutoMoto = new Category {OverCategory = c, Name = "Auto e Moto"};
            ctx.Categories.Add( amAutoMoto );
            ctx.SaveChanges();

            var amAccessori = new Category {OverCategory = c, Name = "Accessori"};
            ctx.Categories.Add( amAccessori );
            ctx.SaveChanges();



            c = new Category() { Name = "Elettronica" };
            ctx.Categories.Add( c );
            ctx.SaveChanges();

            var eComputer = new Category {OverCategory = c, Name = "Computer"};
            ctx.Categories.Add( eComputer );
            ctx.SaveChanges();

            var eCellulari = new Category {OverCategory = c, Name = "Cellulari"};
            ctx.Categories.Add( eCellulari );
            ctx.SaveChanges();

            var eTvSchermi = new Category {OverCategory = c, Name = "Tv e schermi"};
            ctx.Categories.Add( eTvSchermi );
            ctx.SaveChanges();

            var eAudio = new Category {OverCategory = c, Name = "Audio"};
            ctx.Categories.Add( eAudio );
            ctx.SaveChanges();

            var eAccessori = new Category {OverCategory = c, Name = "Accessori"};
            ctx.Categories.Add( eAccessori );
            ctx.SaveChanges();

            var ePeriferiche = new Category {OverCategory = c, Name = "Periferiche"};
            ctx.Categories.Add( ePeriferiche );
            ctx.SaveChanges();

            var eFV = new Category {OverCategory = c, Name = "Foto e videocamera"};
            ctx.Categories.Add( eFV );
            ctx.SaveChanges();


            c = new Category() { Name = "MisteryBox" };
            ctx.Categories.Add( c );
            ctx.SaveChanges();

            c = new Category() { Name = "Bambini 0-3 anni" };
            ctx.Categories.Add( c );
            ctx.SaveChanges();


            var bAbbigliamento = new Category {OverCategory = c, Name = "Abbigliamento"};
            ctx.Categories.Add( bAbbigliamento );
            ctx.SaveChanges();

            var bMobilio = new Category {OverCategory = c, Name = "Mobilio"};
            ctx.Categories.Add( bMobilio );
            ctx.SaveChanges();

            var bAccessori = new Category {OverCategory = c, Name = "Accessori"};
            ctx.Categories.Add( bAccessori );
            ctx.SaveChanges();



            c = new Category() { Name = "Animali" };
            ctx.Categories.Add( c );
            ctx.SaveChanges();

            var anAnimali = new Category {OverCategory = c, Name = "Animali"};
            ctx.Categories.Add( anAnimali );
            ctx.SaveChanges();

            var anAccessori = new Category {OverCategory = c, Name = "Accessori"};
            ctx.Categories.Add( anAccessori );
            ctx.SaveChanges();

         }
      }
   }
}