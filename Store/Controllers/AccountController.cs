using System;
using System.Web.Mvc;
using System.Web.Security;
using Store.Helpers;
using Store.Models;

namespace Store.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult LogOn()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult LogOn(LogOnModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(model.UserName, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                    CartHelper.MigrateCartWhenAuthorizing(HttpContext, model.UserName);

                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 
                        && returnUrl.StartsWith("/")
                        && !returnUrl.StartsWith("//") 
                        && !returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);
                    }

                    return RedirectToAction("Index", "Store");
                }

                ModelState.AddModelError("", "Username or password are incorrect.");
            }
            
            return View(model);
        }
        
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            CartHelper.RemoveSessionCartIdOnLogout(HttpContext);

            return RedirectToAction("Index", "Store");
        }
        
        public ActionResult Register()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                MembershipCreateStatus createStatus;
                Membership.CreateUser(model.UserName, model.Password, model.Email, null, null, true, null, out createStatus);

                if (createStatus == MembershipCreateStatus.Success)
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, false);
                    CartHelper.MigrateCartWhenAuthorizing(HttpContext, model.UserName);
                    return RedirectToAction("Index", "Store");
                }

                ModelState.AddModelError("", ErrorCodeToString(createStatus));
            }
            
            return View(model);
        }
        
        [Authorize]
        public ActionResult ChangePassword()
        {
            return View();
        }
        
        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {
                bool changePasswordSucceeded;
                try
                {
                    MembershipUser currentUser = Membership.GetUser(User.Identity.Name, true);
                    changePasswordSucceeded = currentUser.ChangePassword(model.OldPassword, model.NewPassword);
                }
                catch (Exception)
                {
                    changePasswordSucceeded = false;
                }

                if (changePasswordSucceeded)
                {
                    return RedirectToAction("ChangePasswordSuccess");
                }

                ModelState.AddModelError("", "Current password is incorrect or new password is invalid.");
            }
            
            return View(model);
        }
        
        public ActionResult ChangePasswordSuccess()
        {
            return View();
        }

        #region Status Codes
        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "This username already exists. Enter another one.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "Username for this email address already exists. Enter another email.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password you entered is invalid. Enter a valid password.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The email you entered is invalid. Check it and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The answer you entered for password recovery is invalid. Check it and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The question you entered for password recovery is invalid. Check it and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The username you entered is invalid. Check it and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "Authentication provider returned an error. Check entered value and try again. If the problem still exists contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The request to create a user has been canceled. Check entered value and try again. If the problem still exists contact your system administrator.";

                default:
                    return "An unknown error occurred. Check entered value and try again. If the problem still exists contact your system administrator.";
            }
        }
        #endregion
    }
}
