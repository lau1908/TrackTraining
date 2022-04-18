using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TrackTraining.Models;

namespace TrackTraining.Controllers
{
   [AllowAnonymous]
    public class AuthController : Controller
    {
        private readonly UserManager<AppUser> userManager;

        public AuthController()
            : this(Startup.UserManagerFactory.Invoke()) //Invoke vores usermanger 
        {
        }

        public AuthController(UserManager<AppUser> userManager)
        {
            this.userManager = userManager;
        }

        [HttpGet]
        public ActionResult LogIn(string returnUrl)
        {
            var model = new LogInModel
            {
                ReturnUrl = returnUrl
            };

            return View(model);//retunerer objektet model som indeholder den sidste side som brugeren forsøgte at gå ind på
        }

        [HttpPost]
        public async Task<ActionResult> LogIn(LogInModel model)//LogInModel bliver brugt som inputargument da Formen på loginsiden er bygget derefter
        {
            if (!ModelState.IsValid) //tjekker om forms er udfyldt rigtigt
            {
                return View();
            }

            var user = await userManager.FindAsync(model.Username, model.Password);//tjekker databasen igennem for at se om der er en bruger med det username og parsword
            if (user != null)//hvis der er en bruger med det login som der er blevet indtastet køres koden
            {
                var identity = await userManager.CreateIdentityAsync( //laver en speciel cookie, til den specifikke bruger
                    user, DefaultAuthenticationTypes.ApplicationCookie);

                var ctx = Request.GetOwinContext();
                var authManager = ctx.Authentication; //sætter cookien som værende godkendelse
                Session["username"] = model.Username;
                authManager.SignIn(identity); //logger ind med cookie

                return Redirect(GetRedirectUrl(model.ReturnUrl)); //bliver sendt til returnUrl
            }

            // user authN failed
            ModelState.AddModelError("", "Invalid email or password"); //vist denne fejl, hvis der er en error
            return View();
        }

        private string GetRedirectUrl(string returnUrl)
        {
            if (string.IsNullOrEmpty(returnUrl) || !Url.IsLocalUrl(returnUrl)) //tjekker om returnURl er tom eller ikke er lokal
            {
                return Url.Action("index", "home");  //så bliver de sendt til index
            }

            return returnUrl;
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register(RegisterModel model)
        {
            if (!ModelState.IsValid) //tjekker om forms er udfyldt rigtigt
            {
                return View();
            }

            var user = new AppUser //laver en variabel bruger, som er lig med data fra forms
            {
                UserName = model.Username,
                Email = model.Email
            };

            var result = await userManager.CreateAsync(user, model.Password);

            if (result.Succeeded) //hvis brugeren blev lavet
            {
                await SignIn(user);
                Session["username"] = model.Username;
                return RedirectToAction("index", "home");
            }
             
            foreach (var error in result.Errors) //hvis der er nogle fejl
            {
                ModelState.AddModelError("", error);
            }

            return View();
        }

        private async Task SignIn(AppUser user) //funktion til når brugeren skal logge ind
        {
            var identity = await userManager.CreateIdentityAsync(
                user, DefaultAuthenticationTypes.ApplicationCookie);
            var ctx = Request.GetOwinContext();
            var authManager = ctx.Authentication;
            authManager.SignIn(identity);
        }

        public ActionResult LogOut()//funktion til når brugeren skal logge ud
        {
            var ctx = Request.GetOwinContext();
            var authManager = ctx.Authentication;

            authManager.SignOut("ApplicationCookie");
            return RedirectToAction("index", "home");
        }

        protected override void Dispose(bool disposing) //memory cleanup
        {
            if (disposing && userManager != null)
            {
                userManager.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}