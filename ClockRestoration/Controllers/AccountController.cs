using ClockRestoration.Entities;
using ClockRestoration.Infrustructure;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ClockRestoration.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthenticationManager authenticationManager;
        private readonly IApplicationUserManager applicationUserManager;

        public AccountController(IAuthenticationManager authenticationManager, IApplicationUserManager applicationUserManager)
        {
            this.authenticationManager = authenticationManager;
            this.applicationUserManager = applicationUserManager;
        }

        public ActionResult Login()
        {
            ViewBag.Title = "Sign-In";

            return View();
        }

        public ActionResult Register()
        {
            ViewBag.Title = "Sign-Up";

            return View();
        }

        [Route("SignOut")]
        public ActionResult SignOut()
        {
            this.authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Login");
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = new ApplicationUser()
            {
                UserName = model.Email,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber
            };

            IdentityResult result = await this.applicationUserManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                return View(model);
            }

            ClaimsIdentity claim = await this.applicationUserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);

            this.authenticationManager.SignIn(new AuthenticationProperties { IsPersistent = true }, claim);

            // var a = IsAuthenticated();
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            this.authenticationManager.SignOut();
            var user = await this.applicationUserManager.FindAsync(model.Email, model.Password);
            if (user == null)
            {
                return View("Error");
            }

            ClaimsIdentity claim = await this.applicationUserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            this.authenticationManager.SignIn(new AuthenticationProperties { IsPersistent = true }, claim);
            return RedirectToAction("Index", "Home");
        }
    }
}