using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Cianfrusaglie.Models;
using Cianfrusaglie.Statics;
using Cianfrusaglie.ViewModels.Announce;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc;
using Microsoft.Net.Http.Headers;

namespace Cianfrusaglie.Controllers {
    public class AnnouncesController : Controller {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _environment;

        public AnnouncesController( ApplicationDbContext context, IHostingEnvironment environment ) {
            _context = context;
            _environment = environment;
        }

        // GET: Announces
        public IActionResult Index() {
            ViewData[ "listUsers" ] = _context.Users.ToList();
            ViewData[ "numberOfCategories" ] = _context.Categories.ToList().Count;
            ViewData[ "formCategories" ] = _context.Categories.ToList();
            return View();
        }

        // GET: Announces/Details/5
        public IActionResult Details( int? id ) {
            if( id == null ) {
                return HttpNotFound();
            }

            var announce = _context.Announces.SingleOrDefault( m => m.Id == id );
            if( announce == null ) {
                return HttpNotFound();
            }
            var announceFormFieldsvalues = _context.AnnounceFormFieldsValues.Where( af => af.AnnounceId == id ).ToList();
            var dictionary = new Dictionary< FormField, string >();
            foreach( var f in announceFormFieldsvalues ) {
                var formField = _context.FormFields.Single( ff => ff.Id.Equals( f.FormFieldId ) );
                dictionary.Add( formField, f.Value );
            }
            ViewData[ "formCategories" ] = _context.Categories.ToList();
            ViewData[ "numberOfCategories" ] = _context.Categories.ToList().Count;
            ViewData[ "formFieldsValue" ] = dictionary;
            ViewData[ "Images" ] = _context.ImageUrls.Where( i => i.Announce.Equals( announce ) ).ToList();
            ViewData[ "IdAnnounce" ] = id;
            ViewData[ "AuthorId" ] = announce.AuthorId;
            ViewData[ "Autore" ] =
                _context.Users.Where( u => u.Id == announce.AuthorId ).Select( u => u.UserName ).SingleOrDefault();

            return View( announce );
        }

        // GET: Announces/Create
        public IActionResult Create() {
            //TODO: Aggiungere i campi della risposta di errore.
            if( !LoginChecker.HasLoggedUser( this ) )
                return HttpBadRequest();
            ViewData[ "formCategories" ] = _context.Categories.ToList();
            ViewData[ "numberOfCategories" ] = _context.Categories.ToList().Count;
            ViewData[ "listUsers" ] = _context.Users.ToList();
            ViewData[ "listAnnounces" ] = _context.Announces.OrderBy( u => u.PublishDate ).Take( 4 ).ToList();
            ViewData[ "formCategories" ] = _context.Categories.ToList();
            ViewData[ "numberOfCategories" ] = _context.Categories.ToList().Count;
            ViewData[ "formFields" ] = _context.FormFields.ToList();
            ViewData[ "formMacroCategories" ] = _context.Categories.ToList();
            ViewData[ "numberOfMacroCategories" ] = _context.Categories.ToList().Count;
            //TODO scrivere in maniera più furba ma ora va benissimo così!
            SetViewData();
            return View();
        }

        private void SetViewData() {
            var formField2CategoriesDictionary = new Dictionary< int, List< Category > >();
            foreach( var formField in _context.FormFields.ToList() ) {
                var categories =
                    _context.CategoryFormFields.Where( cf => cf.FormFieldId == formField.Id ).Select( o => o.Category )
                        .ToList();
                formField2CategoriesDictionary.Add( formField.Id, categories );
            }
            ViewData[ "formField2CategoriesDictionary" ] = formField2CategoriesDictionary;
        }

        // POST: Announces/Create
        [HttpPost, ValidateAntiForgeryToken]
        public async Task< IActionResult > Create( CreateAnnounceViewModel model ) {
            //TODO: Aggiungere i campi della risposta di errore.
            if( !LoginChecker.HasLoggedUser( this ) )
                return HttpBadRequest();
            if( ModelState.IsValid ) {
                string idlogged = User.GetUserId();
                var author = _context.Users.First( u => u.Id.Equals( idlogged ) );
                var newAnnounce = new Announce {
                    PublishDate = DateTime.Now,
                    Title = model.Title,
                    Description = model.Description,
                    MeterRange = model.Range,
                    Author = author
                };
                _context.Announces.Add( newAnnounce );
                _context.SaveChanges();

                //Upload delle foto
                string uploads = Path.Combine( _environment.WebRootPath, "images" );
                foreach( var file in model.Photos ) {
                    if( file.Length <= 0 )
                        continue;

                    var imgUrl = new ImageUrl {Announce = newAnnounce, Url = ""};
                    _context.Add( imgUrl );
                    _context.SaveChanges();

                    string fileName = ContentDispositionHeaderValue.Parse( file.ContentDisposition ).FileName.Trim( '"' );
                    fileName = fileName.Replace( Path.GetFileNameWithoutExtension( fileName ), "i" + imgUrl.Id );
                    await file.SaveAsAsync( Path.Combine( uploads, fileName ) );

                    imgUrl.Url = @"/images/" + fileName;
                }
                //Fine upload delle immagini

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

                TempData[ "announceCreated" ] = true;
                return RedirectToAction( nameof( HomeController.Index ), "Home" );
            }
            SetViewData();
            return View( model );

            //return Redirect( "Create" );
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
            ViewData[ "numberOfCategories" ] = _context.Categories.ToList().Count;
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
            SetViewData();
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

        // GET: Announces/Delete/5
        [ActionName( "Delete" )]
        public IActionResult Delete( int? id ) {
            if( id == null ) {
                return HttpNotFound();
            }
            //TODO: Aggiungere i campi della risposta di errore.
            if( !LoginChecker.HasLoggedUser( this ) )
                return HttpBadRequest();
            var announce = _context.Announces.SingleOrDefault( m => m.Id == id );
            if( announce == null ) {
                return HttpNotFound();
            }

            return View( announce );
        }

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
            _context.Announces.Remove( announce );
            _context.SaveChanges();
            return RedirectToAction( "Index" );
        }

        public IActionResult SubmitAnnounce() { return View(); }
    }
}