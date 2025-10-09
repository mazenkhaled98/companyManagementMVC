using Demo.DataAccess.Models.IdentityModule;
using Demo.presentation.Controllers;
using Demo.presentation.ViewModels.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Presentation.Controllers
{
    public class AccountController(UserManager<ApplicationUser> _userManager,SignInManager<ApplicationUser> _signInManager) : Controller
    {
        //Register , SignIn , SignOut

        #region Register
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid) return View(registerViewModel);
            var user = new ApplicationUser
            {
                UserName = registerViewModel.UserName,
                FirstName = registerViewModel.FirstName,
                LastName = registerViewModel.LastName,
                Email = registerViewModel.Email
            };
            var result = _userManager.CreateAsync(user, registerViewModel.Password).Result;
            if (result.Succeeded)
                return RedirectToAction("Login");
            else
            {
                foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description); //Global Error

                return View(registerViewModel);
            }
        }
        #endregion

        #region Login
        [HttpGet]
        public IActionResult Login()=> View();


        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid) return View(loginViewModel);
            //1- Find User By Email
            //2- if user [ is not null ]
            //3- Check Password
            //4- SinIn User
            //5- Check if account is not allowed or locked
            var user = _userManager.FindByEmailAsync(loginViewModel.Email).Result;
            if (user is not null)
            {
                var flag = _userManager.CheckPasswordAsync(user, loginViewModel.Password).Result;
                if (flag)
                {
                    //User with Email exists + Password correct
                    var result = _signInManager.PasswordSignInAsync(user, loginViewModel.Password, loginViewModel.RememberMe, false).Result;
                    if (result.IsNotAllowed)
                        ModelState.AddModelError(string.Empty, "Your account is not allowed to login!");
                    else if (result.IsLockedOut)
                        ModelState.AddModelError(string.Empty, "Your account is locked!");
                    else
                        //Login Success
                        return RedirectToAction(nameof(HomeController.Index), "Home");
                }
            }
            ModelState.AddModelError(string.Empty, "Invalid Login!");
            return View(loginViewModel);
        }






        #endregion

        #region Signout

       [HttpGet]
        public new virtual IActionResult SignOut()
        {
            _signInManager.SignOutAsync().GetAwaiter().GetResult();
            return RedirectToAction(nameof(Login));
        }
        #endregion
    }
}
