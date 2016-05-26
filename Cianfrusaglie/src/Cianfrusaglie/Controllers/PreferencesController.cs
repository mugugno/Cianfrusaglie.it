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

        public PreferencesController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            ViewData["formCategories"] = _context.Categories.ToList();
            ViewData["numberOfCategories"] = _context.Categories.ToList().Count;
            ViewData["formMacroCategories"] = _context.Categories.ToList();
            ViewData["numberOfMacroCategories"] = _context.Categories.ToList().Count;
            ViewData["IsThereNewMessage"] = IsThereNewMessage(User.GetUserId(), _context);
            ViewData[" IsThereNewInterested"] = IsThereNewInterested(User.GetUserId(), _context);
            ViewData["IsThereAnyNotification"] = IsThereAnyNotification(User.GetUserId(), _context);
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

            ViewData["userPreferences"] = userPreferences;
            return View();
        }

        public IActionResult Edit(PreferenceViewModel model)
        {
            var user = _context.Users.Single(u => u.Id.Equals(User.GetUserId()));
            var newUserPreferences = model.CategoryDictionary;
            var userPreferences = _context.UserCategoryPreferenceses.Where(c => c.UserId.Equals(User.GetUserId()));
            _context.UserCategoryPreferenceses.RemoveRange(userPreferences);
            _context.SaveChanges();
            foreach(var newUserPreference in newUserPreferences)
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
    }
}


