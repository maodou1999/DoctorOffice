using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Owin;
using CherryCitySoftware.MedicalOffice.Models;
using CherryCitySoftware.MedicalOffice.Context;
using System.Net;
using CherryCitySoftware.MedicalOffice.extensions;

namespace CherryCitySoftware.MedicalOffice.Controllers
{
    [Authorize]
    [RequireSsl]
    public class AccountController : Controller
    {
        private ApplicationUserManager _userManager;

        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }

        public ApplicationUserManager UserManager {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ActionResult Index()
        {          
            return View(new CherryCitySoftware.MedicalOffice.Context.ApplicationDbContext().Users.OrderBy(u=>u.FirstName).ThenBy(u=>u.LastName).ToList());
        }

        [Authorize(Roles="Admin")]
        public ActionResult Details(string Id)
        {
            ApplicationDbContext db= new ApplicationDbContext();
            var user = db.Users.Find(Id);

            CherryCitySoftware.MedicalOffice.Models.EditUserViewModel model = new EditUserViewModel(user)
            {
                Address=user.Address,
                City=user.City,
                State=user.State,
                Email=user.Email,
                PostalCode=user.PostalCode,
                Id=user.Id,
                BirthDay=user.BirthDay
            };
            return View(model);
        }

        [Authorize(Roles="Admin")]
        public ActionResult UserDetails(string Id)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var user = db.Users.Find(Id);
            var model = new UserDetailsViewModel()
            {
                User = user,
                UserRoles = user.GetRoles(),
                Policies=db.PatientPolicies.Where(p=>p.Patient.Id==Id)
            };

            return View(model);
        }
        [HttpPost]
        [Authorize(Roles= "Admin")]
        public ActionResult Details(EditUserViewModel model)
        {
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles="Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ApplicationUser model)
        {
            return View(model);
        }
        [Authorize(Roles="Admin")]
        public ActionResult Edit(string id)
        {
             ApplicationDbContext db= new ApplicationDbContext();
            CherryCitySoftware.MedicalOffice.Models.ApplicationUser model = db.Users.Find(id);
           return View(model);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditUser(EditUserViewModel model)
        {
        //    return View(model);
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByIdAsync(model.Id); //new ApplicationUser() { UserName = model.Email, Email = model.Email };
               
                // Add the Address properties:
                user.FirstName = model.FirstName;
                user.MiddleName = model.MiddleName;
                user.LastName = model.LastName;
                user.BirthDay = model.BirthDay;
                user.Address = model.Address;
                user.Email = model.Email;
                user.City = model.City;
                user.State = model.State;
                user.PostalCode = model.PostalCode;
                user.PhoneNumber = model.PhoneNumber;
               
                var db = new ApplicationDbContext();
                db.ClearUserRoles(UserManager, user.Id);
                foreach (var r in model.Roles.Where(rr => rr.Selected))
                {
                 //   await UserManager.RemoveFromRoleAsync(user.Id, r.RoleName);
                    await UserManager.AddToRoleAsync(user.Id, r.RoleName);
                }
       //         var result = await UserManager.UpdateAsync(user);
                return RedirectToAction("Index");

                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");
                //AddErrors(result);
                //return View(model);


            }
            // If we got this far, something failed, redisplay form
            return View(model);
        }
        [Authorize(Roles="Admin")]
        public async Task<ActionResult> EditUser(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var user = await UserManager.FindByIdAsync(id);
          
            if (user == null)
            {
                return HttpNotFound();
            }
           
            return View(new EditUserViewModel(user)

            {
                Id = user.Id,
                Email = user.Email,
                FirstName=user.FirstName,
                MiddleName=user.MiddleName,
                LastName=user.LastName,
                BirthDay=user.BirthDay,
                PhoneNumber =user.PhoneNumber,

                // Include the Addresss info:
                Address = user.Address,
                City = user.City,
                State = user.State,
                PostalCode = user.PostalCode
               
               
            });
 
            
        }

        [Authorize]
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = await UserManager.FindByIdAsync(id);
            
            if (user == null)
            {
                return HttpNotFound();
            }
            ApplicationDbContext db = new ApplicationDbContext();
           
            var model = new UserDetailsViewModel()
            {
                User = user,
                UserRoles = user.GetRoles(),
                Policies = db.PatientPolicies.Where(p => p.Patient.Id == id)
            };
            return View("UserDetailsDelete", model);
        }

