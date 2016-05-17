﻿using System;
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

        public static bool IsThereAnyNotification( string userId, ApplicationDbContext context ) {
            return IsThereNewMessage( userId, context ); // && IsThereAnnounceNotification && ...
        }

        public static bool IsThereNewMessage(string userId, ApplicationDbContext context) {
            if( userId == null ) {
                return false;
            }
            var unreadMessages = context.Messages.Where(m => m.Receiver.Id.Equals(userId) && !m.Read).ToList();
            if( unreadMessages.Any() ) {
                return true;
            }
            return false;
        }

    }
}