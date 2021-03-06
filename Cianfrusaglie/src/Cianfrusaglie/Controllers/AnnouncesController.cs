using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Cianfrusaglie.Constants;
using Cianfrusaglie.Models;
using Cianfrusaglie.Statics;
using Cianfrusaglie.ViewModels.Announce;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using Microsoft.Net.Http.Headers;
using static Cianfrusaglie.Constants.CommonFunctions;

namespace Cianfrusaglie.Controllers {
    public class AnnouncesController : Controller {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _environment;

        public AnnouncesController( ApplicationDbContext context, IHostingEnvironment environment ) {
            _context = context;
            _environment = environment;
        }

       /// <summary>
       ///     Genera i Gat a partire da un annuncio.
       /// </summary>
       /// <param name="announce">L'annuncio appena creato.</param>
       /// <returns>I Gat relativi all'annuncio</returns>
       public IEnumerable< Gat > GenerateGats( Announce announce ) {
          return GenerateGats( _context, announce );
       }

       public static IEnumerable< Gat > GenerateGats( ApplicationDbContext context, Announce announce ) {
         var formFieldsValues = context.AnnounceFormFieldsValues.Where( a => a.AnnounceId.Equals( announce.Id ) );
         foreach( var fieldsValue in formFieldsValues ) {
            yield return new Gat { Text = GetStringFromAnnounceFormField( fieldsValue ) };
         }
      } 

        /// <summary>
        ///     Dato un AnnounceFormField, genera il corrispettivo valore stringa.
        /// </summary>
        /// <param name="formField">L'announceFormField da trattare.</param>
        /// <returns>Il valore in formato stringa per inserirlo nel DB.</returns>
        public static string GetStringFromAnnounceFormField( AnnounceFormFieldsValues formField ) {
            return formField.Value;
        }

        //TODO Metodo copiato e incollato da FeedbackController! Refactoring!!!!
        private bool IsUserChoosenForTheAnnounce(int announceId, string userId) {
            var announce = _context.Announces.Include(a => a.ChosenUsers).SingleOrDefault(a => a.Id == announceId);
            return announce != null && announce.ChosenUsers.Any(chosen => chosen.ChosenUserId == userId);
        }

        /// <summary>
        /// Effettua l'upload delle immagini per un determinato annuncio
        /// </summary>
        /// <param name="formFiles">immagini dal form</param>
        /// <param name="announce">l'annuncio</param>
        /// <returns></returns>
        private async Task UploadAnnounceImages( ICollection< IFormFile > formFiles, Announce announce ) {
            string uploads = Path.Combine( _environment.WebRootPath, "upload" );
            foreach( var file in formFiles ) {
                if( file.ContentType != "image/png" && file.ContentType != "image/jpeg" )
                    continue;
                if( file.Length <= 0 )
                    continue;

                var imgUrl = new ImageUrl {Announce = announce, Url = ""};
                _context.Add( imgUrl );
                _context.SaveChanges();

                string fileName = ContentDispositionHeaderValue.Parse( file.ContentDisposition ).FileName.Trim( '"' );
                fileName = "i" + imgUrl.Id + Path.GetExtension( fileName );
                await file.SaveAsAsync( Path.Combine( uploads, fileName ) );

                imgUrl.Url = @"/upload/" + fileName;
            }
        }

        private void SetViewDataForCreate( bool vendita ) {
            CommonFunctions.SetRootLayoutViewData( this, _context );
            ViewData[ "formCategories" ] = _context.Categories.ToList();
            ViewData[ "formFields" ] = _context.FormFields.Include( p => p.DefaultValues ).Include( p => p.CategoriesFormFields ).ToList();
            CommonFunctions.SetMacroCategoriesViewData( this, _context );
            ViewData[ "isVendita" ] = vendita;

            ViewData[ "loggedUser" ] = _context.Users.Single( u => u.Id == User.GetUserId() );
        }

