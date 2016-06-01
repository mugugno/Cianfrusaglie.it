﻿using System.Globalization;
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
using Microsoft.Data.Entity;


namespace Cianfrusaglie.Controllers
{
    public class ProfileController : Controller
    {

        private readonly ApplicationDbContext _context;

        public ProfileController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Restituisce il profilo pubblico di un determinato utente
        /// </summary>
        /// <param name="userId">l'id dell'utente da visualizzare</param>
        /// <returns>la view contenente il profilo pubblico dell'utente specificato</returns>
        // GET: /<controller>/
        public IActionResult Index(string userId)
        {
            if (!LoginChecker.HasLoggedUser(this))
                return HttpBadRequest();
            if( userId == null ) {
                userId = User.GetUserId();
            }
            User user = _context.Users.Include( u=> u.ReceivedFeedBacks ).First( u => u.Id.Equals( userId ) );
            CommonFunctions.SetRootLayoutViewData(this, _context);
            CommonFunctions.SetMacroCategoriesViewData(this, _context);
            return View(user);
        }

        public IActionResult VoteFeedbackUsefulness(int feedbackId, bool useful ) {
           
            var feedback = _context.FeedBacks.Include(f=>f.Author).SingleOrDefault(f => f.Id.Equals(feedbackId));
            if ( feedback == null )
                return HttpBadRequest();
            if( !LoginChecker.HasLoggedUser( this ) )
                return HttpBadRequest();
            var user = _context.Users.Single( u => u.Id.Equals( User.GetUserId() ) );
            if(feedback.Author.Id.Equals( user.Id ))
                return HttpBadRequest();
            var lastScore =
                _context.UserFeedbackScores.SingleOrDefault(
                    s => s.AuthorId.Equals( user.Id ) && s.FeedBackId.Equals( feedback.Id ) );
            if( lastScore != null ) {
                if( lastScore.Useful.Equals( useful ) )
                    return HttpBadRequest();
                lastScore.Useful = useful;
            } else {
                var score = new UserFeedbackScore() { Author = user, FeedBack = feedback, Useful = useful };
                _context.UserFeedbackScores.Add(score);
            }
            
            _context.SaveChanges();
            return View();
        }
    }
}
