using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Cianfrusaglie.Constants;
using Cianfrusaglie.Models;
using Cianfrusaglie.Services;
using Cianfrusaglie.Statics;
using Cianfrusaglie.ViewModels.Account;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using static Cianfrusaglie.Constants.CommonFunctions;
using System.Collections.Generic;
using Cianfrusaglie.ViewModels.Preference;

namespace Cianfrusaglie.Controllers
{
    public class PreferencesController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IHostingEnvironment _environment;


        public PreferencesController(ApplicationDbContext context, UserManager<User> userManager, IHostingEnvironment environment)
        {
            _context = context;
            _userManager = userManager;
            _environment = environment;
        }
        // GET: /<controller>/



        private async Task<string> UploadProfileImage(IFormFile formFile, User user)
        {
            string uploads = Path.Combine(_environment.WebRootPath, "upload");

            if (formFile.ContentType == "image/png" || formFile.ContentType == "image/jpeg")
            {
                if (formFile.Length > 0)
                {
                    string fileName = ContentDispositionHeaderValue.Parse(formFile.ContentDisposition).FileName.Trim('"');
                    fileName = user.Id + Path.GetExtension(fileName);
                    await formFile.SaveAsAsync(Path.Combine(uploads, fileName));

                    return @"/upload/" + fileName;
                }
            }
            return null;
        }


        public IActionResult Index(string passwordError)
        {
            CommonFunctions.SetRootLayoutViewData(this, _context);
            CommonFunctions.SetMacroCategoriesViewData(this, _context);

            var userCategories = _context.UserCategoryPreferenceses.Where(c => c.UserId.Equals(User.GetUserId()));
            var userPreferences = new Dictionary<int, bool>();
            foreach (var category in _context.Categories.ToList())
            {
                int check = 0;
                foreach (var userCategory in userCategories)
                {
                    if (category.Id.Equals(userCategory.CategoryId))
                    {
                        check = 1;
                        userPreferences.Add(category.Id, true);
                    }

                }
                if (check == 0)
                    userPreferences.Add(category.Id, false);
            }
            var user = _context.Users.Single(u => u.Id.Equals(User.GetUserId()));
            ViewData["user"] = user;
            ViewData["lat"] = user.Latitude;
            ViewData["long"] = user.Longitude;
            ViewData["userPreferences"] = userPreferences;
            if (passwordError == null)
                ViewData["errorePassword"] = "";
            else
                ViewData["errorePassword"] = passwordError;
            return View();
        }




        public async Task<IActionResult> Edit(PreferenceViewModel model)
        {
            var user = _context.Users.Single(u => u.Id.Equals(User.GetUserId()));
            var newUserPreferences = model.CategoryDictionary;
            var userPreferences = _context.UserCategoryPreferenceses.Where(c => c.UserId.Equals(User.GetUserId()));
            _context.UserCategoryPreferenceses.RemoveRange(userPreferences);
            _context.SaveChanges();
            string stringaErrorePassword="";

            if (model.VecchiaPassword != null)
            {
                if (model.Password != null && model.ConfirmPassword != null && !model.Password.Equals(model.ConfirmPassword))
                {
                    ModelState.AddModelError("Password", "Password e Conferma password devono essere uguali");
                    stringaErrorePassword = "Password e Conferma password devono essere uguali";
                }
                else if(model.Password != null && model.ConfirmPassword != null && model.Password.Equals(model.ConfirmPassword))
                {
                    var result = await _userManager.ChangePasswordAsync(user, model.VecchiaPassword, model.Password);
                    if(!result.Succeeded)
                    {
                        ModelState.AddModelError("Password", "Errori nel cambio password");
                        stringaErrorePassword = "Errori nel cambio password";
                    }
                }
                else if(model.Password == null || model.ConfirmPassword == null)
                {
                    ModelState.AddModelError("Password", "Devi riempire sia Nuova password che conferma password");
                    stringaErrorePassword="Devi riempire sia Nuova password che conferma password";
                }
            }
            else
            {
                if(model.Password != null || model.ConfirmPassword != null)
                {
                    ModelState.AddModelError("Password", "Devi inserire la vecchia password");
                    stringaErrorePassword = "Devi inserire la vecchia password";
                }
            }

            if (model.Photo != null)
            {


                if (model.Photo.ContentType != "image/png" && model.Photo.ContentType != "image/jpeg")
                    ModelState.AddModelError("Photos", "I file devono essere delle immagini!");
                if (model.Photo.Length > DomainConstraints.AnnouncePhotosMaxLenght)
                {
                    ModelState.AddModelError("Photos",
                        "Non puoi inserire immagini superiori a " + DomainConstraints.AnnouncePhotosMaxLenght / 1000000 +
                        " MB");
                }
            }


                if (ModelState.IsValid)
                {

                    if (model.Photo != null)
                    {
                        string imageUrl = "";
                        var deleteUrl = _context.ImageUrls.Single(i => i.Url.Equals(user.ProfileImageUrl));
                        _context.ImageUrls.Remove(deleteUrl);
                        imageUrl = await UploadProfileImage(model.Photo, user);
                        user.ProfileImageUrl = imageUrl;
                    }
                user.Name = model.Name;
                user.Surname = model.Surname;
                user.Email = model.Email;
                user.BirthDate = model.BirthDate;
                user.Latitude = double.Parse(model.Latitude ?? "0", CultureInfo.InvariantCulture);
                user.Longitude = double.Parse(model.Longitude ?? "0", CultureInfo.InvariantCulture);
                switch (model.Genre)
                {
                    case 1:
                        user.Genre = Genre.Female;
                        break;
                    case 2:
                        user.Genre = Genre.Male;
                        break;
                    case 3:
                        user.Genre = Genre.Unspecified;
                        break;
                }
                _context.SaveChanges();



                foreach (var newUserPreference in newUserPreferences)
                {
                    if (newUserPreference.Value)
                    {
                        var category = _context.Categories.Single(c => c.Id.Equals(newUserPreference.Key));
                        _context.UserCategoryPreferenceses.Add(new UserCategoryPreferences
                        {
                            User = user,
                            UserId = user.Id,
                            Category = category,
                            CategoryId = category.Id

                        });
                    }
                }
                _context.SaveChanges();
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            return RedirectToAction(nameof(PreferencesController.Index),new { passwordError = stringaErrorePassword });
        }
    }
}