        /// <summary>
        ///     Questo metodo carica la pagina degli annunci.
        /// </summary>
        /// <returns>La View con tutti gli annunci.</returns>
        // GET: Announces
        public IActionResult Index() {
            return RedirectToAction( nameof( HistoryController.Index ), "History" );
        }

        /// <summary>
        ///     Dato un ID, ritorna una View contenente i dettagli dell'annuncio collegato a quell'ID.
        ///     In caso di utente non loggato, ritorna una BadRequest.
        ///     Se ID non esiste allora viene ritornato un HttpNotFound.
        /// </summary>
        /// <param name="id">Id dell'annuncio scelto.</param>
        /// <returns>La View contenente i dettagli dell'annuncio.</returns>
        // GET: Announces/Details/5
        public IActionResult Details( int? id ) {
            /*if( !LoginChecker.HasLoggedUser( this ) )
                return HttpBadRequest();*/
            if( id == null ) {
                return HttpNotFound();
            }
            var announce = _context.Announces.Include( u => u.Interested ).SingleOrDefault( m => m.Id == id );
            if( announce == null ) {
                return HttpNotFound();
            }
            var announceFormFieldsvalues = _context.AnnounceFormFieldsValues.Where( af => af.AnnounceId == id ).ToList();
            var formFieldsValue = new Dictionary< FormField, string >();
            foreach ( var f in announceFormFieldsvalues ) {
                var formField = _context.FormFields.Single( ff => ff.Id.Equals( f.FormFieldId ) );
                formFieldsValue.Add( formField, f.Value );
            }
            CommonFunctions.SetRootLayoutViewData( this,_context );
            ViewData[ "formFieldsValue" ] = formFieldsValue;
            ViewData[ "Images" ] = _context.ImageUrls.Where( i => i.Announce.Equals( announce ) ).ToList();
            ViewData[ "IdAnnounce" ] = id;
            ViewData[ "Author" ] = _context.Users.First( u => u.Id.Equals( announce.AuthorId ) );
            ViewData["AuthorId"] = ((User)ViewData["Author"]).Id;
            if(LoginChecker.HasLoggedUser(this))
                ViewData[ "loggedUser" ] = _context.Users.Single( u => u.Id == User.GetUserId() );
            if ( announce.Interested != null )
                ViewData[ "int" ] =
                    announce.Interested.Where( c => c.UserId.Equals( User.GetUserId() ) ).Select( u => u.UserId )
                        .SingleOrDefault();
            if (ViewData["int"] != null)
                ViewData["interested"] = true;
            else
                ViewData["interested"] = false;

            //ViewData Dato un annuncio ho tutti i nomi delle categorie di cui fa parte
            var announces = _context.Announces.Include(u => u.AnnounceCategories).Include(p=>p.Author).Single(a => a.Id == announce.Id);

            var annCat = announces.AnnounceCategories.ToList();
            foreach( var ac in annCat )
              ac.Category = _context.Categories.Single( c => c.Id == ac.CategoryId );
            ViewData["nameAnnounceCategories"] = annCat;
            
            ViewData["numInterested"] = announce.Interested.Count;
            ViewData[ "choosen" ] = IsUserChoosenForTheAnnounce( (int) id, User.GetUserId() );
            ViewData["someoneIsChoosen"] = announce.Interested.Where(u => u.ChooseDate != null).SingleOrDefault();
            ViewData["feedbackGiven"] = _context.FeedBacks.Where(u => u.AuthorId.Equals(User.GetUserId()) && u.AnnounceId.Equals(announce.Id)).SingleOrDefault();
            return View( announce );
        }

        /// <summary>
        ///     Visualizza la pagina per la creazione di un annuncio.
        ///     In caso l'utente non sia loggato, ritorna una Bad Request.
        /// </summary>
        /// <returns>La pagina contenente i campi per la creazione di un annuncio.</returns>
        // GET: Announces/Create
        public IActionResult Create( bool vendita = false ) {
            //TODO: Aggiungere i campi della risposta di errore.
            if( !LoginChecker.HasLoggedUser( this ) )
                return HttpBadRequest();
            SetViewDataForCreate( vendita );
            return View();
        }


