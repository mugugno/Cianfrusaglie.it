﻿using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Cianfrusaglie.Controllers;
using Cianfrusaglie.Models;
using Cianfrusaglie.Services;
using Cianfrusaglie.ViewModels;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Extensions.Logging;

namespace MvcMovie.Controllers {
    [Authorize]
    public class AccountController : Controller {
        private readonly UserManager< User > _userManager;
        private readonly SignInManager< User > _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly ISmsSender _smsSender;
        private readonly ILogger _logger;

        public AccountController( UserManager< User > userManager, SignInManager< User > signInManager,
            IEmailSender emailSender, ISmsSender smsSender, ILoggerFactory loggerFactory ) {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _smsSender = smsSender;
            _logger = loggerFactory.CreateLogger< AccountController >();
        }

        //
        // GET: /Account/Login
        [HttpGet, AllowAnonymous]
        public IActionResult Login( string returnUrl = null ) {
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
                SignInResult result =
                    await _signInManager.PasswordSignInAsync( model.Email, model.Password, model.RememberMe, false );
                if( result.Succeeded ) {
                    _logger.LogInformation( 1, "User logged in." );
                    return RedirectToLocal( returnUrl );
                }
                if( result.RequiresTwoFactor ) {
                    return RedirectToAction( nameof( SendCode ), new {ReturnUrl = returnUrl, model.RememberMe} );
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
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost, AllowAnonymous, ValidateAntiForgeryToken]
        public async Task< IActionResult > Register( RegisterViewModel model ) {
            if( ModelState.IsValid ) {
                var user = new User {Email = model.Email};
                IdentityResult result = await _userManager.CreateAsync( user, model.Password );
                if( result.Succeeded ) {
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
        [HttpPost, ValidateAntiForgeryToken]
        public async Task< IActionResult > LogOff() {
            await _signInManager.SignOutAsync();
            _logger.LogInformation( 4, "User logged out." );
            return RedirectToAction( nameof( HomeController.Index ), "Home" );
        }


        // GET: /Account/ConfirmEmail
        [HttpGet, AllowAnonymous]
        public async Task< IActionResult > ConfirmEmail( string userId, string code ) {
            if( userId == null || code == null ) {
                return View( "Error" );
            }
            User user = await _userManager.FindByIdAsync( userId );
            if( user == null ) {
                return View( "Error" );
            }
            IdentityResult result = await _userManager.ConfirmEmailAsync( user, code );
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
                User user = await _userManager.FindByNameAsync( model.Email );
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
            User user = await _userManager.FindByNameAsync( model.Email );
            if( user == null ) {
                // Don't reveal that the user does not exist
                return RedirectToAction( nameof( ResetPasswordConfirmation ), "Account" );
            }
            IdentityResult result = await _userManager.ResetPasswordAsync( user, model.Code, model.Password );
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
            User user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
            if( user == null ) {
                return View( "Error" );
            }
            IList< string > userFactors = await _userManager.GetValidTwoFactorProvidersAsync( user );
            List< SelectListItem > factorOptions =
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

            User user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
            if( user == null ) {
                return View( "Error" );
                ;
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
            User user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
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
            SignInResult result =
                await
                    _signInManager.TwoFactorSignInAsync( model.Provider, model.Code, model.RememberMe,
                        model.RememberBrowser );
            if( result.Succeeded ) {
                return RedirectToLocal( model.ReturnUrl );
            }
            ModelState.AddModelError( "", "Invalid code." );
            return View( model );
        }

        #region Helpers

        private void AddErrors( IdentityResult result ) {
            foreach( IdentityError error in result.Errors ) {
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
