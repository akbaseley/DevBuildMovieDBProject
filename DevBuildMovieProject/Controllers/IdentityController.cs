using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevBuildMovieProject.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
namespace DevBuildMovieProject.Controllers
{
    public class IdentityController : Controller
    {
        public ActionResult Registration() { return View(); }
        [HttpPost] //takes the stuff in, does things to the
        public ActionResult Registration(LoginModel newUser)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new AppUser();
                user.UserName = newUser.UserName;
                user.Birthday = newUser.Birthday;
                var userManager = HttpContext.GetOwinContext().Get<UserManager<AppUser>>();
                var x = userManager.Create(user, newUser.Password);
                if (x.Succeeded)
                {
                    return RedirectToAction("Login", newUser);
                }
            }

            return RedirectToAction("Registration");
        }
        // GET: Identity
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginModel login)
        {
            if (ModelState.IsValid)
            {
                //creating variable UserManager through Owin by creating a UserManager with the AppUser specification
                var userManager = HttpContext.GetOwinContext().Get<UserManager<AppUser>>();
                //creating variable with authentication via Owin
                var authManager = HttpContext.GetOwinContext().Authentication;

                AppUser user = userManager.Find(login.UserName, login.Password);
                
                if(user != null)
                {   //creating an identity (ident) with the user 
                    //& this is the cookie that
                    //keeps user logged in throughout the program
                    var ident = userManager.CreateIdentity(
                        user, DefaultAuthenticationTypes.ApplicationCookie);
                    //using our instance of Authentication, we use the SignIn method
                    //IsPersistent = false will log out the user and terminate that cookie (ident)
                    authManager.SignIn(new AuthenticationProperties
                        { IsPersistent = false }, ident);

                    return RedirectToAction("Index", "Home");
                }
            }

            ModelState.AddModelError("", "Invalid username or password");
            return View(login);
        }
    }
}