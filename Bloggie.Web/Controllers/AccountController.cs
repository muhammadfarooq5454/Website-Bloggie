using Bloggie.Web.Models.DataTransfers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Bloggie.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            var user = new IdentityUser()
            {
                Email = registerViewModel.Email,
                UserName = registerViewModel.UserName
            };

            var passwordHash = new PasswordHasher<IdentityUser>(); // Hashing Password

            user.PasswordHash = passwordHash.HashPassword(user, registerViewModel.Password);

            var identityresult = await _userManager.CreateAsync(user, registerViewModel.Password);

            if(identityresult.Succeeded) 
            {
                //Giving (User) Role to the newly created user
                var roleidentityresult = await _userManager.AddToRoleAsync(user, "User");

                if (roleidentityresult.Succeeded)
                {
                    //Show Success Notification
                    return RedirectToAction("Register");
                }
            }

            //Show Error Notification
            return View();
        }


        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            var loginModel = new LoginViewModel()
            {
                ReturnUrl = returnUrl
            };
            
            return View(loginModel);
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            var signInResult = await _signInManager.PasswordSignInAsync(loginViewModel.UserName,loginViewModel.Password,false,false);

            if(signInResult.Succeeded && signInResult != null)
            {

                if(!string.IsNullOrWhiteSpace(loginViewModel.ReturnUrl))
                {
                    //Show success Login
                    return Redirect(loginViewModel.ReturnUrl);
                }
                //sucessfully logged in notification
                return RedirectToAction("Index","Home");
            }

            //Show error notification
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index","Home");
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