        /// <summary>
        ///     Dato un modulo compilato per la creazione di un annuncio, rende persistente tale creazione, dopodich� torna alla
        ///     Home.
        ///     Se l'utente non � loggato, ritorna una BadRequest.
        ///     Se il modello non � valido allora rimane sulla pagina della creazione.
        /// </summary>
        /// <param name="model">Il modulo compilato con i dati necessari alla creazione.</param>
        /// <returns>Ritorna un Redirect alla Home</returns>
        // POST: Announces/Create
        [HttpPost, ValidateAntiForgeryToken]
        public async Task< IActionResult > Create( CreateAnnounceViewModel model ) {
            //TODO: Aggiungere i campi della risposta di errore.
            if( !LoginChecker.HasLoggedUser( this ) )
                return HttpBadRequest();
            if( model.Photos.Count < 1 ) {
                ModelState.AddModelError( "Photos", "Devi inserire almeno un immagine" );
            }
            foreach( var formFile in model.Photos ) {
                if( formFile.ContentType != "image/png" && formFile.ContentType != "image/jpeg" )
                    ModelState.AddModelError( "Photos", "I file devono essere delle immagini!" );
                if( formFile.Length > DomainConstraints.AnnouncePhotosMaxLenght ) {
                    ModelState.AddModelError( "Photos",
                        "Non puoi inserire immagini superiori a " + DomainConstraints.AnnouncePhotosMaxLenght / 1000000 +
                        " MB" );
                }
            }

            if( ModelState.IsValid ) {
                string idlogged = User.GetUserId();
                var author = _context.Users.First( u => u.Id.Equals( idlogged ) );
                var newAnnounce = new Announce {
                    PublishDate = DateTime.Now,
                    Title = model.Title,
                    Description = model.Description,
                    Latitude = double.Parse( model.Latitude, CultureInfo.InvariantCulture ),
                    Longitude = double.Parse( model.Longitude, CultureInfo.InvariantCulture ),
                    MeterRange = model.Range,
                    Author = author,
                    Price = model.Price
                };
                _context.Announces.Add( newAnnounce );
                _context.SaveChanges();

                await UploadAnnounceImages( model.Photos, newAnnounce );

                if( model.FormFieldDictionary != null )
                    foreach( var kvPair in model.FormFieldDictionary ) {
                        if( !string.IsNullOrEmpty( kvPair.Value ) ) {
                            _context.AnnounceFormFieldsValues.Add( new AnnounceFormFieldsValues {
                                FormFieldId = kvPair.Key,
                                Value = kvPair.Value,
                                AnnounceId = newAnnounce.Id
                            } );
                        }
                    }

                if( model.CheckboxFormFieldDictionary != null )
                    foreach( var kvPair in model.CheckboxFormFieldDictionary ) {
                        if( !string.IsNullOrEmpty( kvPair.Value.ToString() ) ) {
                            if( kvPair.Value ) {
                                _context.AnnounceFormFieldsValues.Add( new AnnounceFormFieldsValues {
                                    FormFieldId = kvPair.Key,
                                    Value = "si",
                                    AnnounceId = newAnnounce.Id
                                } );
                            }
                        }
                    }

                if( model.SelectFormFieldDictionary != null )
                    foreach( var kvPair in model.SelectFormFieldDictionary ) {
                        if( !string.IsNullOrEmpty( kvPair.Value ) ) {
                            _context.AnnounceFormFieldsValues.Add( new AnnounceFormFieldsValues {
                                FormFieldId = kvPair.Key,
                                Value = kvPair.Value,
                                AnnounceId = newAnnounce.Id
                            } );
                        }
                    }

                if( model.CategoryDictionary != null )
                    foreach( var kvPair in model.CategoryDictionary ) {
                        if( kvPair.Value ) {
                            _context.AnnounceCategories.Add( new AnnounceCategory {
                                AnnounceId = newAnnounce.Id,
                                CategoryId = kvPair.Key
                            } );
                        }
                    }
                _context.SaveChanges();

                //Inserimento Gat
                var gats = GenerateGats( newAnnounce ).ToList();
                _context.Gats.AddRange( gats );
                _context.SaveChanges();
                foreach( var gat in gats ) {
                    var announceGat = new AnnounceGat {Announce = newAnnounce, Gat = gat};
                    _context.AnnounceGats.Add( announceGat );
                }
                _context.SaveChanges();
                TempData[ "announceCreated" ] = true;
                return RedirectToAction( nameof( HomeController.Index ), "Home" );
            }
            SetViewDataForCreate( model.Price!=0);
            return View( model );
        }

