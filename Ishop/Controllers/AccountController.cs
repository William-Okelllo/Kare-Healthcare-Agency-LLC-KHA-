using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Ishop.Models;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Web.Mail;
using EASendMail;
using SmtpMail = EASendMail.SmtpMail;
using Microsoft.AspNet.Identity.EntityFramework;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using Ishop.Infa;

namespace Ishop.Controllers
{
    //[Authorize]
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        ApplicationDbContext context;
        public AccountController()
        {
            context = new ApplicationDbContext();
        }
        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

       
        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                try
                {
                    return RedirectToLocal(returnUrl);
                }
                catch
                {
                    return RedirectToAction("index", "Home");
                }
            }
            else
            {


                return View();
            }
        }

        [AllowAnonymous]
        public JsonResult CheckLogedIn()
        {
            try
            {
                return Json(new { logged_in = User.Identity.IsAuthenticated }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { logged_in = false }, JsonRequestBehavior.AllowGet);
            }
        }
        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            // This doesn't count login failures towards account lockout    
            // To enable password failures to trigger account lockout, change to shouldLockout: true    
            var result = await SignInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid Username or Password");
                    return View(model);
            }

        }

        //
        // GET: /Account/VerifyCode
        [AllowAnonymous]
        public async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe)
        {
            // Require that the user has already logged in via username/password or external login
            if (!await SignInManager.HasBeenVerifiedAsync())
            {
                return View("Error");
            }
            return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/VerifyCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // The following code protects for brute force attacks against the two factor codes. 
            // If a user enters incorrect codes for a specified amount of time then the user account 
            // will be locked out for a specified amount of time. 
            // You can configure the account lockout settings in IdentityConfig
            var result = await SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent: model.RememberMe, rememberBrowser: model.RememberBrowser);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(model.ReturnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid code.");
                    return View(model);
            }
        }
        private ApplicationDbContext dbb = new ApplicationDbContext ();
        public ActionResult CheckValueExists(string Email)
        {
            bool exists = dbb.Users.Any(c => c.Email == Email);
            return Json(exists, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Register_Acc()
        {
            {
                ViewBag.Name = new SelectList(context.Roles.Where(u => (!(u.Name == "Can_Post_Shift" || u.Name == "Can_Take_Shift" || u.Name == "Can_Approve_Shift" || u.Name == "Can_Delete_Shift"
               || u.Name == "Can_View_Shift")))
                                       .ToList(), "Name", "Name");
                return View();
            }
        }

        // POST: /Account/Register
        [HttpPost]

        [ValidateAntiForgeryToken]
        // [Authorize(Roles = "System")]
        public async Task<ActionResult> Register_Acc(RegisterViewModel model)
        {
            if (model.UserRoles == null)
            {
                TempData["msg"] = "Kindly select user account type";
                return RedirectToAction("Register_Acc");
            }
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.UserName, Email = model.Email ,PhoneNumber=model.PhoneNumber};
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {


                    // await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    await this.UserManager.AddToRoleAsync(user.Id, model.UserRoles);
                    //Ends Here     

                    string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    var callbackurl = Url.Action("confirmEmail", "Account", new { userid = user.Id, code = code }, protocol: Request.Url.Scheme);
                   

                    TempData["msg"] = "✔  Account Created successfully ";




                    PushEmail(user.Email, "Confirm Account", "Kindly confirm your account via clicking the link<a href=\"" + callbackurl, DateTime.Now);



                    return RedirectToAction("login", "Account");
                }
               
            }

            // If we got this far, something failed, redisplay form    
            return View();
        }
        private string connectionString = ConfigurationManager.ConnectionStrings["Compassion"].ConnectionString;
        public void PushEmail(string Recipient, string Subject, string Body, DateTime CreatedOn)
        {
            string query = "INSERT INTO OutgoingEmails (Recipient,Subject,Body,Status,CreatedOn,Trials,Response) VALUES " +
            "                                          (@Recipient,@Subject, @Body,0,@CreatedOn,@Trials,@Response)";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {

                    command.Parameters.AddWithValue("@Recipient", Recipient);
                    command.Parameters.AddWithValue("@Subject", Subject);
                    command.Parameters.AddWithValue("@Body", Body);
                    command.Parameters.AddWithValue("@CreatedOn", CreatedOn);
                    command.Parameters.AddWithValue("@Trials", 0);
                    command.Parameters.AddWithValue("@Response", "--waiting--");
                    command.ExecuteNonQuery();
                }
            }
        }
        public void PushSms(string Recipient, string Subject, DateTime CreatedOn)
        {
            string query = "INSERT INTO Outgoingsms (MessageText,IsSent,CreatedOn,RecipientNumber,Trials,Response) VALUES " +
            "                                          (@MessageText,@IsSent,@CreatedOn,@RecipientNumber,@Trials,@Response)";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {

                    command.Parameters.AddWithValue("@MessageText", Subject);
                    command.Parameters.AddWithValue("@IsSent", 0);
                    command.Parameters.AddWithValue("@CreatedOn", CreatedOn);
                    command.Parameters.AddWithValue("@RecipientNumber", Recipient);
                    command.Parameters.AddWithValue("@Trials", 0);
                    command.Parameters.AddWithValue("@Response", "--waiting--");
                    command.ExecuteNonQuery();
                }
            }
        }

        // GET: /Account/Register

        // [Authorize(Roles = "System")]
        public ActionResult Rspec007()
        {
            {
                ViewBag.Name = new SelectList(context.Roles.Where(u => (!(u.Name == "Can_Post_Shift")))
                                       .ToList(), "Name", "Name");
                return View();
            }
        }

        // POST: /Account/Register
        [HttpPost]

        [ValidateAntiForgeryToken]
        // [Authorize(Roles = "System")]
        public async Task<ActionResult> Rspec007(RegisterViewModel model)
        {
            if (model.UserRoles == null)
            {
                TempData["msg"] = "Kindly select user account type";
                return RedirectToAction("Rspec007");
            }
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.UserName, Email = model.Email };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {


                   // await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    await this.UserManager.AddToRoleAsync(user.Id, model.UserRoles);
                    //Ends Here     

                   

                    string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    var callbackurl = Url.Action("confirmEmail", "Account", new { userid = user.Id, code = code }, protocol: Request.Url.Scheme);
                    await UserManager.SendEmailAsync(user.Id, "Confirm your Account", "Kindly confirm your account via clicking the link<a href=\"" + callbackurl + "\">here</a>");

                    TempData["msg"] = "✔ Account Created successfully ,Confirmation sent to user account mail";
                   

                   
                    return RedirectToAction("Index", "Data");
                }
                ViewBag.Name = new SelectList(context.Roles.Where(u => !u.Name.Contains("Admin"))
                                          .ToList(), "Name", "Name");

               
                AddErrors(result);

                



            }

            // If we got this far, something failed, redisplay form    
            return View();
        }

        


        public ActionResult C()
        {

            return View();

        }

        // POST: /Account/Register
        [HttpPost]

        [ValidateAntiForgeryToken]
        // [Authorize(Roles = "System")]
        public async Task<ActionResult> C(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.UserName, Email = model.Email };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {


                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    await this.UserManager.AddToRoleAsync(user.Id, model.UserRoles);
                    //Ends Here     
                    return RedirectToAction("Index", "Home");
                }
                ViewBag.Name = new SelectList(context.Roles.Where(u => !u.Name.Contains("Admin"))
                                          .ToList(), "Name", "Name");
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form    
            return View();
        }



        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var result = await UserManager.ConfirmEmailAsync(userId, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }
      
        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }


        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByEmailAsync(model.Email);
                if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    TempData["msg"] = "No account found under provided mail";
                    return View("ForgotPassword");
                }

                // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                 var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);		
                 await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
                TempData["msg"] = "Password reset sent to provided mail ";
                return RedirectToAction("ForgotPassword", "Account");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await UserManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            AddErrors(result);
            return View();
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }















































        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/SendCode
        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
        {
            var userId = await SignInManager.GetVerifiedUserIdAsync();
            if (userId == null)
            {
                return View("Error");
            }
            var userFactors = await UserManager.GetValidTwoFactorProvidersAsync(userId);
            var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
            return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/SendCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendCode(SendCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Generate the token and send it
            if (!await SignInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
            {
                return View("Error");
            }
            return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
                case SignInStatus.Failure:
                default:
                    // If the user does not have an account, then prompt the user to create an account
                    ViewBag.ReturnUrl = returnUrl;
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                    return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
            }
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }
        public ActionResult ValidateEmail(string email)
        {
            bool isValid = IsValidEmail(email);

            return Json(new { isValid = isValid });
        }

        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            // Regex pattern to validate email addresses
            string pattern = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";

            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(pattern);

            return regex.IsMatch(email);
        }
        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
       
        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}