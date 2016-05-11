using System.Security.Claims;
using Microsoft.AspNet.Mvc;

namespace Cianfrusaglie.Statics
{
    public static class LoginChecker
    {
        public static bool HasLoggedUser( Controller controller ) {
            return controller.User.Identity.IsAuthenticated;
        }
    }
}
