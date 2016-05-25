using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Cianfrusaglie.Models;
using Cianfrusaglie.Statics;
using Cianfrusaglie.ViewModels;
using Microsoft.AspNet.Mvc;

namespace Cianfrusaglie.Constants
{
    public static class CommonFunctions
    {

        /// <summary>
        /// Dato un controller e un contesto dati, imposta i dati da mostrare nella navbar della view _Layout.cshtml
        /// </summary>
        /// <param name="controller">Il controller a cui passare i dati</param>
        /// <param name="context">Il context da cui prendere i dati</param>
        public static void SetRootLayoutViewData( Controller controller, ApplicationDbContext context ) {
            controller.ViewData["formCategories"] = context.Categories.ToList();
            controller.ViewData["numberOfCategories"] = context.Categories.ToList().Count;
            controller.ViewData["IsThereNewMessage"] = IsThereNewMessage(controller.User.GetUserId(), context);
            controller.ViewData["IsThereNewInterested"] = IsThereNewInterested(controller.User.GetUserId(), context);
            controller.ViewData["IsThereAnyNotification"] = IsThereAnyNotification(controller.User.GetUserId(), context);
        }

        public static bool IsThereAnyNotification( string userId, ApplicationDbContext context ) {
            return IsThereNewMessage( userId, context ) || IsThereNewInterested(userId,context); // && IsThereAnnounceNotification && ...
        }

        public static bool IsThereNewMessage(string userId, ApplicationDbContext context) {
            if( userId == null ) {
                return false;
            }
            var unreadMessages = context.Messages.Where(m => m.Receiver.Id.Equals(userId) && !m.Read).ToList();
            return unreadMessages.Any();
        }

        public static bool IsThereNewInterested(string userId,ApplicationDbContext context){
            if (userId == null)
                return false;
            var announces = context.Announces.Where(a => a.AuthorId.Equals(userId));

            foreach(var announce in announces)
            {
                var newInterested = context.Interested.Where(i => !i.Read && i.AnnounceId.Equals(announce.Id));
                if (newInterested.Any())
                    return true;
            }
            return false;
        }

    }
}