        // POST: Messages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            var user = await UserManager.FindByIdAsync(id);
            if (user != null)
            {
                //delete policies
                var db = new ApplicationDbContext();
                var policies = db.PatientPolicies.Where(p => p.Patient.Id.Equals(id, StringComparison.InvariantCultureIgnoreCase)).ToList();
                foreach (var p in policies)
                {
                    db.Entry(p).State = System.Data.Entity.EntityState.Deleted;
                    await db.SaveChangesAsync();
                }
                //delete messages
                var messages = db.Messages
                    .Where(m => m.Recepient.Id.Equals(id, StringComparison.InvariantCultureIgnoreCase)
                    || m.Sender.Id.Equals(id, StringComparison.InvariantCultureIgnoreCase)).ToList();

                foreach(var m in messages)
                {
                    var doc = m.DocFile;
                    if (doc != null)
                    {
                        var list = doc.ToList();
                        for (int i = 0; i < list.Count; i++)
                        {
                            db.DocFiles.Remove(list[i]);
                        }

                    }

                    //if(doc !=null)
                    //{ foreach (var d in doc)
                    //    db.Entry(d).State = System.Data.Entity.EntityState.Deleted;
                    //await db.SaveChangesAsync();
                    //}
                    db.Entry(m).State = System.Data.Entity.EntityState.Deleted;
                    await db.SaveChangesAsync();
                }

                await UserManager.DeleteAsync(user);
            }
            return RedirectToAction("Index");
        }
        

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        private ApplicationSignInManager _signInManager;

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set { _signInManager = value; }
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
            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: true);
            switch (result)
            {
                case SignInStatus.Success:

                    //return RedirectToLocal(returnUrl);
                    var user = await UserManager.FindAsync(model.Email, model.Password);

                    if (user.Forcechangpassword == true)
                    {
                        if (string.IsNullOrEmpty(returnUrl)) returnUrl ="/Messages";
                        ViewBag.ReturnUrl = returnUrl;
                        return RedirectToAction("Manage", new { Message = ManageMessageId.FirstTimeLogin, ReturnUrl = returnUrl });
                    }
                    return RedirectToLocal(returnUrl);



                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
        }

        //
        // GET: /Account/Register
        [Authorize(Roles="Admin")]
        public ActionResult Register()
        {
            RegisterViewModel model = new RegisterViewModel();
            return View(model);
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [Authorize(Roles="Admin")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser() { UserName = model.Email, Email = model.Email };
                // Add the Address properties:
                user.FirstName = model.FirstName;
                user.MiddleName = model.MiddleName;
                user.LastName = model.LastName;
                user.BirthDay = model.BirthDay;
                user.Address = model.Address;
                user.City = model.City;
                user.State = model.State;
                user.PostalCode = model.PostalCode;
                user.Forcechangpassword = true;
                user.EmailConfirmed = true;
                IdentityResult result = await UserManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    //await SignInAsync(user, isPersistent: false);
                    foreach (var r in model.Roles.Where(rr => rr.Selected))
                        await UserManager.AddToRoleAsync(user.Id, r.RoleName);
                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    return RedirectToAction("Index");
                }
                else
                {
                    AddErrors(result);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null) 
            {
                return View("Error");
            }

            IdentityResult result = await UserManager.ConfirmEmailAsync(userId, code);
            if (result.Succeeded)
            {
                return View("ConfirmEmail");
            }
            else
            {
                AddErrors(result);
                return View();
            }
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
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    ModelState.AddModelError("", "The user either does not exist or is not confirmed.");
                    return View();
                }

                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                   string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                
                   var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);		
                   await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
                   return RedirectToAction("ForgotPasswordConfirmation", "Account");
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
            if (code == null)
            {
                return View("Error");
            }
            return View();
        }

     //   public ActionResult ResetPassword()
        //{
            
        //    return View();
        //}
        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user == null)
                {
                    ModelState.AddModelError("", "No user found.");
                    return View();
                }
                IdentityResult result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
                if (result.Succeeded)
                {
                    //user.Forcechangpassword = true;
                    //await UserManager.UpdateAsync(user);
                   await UserManager.SendEmailAsync(user.Id, "Your password has been changed", "Your password has been changed. If you didn't chang it  ,please contact Admin.");
                    return RedirectToAction("ResetPasswordConfirmation", "Account");
                }
                else
                {
                 //   AddErrors(result);
                    ModelState.AddModelError("", "Email is not correct.");
                    return View();
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }
        [Authorize(Roles="Admin")]
        public async Task<ActionResult> ResetPasswordByAdmin(string Id)
        {
            if (Id == null)
            {
                return View("Error");
            }
            
             var user =  UserManager.FindById(Id);
            var model = new ResetPasswordByAdminViewModel();
            string  code =await  UserManager.GeneratePasswordResetTokenAsync(Id);
            
            model.Code = code;
            model.DisplayName = user.DisplayName;
           
            model.Email = user.Email;
            
            return View(model);
        }
        // POST: /Account/ResetPassword
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPasswordByAdmin(ResetPasswordByAdminViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user == null)
                {
                    ModelState.AddModelError("", "No user found.");
                    return View();
                }
                IdentityResult result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
                if (result.Succeeded)
                {
                    user.Forcechangpassword = true;
                    await UserManager.UpdateAsync(user);
                    await UserManager.SendEmailAsync(user.Id, "Your password has been reset by admin", "Your password has been reset. If you didn't ask for reset  ,please contact Admin.");
                    ViewBag.Name = user.DisplayName;
                    return RedirectToAction("ResetPasswordByAdminConfirmation", "Account", new { name= user.DisplayName });
                }
                else
                {
                    AddErrors(result);
                    return View();
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult ResetPasswordByAdminConfirmation(string name)
        {
            ViewBag.Name = name;
            return View();
        }
        //
        // POST: /Account/Disassociate
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Disassociate(string loginProvider, string providerKey)
        {
            ManageMessageId? message = null;
            IdentityResult result = await UserManager.RemoveLoginAsync(User.Identity.GetUserId(), new UserLoginInfo(loginProvider, providerKey));
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                await SignInAsync(user, isPersistent: false);
                message = ManageMessageId.RemoveLoginSuccess;
            }
            else
            {
                message = ManageMessageId.Error;
            }
            return RedirectToAction("Manage", new { Message = message });
        }

        //
        // GET: /Account/Manage
        [Authorize()]
        public ActionResult Manage(ManageMessageId? message, string returnUrl)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                : message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
                : message == ManageMessageId.Error ? "An error has occurred."
                :message==ManageMessageId.FirstTimeLogin? "You must change Password."
                : "";
            ViewBag.HasLocalPassword = HasPassword();
            ViewBag.ReturnUrl = Url.Action("Manage");

            return View(new ManageUserViewModel() { ReturnUrl = returnUrl });
        }

        //
        // POST: /Account/Manage
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize()]
        public async Task<ActionResult> Manage(ManageUserViewModel model)
        {
            bool hasPassword = HasPassword();
            ViewBag.HasLocalPassword = hasPassword;
            ViewBag.ReturnUrl = string.IsNullOrEmpty(model.ReturnUrl) ? Url.Action("Manage") : model.ReturnUrl;
            if (hasPassword)
            {
                if (ModelState.IsValid)
                {
                    IdentityResult result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
                    if (result.Succeeded)
                    {
                        
                        var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                        await UserManager.SendEmailAsync(user.Id, "Your password changed", "Your password has been changed. If you did not change it, please logon the website and change your password by yourself.");
                        if (user.Forcechangpassword == true)
                        {
                            user.Forcechangpassword = false;
                            await UserManager.UpdateAsync(user);
                        }
                        await SignInAsync(user, isPersistent: false);
                        if (!string.IsNullOrEmpty(model.ReturnUrl))
                            return RedirectToLocal(model.ReturnUrl);
                        //    string userid = User.Identity.GetUserId();
                        //     return RedirectToAction("UserDetails", new { Id = userid });
                        return RedirectToAction("Manage", new { Message = ManageMessageId.ChangePasswordSuccess });
                    }
                    else
                    {
                        AddErrors(result);
                    }
                }
            }
            else
            {
                // User does not have a password so remove any validation errors caused by a missing OldPassword field
                ModelState state = ModelState["OldPassword"];
                if (state != null)
                {
                    state.Errors.Clear();
                }

                if (ModelState.IsValid)
                {
                    IdentityResult result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Manage", new { Message = ManageMessageId.SetPasswordSuccess });
                    }
                    else
                    {
                        AddErrors(result);
                    }
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
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
            var user = await UserManager.FindAsync(loginInfo.Login);
            if (user != null)
            {
                await SignInAsync(user, isPersistent: false);
                return RedirectToLocal(returnUrl);
            }
            else
            {
                // If the user does not have an account, then prompt the user to create an account
                ViewBag.ReturnUrl = returnUrl;
                ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
            }
        }

        //
        // POST: /Account/LinkLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LinkLogin(string provider)
        {
            // Request a redirect to the external login provider to link a login for the current user
            return new ChallengeResult(provider, Url.Action("LinkLoginCallback", "Account"), User.Identity.GetUserId());
        }

        //
        // GET: /Account/LinkLoginCallback
        public async Task<ActionResult> LinkLoginCallback()
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync(XsrfKey, User.Identity.GetUserId());
            if (loginInfo == null)
            {
                return RedirectToAction("Manage", new { Message = ManageMessageId.Error });
            }
            IdentityResult result = await UserManager.AddLoginAsync(User.Identity.GetUserId(), loginInfo.Login);
            if (result.Succeeded)
            {
                return RedirectToAction("Manage");
            }
            return RedirectToAction("Manage", new { Message = ManageMessageId.Error });
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
                return RedirectToAction("Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser() { UserName = model.Email, Email = model.Email };
                IdentityResult result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInAsync(user, isPersistent: false);
                        
                        // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                        // Send an email with this link
                        // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                        // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                        // SendEmail(user.Email, callbackUrl, "Confirm your account", "Please confirm your account by clicking this link");
                        
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult RemoveAccountList()
        {
            var linkedAccounts = UserManager.GetLogins(User.Identity.GetUserId());
            ViewBag.ShowRemoveButton = HasPassword() || linkedAccounts.Count > 1;
            return (ActionResult)PartialView("_RemoveAccountPartial", linkedAccounts);
        }

        
        protected override void Dispose(bool disposing)
        {
            if (disposing && UserManager != null)
            {
                UserManager.Dispose();
                UserManager = null;
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

        private async Task SignInAsync(ApplicationUser user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, await user.GenerateUserIdentityAsync(UserManager));
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private bool HasPassword()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

        private void SendEmail(string email, string callbackUrl, string subject, string message)
        {
            // For information on sending mail, please visit http://go.microsoft.com/fwlink/?LinkID=320771
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            FirstTimeLogin,
            Error
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        private class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri) : this(provider, redirectUri, null)
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
                var properties = new AuthenticationProperties() { RedirectUri = RedirectUri };
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