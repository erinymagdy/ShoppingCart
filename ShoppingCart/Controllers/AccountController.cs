using Domain.AuthModels;
using Domain.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<AccountController> _logger;

        public AccountController(SignInManager<IdentityUser> signInManager,
            ILogger<AccountController> logger,
            UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel user, string ReturnUrl = null)
        {
            try
            {
                if (!string.IsNullOrEmpty(user.ErrorMessage))
                {
                    ModelState.AddModelError(string.Empty, user.ErrorMessage);
                }
                ReturnUrl ??= Url.Content("~/");

                // Clear the existing external cookie to ensure a clean login process
                await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

                user.ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

                user.ReturnUrl = ReturnUrl;
                if (ModelState.IsValid)
                {
                    var result = await _signInManager.PasswordSignInAsync(user.UserName, user.Password, user.RememberMe, lockoutOnFailure: false);
                    if (result.Succeeded)
                    {
                        _logger.LogInformation("User logged in.");
                        return LocalRedirect(ReturnUrl);
                    }
                    if (result.RequiresTwoFactor)
                    {
                        return RedirectToPage("./LoginWith2fa", new { ReturnUrl = ReturnUrl, RememberMe = user.RememberMe });
                    }
                    if (result.IsLockedOut)
                    {
                        _logger.LogWarning("User account locked out.");
                        return RedirectToPage("./Lockout");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                        return View();
                    }
                }

                return View(user);
            }
            catch
            {
                return View();
            }
        }
        //[Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [Authorize(Roles = "Admin , Customer")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterModel user, string ReturnUrl = null)
        {
            try
            {

                ReturnUrl ??= Url.Content("~/");
                user.ReturnUrl = ReturnUrl;
                user.ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
                if (ModelState.IsValid)
                {

                    var appuser = new IdentityUser { UserName = user.UserName, Email = user.Email , PhoneNumber = user.PhoneNumber };
                    appuser.EmailConfirmed = true;
                    var result = await _userManager.CreateAsync(appuser, user.Password);
                    if (result.Succeeded)
                    {
                        _logger.LogInformation("User created a new account with password.");
                        await _userManager.AddToRoleAsync(appuser, "Customer");
                        var code = await _userManager.GenerateEmailConfirmationTokenAsync(appuser);
                        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

                        return RedirectToAction("Index", "Home", new { email = appuser.Email, returnUrl = ReturnUrl });
                    }

                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return View(user);
                }
                return View();
            }
            catch
            {
                return View();
            }
        }
        public async Task<ActionResult> Logout(string ReturnUrl = null)
        {
            try
            {

                await _signInManager.SignOutAsync();
                _logger.LogInformation("User logged out.");
                if (ReturnUrl != null)
                {
                    return LocalRedirect(ReturnUrl);
                }
                else
                {
                    return RedirectToAction("Login");
                }
            }
            catch
            {
                return View();
            }
        }
        [Authorize]
        [HttpGet]
        public ActionResult ResetPassword( string id)
        {
            var user = _userManager.Users.Where(e => e.Id == id).FirstOrDefault();
            ResetPasswordModel reset = new ResetPasswordModel();
            reset.UserName = user.UserName;
            return View(reset);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordModel model)
        {
            try
            {
                //model.Code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
                if (!ModelState.IsValid)
                {
                    return View();
                }

                var user = await _userManager.FindByNameAsync(model.UserName);

                //model.Code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                if (user == null)
                {

                    ModelState.AddModelError(string.Empty, "Email is not exist");
                    return View(model);

                }
                model.Code = await _userManager.GeneratePasswordResetTokenAsync(user);
                var result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home", new { username = user.UserName });
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(model);

            }
            catch
            {
                return View();
            }
        }
        [Authorize(Roles = "Admin")]
        // GET: AccountController/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            var account = await _userManager.FindByIdAsync(id);
            return View(account);
        }

        // POST: AccountController/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(string id, IFormCollection collection)
        {
            try
            {
                _logger.LogInformation("Attempting to delete an account....");
                var account = await _userManager.FindByIdAsync(id);
                if (account == null)
                {
                    return BadRequest();
                }
                else
                {
                    var Roles = await _userManager.GetRolesAsync(account);
                    if (Roles.Contains("Admin"))
                    {
                        _logger.LogInformation("Can't delete this user"); 
                        return BadRequest();
                    }
                    else
                    {
                        await _userManager.DeleteAsync(account);
                        _logger.LogInformation("user deleted");

                        return RedirectToAction(nameof(Index));
                    }
                }

            }
            catch
            {
                return View();
            }
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> GetAllSellers()
        {
            var users = _userManager.Users;
            List<IdentityUser> returnedusers = new List<IdentityUser>();
            foreach (var user in users.ToArray())
            {
                var userroles = await _userManager.GetRolesAsync(user);
                if (userroles.Contains("Seller"))
                {
                    returnedusers.Add(user);
                }
                
            }
            return View(returnedusers);
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult CreateSeller()
        {
            
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> CreateSeller(RegisterModel user , string ReturnUrl=null)
        {
            try
            {

                ReturnUrl ??= Url.Content("~/");
                user.ReturnUrl = ReturnUrl;
                user.ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
                if (ModelState.IsValid)
                {

                    var appuser = new IdentityUser { UserName = user.UserName, Email = user.Email, PhoneNumber = user.PhoneNumber };
                    appuser.EmailConfirmed = true;
                    var result = await _userManager.CreateAsync(appuser, user.Password);
                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(appuser, "Seller");
                        _logger.LogInformation("User created a new account with password.");

                        var code = await _userManager.GenerateEmailConfirmationTokenAsync(appuser);
                        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

                        return RedirectToAction("Index", "Home", new { email = appuser.Email, returnUrl = ReturnUrl });
                    }

                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return View(user);
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

    }
}
