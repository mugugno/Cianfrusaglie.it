using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Cianfrusaglie.GeoPosition;
using Cianfrusaglie.Models;
using Cianfrusaglie.Suggestions;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;

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
            controller.ViewData["IsThereNewMessages"] = IsThereNewMessages(controller.User.GetUserId(), context);
            controller.ViewData["IsThereNewInterested"] = IsThereNewInterested(controller.User.GetUserId(), context);
            controller.ViewData["IsThereNewAnnouncesWhereIAmChoosed"] = IsThereNewAnnouncesWhereIAmChoosed(controller.User.GetUserId(), context);
            controller.ViewData["IsNewFeedbackRequired"] = IsNewFeedbackRequired(controller.User.GetUserId(), context);
            controller.ViewData["IsThereNewFeedback"] = IsThereNewFeedback(controller.User.GetUserId(), context);
            controller.ViewData["IsThereAnyNotification"] = IsThereAnyNotification(controller.User.GetUserId(), context);

        }

        public static void SetMacroCategoriesViewData(Controller controller, ApplicationDbContext context)
        {
            controller.ViewData["formMacroCategories"] = context.Categories.ToList< Category >();
            controller.ViewData["numberOfMacroCategories"] = context.Categories.ToList< Category >().Count;
        }

        public static IEnumerable< Announce > GetSuggestedAnnounces(ApplicationDbContext context, Controller controller) {
            var user = context.Users.Single( u => u.Id.Equals( controller.User.GetUserId() ) );
            var rankAlgorithm = new RankAlgorithm( context );
            return
                context.Announces.Include( a=>a.Author ).Where(
                    a =>
                        !a.AuthorId.Equals( controller.User.GetUserId() ) && !a.Closed &&
                        GeoCoordinate.Distance( a.Latitude.Value, a.Longitude.Value, user.Latitude.Value, user.Longitude.Value ) <=
                        100 ).OrderByDescending( a => rankAlgorithm.CalculateRank( a, user ) );
        }

        /// <summary>
        /// controlla se ci sono modifiche
        /// </summary>
        /// <param name="userId">utente che deve visualizzarle</param>
        /// <param name="context">l'entità del database, notification </param>
        /// <returns>true se c'è almeno una notifica</returns>

        public static bool IsThereAnyNotification(string userId, ApplicationDbContext context)
        {
            //query che prende tutte le notifiche dell'utente
            var userNotification = context.NotificationCenter.Where(n => n.UserId.Equals(userId) && !n.Read);
            //se ce n'è almeno una lo ritorna
            return userNotification.Any();
        }


        /// <summary>
        /// controlla quanti nuovi messaggi l'utente deve visualizzare
        /// </summary>
        /// <param name="userId">utente che deve visualizzarle</param>
        /// <param name="context">l'entità del database, notification</param>
        /// <returns>il numero di messaggi non ancora letti</returns>

        public static int IsThereNewMessages(string userId, ApplicationDbContext context) {
            if( userId == null ) {
                return -1;
            }
            var unreadMessages = context.NotificationCenter.Where(n => n.UserId.Equals(userId) && !n.Read && n.TypeNotification==MessageTypeNotification.NewMessage).ToList();
            return unreadMessages.Count();
        }

        /// <summary>
        /// controlla se c'è un nuovo interessato all'annuncio dell'utente che deve visualizzare la notifica
        /// </summary>
        /// <param name="userId">utente che deve visualizzarle</param>
        /// <param name="context">l'entità del database, notification</param>
        /// <returns>true, se ci sono nuovi interessati</returns>
        public static bool IsThereNewInterested(string userId, ApplicationDbContext context)
        {
            if (userId == null)
            {
                return false;
            }
            var interested =
                context.NotificationCenter.Where(
                    n =>
                        n.UserId.Equals(userId) && !n.Read &&
                        n.TypeNotification == MessageTypeNotification.NewInterested);
            return interested.Any();

        }

        //TODO SUMMARY
        public static bool IsThereNewAnnouncesWhereIAmChoosed(string userId, ApplicationDbContext context)
        {
            if (userId == null)
            {
                return false;
            }
            var newChoosed = context.NotificationCenter.Where(n =>
                n.UserId.Equals(userId) && !n.Read &&
                n.TypeNotification == MessageTypeNotification.NewChoosed);
            return newChoosed.Any();
        }

        //TODO SUMMARY
        public static bool IsThereNewAnnouncesWhereIAmNotChoosed(string userId, ApplicationDbContext context)
        {
            if (userId == null)
            {
                return false;
            }
            //quando la transazione è chiusa
            var newChoosed = context.NotificationCenter.Where(n =>
                n.UserId.Equals(userId) && !n.Read &&
                n.TypeNotification == MessageTypeNotification.NewAnotherChoosed);
            return newChoosed.Any();
        }

        //TODO SUMMARY
        public static bool IsNewFeedbackRequired(string userId, ApplicationDbContext context)
        {
            if (userId == null)
            {
                return false;
            }
            var newFeedback = context.NotificationCenter.Where(n =>
                n.UserId.Equals(userId) && !n.Read &&
                n.TypeNotification == MessageTypeNotification.NewFeedback);
            return newFeedback.Any();
        }

        //TODO SUMMARY
        /// <summary>
        /// controlla che ci siano nuove notifiche di feedback ricevuti da un utente
        /// </summary>
        /// <param name="userId"> l'utente che deve ricevere la notifica</param>
        /// <param name="context"> database</param>
        /// <returns>true, se ci sono feedback che l'utente deve ancora leggere</returns>
        public static bool IsThereNewFeedback(string userId, ApplicationDbContext context)
        {
            if (userId == null)
            {
                return false;
            }
            var newFeedback = context.NotificationCenter.Where(n =>
                n.UserId.Equals(userId) && !n.Read &&
                n.TypeNotification == MessageTypeNotification.NewReceivedFeedback);
            return newFeedback.Any();
        }
    }
}