        /// <summary>
        ///     Aggiunge un utente agli interessati di un annuncio
        /// </summary>
        /// <param name="announceId"></param>
        /// <returns></returns>
        [HttpPost, ValidateAntiForgeryToken]
        public bool Interested( int? announceId ) {
            if( announceId == null ) {
                return false;
            }
            if( !LoginChecker.HasLoggedUser( this ) )
                return false;
            if( IsUserChoosenForTheAnnounce( (int) announceId, User.GetUserId() ) )
                return false;
            var userTmp = _context.Users.SingleOrDefault( c => c.Id.Equals( User.GetUserId() ) );
            var annTmp = _context.Announces.Include( u => u.Interested ).SingleOrDefault( c => c.Id == announceId );
            var announceGats = _context.AnnounceGats.Where( a => a.AnnounceId.Equals( announceId ) ).Select( a => a.Gat );
            var userGats = _context.UserGatHistograms.Where( u => u.UserId.Equals( User.GetUserId() ) );

            if( annTmp.Closed )
                return false;
            if( annTmp.AuthorId.Equals( User.GetUserId() ) )
                return false;
            Interested exis2 = null;
            if( annTmp.Interested != null )
                exis2 = annTmp.Interested.SingleOrDefault( c => c.ChooseDate != null );
            if( exis2 != null )
                return false;


            Interested exis = null;
            if( annTmp.Interested != null )
                exis = annTmp.Interested.SingleOrDefault( c => c.UserId.Equals( User.GetUserId() ) );
            if( exis == null ) {
                var interestedTmp = new Interested {User = userTmp, Announce = annTmp, DateTime = DateTime.Now};
                _context.Interested.Add( interestedTmp );
                ViewData["interested"] = true;

                _context.NotificationCenter.Add(new Notification
                {
                    User = _context.Users.Single(u=> u.Id.Equals(annTmp.AuthorId)),
                    TypeNotification = MessageTypeNotification.NewInterested
                });
                _context.SaveChanges();


                foreach( var gat in announceGats ) {
                    if( userGats.Select( a => a.Gat ).Contains( gat ) ) {
                        var userGatHistogram =
                            userGats.Single( a => a.UserId.Equals( User.GetUserId() ) && a.Gat.Equals( gat ) );
                        userGatHistogram.Count++;
                        _context.UserGatHistograms.Update( userGatHistogram );
                    } else {
                        var newGat = new UserGatHistogram {Count = 1, Gat = gat, User = userTmp};
                        _context.UserGatHistograms.Add( newGat );
                    }
                }

                _context.SaveChanges();
            } else {
                foreach( var gat in announceGats ) {
                    if( userGats.Select( a => a.Gat ).Contains( gat ) ) {
                        var userGatHistogram =
                            userGats.Single( a => a.UserId.Equals( User.GetUserId() ) && a.Gat.Equals( gat ) );
                        if( --userGatHistogram.Count <= 0 )
                            _context.UserGatHistograms.Remove( userGatHistogram );
                        else
                            _context.UserGatHistograms.Update( userGatHistogram );
                    }
                    //else {
                    //    var newGat = new UserGatHistogram() { Count = 1, Gat = gat, User = UserTmp };
                    //    _context.UserGatHistograms.Add(newGat);
                    //}
                }

                _context.Interested.Remove( exis );
                ViewData["interested"] = false;
                _context.SaveChanges();
            }

            return true;
        }


