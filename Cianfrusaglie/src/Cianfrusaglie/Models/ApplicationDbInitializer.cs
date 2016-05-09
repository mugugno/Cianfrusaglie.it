using System.Linq;

namespace Cianfrusaglie.Models {
   public static class ApplicationDbInitializer {
      public static void EnsureSeedData( this ApplicationDbContext ctx ) {
         if( !ctx.Categories.Any() ) {
            var c = new Category() {Name = "Libri Musica Film"};
            ctx.Categories.Add( c );
            ctx.SaveChanges();

            var lmfLibri = new Category() {OverCategory = c, Name = "Libri"};
            ctx.Categories.Add( lmfLibri );
            ctx.SaveChanges();

            var lmfMusica = new Category {OverCategory = c, Name = "Musica"};
            ctx.Categories.Add( lmfMusica );
            ctx.SaveChanges();

            var lmfFilm = new Category {OverCategory = c, Name = "Film"};
            ctx.Categories.Add( lmfFilm );
            ctx.SaveChanges();


            c = new Category() {Name = "Videogiochi e Console"};
            ctx.Categories.Add( c );
            ctx.SaveChanges();

            var vcVideogiochi = new Category {OverCategory = c, Name = "Videogiochi"};
            ctx.Categories.Add( vcVideogiochi );
            ctx.SaveChanges();

            var vcConsole = new Category {OverCategory = c, Name = "Console"};
            ctx.Categories.Add( vcConsole );
            ctx.SaveChanges();


            c = new Category() {Name = "Arredamento e accessori per la casa"};
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


            c = new Category() {Name = "Oggettistica"};
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



            c = new Category() {Name = "Sport Tempo libero Fai da te"};
            ctx.Categories.Add( c );
            ctx.SaveChanges();


            var stlfSport = new Category {OverCategory = c, Name = "Sport"};
            ctx.Categories.Add( stlfSport );
            ctx.SaveChanges();

            var stlfTempoLibero = new Category {OverCategory = c, Name = "Tempo libero"};
            ctx.Categories.Add( stlfTempoLibero );
            ctx.SaveChanges();

            var stlfFaiDaTe = new Category {OverCategory = c, Name = "Fai da te"};
            ctx.Categories.Add( stlfFaiDaTe );
            ctx.SaveChanges();


            c = new Category() {Name = "Abbigliamento"};
            ctx.Categories.Add( c );
            ctx.SaveChanges();


            var aAbbigliamento = new Category {OverCategory = c, Name = "Abbigliamento"};
            ctx.Categories.Add( aAbbigliamento );
            ctx.SaveChanges();

            var aAccessori = new Category {OverCategory = c, Name = "Accessori"};
            ctx.Categories.Add( aAccessori );
            ctx.SaveChanges();

            var aScarpe = new Category() {OverCategory = c, Name = "Scarpe"};
            ctx.Categories.Add( aScarpe );
            ctx.SaveChanges();


            c = new Category() {Name = "Auto e Moto"};
            ctx.Categories.Add( c );
            ctx.SaveChanges();

            var amAutoMoto = new Category {OverCategory = c, Name = "Auto e Moto"};
            ctx.Categories.Add( amAutoMoto );
            ctx.SaveChanges();

            var amAccessori = new Category {OverCategory = c, Name = "Accessori"};
            ctx.Categories.Add( amAccessori );
            ctx.SaveChanges();



            c = new Category() {Name = "Elettronica"};
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


            c = new Category() {Name = "MisteryBox"};
            ctx.Categories.Add( c );
            ctx.SaveChanges();

            c = new Category() {Name = "Bambini 0-3 anni"};
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



            c = new Category() {Name = "Animali"};
            ctx.Categories.Add( c );
            ctx.SaveChanges();

            var anAnimali = new Category {OverCategory = c, Name = "Animali"};
            ctx.Categories.Add( anAnimali );
            ctx.SaveChanges();

            var anAccessori = new Category {OverCategory = c, Name = "Accessori"};
            ctx.Categories.Add( anAccessori );
            ctx.SaveChanges();

            //FormField
          //  var titolo = new FormField() {Name = "Titolo", Type = "s"};
          //  ctx.FormFields.Add( titolo );
          //  ctx.SaveChanges();

          //  ctx.CategoryFormFields.Add( new CategoryFormField() { CategoryId = lmfFilm.Id, FormFieldId = titolo.Id } );
          //  ctx.SaveChanges();

          //  ctx.CategoryFormFields.Add( new CategoryFormField() { CategoryId = lmfMusica.Id, FormFieldId = titolo.Id } );
          //  ctx.SaveChanges();

          //  ctx.CategoryFormFields.Add( new CategoryFormField() { CategoryId = lmfLibri.Id, FormFieldId = titolo.Id } );
          //  ctx.SaveChanges();

          //  ctx.CategoryFormFields.Add( new CategoryFormField() { CategoryId = vcVideogiochi.Id, FormFieldId = titolo.Id } );
          //  ctx.SaveChanges();


          //  var genereLibri = new FormField() {Name = "genere", Type = "s"};
          //  ctx.FormFields.Add( genereLibri );
          //  ctx.SaveChanges();

          //  ctx.FieldDefaultValues.AddRange(
          //     new FieldDefaultValue() { Value = "arte, cinema, musica e spettacolo", FormField = genereLibri },
          //     new FieldDefaultValue() { Value = "attualità e reportage", FormField = genereLibri },
          //     new FieldDefaultValue() { Value = "benessere", FormField = genereLibri },
          //     new FieldDefaultValue() { Value = "biografia", FormField = genereLibri },
          //     new FieldDefaultValue() { Value = "classici", FormField = genereLibri },
          //     new FieldDefaultValue() { Value = "cucina", FormField = genereLibri },
          //     new FieldDefaultValue() { Value = "fantasy", FormField = genereLibri },
          //     new FieldDefaultValue() { Value = "gialli e thriller", FormField = genereLibri },
          //     new FieldDefaultValue() { Value = "grafic novel e fumetti", FormField = genereLibri },
          //     new FieldDefaultValue() { Value = "hobby e tempo libero", FormField = genereLibri },
          //     new FieldDefaultValue() { Value = "moda", FormField = genereLibri },
          //     new FieldDefaultValue() { Value = "narrativa", FormField = genereLibri },
          //     new FieldDefaultValue() { Value = "poesia", FormField = genereLibri },
          //     new FieldDefaultValue() { Value = "politica", FormField = genereLibri },
          //     new FieldDefaultValue() { Value = "psicologia e sociologia", FormField = genereLibri },
          //     new FieldDefaultValue() { Value = "religione", FormField = genereLibri },
          //     new FieldDefaultValue() { Value = "romanzi", FormField = genereLibri },
          //     new FieldDefaultValue() { Value = "scienze", FormField = genereLibri },
          //     new FieldDefaultValue() { Value = "sport", FormField = genereLibri },
          //     new FieldDefaultValue() { Value = "storia", FormField = genereLibri },
          //     new FieldDefaultValue() { Value = "viaggi", FormField = genereLibri }
          //  );
          //  ctx.SaveChanges();


          //  var autore = new FormField() { Name = "Autore", Type = "s" };
          //  ctx.FormFields.Add( autore );
          //  ctx.SaveChanges();

          //  ctx.CategoryFormFields.Add( new CategoryFormField() { CategoryId = lmfMusica.Id, FormFieldId = autore.Id } );
          //  ctx.SaveChanges();

          //  ctx.CategoryFormFields.Add( new CategoryFormField() { CategoryId = lmfLibri.Id, FormFieldId = autore.Id } );
          //  ctx.SaveChanges();


          //  var editore = new FormField() { Name = "Editore", Type = "s" };
          //  ctx.FormFields.Add( editore );
          //  ctx.SaveChanges();

          //  ctx.CategoryFormFields.Add( new CategoryFormField() { CategoryId = lmfLibri.Id, FormFieldId = editore.Id } );
          //  ctx.SaveChanges();


          //  var numero = new FormField() { Name = "Numero", Type = "i" };
          //  ctx.FormFields.Add( numero );
          //  ctx.SaveChanges();

          //  ctx.CategoryFormFields.Add( new CategoryFormField() { CategoryId = lmfLibri.Id, FormFieldId = numero.Id } );
          //  ctx.SaveChanges();


          //  var stato = new FormField() { Name = "Stato", Type = "s" };
          //  ctx.FormFields.Add( stato );
          //  ctx.SaveChanges();

          //  ctx.CategoryFormFields.Add( new CategoryFormField() { CategoryId = lmfLibri.Id, FormFieldId = stato.Id } );
          //  ctx.SaveChanges();

          //  ctx.CategoryFormFields.Add( new CategoryFormField() { CategoryId = lmfMusica.Id, FormFieldId = stato.Id } );
          //  ctx.SaveChanges();

          //  ctx.CategoryFormFields.Add( new CategoryFormField() { CategoryId = lmfFilm.Id, FormFieldId = stato.Id } );
          //  ctx.SaveChanges();

          //  ctx.CategoryFormFields.Add( new CategoryFormField() { CategoryId = vcConsole.Id, FormFieldId = stato.Id } );
          //  ctx.SaveChanges();

          //  ctx.CategoryFormFields.Add( new CategoryFormField() { CategoryId = aacMobili.Id, FormFieldId = stato.Id } );
          //  ctx.SaveChanges();

          //  ctx.CategoryFormFields.Add( new CategoryFormField() { CategoryId = aacElettrodomestici.Id, FormFieldId = stato.Id } );
          //  ctx.SaveChanges();

          //  ctx.CategoryFormFields.Add( new CategoryFormField() { CategoryId = aacCucina.Id, FormFieldId = stato.Id } );
          //  ctx.SaveChanges();

          //  ctx.CategoryFormFields.Add( new CategoryFormField() { CategoryId = aacArredamento.Id, FormFieldId = stato.Id } );
          //  ctx.SaveChanges();

          //  ctx.CategoryFormFields.Add( new CategoryFormField() { CategoryId = aacAcplc.Id, FormFieldId = stato.Id } );
          //  ctx.SaveChanges();

          //  ctx.CategoryFormFields.Add( new CategoryFormField() { CategoryId = oGiocattoli.Id, FormFieldId = stato.Id } );
          //  ctx.SaveChanges();

          //  ctx.CategoryFormFields.Add( new CategoryFormField() { CategoryId = oModCol.Id, FormFieldId = stato.Id } );
          //  ctx.SaveChanges();

          //  ctx.CategoryFormFields.Add( new CategoryFormField() { CategoryId = oCianfrusaglie.Id, FormFieldId = stato.Id } );
          //  ctx.SaveChanges();

          //  ctx.CategoryFormFields.Add( new CategoryFormField() { CategoryId = stlfSport.Id, FormFieldId = stato.Id } );
          //  ctx.SaveChanges();

          //  ctx.CategoryFormFields.Add( new CategoryFormField() { CategoryId = stlfTempoLibero.Id, FormFieldId = stato.Id } );
          //  ctx.SaveChanges();

          //  ctx.CategoryFormFields.Add( new CategoryFormField() { CategoryId = stlfFaiDaTe.Id, FormFieldId = stato.Id } );
          //  ctx.SaveChanges();

          //  ctx.CategoryFormFields.Add( new CategoryFormField() { CategoryId = eComputer.Id, FormFieldId = stato.Id } );
          //  ctx.SaveChanges();

          //  ctx.CategoryFormFields.Add( new CategoryFormField() { CategoryId = eCellulari.Id, FormFieldId = stato.Id } );
          //  ctx.SaveChanges();

          //  ctx.CategoryFormFields.Add( new CategoryFormField() { CategoryId = eFV.Id, FormFieldId = stato.Id } );
          //  ctx.SaveChanges();

          //  ctx.CategoryFormFields.Add( new CategoryFormField() { CategoryId = eTvSchermi.Id, FormFieldId = stato.Id } );
          //  ctx.SaveChanges();

          //  ctx.CategoryFormFields.Add( new CategoryFormField() { CategoryId = bAbbigliamento.Id, FormFieldId = stato.Id } );
          //  ctx.SaveChanges();

          //  ctx.CategoryFormFields.Add( new CategoryFormField() { CategoryId = bAccessori.Id, FormFieldId = stato.Id } );
          //  ctx.SaveChanges();

          //  ctx.CategoryFormFields.Add( new CategoryFormField() { CategoryId = bMobilio.Id, FormFieldId = stato.Id } );
          //  ctx.SaveChanges();

          //  ctx.CategoryFormFields.Add( new CategoryFormField() { CategoryId = anAccessori.Id, FormFieldId = stato.Id } );
          //  ctx.SaveChanges();

          //  ctx.FieldDefaultValues.AddRange(
          //     new FieldDefaultValue() { Value = "Nuovo", FormField = stato },
          //     new FieldDefaultValue() { Value = "Ottimo", FormField = stato },
          //     new FieldDefaultValue() { Value = "Buono", FormField = stato },
          //     new FieldDefaultValue() { Value = "Scarso", FormField = stato }
          //  );
          //  ctx.SaveChanges();

          //  var lingua = new FormField() { Name = "Lingua", Type = "s" };
          //  ctx.FormFields.Add( lingua );
          //  ctx.SaveChanges();

          //  ctx.CategoryFormFields.Add( new CategoryFormField() { CategoryId = lmfLibri.Id, FormFieldId = lingua.Id } );
          //  ctx.SaveChanges();
          //  ctx.CategoryFormFields.Add( new CategoryFormField() { CategoryId = lmfFilm.Id, FormFieldId = lingua.Id } );
          //  ctx.SaveChanges();

          //  ctx.FieldDefaultValues.AddRange(
          //     new FieldDefaultValue() { Value = "Italiano", FormField = lingua },
          //     new FieldDefaultValue() { Value = "Francese", FormField = lingua },
          //     new FieldDefaultValue() { Value = "Inglese", FormField = lingua },
          //     new FieldDefaultValue() { Value = "Tedesco", FormField = lingua },
          //     new FieldDefaultValue() { Value = "Spagnolo", FormField = lingua },
          //     new FieldDefaultValue() { Value = "Altro", FormField = lingua }
          //  );
          //  ctx.SaveChanges();

          //  var anno= new FormField() { Name = "Anno", Type = "i" };
          //  ctx.FormFields.Add( anno );
          //  ctx.SaveChanges();

          //  ctx.CategoryFormFields.Add( new CategoryFormField() { CategoryId = lmfLibri.Id, FormFieldId = anno.Id } );
          //  ctx.SaveChanges();

          //  ctx.CategoryFormFields.Add( new CategoryFormField() { CategoryId = lmfMusica.Id, FormFieldId = anno.Id } );
          //  ctx.SaveChanges();

          //  ctx.CategoryFormFields.Add( new CategoryFormField() { CategoryId = lmfFilm.Id, FormFieldId = anno.Id } );
          //  ctx.SaveChanges();

          //  ctx.CategoryFormFields.Add( new CategoryFormField() { CategoryId = amAutoMoto.Id, FormFieldId = anno.Id } );
          //  ctx.SaveChanges();


          //  var copertina = new FormField() { Name = "Copertina", Type = "s" };
          //  ctx.FormFields.Add( copertina );
          //  ctx.SaveChanges();

          //  ctx.CategoryFormFields.Add( new CategoryFormField() { CategoryId = lmfLibri.Id, FormFieldId = copertina.Id } );
          //  ctx.SaveChanges();

          //  ctx.FieldDefaultValues.AddRange(
          //     new FieldDefaultValue() { Value = "Rigida", FormField = copertina },
          //     new FieldDefaultValue() { Value = "Flessibile", FormField = copertina }
          //  );
          //  ctx.SaveChanges();


          //  var genereMusica = new FormField() { Name = "Genere Musica", Type = "s" };
          //  ctx.FormFields.Add( copertina );
          //  ctx.SaveChanges();

          //  ctx.CategoryFormFields.Add( new CategoryFormField() { CategoryId = lmfMusica.Id, FormFieldId = genereMusica.Id } );
          //  ctx.SaveChanges();


          //  ctx.FieldDefaultValues.AddRange(
          //    new FieldDefaultValue() { Value = "Rock", FormField = genereMusica },
          //    new FieldDefaultValue() { Value = "Elettronica", FormField = genereMusica },
          //    new FieldDefaultValue() { Value = "Pop", FormField = genereMusica },
          //    new FieldDefaultValue() { Value = "Folk", FormField = genereMusica },
          //    new FieldDefaultValue() { Value = "Funk/Soul", FormField = genereMusica },
          //    new FieldDefaultValue() { Value = "Jazz", FormField = genereMusica },
          //    new FieldDefaultValue() { Value = "Hip Hop", FormField = genereMusica },
          //    new FieldDefaultValue() { Value = "Classica", FormField = genereMusica },
          //    new FieldDefaultValue() { Value = "Raggae", FormField = genereMusica },
          //    new FieldDefaultValue() { Value = "Latin", FormField = genereMusica },
          //    new FieldDefaultValue() { Value = "Blues", FormField = genereMusica },
          //    new FieldDefaultValue() { Value = "Rap", FormField = genereMusica },
          //    new FieldDefaultValue() { Value = "Bambini", FormField = genereMusica },
          //    new FieldDefaultValue() { Value = "Altro", FormField = genereMusica }
          // );
          //  ctx.SaveChanges();


          //  var casaDiscografica = new FormField() { Name = "Casa Discografica", Type = "s" };
          //  ctx.FormFields.Add( casaDiscografica );
          //  ctx.SaveChanges();

          //  ctx.CategoryFormFields.Add( new CategoryFormField() { CategoryId = lmfMusica.Id, FormFieldId = casaDiscografica.Id } );
          //  ctx.SaveChanges();

          //  var supportoMusica = new FormField() { Name = "Supporto Musica", Type = "s" };
          //  ctx.FormFields.Add( supportoMusica );
          //  ctx.SaveChanges();

          //  ctx.CategoryFormFields.Add( new CategoryFormField() { CategoryId = lmfMusica.Id, FormFieldId = supportoMusica.Id } );
          //  ctx.SaveChanges();

          //  ctx.FieldDefaultValues.AddRange(
          //   new FieldDefaultValue() { Value = "Cd", FormField = supportoMusica},
          //   new FieldDefaultValue() { Value = "Vinile", FormField = supportoMusica },
          //   new FieldDefaultValue() { Value = "Musicassetta", FormField = supportoMusica },
          //   new FieldDefaultValue() { Value = "Dvd", FormField = supportoMusica },
          //   new FieldDefaultValue() { Value = "Altro", FormField = supportoMusica }
          //);
          //  ctx.SaveChanges();


          //  var regista = new FormField() { Name = "Regista", Type = "s" };
          //  ctx.FormFields.Add( regista );
          //  ctx.SaveChanges();

          //  ctx.CategoryFormFields.Add( new CategoryFormField() { CategoryId = lmfFilm.Id, FormFieldId = regista.Id } );
          //  ctx.SaveChanges();


          //  var genereFilm = new FormField() { Name = "Genere Film", Type = "s" };
          //  ctx.FormFields.Add( genereFilm );
          //  ctx.SaveChanges();

          //  ctx.CategoryFormFields.Add( new CategoryFormField() { CategoryId = lmfFilm.Id, FormFieldId = genereFilm.Id } );
          //  ctx.SaveChanges();


          //  ctx.FieldDefaultValues.AddRange(
          //   new FieldDefaultValue() { Value = "Azione", FormField = genereFilm },
          //   new FieldDefaultValue() { Value = "Avventura", FormField = genereFilm },
          //   new FieldDefaultValue() { Value = "Animazione", FormField = genereFilm },
          //   new FieldDefaultValue() { Value = "Biografia", FormField = genereFilm },
          //   new FieldDefaultValue() { Value = "Commedia", FormField = genereFilm },
          //   new FieldDefaultValue() { Value = "Crimine", FormField = genereFilm },
          //   new FieldDefaultValue() { Value = "Documentario", FormField = genereFilm },
          //   new FieldDefaultValue() { Value = "Drama", FormField = genereFilm },
          //   new FieldDefaultValue() { Value = "Fantascienza", FormField = genereFilm },
          //   new FieldDefaultValue() { Value = "Fantasy", FormField = genereFilm },
          //   new FieldDefaultValue() { Value = "Guerra", FormField = genereFilm },
          //   new FieldDefaultValue() { Value = "Horror", FormField = genereFilm },
          //   new FieldDefaultValue() { Value = "Musical", FormField = genereFilm },
          //   new FieldDefaultValue() { Value = "Romantico", FormField = genereFilm },
          //   new FieldDefaultValue() { Value = "Thriller", FormField = genereFilm },
          //   new FieldDefaultValue() { Value = "Western", FormField = genereFilm }
          //);
          //  ctx.SaveChanges();

          //  var casaProduttrice = new FormField() { Name = "Casa Produttrice", Type = "s" };
          //  ctx.FormFields.Add( casaProduttrice );
          //  ctx.SaveChanges();

          //  ctx.CategoryFormFields.Add( new CategoryFormField() { CategoryId = lmfFilm.Id, FormFieldId = casaProduttrice.Id } );
          //  ctx.SaveChanges();

          //  var supportoFilm= new FormField() { Name = "Supporto Film", Type = "s" };
          //  ctx.FormFields.Add( supportoFilm );
          //  ctx.SaveChanges();

          //  ctx.CategoryFormFields.Add( new CategoryFormField() { CategoryId = lmfFilm.Id, FormFieldId = supportoFilm.Id } );
          //  ctx.SaveChanges();

          //  ctx.FieldDefaultValues.AddRange(
          //   new FieldDefaultValue() { Value = "CD", FormField = supportoFilm },
          //   new FieldDefaultValue() { Value = "BlueRay", FormField = supportoFilm },
          //   new FieldDefaultValue() { Value = "Dvd", FormField = supportoFilm },
          //   new FieldDefaultValue() { Value = "VHS", FormField = supportoFilm }

          //);
          //  ctx.SaveChanges();

          //  var attori = new FormField() { Name = "Attori", Type = "s" };
          //  ctx.FormFields.Add( attori );
          //  ctx.SaveChanges();

          //  ctx.CategoryFormFields.Add( new CategoryFormField() { CategoryId = lmfFilm.Id, FormFieldId = attori.Id } );
          //  ctx.SaveChanges();

          //  var genereVideogioco = new FormField() { Name = "Genere Videogioco", Type = "s" };
          //  ctx.FormFields.Add( genereVideogioco );
          //  ctx.SaveChanges();

          //  ctx.CategoryFormFields.Add( new CategoryFormField() { CategoryId = vcVideogiochi.Id, FormFieldId = genereVideogioco.Id } );
          //  ctx.SaveChanges();

          //  ctx.FieldDefaultValues.AddRange(
          //   new FieldDefaultValue() { Value = "Action/Adventure", FormField = genereVideogioco },
          //   new FieldDefaultValue() { Value = "Casual Game", FormField = genereVideogioco },
          //   new FieldDefaultValue() { Value = "Guida", FormField = genereVideogioco },
          //   new FieldDefaultValue() { Value = "Picchiaduro", FormField = genereVideogioco },
          //   new FieldDefaultValue() { Value = "RPG", FormField = genereVideogioco },
          //   new FieldDefaultValue() { Value = "Simulazione", FormField = genereVideogioco },
          //   new FieldDefaultValue() { Value = "Sparatutto", FormField = genereVideogioco },
          //   new FieldDefaultValue() { Value = "Sport", FormField = genereVideogioco },
          //   new FieldDefaultValue() { Value = "Strategia", FormField = genereVideogioco }
          //);
          //  ctx.SaveChanges();

          //  var piattaforma = new FormField() { Name = "Piattaforma", Type = "s" };
          //  ctx.FormFields.Add( piattaforma );
          //  ctx.SaveChanges();

          //  ctx.CategoryFormFields.Add( new CategoryFormField() { CategoryId = vcVideogiochi.Id, FormFieldId = piattaforma.Id } );
          //  ctx.SaveChanges();

          //  ctx.FieldDefaultValues.AddRange(
          //   new FieldDefaultValue() { Value = "Pc", FormField = piattaforma },
          //   new FieldDefaultValue() { Value = "PlayStation", FormField = piattaforma },
          //   new FieldDefaultValue() { Value = "XBox", FormField = piattaforma },
          //   new FieldDefaultValue() { Value = "Wii", FormField = piattaforma },
          //   new FieldDefaultValue() { Value = "GameBoy", FormField = piattaforma },
          //   new FieldDefaultValue() { Value = "Altro", FormField = piattaforma }
          //);
          //  ctx.SaveChanges();

          //  var produttoreVideogioco = new FormField() { Name = "Produttore Videogioco", Type = "s" };
          //  ctx.FormFields.Add( produttoreVideogioco );
          //  ctx.SaveChanges();

          //  ctx.CategoryFormFields.Add( new CategoryFormField() { CategoryId = vcVideogiochi.Id, FormFieldId = produttoreVideogioco.Id } );
          //  ctx.SaveChanges();


         }
      }
   }
}