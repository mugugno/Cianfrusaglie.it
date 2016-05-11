using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Cianfrusaglie.Models;
using Cianfrusaglie.Statics;
using Cianfrusaglie.ViewModels.Announce;
using Microsoft.AspNet.Mvc;

namespace Cianfrusaglie.Controllers {
    public class AnnouncesController : Controller {
        private readonly ApplicationDbContext _context;

        public AnnouncesController( ApplicationDbContext context ) { _context = context; }

        // GET: Announces
        public IActionResult Index() {
            ViewData[ "formCategories" ] = _context.Categories.ToList();
            ViewData[ "numberOfCategories" ] = _context.Categories.ToList().Count;
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

            return View( announce );
        }

        // GET: Announces/Create
        public IActionResult Create()
        {
            //TODO scrivere in maniera più furba ma ora va benissimo così!
            SetViewDataForCreateAction();
            return View();
        }

        private void SetViewDataForCreateAction()
        {
            ViewData["formFields"] = _context.FormFields.ToList();
            ViewData["formMacroCategories"] = _context.Categories.ToList();
            ViewData["numberOfMacroCategories"] = _context.Categories.ToList().Count;
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
        public IActionResult Create( CreateAnnounceViewModel model ) {
            //TODO: Aggiungere i campi della risposta di errore.

            if( ModelState.IsValid ) {
                if( !LoginChecker.HasLoggedUser( this ) )
                    return HttpBadRequest();
                string idlogged = User.GetUserId();
                User author = _context.Users.First( u => u.Id.Equals( idlogged ) );
                var newAnnounce = new Announce {
                    Title = model.Title,
                    Description = model.Description,
                    MeterRange = model.Range,
                    Author = author
                };
                _context.Announces.Add( newAnnounce );
                if(model.FormFieldDictionary != null)
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
            if( !User.GetUserId().Equals( announce.Author.Id ) ) {
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
            if( !User.GetUserId().Equals( announce.Author.Id ) )
                return HttpBadRequest();
            _context.Announces.Remove( announce );
            _context.SaveChanges();
            return RedirectToAction( "Index" );
        }

        public IActionResult SubmitAnnounce() { return View(); }
    }
}
