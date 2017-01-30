using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using BLL.Interface.Entities;
using BLL.Interface.Services;
using MvcPL.Infrastructure.Mappers;
using MvcPL.Models;
using WebMatrix.WebData;

namespace MvcPL.Controllers
{
    [Authorize]
    public class AccountController : _BaseController
    {

        public AccountController(IUserService userService, ISongService songService,
            IAlbumService albumService, ISingerService singerService,
            ICommentSongService commentSongService)
            : base(userService, songService, singerService, albumService, commentSongService)
        {

        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            LoginViewModel lvm = new LoginViewModel();

            InitializeBaseModel(lvm);

            return View(lvm);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid && UserService.LoginUser(model.UserData.ToBllUser()))
            {
                if (returnUrl == null)
                    return RedirectToAction("Index", "Home");

                return Redirect(returnUrl);
            }

            // If we got this far, something failed, redisplay form
            ModelState.AddModelError("", "Wrong Login or Password");
            InitializeBaseModel(model);
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult Registration()
        {
            RegistrateViewModel lvm = new RegistrateViewModel();

            InitializeBaseModel(lvm);

            return View(lvm);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Registration(RegistrateViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Attempt to register the user
                try
                {
                    UserService.RegistrateUser(model.UserData.ToBllUser());
                    return RedirectToAction("Index", "Home");
                }
                catch (MembershipCreateUserException e)
                {
                    ModelState.AddModelError("", ErrorCodeToString(e.StatusCode));
                }
            }

            // If we got this far, something failed, redisplay form
            InitializeBaseModel(model);
            return View(model);
        }

        [HttpPost]

        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            WebSecurity.Logout();

            return RedirectToAction("Index", "Home");
        }

        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
    }
}
