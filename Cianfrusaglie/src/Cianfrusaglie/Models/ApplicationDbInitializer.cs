using System.Linq;

namespace Cianfrusaglie.Models {
    public static class ApplicationDbInitializer {
        public static void EnsureSeedData( this ApplicationDbContext ctx ) {
            if( !ctx.Categories.Any() ) {
                var c = new Category {Name = "Libri, Musica e Film"};
                ctx.Categories.Add( c );
                ctx.SaveChanges();

                var lmfLibri = new Category {OverCategory = c, Name = "Libri"};
                ctx.Categories.Add( lmfLibri );
                ctx.SaveChanges();

                var lmfMusica = new Category {OverCategory = c, Name = "Musica"};
                ctx.Categories.Add( lmfMusica );
                ctx.SaveChanges();

                var lmfFilm = new Category {OverCategory = c, Name = "Film"};
                ctx.Categories.Add( lmfFilm );
                ctx.SaveChanges();


                c = new Category {Name = "Videogiochi e Console"};
                ctx.Categories.Add( c );
                ctx.SaveChanges();

                var vcVideogiochi = new Category {OverCategory = c, Name = "Videogiochi"};
                ctx.Categories.Add( vcVideogiochi );
                ctx.SaveChanges();

                var vcConsole = new Category {OverCategory = c, Name = "Console"};
                ctx.Categories.Add( vcConsole );
                ctx.SaveChanges();


                c = new Category {Name = "Arredamento e accessori per la casa"};
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


                c = new Category {Name = "Oggettistica"};
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


                c = new Category {Name = "Sport, Tempo libero e Fai da te"};
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


                c = new Category {Name = "Abbigliamento"};
                ctx.Categories.Add( c );
                ctx.SaveChanges();


                var aAbbigliamento = new Category {OverCategory = c, Name = "Abbigliamento"};
                ctx.Categories.Add( aAbbigliamento );
                ctx.SaveChanges();

                var aAccessori = new Category {OverCategory = c, Name = "Accessori"};
                ctx.Categories.Add( aAccessori );
                ctx.SaveChanges();

                var aScarpe = new Category {OverCategory = c, Name = "Scarpe"};
                ctx.Categories.Add( aScarpe );
                ctx.SaveChanges();


                c = new Category {Name = "Auto e Moto"};
                ctx.Categories.Add( c );
                ctx.SaveChanges();

                var amAutoMoto = new Category {OverCategory = c, Name = "Auto e Moto"};
                ctx.Categories.Add( amAutoMoto );
                ctx.SaveChanges();

                var amAccessori = new Category {OverCategory = c, Name = "Accessori"};
                ctx.Categories.Add( amAccessori );
                ctx.SaveChanges();


                c = new Category {Name = "Elettronica"};
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

                var eFV = new Category {OverCategory = c, Name = "Foto e videocamere"};
                ctx.Categories.Add( eFV );
                ctx.SaveChanges();


                c = new Category {Name = "MisteryBox"};
                ctx.Categories.Add( c );
                ctx.SaveChanges();

                c = new Category {Name = "Bambini 0-3 anni"};
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


                c = new Category {Name = "Animali"};
                ctx.Categories.Add( c );
                ctx.SaveChanges();

                var anAnimali = new Category {OverCategory = c, Name = "Animali"};
                ctx.Categories.Add( anAnimali );
                ctx.SaveChanges();

                var anAccessori = new Category {OverCategory = c, Name = "Accessori"};
                ctx.Categories.Add( anAccessori );
                ctx.SaveChanges();

                //FormField
                var titolo = new FormField {Name = "Titolo", Type = FormFieldType.text};
                ctx.FormFields.Add( titolo );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {CategoryId = lmfFilm.Id, FormFieldId = titolo.Id} );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {CategoryId = lmfMusica.Id, FormFieldId = titolo.Id} );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {CategoryId = lmfLibri.Id, FormFieldId = titolo.Id} );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {
                    CategoryId = vcVideogiochi.Id,
                    FormFieldId = titolo.Id
                } );
                ctx.SaveChanges();


                var genereLibri = new FormField {Name = "Genere letterario", Type = FormFieldType.select};
                ctx.FormFields.Add( genereLibri );
                ctx.SaveChanges();

                ctx.FieldDefaultValues.AddRange(
                    new FieldDefaultValue {Value = "arte, cinema, musica e spettacolo", FormField = genereLibri},
                    new FieldDefaultValue {Value = "attualità e reportage", FormField = genereLibri},
                    new FieldDefaultValue {Value = "benessere", FormField = genereLibri},
                    new FieldDefaultValue {Value = "biografia", FormField = genereLibri},
                    new FieldDefaultValue {Value = "classici", FormField = genereLibri},
                    new FieldDefaultValue {Value = "cucina", FormField = genereLibri},
                    new FieldDefaultValue {Value = "fantasy", FormField = genereLibri},
                    new FieldDefaultValue {Value = "gialli e thriller", FormField = genereLibri},
                    new FieldDefaultValue {Value = "grafic novel e fumetti", FormField = genereLibri},
                    new FieldDefaultValue {Value = "hobby e tempo libero", FormField = genereLibri},
                    new FieldDefaultValue {Value = "moda", FormField = genereLibri},
                    new FieldDefaultValue {Value = "narrativa", FormField = genereLibri},
                    new FieldDefaultValue {Value = "poesia", FormField = genereLibri},
                    new FieldDefaultValue {Value = "politica", FormField = genereLibri},
                    new FieldDefaultValue {Value = "psicologia e sociologia", FormField = genereLibri},
                    new FieldDefaultValue {Value = "religione", FormField = genereLibri},
                    new FieldDefaultValue {Value = "romanzi", FormField = genereLibri},
                    new FieldDefaultValue {Value = "scienze", FormField = genereLibri},
                    new FieldDefaultValue {Value = "sport", FormField = genereLibri},
                    new FieldDefaultValue {Value = "storia", FormField = genereLibri},
                    new FieldDefaultValue {Value = "viaggi", FormField = genereLibri} );
                ctx.SaveChanges();


                var autore = new FormField {Name = "Autore", Type = FormFieldType.text};
                ctx.FormFields.Add( autore );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {CategoryId = lmfMusica.Id, FormFieldId = autore.Id} );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {CategoryId = lmfLibri.Id, FormFieldId = autore.Id} );
                ctx.SaveChanges();


                var editore = new FormField {Name = "Editore", Type = FormFieldType.text};
                ctx.FormFields.Add( editore );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {CategoryId = lmfLibri.Id, FormFieldId = editore.Id} );
                ctx.SaveChanges();


                var numero = new FormField {Name = "Numero", Type = FormFieldType.text};
                ctx.FormFields.Add( numero );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {CategoryId = lmfLibri.Id, FormFieldId = numero.Id} );
                ctx.SaveChanges();


                var stato = new FormField {Name = "Stato", Type = FormFieldType.select};
                ctx.FormFields.Add( stato );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {CategoryId = lmfLibri.Id, FormFieldId = stato.Id} );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {CategoryId = lmfMusica.Id, FormFieldId = stato.Id} );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {CategoryId = lmfFilm.Id, FormFieldId = stato.Id} );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {CategoryId = vcConsole.Id, FormFieldId = stato.Id} );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {CategoryId = aacMobili.Id, FormFieldId = stato.Id} );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {
                    CategoryId = aacElettrodomestici.Id,
                    FormFieldId = stato.Id
                } );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {CategoryId = aacCucina.Id, FormFieldId = stato.Id} );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {
                    CategoryId = aacArredamento.Id,
                    FormFieldId = stato.Id
                } );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {CategoryId = aacAcplc.Id, FormFieldId = stato.Id} );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {CategoryId = oGiocattoli.Id, FormFieldId = stato.Id} );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {CategoryId = oModCol.Id, FormFieldId = stato.Id} );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {
                    CategoryId = oCianfrusaglie.Id,
                    FormFieldId = stato.Id
                } );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {CategoryId = stlfSport.Id, FormFieldId = stato.Id} );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {
                    CategoryId = stlfTempoLibero.Id,
                    FormFieldId = stato.Id
                } );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {CategoryId = stlfFaiDaTe.Id, FormFieldId = stato.Id} );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {CategoryId = eComputer.Id, FormFieldId = stato.Id} );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {CategoryId = eCellulari.Id, FormFieldId = stato.Id} );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {CategoryId = eFV.Id, FormFieldId = stato.Id} );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {CategoryId = eTvSchermi.Id, FormFieldId = stato.Id} );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {
                    CategoryId = bAbbigliamento.Id,
                    FormFieldId = stato.Id
                } );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {CategoryId = bAccessori.Id, FormFieldId = stato.Id} );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {CategoryId = bMobilio.Id, FormFieldId = stato.Id} );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {CategoryId = anAccessori.Id, FormFieldId = stato.Id} );
                ctx.SaveChanges();

                ctx.FieldDefaultValues.AddRange( new FieldDefaultValue {Value = "Nuovo", FormField = stato},
                    new FieldDefaultValue {Value = "Ottimo", FormField = stato},
                    new FieldDefaultValue {Value = "Buono", FormField = stato},
                    new FieldDefaultValue {Value = "Scarso", FormField = stato} );
                ctx.SaveChanges();

                var lingua = new FormField {Name = "Lingua", Type = FormFieldType.select};
                ctx.FormFields.Add( lingua );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {CategoryId = lmfLibri.Id, FormFieldId = lingua.Id} );
                ctx.SaveChanges();
                ctx.CategoryFormFields.Add( new CategoryFormField {CategoryId = lmfFilm.Id, FormFieldId = lingua.Id} );
                ctx.SaveChanges();

                ctx.FieldDefaultValues.AddRange( new FieldDefaultValue {Value = "Italiano", FormField = lingua},
                    new FieldDefaultValue {Value = "Francese", FormField = lingua},
                    new FieldDefaultValue {Value = "Inglese", FormField = lingua},
                    new FieldDefaultValue {Value = "Tedesco", FormField = lingua},
                    new FieldDefaultValue {Value = "Spagnolo", FormField = lingua},
                    new FieldDefaultValue {Value = "Altro", FormField = lingua} );
                ctx.SaveChanges();

                var anno = new FormField {Name = "Anno", Type = FormFieldType.number};
                ctx.FormFields.Add( anno );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {CategoryId = lmfLibri.Id, FormFieldId = anno.Id} );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {CategoryId = lmfMusica.Id, FormFieldId = anno.Id} );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {CategoryId = lmfFilm.Id, FormFieldId = anno.Id} );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {CategoryId = amAutoMoto.Id, FormFieldId = anno.Id} );
                ctx.SaveChanges();


                var copertina = new FormField {Name = "Copertina", Type = FormFieldType.select};
                ctx.FormFields.Add( copertina );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {CategoryId = lmfLibri.Id, FormFieldId = copertina.Id} );
                ctx.SaveChanges();

                ctx.FieldDefaultValues.AddRange( new FieldDefaultValue {Value = "Rigida", FormField = copertina},
                    new FieldDefaultValue {Value = "Flessibile", FormField = copertina} );
                ctx.SaveChanges();


                var genereMusica = new FormField {Name = "Genere musicale", Type = FormFieldType.select};
                ctx.FormFields.Add( genereMusica );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {
                    CategoryId = lmfMusica.Id,
                    FormFieldId = genereMusica.Id
                } );
                ctx.SaveChanges();


                ctx.FieldDefaultValues.AddRange( new FieldDefaultValue {Value = "Rock", FormField = genereMusica},
                    new FieldDefaultValue {Value = "Elettronica", FormField = genereMusica},
                    new FieldDefaultValue {Value = "Pop", FormField = genereMusica},
                    new FieldDefaultValue {Value = "Folk", FormField = genereMusica},
                    new FieldDefaultValue {Value = "Funk/Soul", FormField = genereMusica},
                    new FieldDefaultValue {Value = "Jazz", FormField = genereMusica},
                    new FieldDefaultValue {Value = "Hip Hop", FormField = genereMusica},
                    new FieldDefaultValue {Value = "Classica", FormField = genereMusica},
                    new FieldDefaultValue {Value = "Raggae", FormField = genereMusica},
                    new FieldDefaultValue {Value = "Latin", FormField = genereMusica},
                    new FieldDefaultValue {Value = "Blues", FormField = genereMusica},
                    new FieldDefaultValue {Value = "Rap", FormField = genereMusica},
                    new FieldDefaultValue {Value = "Bambini", FormField = genereMusica},
                    new FieldDefaultValue {Value = "Altro", FormField = genereMusica} );
                ctx.SaveChanges();


                var casaDiscografica = new FormField {Name = "Casa Discografica", Type = FormFieldType.text};
                ctx.FormFields.Add( casaDiscografica );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {
                    CategoryId = lmfMusica.Id,
                    FormFieldId = casaDiscografica.Id
                } );
                ctx.SaveChanges();

                var supportoMusica = new FormField {Name = "Supporto Musica", Type = FormFieldType.select};
                ctx.FormFields.Add( supportoMusica );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {
                    CategoryId = lmfMusica.Id,
                    FormFieldId = supportoMusica.Id
                } );
                ctx.SaveChanges();

                ctx.FieldDefaultValues.AddRange( new FieldDefaultValue {Value = "Cd", FormField = supportoMusica},
                    new FieldDefaultValue {Value = "Vinile", FormField = supportoMusica},
                    new FieldDefaultValue {Value = "Musicassetta", FormField = supportoMusica},
                    new FieldDefaultValue {Value = "Dvd", FormField = supportoMusica},
                    new FieldDefaultValue {Value = "Altro", FormField = supportoMusica} );
                ctx.SaveChanges();


                var regista = new FormField {Name = "Regista", Type = FormFieldType.text};
                ctx.FormFields.Add( regista );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {CategoryId = lmfFilm.Id, FormFieldId = regista.Id} );
                ctx.SaveChanges();


                var genereFilm = new FormField {Name = "Genere Film", Type = FormFieldType.select};
                ctx.FormFields.Add( genereFilm );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {CategoryId = lmfFilm.Id, FormFieldId = genereFilm.Id} );
                ctx.SaveChanges();


                ctx.FieldDefaultValues.AddRange( new FieldDefaultValue {Value = "Azione", FormField = genereFilm},
                    new FieldDefaultValue {Value = "Avventura", FormField = genereFilm},
                    new FieldDefaultValue {Value = "Animazione", FormField = genereFilm},
                    new FieldDefaultValue {Value = "Biografia", FormField = genereFilm},
                    new FieldDefaultValue {Value = "Commedia", FormField = genereFilm},
                    new FieldDefaultValue {Value = "Crimine", FormField = genereFilm},
                    new FieldDefaultValue {Value = "Documentario", FormField = genereFilm},
                    new FieldDefaultValue {Value = "Drama", FormField = genereFilm},
                    new FieldDefaultValue {Value = "Fantascienza", FormField = genereFilm},
                    new FieldDefaultValue {Value = "Fantasy", FormField = genereFilm},
                    new FieldDefaultValue {Value = "Guerra", FormField = genereFilm},
                    new FieldDefaultValue {Value = "Horror", FormField = genereFilm},
                    new FieldDefaultValue {Value = "Musical", FormField = genereFilm},
                    new FieldDefaultValue {Value = "Romantico", FormField = genereFilm},
                    new FieldDefaultValue {Value = "Thriller", FormField = genereFilm},
                    new FieldDefaultValue {Value = "Western", FormField = genereFilm} );
                ctx.SaveChanges();

                var casaProduttrice = new FormField {Name = "Casa Produttrice", Type = FormFieldType.text};
                ctx.FormFields.Add( casaProduttrice );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {
                    CategoryId = lmfFilm.Id,
                    FormFieldId = casaProduttrice.Id
                } );
                ctx.SaveChanges();

                var supportoFilm = new FormField {Name = "Supporto Film", Type = FormFieldType.select};
                ctx.FormFields.Add( supportoFilm );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {
                    CategoryId = lmfFilm.Id,
                    FormFieldId = supportoFilm.Id
                } );
                ctx.SaveChanges();

                ctx.FieldDefaultValues.AddRange( new FieldDefaultValue {Value = "CD", FormField = supportoFilm},
                    new FieldDefaultValue {Value = "BlueRay", FormField = supportoFilm},
                    new FieldDefaultValue {Value = "Dvd", FormField = supportoFilm},
                    new FieldDefaultValue {Value = "VHS", FormField = supportoFilm} );
                ctx.SaveChanges();

                var attori = new FormField {Name = "Attori", Type = FormFieldType.text};
                ctx.FormFields.Add( attori );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {CategoryId = lmfFilm.Id, FormFieldId = attori.Id} );
                ctx.SaveChanges();

                var genereVideogioco = new FormField {Name = "Genere Videogioco", Type = FormFieldType.select};
                ctx.FormFields.Add( genereVideogioco );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {
                    CategoryId = vcVideogiochi.Id,
                    FormFieldId = genereVideogioco.Id
                } );
                ctx.SaveChanges();

                ctx.FieldDefaultValues.AddRange(
                    new FieldDefaultValue {Value = "Action/Adventure", FormField = genereVideogioco},
                    new FieldDefaultValue {Value = "Casual Game", FormField = genereVideogioco},
                    new FieldDefaultValue {Value = "Guida", FormField = genereVideogioco},
                    new FieldDefaultValue {Value = "Picchiaduro", FormField = genereVideogioco},
                    new FieldDefaultValue {Value = "RPG", FormField = genereVideogioco},
                    new FieldDefaultValue {Value = "Simulazione", FormField = genereVideogioco},
                    new FieldDefaultValue {Value = "Sparatutto", FormField = genereVideogioco},
                    new FieldDefaultValue {Value = "Sport", FormField = genereVideogioco},
                    new FieldDefaultValue {Value = "Strategia", FormField = genereVideogioco} );
                ctx.SaveChanges();

                var piattaforma = new FormField {Name = "Piattaforma", Type = FormFieldType.select};
                ctx.FormFields.Add( piattaforma );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {
                    CategoryId = vcVideogiochi.Id,
                    FormFieldId = piattaforma.Id
                } );
                ctx.SaveChanges();

                ctx.FieldDefaultValues.AddRange( new FieldDefaultValue {Value = "Pc", FormField = piattaforma},
                    new FieldDefaultValue {Value = "PlayStation", FormField = piattaforma},
                    new FieldDefaultValue {Value = "XBox", FormField = piattaforma},
                    new FieldDefaultValue {Value = "Wii", FormField = piattaforma},
                    new FieldDefaultValue {Value = "GameBoy", FormField = piattaforma},
                    new FieldDefaultValue {Value = "Altro", FormField = piattaforma} );
                ctx.SaveChanges();

                var produttoreVideogioco = new FormField {Name = "Produttore Videogioco", Type = FormFieldType.text};
                ctx.FormFields.Add( produttoreVideogioco );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {
                    CategoryId = vcVideogiochi.Id,
                    FormFieldId = produttoreVideogioco.Id
                } );
                ctx.SaveChanges();


                //Da qui Claudio


                var modello = new FormField {Name = "Modello", Type = FormFieldType.text};
                ctx.FormFields.Add( modello );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {CategoryId = vcConsole.Id, FormFieldId = modello.Id} );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {
                    CategoryId = aacElettrodomestici.Id,
                    FormFieldId = modello.Id
                } );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {
                    CategoryId = oGiocattoli.Id,
                    FormFieldId = modello.Id
                } );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {CategoryId = oModCol.Id, FormFieldId = modello.Id} );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {CategoryId = stlfSport.Id, FormFieldId = modello.Id} );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {
                    CategoryId = stlfTempoLibero.Id,
                    FormFieldId = modello.Id
                } );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {
                    CategoryId = stlfFaiDaTe.Id,
                    FormFieldId = modello.Id
                } );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {CategoryId = aAccessori.Id, FormFieldId = modello.Id} );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {CategoryId = aScarpe.Id, FormFieldId = modello.Id} );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {CategoryId = amAutoMoto.Id, FormFieldId = modello.Id} );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {CategoryId = eComputer.Id, FormFieldId = modello.Id} );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {CategoryId = eCellulari.Id, FormFieldId = modello.Id} );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {CategoryId = eFV.Id, FormFieldId = modello.Id} );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {CategoryId = eTvSchermi.Id, FormFieldId = modello.Id} );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {CategoryId = eAudio.Id, FormFieldId = modello.Id} );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {CategoryId = eAccessori.Id, FormFieldId = modello.Id} );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {
                    CategoryId = ePeriferiche.Id,
                    FormFieldId = modello.Id
                } );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {CategoryId = bMobilio.Id, FormFieldId = modello.Id} );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {CategoryId = bAccessori.Id, FormFieldId = modello.Id} );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {
                    CategoryId = anAccessori.Id,
                    FormFieldId = modello.Id
                } );
                ctx.SaveChanges();


                var numeroJoypad = new FormField {Name = "Numero Joypad", Type = FormFieldType.select};
                ctx.FormFields.Add( numeroJoypad );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {
                    CategoryId = vcConsole.Id,
                    FormFieldId = numeroJoypad.Id
                } );
                ctx.SaveChanges();

                ctx.FieldDefaultValues.AddRange( new FieldDefaultValue {Value = "0", FormField = numeroJoypad},
                    new FieldDefaultValue {Value = "1", FormField = numeroJoypad},
                    new FieldDefaultValue {Value = "2", FormField = numeroJoypad},
                    new FieldDefaultValue {Value = "3", FormField = numeroJoypad},
                    new FieldDefaultValue {Value = "Oltre", FormField = numeroJoypad} );
                ctx.SaveChanges();


                var altezza = new FormField {Name = "Altezza", Type = FormFieldType.text};
                ctx.FormFields.Add( altezza );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {CategoryId = aacMobili.Id, FormFieldId = altezza.Id} );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {
                    CategoryId = aacElettrodomestici.Id,
                    FormFieldId = altezza.Id
                } );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {
                    CategoryId = aacIlluminazione.Id,
                    FormFieldId = altezza.Id
                } );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {
                    CategoryId = aacArredamento.Id,
                    FormFieldId = altezza.Id
                } );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {CategoryId = aacAcplc.Id, FormFieldId = altezza.Id} );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {
                    CategoryId = oGiocattoli.Id,
                    FormFieldId = altezza.Id
                } );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {CategoryId = oModCol.Id, FormFieldId = altezza.Id} );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {CategoryId = stlfSport.Id, FormFieldId = altezza.Id} );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {
                    CategoryId = stlfTempoLibero.Id,
                    FormFieldId = altezza.Id
                } );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {
                    CategoryId = stlfFaiDaTe.Id,
                    FormFieldId = altezza.Id
                } );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {CategoryId = eAudio.Id, FormFieldId = altezza.Id} );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {CategoryId = bMobilio.Id, FormFieldId = altezza.Id} );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {CategoryId = bAccessori.Id, FormFieldId = altezza.Id} );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {
                    CategoryId = anAccessori.Id,
                    FormFieldId = altezza.Id
                } );
                ctx.SaveChanges();


                var profondita = new FormField {Name = "Profondita'", Type = FormFieldType.text};
                ctx.FormFields.Add( profondita );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {
                    CategoryId = aacMobili.Id,
                    FormFieldId = profondita.Id
                } );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {
                    CategoryId = aacElettrodomestici.Id,
                    FormFieldId = profondita.Id
                } );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {
                    CategoryId = aacIlluminazione.Id,
                    FormFieldId = profondita.Id
                } );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {
                    CategoryId = aacArredamento.Id,
                    FormFieldId = profondita.Id
                } );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {
                    CategoryId = aacAcplc.Id,
                    FormFieldId = profondita.Id
                } );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {
                    CategoryId = oGiocattoli.Id,
                    FormFieldId = profondita.Id
                } );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {CategoryId = oModCol.Id, FormFieldId = profondita.Id} );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {
                    CategoryId = stlfSport.Id,
                    FormFieldId = profondita.Id
                } );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {
                    CategoryId = stlfTempoLibero.Id,
                    FormFieldId = profondita.Id
                } );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {
                    CategoryId = stlfFaiDaTe.Id,
                    FormFieldId = profondita.Id
                } );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {CategoryId = eAudio.Id, FormFieldId = profondita.Id} );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {
                    CategoryId = bMobilio.Id,
                    FormFieldId = profondita.Id
                } );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {
                    CategoryId = bAccessori.Id,
                    FormFieldId = profondita.Id
                } );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {
                    CategoryId = anAccessori.Id,
                    FormFieldId = profondita.Id
                } );
                ctx.SaveChanges();


                var larghezza = new FormField {Name = "Larghezza", Type = FormFieldType.text};
                ctx.FormFields.Add( larghezza );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {
                    CategoryId = aacMobili.Id,
                    FormFieldId = larghezza.Id
                } );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {
                    CategoryId = aacElettrodomestici.Id,
                    FormFieldId = larghezza.Id
                } );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {
                    CategoryId = aacIlluminazione.Id,
                    FormFieldId = larghezza.Id
                } );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {
                    CategoryId = aacArredamento.Id,
                    FormFieldId = larghezza.Id
                } );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {CategoryId = aacAcplc.Id, FormFieldId = larghezza.Id} );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {
                    CategoryId = oGiocattoli.Id,
                    FormFieldId = larghezza.Id
                } );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {CategoryId = oModCol.Id, FormFieldId = larghezza.Id} );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {
                    CategoryId = stlfSport.Id,
                    FormFieldId = larghezza.Id
                } );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {
                    CategoryId = stlfTempoLibero.Id,
                    FormFieldId = larghezza.Id
                } );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {
                    CategoryId = stlfFaiDaTe.Id,
                    FormFieldId = larghezza.Id
                } );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {CategoryId = eAudio.Id, FormFieldId = larghezza.Id} );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {CategoryId = bMobilio.Id, FormFieldId = larghezza.Id} );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {
                    CategoryId = bAccessori.Id,
                    FormFieldId = larghezza.Id
                } );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {
                    CategoryId = anAccessori.Id,
                    FormFieldId = larghezza.Id
                } );
                ctx.SaveChanges();


                var tipoMobili = new FormField {Name = "Tipo Mobile", Type = FormFieldType.select};
                ctx.FormFields.Add( tipoMobili );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {
                    CategoryId = aacMobili.Id,
                    FormFieldId = tipoMobili.Id
                } );
                ctx.SaveChanges();

                ctx.FieldDefaultValues.AddRange(
                    new FieldDefaultValue {Value = "Mobile per Bagno", FormField = tipoMobili},
                    new FieldDefaultValue {Value = "Scrivania", FormField = tipoMobili},
                    new FieldDefaultValue {Value = "Letto", FormField = tipoMobili},
                    new FieldDefaultValue {Value = "Sedia", FormField = tipoMobili},
                    new FieldDefaultValue {Value = "Poltrona", FormField = tipoMobili},
                    new FieldDefaultValue {Value = "Sgabello", FormField = tipoMobili},
                    new FieldDefaultValue {Value = "Panca", FormField = tipoMobili},
                    new FieldDefaultValue {Value = "Armadio", FormField = tipoMobili},
                    new FieldDefaultValue {Value = "Scarpiera", FormField = tipoMobili},
                    new FieldDefaultValue {Value = "Attaccapanni", FormField = tipoMobili},
                    new FieldDefaultValue {Value = "Comodino", FormField = tipoMobili},
                    new FieldDefaultValue {Value = "Libreria", FormField = tipoMobili},
                    new FieldDefaultValue {Value = "Tavolo", FormField = tipoMobili},
                    new FieldDefaultValue {Value = "Altro", FormField = tipoMobili} );
                ctx.SaveChanges();

                var materiale = new FormField {Name = "Materiale", Type = FormFieldType.text};
                ctx.FormFields.Add( materiale );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {
                    CategoryId = aacMobili.Id,
                    FormFieldId = materiale.Id
                } );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {
                    CategoryId = aacCucina.Id,
                    FormFieldId = materiale.Id
                } );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {
                    CategoryId = aacArredamento.Id,
                    FormFieldId = materiale.Id
                } );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {CategoryId = aacAcplc.Id, FormFieldId = materiale.Id} );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {
                    CategoryId = aAbbigliamento.Id,
                    FormFieldId = materiale.Id
                } );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {
                    CategoryId = aAccessori.Id,
                    FormFieldId = materiale.Id
                } );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {CategoryId = aScarpe.Id, FormFieldId = materiale.Id} );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {
                    CategoryId = bAbbigliamento.Id,
                    FormFieldId = materiale.Id
                } );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {CategoryId = bMobilio.Id, FormFieldId = materiale.Id} );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {
                    CategoryId = anAccessori.Id,
                    FormFieldId = materiale.Id
                } );
                ctx.SaveChanges();

                var marca = new FormField {Name = "Marca", Type = FormFieldType.text};
                ctx.FormFields.Add( marca );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {CategoryId = aacMobili.Id, FormFieldId = marca.Id} );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {
                    CategoryId = aacElettrodomestici.Id,
                    FormFieldId = marca.Id
                } );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {
                    CategoryId = aacIlluminazione.Id,
                    FormFieldId = marca.Id
                } );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {CategoryId = aacCucina.Id, FormFieldId = marca.Id} );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {
                    CategoryId = aacArredamento.Id,
                    FormFieldId = marca.Id
                } );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {CategoryId = aacAcplc.Id, FormFieldId = marca.Id} );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {CategoryId = oGiocattoli.Id, FormFieldId = marca.Id} );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {CategoryId = oModCol.Id, FormFieldId = marca.Id} );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {CategoryId = stlfSport.Id, FormFieldId = marca.Id} );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {CategoryId = stlfFaiDaTe.Id, FormFieldId = marca.Id} );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {
                    CategoryId = stlfTempoLibero.Id,
                    FormFieldId = marca.Id
                } );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {
                    CategoryId = aAbbigliamento.Id,
                    FormFieldId = marca.Id
                } );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {CategoryId = aAccessori.Id, FormFieldId = marca.Id} );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {CategoryId = aScarpe.Id, FormFieldId = marca.Id} );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {CategoryId = amAutoMoto.Id, FormFieldId = marca.Id} );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {CategoryId = eComputer.Id, FormFieldId = marca.Id} );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {CategoryId = eCellulari.Id, FormFieldId = marca.Id} );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {CategoryId = eFV.Id, FormFieldId = marca.Id} );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {CategoryId = eTvSchermi.Id, FormFieldId = marca.Id} );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {CategoryId = eAudio.Id, FormFieldId = marca.Id} );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {CategoryId = ePeriferiche.Id, FormFieldId = marca.Id} );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {
                    CategoryId = bAbbigliamento.Id,
                    FormFieldId = marca.Id
                } );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {CategoryId = bAccessori.Id, FormFieldId = marca.Id} );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {CategoryId = bMobilio.Id, FormFieldId = marca.Id} );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {CategoryId = anAccessori.Id, FormFieldId = marca.Id} );
                ctx.SaveChanges();


                var colore = new FormField {Name = "Colore", Type = FormFieldType.text};
                ctx.FormFields.Add( colore );
                ctx.SaveChanges();


                ctx.CategoryFormFields.Add( new CategoryFormField {CategoryId = aacMobili.Id, FormFieldId = colore.Id} );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {
                    CategoryId = aacElettrodomestici.Id,
                    FormFieldId = colore.Id
                } );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {CategoryId = aacCucina.Id, FormFieldId = colore.Id} );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {
                    CategoryId = aacArredamento.Id,
                    FormFieldId = colore.Id
                } );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {CategoryId = aacAcplc.Id, FormFieldId = colore.Id} );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {CategoryId = stlfSport.Id, FormFieldId = colore.Id} );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {
                    CategoryId = stlfTempoLibero.Id,
                    FormFieldId = colore.Id
                } );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {CategoryId = stlfFaiDaTe.Id, FormFieldId = colore.Id} );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {
                    CategoryId = aAbbigliamento.Id,
                    FormFieldId = colore.Id
                } );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {CategoryId = aAccessori.Id, FormFieldId = colore.Id} );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {CategoryId = aScarpe.Id, FormFieldId = colore.Id} );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {CategoryId = amAutoMoto.Id, FormFieldId = colore.Id} );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {CategoryId = amAccessori.Id, FormFieldId = colore.Id} );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {
                    CategoryId = bAbbigliamento.Id,
                    FormFieldId = colore.Id
                } );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {CategoryId = bMobilio.Id, FormFieldId = colore.Id} );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {CategoryId = bAccessori.Id, FormFieldId = colore.Id} );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {CategoryId = anAnimali.Id, FormFieldId = colore.Id} );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {CategoryId = anAccessori.Id, FormFieldId = colore.Id} );
                ctx.SaveChanges();


                var tipoElettrodomestici = new FormField {Name = "Tipo Elettrodomestico", Type = FormFieldType.select};
                ctx.FormFields.Add( tipoElettrodomestici );
                ctx.SaveChanges();


                ctx.CategoryFormFields.Add( new CategoryFormField {
                    CategoryId = aacElettrodomestici.Id,
                    FormFieldId = tipoElettrodomestici.Id
                } );
                ctx.SaveChanges();

                ctx.FieldDefaultValues.AddRange(
                    new FieldDefaultValue {Value = "Lavatrice", FormField = tipoElettrodomestici},
                    new FieldDefaultValue {Value = "Lavasciuga", FormField = tipoElettrodomestici},
                    new FieldDefaultValue {Value = "Asciugatrice", FormField = tipoElettrodomestici},
                    new FieldDefaultValue {Value = "Frigorifero", FormField = tipoElettrodomestici},
                    new FieldDefaultValue {Value = "Congelatore", FormField = tipoElettrodomestici},
                    new FieldDefaultValue {Value = "Lavastoviglie", FormField = tipoElettrodomestici},
                    new FieldDefaultValue {Value = "Macchina caffe'", FormField = tipoElettrodomestici},
                    new FieldDefaultValue {Value = "Forno a Microonde", FormField = tipoElettrodomestici},
                    new FieldDefaultValue {Value = "Robot da Cucina", FormField = tipoElettrodomestici},
                    new FieldDefaultValue {Value = "Cottura", FormField = tipoElettrodomestici},
                    new FieldDefaultValue {Value = "Frullatore", FormField = tipoElettrodomestici},
                    new FieldDefaultValue {Value = "Tostapane", FormField = tipoElettrodomestici},
                    new FieldDefaultValue {Value = "Spremi Agrumi", FormField = tipoElettrodomestici},
                    new FieldDefaultValue {Value = "Aspirapolvere", FormField = tipoElettrodomestici},
                    new FieldDefaultValue {Value = "Ferro da Stiro", FormField = tipoElettrodomestici},
                    new FieldDefaultValue {Value = "Asciuga Capelli", FormField = tipoElettrodomestici},
                    new FieldDefaultValue {Value = "Piastra per capelli", FormField = tipoElettrodomestici},
                    new FieldDefaultValue {Value = "Bilancia", FormField = tipoElettrodomestici},
                    new FieldDefaultValue {Value = "Ventilatore", FormField = tipoElettrodomestici},
                    new FieldDefaultValue {Value = "Condizionatore", FormField = tipoElettrodomestici},
                    new FieldDefaultValue {Value = "Altro", FormField = tipoElettrodomestici} );
                ctx.SaveChanges();


                var tipoIlluminazione = new FormField {Name = "Tipo Illuminazione", Type = FormFieldType.select};
                ctx.FormFields.Add( tipoIlluminazione );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {
                    CategoryId = aacIlluminazione.Id,
                    FormFieldId = tipoIlluminazione.Id
                } );
                ctx.SaveChanges();

                ctx.FieldDefaultValues.AddRange(
                    new FieldDefaultValue {Value = "Lampada da soffitto", FormField = tipoIlluminazione},
                    new FieldDefaultValue {Value = "Lampada da tavolo", FormField = tipoIlluminazione},
                    new FieldDefaultValue {Value = "Lampada da terra", FormField = tipoIlluminazione},
                    new FieldDefaultValue {Value = "Lampada da parete", FormField = tipoIlluminazione},
                    new FieldDefaultValue {Value = "Lampada da esterno", FormField = tipoIlluminazione},
                    new FieldDefaultValue {Value = "Faretti", FormField = tipoIlluminazione},
                    new FieldDefaultValue {Value = "Altro", FormField = tipoIlluminazione} );
                ctx.SaveChanges();


                var potenza = new FormField {Name = "Potenza", Type = FormFieldType.text};
                ctx.FormFields.Add( potenza );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {
                    CategoryId = aacIlluminazione.Id,
                    FormFieldId = potenza.Id
                } );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {CategoryId = eAudio.Id, FormFieldId = potenza.Id} );
                ctx.SaveChanges();


                var tipoCucina = new FormField {Name = "Tipo Cucina", Type = FormFieldType.select};
                ctx.FormFields.Add( tipoCucina );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {
                    CategoryId = aacCucina.Id,
                    FormFieldId = tipoCucina.Id
                } );
                ctx.SaveChanges();


                ctx.FieldDefaultValues.AddRange(
                    new FieldDefaultValue {Value = "Pentola/Padella", FormField = tipoCucina},
                    new FieldDefaultValue {Value = "Contenitore per alimenti", FormField = tipoCucina},
                    new FieldDefaultValue {Value = "Utensile da cucina", FormField = tipoCucina},
                    new FieldDefaultValue {Value = "Set coltelli", FormField = tipoCucina},
                    new FieldDefaultValue {Value = "Tagliere", FormField = tipoCucina},
                    new FieldDefaultValue {Value = "Teglia", FormField = tipoCucina},
                    new FieldDefaultValue {Value = "Utensile per la misurazione", FormField = tipoCucina},
                    new FieldDefaultValue {Value = "Utensile da cucina", FormField = tipoCucina},
                    new FieldDefaultValue {Value = "Accessorio per lavare i piatti", FormField = tipoCucina},
                    new FieldDefaultValue {Value = "Servizio piatti", FormField = tipoCucina},
                    new FieldDefaultValue {Value = "Servizio bicchieri", FormField = tipoCucina},
                    new FieldDefaultValue {Value = "Servizio posate", FormField = tipoCucina},
                    new FieldDefaultValue {Value = "Altro", FormField = tipoCucina} );
                ctx.SaveChanges();


                var tipoArredamento = new FormField {Name = "Tipo Arredamento", Type = FormFieldType.select};
                ctx.FormFields.Add( tipoArredamento );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {
                    CategoryId = aacArredamento.Id,
                    FormFieldId = tipoArredamento.Id
                } );
                ctx.SaveChanges();


                ctx.FieldDefaultValues.AddRange(
                    new FieldDefaultValue {Value = "Tappeto", FormField = tipoArredamento},
                    new FieldDefaultValue {Value = "Specchio", FormField = tipoArredamento},
                    new FieldDefaultValue {Value = "Quadro", FormField = tipoArredamento},
                    new FieldDefaultValue {Value = "Cornice", FormField = tipoArredamento},
                    new FieldDefaultValue {Value = "Candela/Candeliere", FormField = tipoArredamento},
                    new FieldDefaultValue {Value = "Vaso", FormField = tipoArredamento},
                    new FieldDefaultValue {Value = "Orologio", FormField = tipoArredamento},
                    new FieldDefaultValue {Value = "Arredamento decorativo", FormField = tipoArredamento},
                    new FieldDefaultValue {Value = "Altro", FormField = tipoArredamento} );
                ctx.SaveChanges();


                var tipoAccessorioCasa = new FormField {Name = "Tipo Accessorio", Type = FormFieldType.select};
                ctx.FormFields.Add( tipoAccessorioCasa );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {
                    CategoryId = aacAcplc.Id,
                    FormFieldId = tipoAccessorioCasa.Id
                } );
                ctx.SaveChanges();


                ctx.FieldDefaultValues.AddRange(
                    new FieldDefaultValue {Value = "Scopa/Paletta", FormField = tipoAccessorioCasa},
                    new FieldDefaultValue {Value = "Mensola/Ripiano", FormField = tipoAccessorioCasa},
                    new FieldDefaultValue {Value = "Contenitore/Cesta", FormField = tipoAccessorioCasa},
                    new FieldDefaultValue {Value = "Altro", FormField = tipoAccessorioCasa} );
                ctx.SaveChanges();


                var tipoGiocattolo = new FormField {Name = "Tipo Giocattolo", Type = FormFieldType.select};
                ctx.FormFields.Add( tipoGiocattolo );
                ctx.SaveChanges();


                ctx.CategoryFormFields.Add( new CategoryFormField {
                    CategoryId = oGiocattoli.Id,
                    FormFieldId = tipoGiocattolo.Id
                } );
                ctx.SaveChanges();

                ctx.FieldDefaultValues.AddRange(
                    new FieldDefaultValue {Value = "Accessorio/Kit", FormField = tipoGiocattolo},
                    new FieldDefaultValue {Value = "Gioco Creativo", FormField = tipoGiocattolo},
                    new FieldDefaultValue {Value = "Gioco da Tavola", FormField = tipoGiocattolo},
                    new FieldDefaultValue {Value = "Puzzle", FormField = tipoGiocattolo},
                    new FieldDefaultValue {Value = "Peluche", FormField = tipoGiocattolo},
                    new FieldDefaultValue {Value = "Gioco Elettronico", FormField = tipoGiocattolo},
                    new FieldDefaultValue {Value = "Gioco Interattivo", FormField = tipoGiocattolo},
                    new FieldDefaultValue {Value = "Gioco di Costruzione", FormField = tipoGiocattolo},
                    new FieldDefaultValue {Value = "Bambola", FormField = tipoGiocattolo},
                    new FieldDefaultValue {Value = "Veicolo", FormField = tipoGiocattolo},
                    new FieldDefaultValue {Value = "Piste", FormField = tipoGiocattolo},
                    new FieldDefaultValue {Value = "Gioco Educativo", FormField = tipoGiocattolo},
                    new FieldDefaultValue {Value = "Altro", FormField = tipoGiocattolo} );
                ctx.SaveChanges();

                var etaMinima = new FormField {Name = "Eta Minima", Type = FormFieldType.number};
                ctx.FormFields.Add( etaMinima );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {
                    CategoryId = oGiocattoli.Id,
                    FormFieldId = etaMinima.Id
                } );
                ctx.SaveChanges();

                var tipoModCol = new FormField {Name = "Tipo Modellino/Collezione", Type = FormFieldType.text};
                ctx.FormFields.Add( tipoModCol );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {CategoryId = oModCol.Id, FormFieldId = tipoModCol.Id} );
                ctx.SaveChanges();

                var tipoCianfrusaglia = new FormField {Name = "Tipo Cianfrusaglia", Type = FormFieldType.text};
                ctx.FormFields.Add( tipoCianfrusaglia );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {
                    CategoryId = oCianfrusaglie.Id,
                    FormFieldId = tipoCianfrusaglia.Id
                } );
                ctx.SaveChanges();


                var livInutilita = new FormField {Name = "Livello di Inutilità", Type = FormFieldType.select};
                ctx.FormFields.Add( livInutilita );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {
                    CategoryId = oCianfrusaglie.Id,
                    FormFieldId = livInutilita.Id
                } );
                ctx.SaveChanges();

                ctx.FieldDefaultValues.AddRange(
                    new FieldDefaultValue {Value = "Decisamente Inutile", FormField = livInutilita},
                    new FieldDefaultValue {Value = "Inutile", FormField = livInutilita},
                    new FieldDefaultValue {Value = "Utile", FormField = livInutilita},
                    new FieldDefaultValue {Value = "Decisamente Utile", FormField = livInutilita} );
                ctx.SaveChanges();


                var tipoSport = new FormField {Name = "Sport", Type = FormFieldType.select};
                ctx.FormFields.Add( tipoSport );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {
                    CategoryId = stlfSport.Id,
                    FormFieldId = tipoSport.Id
                } );
                ctx.SaveChanges();


                ctx.FieldDefaultValues.AddRange( new FieldDefaultValue {Value = "Ciclismo", FormField = tipoSport},
                    new FieldDefaultValue {Value = "Nuoto", FormField = tipoSport},
                    new FieldDefaultValue {Value = "Atletica", FormField = tipoSport},
                    new FieldDefaultValue {Value = "Sport Acquatici", FormField = tipoSport},
                    new FieldDefaultValue {Value = "Equitazione", FormField = tipoSport},
                    new FieldDefaultValue {Value = "Ciclismo", FormField = tipoSport},
                    new FieldDefaultValue {Value = "Danza", FormField = tipoSport},
                    new FieldDefaultValue {Value = "Calcio", FormField = tipoSport},
                    new FieldDefaultValue {Value = "Tennis", FormField = tipoSport},
                    new FieldDefaultValue {Value = "Combattimento", FormField = tipoSport},
                    new FieldDefaultValue {Value = "Sci", FormField = tipoSport},
                    new FieldDefaultValue {Value = "Basket", FormField = tipoSport},
                    new FieldDefaultValue {Value = "Pallavolo", FormField = tipoSport},
                    new FieldDefaultValue {Value = "Rugby", FormField = tipoSport},
                    new FieldDefaultValue {Value = "Baseball", FormField = tipoSport},
                    new FieldDefaultValue {Value = "Golf", FormField = tipoSport},
                    new FieldDefaultValue {Value = "Altro", FormField = tipoSport} );
                ctx.SaveChanges();

                var tipoOggettoSTlFt = new FormField {Name = "Tipo Oggetto", Type = FormFieldType.text};
                ctx.FormFields.Add( tipoOggettoSTlFt );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {
                    CategoryId = stlfSport.Id,
                    FormFieldId = tipoOggettoSTlFt.Id
                } );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {
                    CategoryId = stlfFaiDaTe.Id,
                    FormFieldId = tipoOggettoSTlFt.Id
                } );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {
                    CategoryId = stlfTempoLibero.Id,
                    FormFieldId = tipoOggettoSTlFt.Id
                } );
                ctx.SaveChanges();


                var tipoAbbigliamento = new FormField {Name = "Tipo Abbigliamento", Type = FormFieldType.select};
                ctx.FormFields.Add( tipoAbbigliamento );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {
                    CategoryId = aAbbigliamento.Id,
                    FormFieldId = tipoAbbigliamento.Id
                } );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {
                    CategoryId = bAbbigliamento.Id,
                    FormFieldId = tipoAbbigliamento.Id
                } );
                ctx.SaveChanges();

                ctx.FieldDefaultValues.AddRange(
                    new FieldDefaultValue {Value = "Vestito", FormField = tipoAbbigliamento},
                    new FieldDefaultValue {Value = "T- shirt", FormField = tipoAbbigliamento},
                    new FieldDefaultValue {Value = "Top", FormField = tipoAbbigliamento},
                    new FieldDefaultValue {Value = "Pantalone", FormField = tipoAbbigliamento},
                    new FieldDefaultValue {Value = "Camicia", FormField = tipoAbbigliamento},
                    new FieldDefaultValue {Value = "Felpa", FormField = tipoAbbigliamento},
                    new FieldDefaultValue {Value = "Jeans", FormField = tipoAbbigliamento},
                    new FieldDefaultValue {Value = "Cardigan/Pullover", FormField = tipoAbbigliamento},
                    new FieldDefaultValue {Value = "Shorts", FormField = tipoAbbigliamento},
                    new FieldDefaultValue {Value = "Giacca", FormField = tipoAbbigliamento},
                    new FieldDefaultValue {Value = "Cappotto", FormField = tipoAbbigliamento},
                    new FieldDefaultValue {Value = "Gonna", FormField = tipoAbbigliamento},
                    new FieldDefaultValue {Value = "Intimo", FormField = tipoAbbigliamento},
                    new FieldDefaultValue {Value = "Accappatoio", FormField = tipoAbbigliamento},
                    new FieldDefaultValue {Value = "Vestaglia", FormField = tipoAbbigliamento},
                    new FieldDefaultValue {Value = "Mare", FormField = tipoAbbigliamento},
                    new FieldDefaultValue {Value = "Altro", FormField = tipoAbbigliamento} );
                ctx.SaveChanges();

                var tagliaAbbigliamento = new FormField {Name = "Taglia Abbigliamento", Type = FormFieldType.select};
                ctx.FormFields.Add( tagliaAbbigliamento );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {
                    CategoryId = aAbbigliamento.Id,
                    FormFieldId = tagliaAbbigliamento.Id
                } );
                ctx.SaveChanges();

                ctx.FieldDefaultValues.AddRange( new FieldDefaultValue {Value = "XS", FormField = tagliaAbbigliamento},
                    new FieldDefaultValue {Value = "S", FormField = tagliaAbbigliamento},
                    new FieldDefaultValue {Value = "M", FormField = tagliaAbbigliamento},
                    new FieldDefaultValue {Value = "L", FormField = tagliaAbbigliamento},
                    new FieldDefaultValue {Value = "XL", FormField = tagliaAbbigliamento},
                    new FieldDefaultValue {Value = "XXL", FormField = tagliaAbbigliamento},
                    new FieldDefaultValue {Value = "XXXL", FormField = tagliaAbbigliamento},
                    new FieldDefaultValue {Value = "Altro", FormField = tagliaAbbigliamento} );
                ctx.SaveChanges();

                var sessoUDB = new FormField {Name = "Genere", Type = FormFieldType.select};
                ctx.FormFields.Add( sessoUDB );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {
                    CategoryId = aAbbigliamento.Id,
                    FormFieldId = sessoUDB.Id
                } );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {
                    CategoryId = aAccessori.Id,
                    FormFieldId = sessoUDB.Id
                } );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {CategoryId = aScarpe.Id, FormFieldId = sessoUDB.Id} );
                ctx.SaveChanges();

                ctx.FieldDefaultValues.AddRange( new FieldDefaultValue {Value = "Uomo", FormField = sessoUDB},
                    new FieldDefaultValue {Value = "Donna", FormField = sessoUDB},
                    new FieldDefaultValue {Value = "Bambino", FormField = sessoUDB} );
                ctx.SaveChanges();

                var tipoAbbAccessori = new FormField {Name = "Tipo Accessorio", Type = FormFieldType.select};
                ctx.FormFields.Add( tipoAbbAccessori );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {
                    CategoryId = aAccessori.Id,
                    FormFieldId = tipoAbbAccessori.Id
                } );
                ctx.SaveChanges();

                ctx.FieldDefaultValues.AddRange( new FieldDefaultValue {Value = "Borsa", FormField = tipoAbbAccessori},
                    new FieldDefaultValue {Value = "Valigia", FormField = tipoAbbAccessori},
                    new FieldDefaultValue {Value = "Collana", FormField = tipoAbbAccessori},
                    new FieldDefaultValue {Value = "Bracciale", FormField = tipoAbbAccessori},
                    new FieldDefaultValue {Value = "Anello", FormField = tipoAbbAccessori},
                    new FieldDefaultValue {Value = "Orecchini", FormField = tipoAbbAccessori},
                    new FieldDefaultValue {Value = "Occhiali", FormField = tipoAbbAccessori},
                    new FieldDefaultValue {Value = "Foulard/Sciarpa", FormField = tipoAbbAccessori},
                    new FieldDefaultValue {Value = "Portafoglio", FormField = tipoAbbAccessori},
                    new FieldDefaultValue {Value = "Cintura", FormField = tipoAbbAccessori},
                    new FieldDefaultValue {Value = "Copricapo", FormField = tipoAbbAccessori},
                    new FieldDefaultValue {Value = "Orologio", FormField = tipoAbbAccessori},
                    new FieldDefaultValue {Value = "Ombrello", FormField = tipoAbbAccessori},
                    new FieldDefaultValue {Value = "Guanti", FormField = tipoAbbAccessori},
                    new FieldDefaultValue {Value = "Altro", FormField = tipoAbbAccessori} );
                ctx.SaveChanges();


                var tipoScarpe = new FormField {Name = "Tipo Scarpa", Type = FormFieldType.select};
                ctx.FormFields.Add( tipoScarpe );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {CategoryId = aScarpe.Id, FormFieldId = tipoScarpe.Id} );
                ctx.SaveChanges();


                ctx.FieldDefaultValues.AddRange( new FieldDefaultValue {Value = "Sneakers", FormField = tipoScarpe},
                    new FieldDefaultValue {Value = "Sandali", FormField = tipoScarpe},
                    new FieldDefaultValue {Value = "Scarpe col Tacco", FormField = tipoScarpe},
                    new FieldDefaultValue {Value = "Ballerine", FormField = tipoScarpe},
                    new FieldDefaultValue {Value = "Scarpe Basse", FormField = tipoScarpe},
                    new FieldDefaultValue {Value = "Stivaletti", FormField = tipoScarpe},
                    new FieldDefaultValue {Value = "Stivali", FormField = tipoScarpe},
                    new FieldDefaultValue {Value = "Ciabatte/Pantofole", FormField = tipoScarpe},
                    new FieldDefaultValue {Value = "Zoccoli", FormField = tipoScarpe},
                    new FieldDefaultValue {Value = "Sportive", FormField = tipoScarpe},
                    new FieldDefaultValue {Value = "Trekking", FormField = tipoScarpe},
                    new FieldDefaultValue {Value = "Mare", FormField = tipoScarpe} );
                ctx.SaveChanges();


                var tagliaScarpe = new FormField {Name = "Taglia Scarpa", Type = FormFieldType.text};
                ctx.FormFields.Add( tagliaScarpe );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {
                    CategoryId = aScarpe.Id,
                    FormFieldId = tagliaScarpe.Id
                } );
                ctx.SaveChanges();


                var autoMoto = new FormField {Name = "Tipo di Auto/Moto", Type = FormFieldType.select};
                ctx.FormFields.Add( autoMoto );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {
                    CategoryId = amAutoMoto.Id,
                    FormFieldId = autoMoto.Id
                } );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {
                    CategoryId = amAccessori.Id,
                    FormFieldId = autoMoto.Id
                } );
                ctx.SaveChanges();

                ctx.FieldDefaultValues.AddRange( new FieldDefaultValue {Value = "Auto", FormField = autoMoto},
                    new FieldDefaultValue {Value = "Moto", FormField = autoMoto},
                    new FieldDefaultValue {Value = "Quad", FormField = autoMoto},
                    new FieldDefaultValue {Value = "Furgone", FormField = autoMoto} );
                ctx.SaveChanges();


                var tipoAutoMoto = new FormField {Name = "Tipologia", Type = FormFieldType.text};
                ctx.FormFields.Add( tipoAutoMoto );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {
                    CategoryId = amAutoMoto.Id,
                    FormFieldId = tipoAutoMoto.Id
                } );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {
                    CategoryId = amAccessori.Id,
                    FormFieldId = tipoAutoMoto.Id
                } );
                ctx.SaveChanges();


                var cavalli = new FormField {Name = "Cavalli", Type = FormFieldType.text};
                ctx.FormFields.Add( cavalli );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {CategoryId = amAutoMoto.Id, FormFieldId = cavalli.Id} );
                ctx.SaveChanges();


                var cilindrata = new FormField {Name = "Cilindrata", Type = FormFieldType.text};
                ctx.FormFields.Add( cilindrata );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {
                    CategoryId = amAutoMoto.Id,
                    FormFieldId = cilindrata.Id
                } );
                ctx.SaveChanges();


                var compCon = new FormField {Name = "Compatibile Con", Type = FormFieldType.text};
                ctx.FormFields.Add( compCon );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {
                    CategoryId = amAccessori.Id,
                    FormFieldId = compCon.Id
                } );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {
                    CategoryId = ePeriferiche.Id,
                    FormFieldId = compCon.Id
                } );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {CategoryId = eAccessori.Id, FormFieldId = compCon.Id} );
                ctx.SaveChanges();


                var tipoComputer = new FormField {Name = "Tipo Computer", Type = FormFieldType.select};
                ctx.FormFields.Add( tipoComputer );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {
                    CategoryId = eComputer.Id,
                    FormFieldId = tipoComputer.Id
                } );
                ctx.SaveChanges();


                ctx.FieldDefaultValues.AddRange( new FieldDefaultValue {Value = "Fisso", FormField = tipoComputer},
                    new FieldDefaultValue {Value = "Portatile", FormField = tipoComputer},
                    new FieldDefaultValue {Value = "Tablet", FormField = tipoComputer},
                    new FieldDefaultValue {Value = "NoteBook", FormField = tipoComputer},
                    new FieldDefaultValue {Value = "Altro", FormField = tipoComputer} );
                ctx.SaveChanges();

                var ram = new FormField {Name = "Ram", Type = FormFieldType.text};
                ctx.FormFields.Add( ram );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {CategoryId = eComputer.Id, FormFieldId = ram.Id} );
                ctx.SaveChanges();

                var processore = new FormField {Name = "Processore", Type = FormFieldType.text};
                ctx.FormFields.Add( processore );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {
                    CategoryId = eComputer.Id,
                    FormFieldId = processore.Id
                } );
                ctx.SaveChanges();

                var hardDisk = new FormField {Name = "Hard Disk", Type = FormFieldType.text};
                ctx.FormFields.Add( hardDisk );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {CategoryId = eComputer.Id, FormFieldId = hardDisk.Id} );
                ctx.SaveChanges();

                var pollici = new FormField {Name = "Pollici", Type = FormFieldType.text};
                ctx.FormFields.Add( pollici );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {CategoryId = eComputer.Id, FormFieldId = pollici.Id} );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {CategoryId = eTvSchermi.Id, FormFieldId = pollici.Id} );
                ctx.SaveChanges();


                var tipoFotoVideo = new FormField {Name = "Tipo Fotocamera Videocamera", Type = FormFieldType.select};
                ctx.FormFields.Add( tipoFotoVideo );
                ctx.SaveChanges();


                ctx.CategoryFormFields.Add( new CategoryFormField {CategoryId = eFV.Id, FormFieldId = pollici.Id} );
                ctx.SaveChanges();


                ctx.FieldDefaultValues.AddRange(
                    new FieldDefaultValue {Value = "Fotocamera", FormField = tipoFotoVideo},
                    new FieldDefaultValue {Value = "Fotocamera Reflex", FormField = tipoFotoVideo},
                    new FieldDefaultValue {Value = "Videocamera", FormField = tipoFotoVideo},
                    new FieldDefaultValue {Value = "Obbiettivo Fotocamera", FormField = tipoFotoVideo},
                    new FieldDefaultValue {Value = "SportCam", FormField = tipoFotoVideo},
                    new FieldDefaultValue {Value = "Accessori", FormField = tipoFotoVideo} );
                ctx.SaveChanges();

                var risoluzione = new FormField {Name = "Risoluzione", Type = FormFieldType.text};
                ctx.FormFields.Add( risoluzione );
                ctx.SaveChanges();


                ctx.CategoryFormFields.Add( new CategoryFormField {CategoryId = eFV.Id, FormFieldId = risoluzione.Id} );
                ctx.SaveChanges();


                var memoria = new FormField {Name = "Memoria", Type = FormFieldType.text};
                ctx.FormFields.Add( memoria );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {CategoryId = eFV.Id, FormFieldId = memoria.Id} );
                ctx.SaveChanges();


                var memoriaExp = new FormField {Name = "Memoria Espandibile", Type = FormFieldType.text};
                ctx.FormFields.Add( memoriaExp );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {CategoryId = eFV.Id, FormFieldId = memoriaExp.Id} );
                ctx.SaveChanges();


                var sintTv = new FormField {Name = "Sintonizzatore Tv", Type = FormFieldType.checkbox};
                ctx.FormFields.Add( sintTv );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {CategoryId = eTvSchermi.Id, FormFieldId = sintTv.Id} );
                ctx.SaveChanges();


                var smartTv = new FormField {Name = "Smart Tv", Type = FormFieldType.checkbox};
                ctx.FormFields.Add( smartTv );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {CategoryId = eTvSchermi.Id, FormFieldId = smartTv.Id} );
                ctx.SaveChanges();

                var wifiTv = new FormField {Name = "Wifi Tv", Type = FormFieldType.checkbox};
                ctx.FormFields.Add( wifiTv );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {CategoryId = eTvSchermi.Id, FormFieldId = wifiTv.Id} );
                ctx.SaveChanges();


                var portaUsb = new FormField {Name = "Porta Usb", Type = FormFieldType.checkbox};
                ctx.FormFields.Add( portaUsb );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {
                    CategoryId = eTvSchermi.Id,
                    FormFieldId = portaUsb.Id
                } );
                ctx.SaveChanges();


                var portaVga = new FormField {Name = "Porta Vga", Type = FormFieldType.checkbox};
                ctx.FormFields.Add( portaVga );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {
                    CategoryId = eTvSchermi.Id,
                    FormFieldId = portaVga.Id
                } );
                ctx.SaveChanges();

                var portaHdmi = new FormField {Name = "Porta Hdmi", Type = FormFieldType.checkbox};
                ctx.FormFields.Add( portaHdmi );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {
                    CategoryId = eTvSchermi.Id,
                    FormFieldId = portaHdmi.Id
                } );
                ctx.SaveChanges();

                var portaDvi = new FormField {Name = "Porta Dvi", Type = FormFieldType.checkbox};
                ctx.FormFields.Add( portaDvi );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {
                    CategoryId = eTvSchermi.Id,
                    FormFieldId = portaDvi.Id
                } );
                ctx.SaveChanges();

                var portaEthernet = new FormField {Name = "Porta Ethernet", Type = FormFieldType.checkbox};
                ctx.FormFields.Add( portaEthernet );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {
                    CategoryId = eTvSchermi.Id,
                    FormFieldId = portaEthernet.Id
                } );
                ctx.SaveChanges();


                var tipoAudio = new FormField {Name = "Tipo Audio ", Type = FormFieldType.select};
                ctx.FormFields.Add( tipoAudio );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {CategoryId = eAudio.Id, FormFieldId = tipoAudio.Id} );
                ctx.SaveChanges();


                ctx.FieldDefaultValues.AddRange( new FieldDefaultValue {Value = "Ipod", FormField = tipoAudio},
                    new FieldDefaultValue {Value = "Lettore Mp3", FormField = tipoAudio},
                    new FieldDefaultValue {Value = "Casse", FormField = tipoAudio},
                    new FieldDefaultValue {Value = "Karaoke", FormField = tipoAudio},
                    new FieldDefaultValue {Value = "Microfono", FormField = tipoAudio},
                    new FieldDefaultValue {Value = "Testo", FormField = tipoAudio},
                    new FieldDefaultValue {Value = "Subwoofer", FormField = tipoAudio},
                    new FieldDefaultValue {Value = "Altro", FormField = tipoAudio} );
                ctx.SaveChanges();

                var tipo = new FormField {Name = "Tipo", Type = FormFieldType.text};
                ctx.FormFields.Add( tipo );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {CategoryId = ePeriferiche.Id, FormFieldId = tipo.Id} );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {CategoryId = eAccessori.Id, FormFieldId = tipo.Id} );
                ctx.SaveChanges();


                var sessoB = new FormField {Name = "Genere", Type = FormFieldType.select};
                ctx.FormFields.Add( sessoB );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {
                    CategoryId = bAbbigliamento.Id,
                    FormFieldId = sessoB.Id
                } );
                ctx.SaveChanges();


                ctx.FieldDefaultValues.AddRange( new FieldDefaultValue {Value = "Maschio", FormField = sessoB},
                    new FieldDefaultValue {Value = "Femmina", FormField = sessoB} );
                ctx.SaveChanges();


                var tagliaB = new FormField {Name = "Taglia Bambino", Type = FormFieldType.select};
                ctx.FormFields.Add( tagliaB );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {
                    CategoryId = bAbbigliamento.Id,
                    FormFieldId = tagliaB.Id
                } );
                ctx.SaveChanges();


                ctx.FieldDefaultValues.AddRange( new FieldDefaultValue {Value = "1 Mese", FormField = tagliaB},
                    new FieldDefaultValue {Value = "3 Mesi", FormField = tagliaB},
                    new FieldDefaultValue {Value = "6 Mesi", FormField = tagliaB},
                    new FieldDefaultValue {Value = "12 Mesi", FormField = tagliaB},
                    new FieldDefaultValue {Value = "18 Mesi", FormField = tagliaB},
                    new FieldDefaultValue {Value = "24 Mesi", FormField = tagliaB},
                    new FieldDefaultValue {Value = "36 Mesi", FormField = tagliaB} );
                ctx.SaveChanges();


                var tipoMobilioB = new FormField {Name = "Tipo Mobilio", Type = FormFieldType.select};
                ctx.FormFields.Add( tipoMobilioB );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {
                    CategoryId = bMobilio.Id,
                    FormFieldId = tipoMobilioB.Id
                } );
                ctx.SaveChanges();


                ctx.FieldDefaultValues.AddRange(
                    new FieldDefaultValue {Value = "Sdraietta/Altalena/Poltroncina", FormField = tipoMobilioB},
                    new FieldDefaultValue {Value = "Box", FormField = tipoMobilioB},
                    new FieldDefaultValue {Value = "Culla", FormField = tipoMobilioB},
                    new FieldDefaultValue {Value = "Lettino", FormField = tipoMobilioB},
                    new FieldDefaultValue {Value = "Cuscino", FormField = tipoMobilioB},
                    new FieldDefaultValue {Value = "Seggiolone", FormField = tipoMobilioB},
                    new FieldDefaultValue {Value = "Fasciaotio", FormField = tipoMobilioB},
                    new FieldDefaultValue {Value = "Altro", FormField = tipoMobilioB} );

                ctx.SaveChanges();

                var tipoAccessoriB = new FormField {Name = "Tipo Accessorio", Type = FormFieldType.select};
                ctx.FormFields.Add( tipoAccessoriB );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {
                    CategoryId = bAccessori.Id,
                    FormFieldId = tipoAccessoriB.Id
                } );
                ctx.SaveChanges();


                ctx.FieldDefaultValues.AddRange(
                    new FieldDefaultValue {Value = "Tiralatte", FormField = tipoAccessoriB},
                    new FieldDefaultValue {Value = "Biberon", FormField = tipoAccessoriB},
                    new FieldDefaultValue {Value = "ScaldaBiberon", FormField = tipoAccessoriB},
                    new FieldDefaultValue {Value = "Sterilizzatore", FormField = tipoAccessoriB},
                    new FieldDefaultValue {Value = "Cuoci Pappa", FormField = tipoAccessoriB},
                    new FieldDefaultValue {Value = "Aereosol", FormField = tipoAccessoriB},
                    new FieldDefaultValue {Value = "Umidificatore", FormField = tipoAccessoriB},
                    new FieldDefaultValue {Value = "Termometro", FormField = tipoAccessoriB},
                    new FieldDefaultValue {Value = "Ciuccio", FormField = tipoAccessoriB},
                    new FieldDefaultValue {Value = "Baby Monitor", FormField = tipoAccessoriB},
                    new FieldDefaultValue {Value = "Passeggino", FormField = tipoAccessoriB},
                    new FieldDefaultValue {Value = "Seggiolino Automobile", FormField = tipoAccessoriB},
                    new FieldDefaultValue {Value = "Accessori Bagno", FormField = tipoAccessoriB},
                    new FieldDefaultValue {Value = "Accessori Auto", FormField = tipoAccessoriB},
                    new FieldDefaultValue {Value = "Posate", FormField = tipoAccessoriB},
                    new FieldDefaultValue {Value = "Altro", FormField = tipoAccessoriB} );

                ctx.SaveChanges();

                var specieAnimale = new FormField {Name = "Specie", Type = FormFieldType.select};
                ctx.FormFields.Add( specieAnimale );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {
                    CategoryId = anAnimali.Id,
                    FormFieldId = specieAnimale.Id
                } );
                ctx.SaveChanges();


                ctx.FieldDefaultValues.AddRange( new FieldDefaultValue {Value = "Cane", FormField = specieAnimale},
                    new FieldDefaultValue {Value = "Gatto", FormField = specieAnimale},
                    new FieldDefaultValue {Value = "Pesci", FormField = specieAnimale},
                    new FieldDefaultValue {Value = "Uccello", FormField = specieAnimale},
                    new FieldDefaultValue {Value = "Roditore", FormField = specieAnimale},
                    new FieldDefaultValue {Value = "Rettile", FormField = specieAnimale} );

                ctx.SaveChanges();


                var razzaAnimale = new FormField {Name = "Razza", Type = FormFieldType.text};
                ctx.FormFields.Add( razzaAnimale );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {
                    CategoryId = anAnimali.Id,
                    FormFieldId = razzaAnimale.Id
                } );
                ctx.SaveChanges();

                var etaAnimale = new FormField {Name = "Eta' Animale", Type = FormFieldType.text};
                ctx.FormFields.Add( etaAnimale );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {
                    CategoryId = anAnimali.Id,
                    FormFieldId = etaAnimale.Id
                } );
                ctx.SaveChanges();

                var tagliaAnimale = new FormField {Name = "Taglia Animale", Type = FormFieldType.select};
                ctx.FormFields.Add( tagliaAnimale );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {
                    CategoryId = anAnimali.Id,
                    FormFieldId = tagliaAnimale.Id
                } );
                ctx.SaveChanges();


                ctx.FieldDefaultValues.AddRange( new FieldDefaultValue {Value = "Mini", FormField = tagliaAnimale},
                    new FieldDefaultValue {Value = "Piccola", FormField = tagliaAnimale},
                    new FieldDefaultValue {Value = "Media", FormField = tagliaAnimale},
                    new FieldDefaultValue {Value = "Grande", FormField = tagliaAnimale},
                    new FieldDefaultValue {Value = "Gigante", FormField = tagliaAnimale} );

                ctx.SaveChanges();


                var lunghezzaPelo = new FormField {Name = "Lunghezza Pelo", Type = FormFieldType.select};
                ctx.FormFields.Add( lunghezzaPelo );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {
                    CategoryId = anAnimali.Id,
                    FormFieldId = lunghezzaPelo.Id
                } );
                ctx.SaveChanges();

                ctx.FieldDefaultValues.AddRange( new FieldDefaultValue {Value = "Assente", FormField = lunghezzaPelo},
                    new FieldDefaultValue {Value = "Corto", FormField = lunghezzaPelo},
                    new FieldDefaultValue {Value = "Medio", FormField = lunghezzaPelo},
                    new FieldDefaultValue {Value = "Lungo", FormField = lunghezzaPelo} );

                ctx.SaveChanges();


                var tipoAccessoriAnimale = new FormField {Name = "Tipo Accessorio", Type = FormFieldType.select};
                ctx.FormFields.Add( tipoAccessoriAnimale );
                ctx.SaveChanges();

                ctx.CategoryFormFields.Add( new CategoryFormField {
                    CategoryId = anAccessori.Id,
                    FormFieldId = tipoAccessoriAnimale.Id
                } );
                ctx.SaveChanges();

                ctx.FieldDefaultValues.AddRange(
                    new FieldDefaultValue {Value = "Abbigliamento", FormField = tipoAccessoriAnimale},
                    new FieldDefaultValue {Value = "Collare", FormField = tipoAccessoriAnimale},
                    new FieldDefaultValue {Value = "Pettorina", FormField = tipoAccessoriAnimale},
                    new FieldDefaultValue {Value = "Giochi", FormField = tipoAccessoriAnimale},
                    new FieldDefaultValue {Value = "Cuccia", FormField = tipoAccessoriAnimale},
                    new FieldDefaultValue {Value = "Cuscino", FormField = tipoAccessoriAnimale},
                    new FieldDefaultValue {Value = "Trasportino", FormField = tipoAccessoriAnimale},
                    new FieldDefaultValue {Value = "Acquario", FormField = tipoAccessoriAnimale},
                    new FieldDefaultValue {Value = "Gabbia", FormField = tipoAccessoriAnimale},
                    new FieldDefaultValue {Value = "Terrario", FormField = tipoAccessoriAnimale},
                    new FieldDefaultValue {Value = "Altro", FormField = tipoAccessoriAnimale} );

                ctx.SaveChanges();
            }
        }
    }
}