        // GET: Announces/Edit/5
        public IActionResult Edit( int? id ) {
            if( id == null ) {
                return HttpNotFound();
            }
            //TODO: BadRequest da trattare
            if( !LoginChecker.HasLoggedUser( this ) )
                return HttpBadRequest();
            var announce = _context.Announces.SingleOrDefault( m => m.Id == id );

            ViewData[ "formCategories" ] = _context.Categories.ToList();
            if( announce == null ) {
                return HttpNotFound();
            }
            //TODO: BadRequest da trattare
            if( !announce.AuthorId.Equals( User.GetUserId() ) )
                return HttpBadRequest();
            var formFieldDictionary = new Dictionary< int, string >();
            var categoryDictionary = new Dictionary< int, bool >();
            ICollection< IFormFile > photos = null; //TODO
            foreach( var formField in _context.FormFields ) {
                formFieldDictionary.Add( formField.Id, "" );
            }
            foreach( var formField in _context.AnnounceFormFieldsValues ) {
                if( formField.AnnounceId == announce.Id ) {
                    /*formFieldDictionary[ formField.FormFieldId ] = formField.Value;*/
                    formFieldDictionary[ formField.FormFieldId ] = formField.Value;
                }
            }
            /*foreach( var formField in announce.AnnouncesFormFields ) {
                formFieldDictionary[ formField.FormFieldId ] = formField.FormField.Name;
                formFieldValuesDictionary[ formField.FormFieldId ] = formField.Value;
            }*/
            foreach( var category in _context.Categories ) {
                categoryDictionary.Add( category.Id, false );
            }
            foreach( var category in _context.AnnounceCategories ) {
                if( category.AnnounceId == announce.Id ) {
                    /*categoryDictionary[ category.CategoryId ] = true;*/
                    categoryDictionary[ category.CategoryId ] = true;
                }
            }
            /*foreach( var category in announce.AnnounceCategories ) {
                categoryDictionary[ category.CategoryId ] = true;
            }*/
            var editAnnounce = new EditAnnounceViewModel {
                AnnounceId = (int) id,
                CategoryDictionary = categoryDictionary,
                Description = announce.Description,
                FormFieldDictionary = formFieldDictionary,
                Photos = photos,
                Range = announce.MeterRange,
                Title = announce.Title,
                AuthorId = announce.AuthorId
            };

            ViewData[ "formFields" ] = _context.FormFields.Include( p => p.DefaultValues ).Include( p => p.CategoriesFormFields ).ToList();

            return View( editAnnounce );
        }

