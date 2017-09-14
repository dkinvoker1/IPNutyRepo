using IPNuty.Models;
using IPNuty.Models.Managers.Admin;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace IPNuty.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
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
            if(this.HttpContext.User!=null && this.HttpContext.User.Identity!=null && this.HttpContext.User.Identity.IsAuthenticated)
            {
                AuthenticationManager.SignOut();
                Session.Abandon();
                return RedirectToAction("Index", "Home");
            }
            ViewBag.ReturnUrl = returnUrl;
            return View();
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


            var result = await SignInManager.PasswordSignInAsync(model.Login, model.Password, model.RememberMe, shouldLockout: false);

            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Nieudana próba logowania, spróbuj ponownie.");
                    return View(model);
            }
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }


        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                Singer singer = new Singer.Builder(model.Name,model.Surename).SetActivicity(model.Activity).build();
                
                var user = new ApplicationUser {UserName = model.Name+model.Surename ,SingerId=singer};
                var result = await UserManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    //to powinno nadać rolę
                    var currentUser = UserManager.FindByName(user.UserName);
                    var roleresult = UserManager.AddToRole(currentUser.Id, "Singer");

                    //await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    ViewBag.Message = "Dodano chórzystę do bazy!";
                    ModelState.Clear();
                    return View();
                    //return RedirectToAction("Register", "Account");
                }
                AddErrors(result);
            }

            return View(model);
        }

        ///// <summary>
        ///// Usuwanie singera przez admina
        ///// </summary>
        ///// <param name="singerToBeDeleted"> Singer który ma być usunięty </param>
        ///// <returns></returns>

        //[HttpGet]
        //[Authorize(Roles = "Admin")]
        //public ActionResult DeleteUser(Singer singerToBeDeleted)
        //{
        //    //pobranie identity singera (hasła, role, logint)
        //    var identitySinger=UserManager.Users.Where(e => e.SingerId == singerToBeDeleted).FirstOrDefault();
        //    //usunięcie wszystkich nut singera
        //    SheetMusicManager musicManager = new SheetMusicManager();
        //    musicManager.RemoveSheetMusic(singerToBeDeleted);
        //    //tutaj to samo co wyżej ale dla orderów jak Ola zrobi
        //    //====================================================

        //    //====================================================
        //    UserManager.RemoveFromRole(identitySinger.Id, "Singer");
        //    UserManager.Delete(identitySinger);
        //    //return View("Index");
        //    return new EmptyResult();
        //}

        //z jakiegoś powodu nie działało, przeniesione do loginu
        // POST: /Account/LogOff
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult LogOff()
        //{
        //    AuthenticationManager.SignOut();
        //    Session.Abandon();
        //    return RedirectToAction("Index", "Home");
        //}

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
    }
}