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

namespace Cianfrusaglie.Controllers {
    [Authorize]
    public class AccountController : Controller {
        private readonly IEmailSender _emailSender;
        private readonly ILogger _logger;
        private readonly SignInManager< User > _signInManager;
        private readonly ISmsSender _smsSender;
        private readonly UserManager< User > _userManager;
        private readonly IHostingEnvironment _environment;
        private readonly ApplicationDbContext _context;

        public AccountController( UserManager< User > userManager, SignInManager< User > signInManager,
            IEmailSender emailSender, ISmsSender smsSender, ILoggerFactory loggerFactory, IHostingEnvironment environment, ApplicationDbContext context) {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _smsSender = smsSender;
            _environment = environment;
            _logger = loggerFactory.CreateLogger< AnnouncesController >();
            _context = context;
        }

        /// <summary>
        ///     Effettua l'upload dell'immagine profilo utente
        /// </summary>
        /// <param name="formFile">immagine dal form</param>
        /// <param name="user">utente</param>
        /// <returns></returns>
        private async Task< string > UploadProfileImage( IFormFile formFile, User user ) {
            string uploads = Path.Combine( _environment.WebRootPath, "images" );

            if(formFile.ContentType == "image/png" || formFile.ContentType == "image/jpeg" ) {
                if(formFile.Length > 0 ) {
                    string fileName = ContentDispositionHeaderValue.Parse(formFile.ContentDisposition ).FileName.Trim( '"' );
                    fileName = user.Id + Path.GetExtension( fileName );
                    await formFile.SaveAsAsync( Path.Combine( uploads, fileName ) );

                    return @"/images/" + fileName;
                }
            }
            return null;
        }

        //
        // GET: /Account/Login
        [HttpGet, AllowAnonymous]
        public IActionResult Login( string returnUrl = null ) {
            //TODO: BadRequest da sistemare.
            if( LoginChecker.HasLoggedUser( this ) )
                return HttpBadRequest();
            ViewData[ "ReturnUrl" ] = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost, AllowAnonymous, ValidateAntiForgeryToken]
        public async Task< IActionResult > Login( LoginViewModel model, string returnUrl = null ) {
            ViewData[ "ReturnUrl" ] = returnUrl;
            if( ModelState.IsValid ) {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result =
                    await _signInManager.PasswordSignInAsync( model.UserName, model.Password, model.RememberMe, false );
                if( result.Succeeded ) {
                    _logger.LogInformation( 1, "User logged in." );
                    return RedirectToLocal( returnUrl );
                }
                if( result.RequiresTwoFactor ) {
                    return RedirectToAction( nameof( SendCode ), new {ReturnUrl = returnUrl, model.RememberMe} );
                }
                if( result.IsLockedOut ) {
                    _logger.LogWarning( 2, "User account locked out." );
                    return View( "Lockout" );
                }
                ModelState.AddModelError( string.Empty, "Invalid login attempt." );
                return View( model );
            }

            // If we got this far, something failed, redisplay form
            return View( model );
        }

        //
        // GET: /Account/Register
        [HttpGet, AllowAnonymous]
        public IActionResult Register() {
            //TODO: BadRequest da trattare
            if( LoginChecker.HasLoggedUser( this ) )
                return HttpBadRequest();

            ViewData["formMacroCategories"] = _context.Categories.ToList();
            ViewData["numberOfMacroCategories"] = _context.Categories.ToList().Count;

            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost, AllowAnonymous, ValidateAntiForgeryToken]
        public async Task< IActionResult > Register( RegisterViewModel model ) {
            if( model.Photo != null ) {
                if( model.Photo.ContentType != "image/png" && model.Photo.ContentType != "image/jpeg" )
                    ModelState.AddModelError( "Photos", "I file devono essere delle immagini!" );
                if( model.Photo.Length > DomainConstraints.AnnouncePhotosMaxLenght ) {
                    ModelState.AddModelError( "Photos",
                        "Non puoi inserire immagini superiori a " + DomainConstraints.AnnouncePhotosMaxLenght / 1000000 +
                        " MB" );
                }
            }
            //QUI BISOGNA INSERIRE TUTTI GLI ALTRI CAMPI
            if( ModelState.IsValid ) {
                var user = new User {
                   Name = model.Name,
                   Surname = model.Surname,
                   BirthDate = model.BirthDate,
                   Genre = model.Genre,
                   UserName = model.UserName,
                   Email = model.Email,
                   Latitude = double.Parse( model.Latitude, CultureInfo.InvariantCulture ),
                   Longitude = double.Parse( model.Longitude, CultureInfo.InvariantCulture )
                };
                string imageUrl = "";
                if ( model.Photo != null ) {
                    imageUrl = await UploadProfileImage( model.Photo, user );
                } else {
                    imageUrl = CommonStrings.DefaultProfileImageUrl;
                }
                user.ProfileImageUrl = imageUrl;
                var result = await _userManager.CreateAsync( user, model.Password );
                if( result.Succeeded ) {
                    if (model.CategoryDictionary != null)
                        foreach (var kvPair in model.CategoryDictionary)
                        {
                            if (kvPair.Value)
                            {
                                _context.UserCategoryPreferenceses.Add(new UserCategoryPreferences
                                {
                                    CategoryId = kvPair.Key,
                                    UserId = user.Id
                                });
                            }
                        }
                    _context.SaveChanges();
                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=532713
                    // Send an email with this link
                    //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    //var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: HttpContext.Request.Scheme);
                    //await _emailSender.SendEmailAsync(model.Email, "Confirm your account",
                    //    "Please confirm your account by clicking this link: <a href=\"" + callbackUrl + "\">link</a>");
                    await _signInManager.SignInAsync( user, false );
                    _logger.LogInformation( 3, "User created a new account with password." );
                    return RedirectToAction( nameof( HomeController.Index ), "Home" );
                }
                AddErrors( result );
            }

            // If we got this far, something failed, redisplay form
            return View( model );
        }