        // POST: Announces/Edit/5
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Edit( EditAnnounceViewModel editAnnounceViewModel ) {
            //TODO: Aggiungere i campi della risposta di errore.
            if( !LoginChecker.HasLoggedUser( this ) )
                return HttpBadRequest();
            /*if( !User.GetUserId().Equals( editAnnounceViewModel.AuthorId ) ) {
                return HttpBadRequest();
            }*/
            if( ModelState.IsValid ) {
                string idlogged = User.GetUserId();
                var author = _context.Users.First( u => u.Id.Equals( idlogged ) );
                var newAnnounce = _context.Announces.SingleOrDefault( m => m.Id == editAnnounceViewModel.AnnounceId );
                newAnnounce.PublishDate = DateTime.Now;
                newAnnounce.Title = editAnnounceViewModel.Title;
                newAnnounce.Description = editAnnounceViewModel.Description;
                newAnnounce.MeterRange = editAnnounceViewModel.Range;
                newAnnounce.Author = author;
                newAnnounce.Price = editAnnounceViewModel.Price;
                _context.Announces.Update( newAnnounce );
                /*if (editAnnounceViewModel.FormFieldDictionary != null)
                    foreach (var kvPair in editAnnounceViewModel.FormFieldDictionary)
                    {
                        if (!string.IsNullOrEmpty(kvPair.Value)) {
                            var formFieldValue =_context.AnnounceFormFieldsValues.SingleOrDefault(
                                a => a.AnnounceId == newAnnounce.Id && a.FormFieldId == kvPair.Key );
                            formFieldValue.Value = kvPair.Value;
                            _context.AnnounceFormFieldsValues.Update( formFieldValue );
                        }
                    }
                if (editAnnounceViewModel.CategoryDictionary != null)
                    foreach (var kvPair in editAnnounceViewModel.CategoryDictionary)
                    {
                        if (kvPair.Value)
                        {
                            _context.AnnounceCategories.Update(new AnnounceCategory
                            {
                                AnnounceId = newAnnounce.Id,
                                CategoryId = kvPair.Key
                            });
                        }
                    }
                //_context.Announces.Update(newAnnounce);*/
                _context.SaveChanges();
                return RedirectToAction( "Index" );
            }
            return View( editAnnounceViewModel );
        }

        /// <summary>
        ///     Dato un ID, ritorna una View per la cancellazione di tale annuncio.
        ///     Se l'utente non � loggato, ritorna una BadRequest.
        ///     Se l'id non esiste, ritorna un HttpNotFound.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>La View per la cancellazione di un annuncio.</returns>
        // GET: Announces/Delete/5
        [ActionName( "Delete" )]
        public IActionResult Delete( int? id ) {
            if( id == null )
                return HttpNotFound();
            //TODO: Aggiungere i campi della risposta di errore.
            if( !LoginChecker.HasLoggedUser( this ) )
                return HttpBadRequest();
            var announce = _context.Announces.Include( a => a.Images ).SingleOrDefault( m => m.Id == id );
            if( announce == null )
                return HttpNotFound();
            if( !User.GetUserId().Equals( announce.AuthorId ) )
                return HttpBadRequest();

            CommonFunctions.SetRootLayoutViewData( this,_context );
            TempData["announceDeleted"] = true;
            return View( announce );
        }

        /// <summary>
        ///     Conferma la cancellazione di un annuncio dal sistema (rendendo la scelta persistente).
        ///     Se l'utente non � loggato allora ritorna una BadRequest.
        ///     Se l'id non esiste allora ritorna un HttpNotFound.
        /// </summary>
        /// <param name="id">L'id dell'annuncio da cancellare</param>
        /// <returns>Un Redirect all'indice di tutti gli annunci</returns>
        // POST: Announces/Delete/5
        [HttpPost, ActionName( "Delete" ), ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed( int id ) {
            var announce = _context.Announces.SingleOrDefault( m => m.Id == id );
            if( announce == null )
                return HttpBadRequest();
            //TODO: Aggiungere i campi della risposta di errore.
            if( !LoginChecker.HasLoggedUser( this ) )
                return HttpBadRequest();
            if( !User.GetUserId().Equals( announce.AuthorId ) )
                return HttpBadRequest();

            var im = _context.ImageUrls.Where( i => i.AnnounceId.Equals( announce.Id ) );
            _context.ImageUrls.RemoveRange( im );
            _context.Interested.RemoveRange( _context.Interested.Where( i => i.AnnounceId == announce.Id ) );
            _context.FeedBacks.RemoveRange(_context.FeedBacks.Where(f => f.AnnounceId == announce.Id));
            _context.Announces.Remove( announce );


            _context.SaveChanges();
            return RedirectToAction( nameof( HistoryController.Index ), "History" );
        }
    }
}