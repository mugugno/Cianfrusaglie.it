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
            ViewData["listUsers"] = _context.Users.ToList();
            ViewData[ "numberOfCategories" ] = _context.Categories.ToList().Count;
            ViewData["formCategories"] = _context.Categories.ToList();
            return View();
        }

        // GET: Announces/Details/5
        public IActionResult Details( int? id ) {
            if( id == null ) {
                return HttpNotFound();
            }

            Announce announce = _context.Announces.SingleOrDefault( m => m.Id == id );
            if( announce == null ) {
                return HttpNotFound();
            }
            var announceFormFieldsvalues = _context.AnnounceFormFieldsValues.Where(af => af.AnnounceId == id).ToList();
            Dictionary<FormField, string> dictionary = new Dictionary<FormField, string>();
            foreach (var f in announceFormFieldsvalues)
            {
                var formField=(_context.FormFields.Single(ff=> ff.Id.Equals(f.FormFieldId)));
                dictionary.Add(formField, f.Value);
            }
            ViewData["formCategories"] = _context.Categories.ToList();
            ViewData["numberOfCategories"] = _context.Categories.ToList().Count;
            ViewData["formFieldsValue"] = dictionary;
            ViewData["Images"] = _context.ImageUrls.Where(i => i.Announce.Equals(announce)).ToList();
            ViewData["IdAnnounce"] = id;
            ViewData["AuthorId"] = announce.AuthorId;
            ViewData["Autore"] =
                _context.Users.Where(u => u.Id == announce.AuthorId).Select(u => u.UserName).SingleOrDefault();

            return View( announce );
        }

        // GET: Announces/Create
        public IActionResult Create()
        {
            ViewData["formCategories"] = _context.Categories.ToList();
            ViewData["numberOfCategories"] = _context.Categories.ToList().Count;
            ViewData["listUsers"] = _context.Users.ToList();
            ViewData["listAnnounces"] = _context.Announces.OrderBy(u => u.PublishDate).Take(4).ToList();
            ViewData["formCategories"] = _context.Categories.ToList();
            ViewData["numberOfCategories"] = _context.Categories.ToList().Count;
            ViewData["formFields"] = _context.FormFields.ToList();
            ViewData["formMacroCategories"] = _context.Categories.ToList();
            ViewData["numberOfMacroCategories"] = _context.Categories.ToList().Count;
            //TODO scrivere in maniera più furba ma ora va benissimo così!
            SetViewDataForCreateAction();
            return View();
        }

        private void SetViewDataForCreateAction()
        {
            
            var formField2CategoriesDictionary = new Dictionary<int, List<Category>>();
            foreach (FormField formField in _context.FormFields.ToList())
            {
                List<Category> categories =
                    _context.CategoryFormFields.Where(cf => cf.FormFieldId == formField.Id).Select(o => o.Category)
                        .ToList();
                formField2CategoriesDictionary.Add(formField.Id, categories);
            }
            ViewData["formField2CategoriesDictionary"] = formField2CategoriesDictionary;
        }

        // POST: Announces/Create
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( CreateAnnounceViewModel model ) {

            //TODO: Aggiungere i campi della risposta di errore.

            if ( ModelState.IsValid ) {
                if( !LoginChecker.HasLoggedUser( this ) )
                    return HttpBadRequest();
                //Upload delle foto
                string uploads = Path.Combine(_environment.WebRootPath, "images");
                var imagesUrls = new List< string >();
                foreach (var file in model.Photos)
                {
                    if (file.Length > 0)
                    {
                        var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                        imagesUrls.Add(@"\images\"+fileName);
                        await file.SaveAsAsync(Path.Combine(uploads, fileName));
                    }
                }
                //Fine upload delle immagini
                string idlogged = User.GetUserId();
                User author = _context.Users.First( u => u.Id.Equals( idlogged ) );
                var newAnnounce = new Announce {
                    PublishDate = DateTime.Now,
                    Title = model.Title,
                    Description = model.Description,
                    MeterRange = model.Range,
                    Author = author
                };
                
                _context.Announces.Add( newAnnounce );
                foreach (string url in imagesUrls) {
                    _context.Add( new ImageUrl {Announce = newAnnounce, Url = url} );
                }
                if (model.FormFieldDictionary != null)
                    foreach( var kvPair in model.FormFieldDictionary ) {
                        if( !string.IsNullOrEmpty( kvPair.Value ) ) {
                            _context.AnnounceFormFieldsValues.Add( new AnnounceFormFieldsValues {
                                FormFieldId = kvPair.Key,
                                Value = kvPair.Value,
                                AnnounceId = newAnnounce.Id
                            } );
                        }
                    }
                if(model.CategoryDictionary != null)
                    foreach( var kvPair in model.CategoryDictionary ) {
                        if( kvPair.Value ) {
                            _context.AnnounceCategories.Add( new AnnounceCategory {
                                AnnounceId = newAnnounce.Id,
                                CategoryId = kvPair.Key
                            } );
                        }
                    }
                _context.SaveChanges();
                TempData[ "announceCreated" ] = "Il tuo annuncio è stato creato correttamente!";
                return RedirectToAction( nameof( HomeController.Index ), "Home" );
            }
            SetViewDataForCreateAction();
            return View( model );

            //return Redirect( "Create" );

        }

        // GET: Announces/Edit/5
        public IActionResult Edit( int? id ) {
            if( id == null ) {
                return HttpNotFound();
            }
            ViewData["formCategories"] = _context.Categories.ToList();
            ViewData["numberOfCategories"] = _context.Categories.ToList().Count;
            Announce announce = _context.Announces.SingleOrDefault( m => m.Id == id );
            if( announce == null ) {
                return HttpNotFound();
            }
            return View( announce );
        }

        // POST: Announces/Edit/5
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Edit( Announce announce ) {
            //TODO: Aggiungere i campi della risposta di errore.
            if( !LoginChecker.HasLoggedUser( this ) )
                return HttpBadRequest();
            if( !User.GetUserId().Equals( announce.AuthorId ) ) {
                return HttpBadRequest();
            }
            if( ModelState.IsValid ) {
                _context.Update( announce );
                _context.SaveChanges();
                return RedirectToAction( "Index" );
            }
            return View( announce );
        }

        // GET: Announces/Delete/5
        [ActionName( "Delete" )]
        public IActionResult Delete( int? id ) {
            if( id == null ) {
                return HttpNotFound();
            }

            Announce announce = _context.Announces.SingleOrDefault( m => m.Id == id );
            if( announce == null ) {
                return HttpNotFound();
            }

            return View( announce );
        }

        // POST: Announces/Delete/5
        [HttpPost, ActionName( "Delete" ), ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed( int id ) {
            Announce announce = _context.Announces.SingleOrDefault( m => m.Id == id );
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