        //
        // POST: /Account/LogOff
        public async Task< IActionResult > LogOff() {
            await _signInManager.SignOutAsync();
            _logger.LogInformation( 4, "User logged out." );
            return RedirectToAction( nameof( HomeController.Index ), "Home" );
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost, AllowAnonymous, ValidateAntiForgeryToken]
        public IActionResult ExternalLogin( string provider, string returnUrl = null ) {
            // Request a redirect to the external login provider.
            string redirectUrl = Url.Action( "ExternalLoginCallback", "Account", new {ReturnUrl = returnUrl} );
            var properties = _signInManager.ConfigureExternalAuthenticationProperties( provider, redirectUrl );
            return new ChallengeResult( provider, properties );
        }

        //
        // GET: /Account/ExternalLoginCallback
        [HttpGet, AllowAnonymous]
        public async Task< IActionResult > ExternalLoginCallback( string returnUrl = null ) {
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if( info == null ) {
                return RedirectToAction( nameof( Login ) );
            }

            // Sign in the user with this external login provider if the user already has a login.
            var result = await _signInManager.ExternalLoginSignInAsync( info.LoginProvider, info.ProviderKey, false );
            if( result.Succeeded ) {
                _logger.LogInformation( 5, "User logged in with {Name} provider.", info.LoginProvider );
                return RedirectToLocal( returnUrl );
            }
            if( result.RequiresTwoFactor ) {
                return RedirectToAction( nameof( SendCode ), new {ReturnUrl = returnUrl} );
            }
            if( result.IsLockedOut ) {
                return View( "Lockout" );
            }
            // If the user does not have an account, then ask the user to create an account.
            ViewData[ "ReturnUrl" ] = returnUrl;
            ViewData[ "LoginProvider" ] = info.LoginProvider;
            string email = info.ExternalPrincipal.FindFirstValue( ClaimTypes.Email );
            return View( "ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel {Email = email} );
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost, AllowAnonymous, ValidateAntiForgeryToken]
        public async Task< IActionResult > ExternalLoginConfirmation( ExternalLoginConfirmationViewModel model,
            string returnUrl = null ) {
            if( User.IsSignedIn() ) {
                return RedirectToAction( nameof( ManageController.Index ), "Manage" );
            }

            if( ModelState.IsValid ) {
                // Get the information about the user from the external login provider
                var info = await _signInManager.GetExternalLoginInfoAsync();
                if( info == null ) {
                    return View( "ExternalLoginFailure" );
                }
                var user = new User {UserName = model.Email, Email = model.Email};
                var result = await _userManager.CreateAsync( user );
                if( result.Succeeded ) {
                    result = await _userManager.AddLoginAsync( user, info );
                    if( result.Succeeded ) {
                        await _signInManager.SignInAsync( user, false );
                        _logger.LogInformation( 6, "User created an account using {Name} provider.", info.LoginProvider );
                        return RedirectToLocal( returnUrl );
                    }
                }
                AddErrors( result );
            }

            ViewData[ "ReturnUrl" ] = returnUrl;
            return View( model );
        }

        // GET: /Account/ConfirmEmail
        [HttpGet, AllowAnonymous]
        public async Task< IActionResult > ConfirmEmail( string userId, string code ) {
            if( userId == null || code == null ) {
                return View( "Error" );
            }
            var user = await _userManager.FindByIdAsync( userId );
            if( user == null ) {
                return View( "Error" );
            }
            var result = await _userManager.ConfirmEmailAsync( user, code );
            return View( result.Succeeded ? "ConfirmEmail" : "Error" );
        }

        //
        // GET: /Account/ForgotPassword
        [HttpGet, AllowAnonymous]
        public IActionResult ForgotPassword() {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost, AllowAnonymous, ValidateAntiForgeryToken]
        public async Task< IActionResult > ForgotPassword( ForgotPasswordViewModel model ) {
            if( ModelState.IsValid ) {
                var user = await _userManager.FindByNameAsync( model.Email );
                if( user == null || !await _userManager.IsEmailConfirmedAsync( user ) ) {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View( "ForgotPasswordConfirmation" );
                }

                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=532713
                // Send an email with this link
                //var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                //var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: HttpContext.Request.Scheme);
                //await _emailSender.SendEmailAsync(model.Email, "Reset Password",
                //   "Please reset your password by clicking here: <a href=\"" + callbackUrl + "\">link</a>");
                //return View("ForgotPasswordConfirmation");
            }

            // If we got this far, something failed, redisplay form
            return View( model );
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [HttpGet, AllowAnonymous]
        public IActionResult ForgotPasswordConfirmation() {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        [HttpGet, AllowAnonymous]
        public IActionResult ResetPassword( string code = null ) {
            return code == null ? View( "Error" ) : View();
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost, AllowAnonymous, ValidateAntiForgeryToken]
        public async Task< IActionResult > ResetPassword( ResetPasswordViewModel model ) {
            if( !ModelState.IsValid ) {
                return View( model );
            }
            var user = await _userManager.FindByNameAsync( model.Email );
            if( user == null ) {
                // Don't reveal that the user does not exist
                return RedirectToAction( nameof( ResetPasswordConfirmation ), "Account" );
            }
            var result = await _userManager.ResetPasswordAsync( user, model.Code, model.Password );
            if( result.Succeeded ) {
                return RedirectToAction( nameof( ResetPasswordConfirmation ), "Account" );
            }
            AddErrors( result );
            return View();
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [HttpGet, AllowAnonymous]
        public IActionResult ResetPasswordConfirmation() {
            return View();
        }

        //
        // GET: /Account/SendCode
        [HttpGet, AllowAnonymous]
        public async Task< ActionResult > SendCode( string returnUrl = null, bool rememberMe = false ) {
            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
            if( user == null ) {
                return View( "Error" );
            }
            var userFactors = await _userManager.GetValidTwoFactorProvidersAsync( user );
            var factorOptions =
                userFactors.Select( purpose => new SelectListItem {Text = purpose, Value = purpose} ).ToList();
            return
                View( new SendCodeViewModel {Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe} );
        }

        //
        // POST: /Account/SendCode
        [HttpPost, AllowAnonymous, ValidateAntiForgeryToken]
        public async Task< IActionResult > SendCode( SendCodeViewModel model ) {
            if( !ModelState.IsValid ) {
                return View();
            }

            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
            if( user == null ) {
                return View( "Error" );
            }

            // Generate the token and send it
            string code = await _userManager.GenerateTwoFactorTokenAsync( user, model.SelectedProvider );
            if( string.IsNullOrWhiteSpace( code ) ) {
                return View( "Error" );
            }

            string message = "Your security code is: " + code;
            if( model.SelectedProvider == "Email" ) {
                await _emailSender.SendEmailAsync( await _userManager.GetEmailAsync( user ), "Security Code", message );
            } else if( model.SelectedProvider == "Phone" ) {
                await _smsSender.SendSmsAsync( await _userManager.GetPhoneNumberAsync( user ), message );
            }

            return RedirectToAction( nameof( VerifyCode ),
                new {Provider = model.SelectedProvider, model.ReturnUrl, model.RememberMe} );
        }

        //
        // GET: /Account/VerifyCode
        [HttpGet, AllowAnonymous]
        public async Task< IActionResult > VerifyCode( string provider, bool rememberMe, string returnUrl = null ) {
            // Require that the user has already logged in via username/password or external login
            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
            if( user == null ) {
                return View( "Error" );
            }
            return View( new VerifyCodeViewModel {Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe} );
        }

        //
        // POST: /Account/VerifyCode
        [HttpPost, AllowAnonymous, ValidateAntiForgeryToken]
        public async Task< IActionResult > VerifyCode( VerifyCodeViewModel model ) {
            if( !ModelState.IsValid ) {
                return View( model );
            }

            // The following code protects for brute force attacks against the two factor codes.
            // If a user enters incorrect codes for a specified amount of time then the user account
            // will be locked out for a specified amount of time.
            var result =
                await
                    _signInManager.TwoFactorSignInAsync( model.Provider, model.Code, model.RememberMe,
                        model.RememberBrowser );
            if( result.Succeeded ) {
                return RedirectToLocal( model.ReturnUrl );
            }
            if( result.IsLockedOut ) {
                _logger.LogWarning( 7, "User account locked out." );
                return View( "Lockout" );
            }
            ModelState.AddModelError( "", "Invalid code." );
            return View( model );
        }

        #region Helpers

        private void AddErrors( IdentityResult result ) {
            foreach( var error in result.Errors ) {
                ModelState.AddModelError( string.Empty, error.Description );
            }
        }

        private async Task< User > GetCurrentUserAsync() {
            return await _userManager.FindByIdAsync( HttpContext.User.GetUserId() );
        }

        private IActionResult RedirectToLocal( string returnUrl ) {
            if( Url.IsLocalUrl( returnUrl ) ) {
                return Redirect( returnUrl );
            }
            return RedirectToAction( nameof( HomeController.Index ), "Home" );
        }

        #endregion
    }
}