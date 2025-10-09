using Demo.BusinessLogic.Services.EmailSender;
using Demo.DataAccess.Models.IdentityModule;
using Demo.DataAccess.Models.Shared;
using Demo.presentation.Controllers;
using Demo.presentation.ViewModels.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Presentation.Controllers
{
    public class AccountController(UserManager<ApplicationUser> _userManager,SignInManager<ApplicationUser> _signInManager ,IEmailSender _emailSender) : Controller

    {
        //Register , SignIn , SignOut , forget password

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

        #region ForgetPassword
        [HttpGet]
        public IActionResult ForgetPassword() => View(); //return form with one input field [Email]


        [HttpPost]
        [AutoValidateAntiforgeryToken]

        public IActionResult SendResetPasswordUrl(ForgetPasswordViewModel forgetPasswordViewModel)
        {
           if(ModelState.IsValid)
            {
              var user=  _userManager.FindByEmailAsync(forgetPasswordViewModel.Email).Result;
                if (user != null)
                {
                    //email ==> to , subject , body
                    //user defined data tybe for this ==> to string , subject string ,body string
                    var token=_userManager.GeneratePasswordResetTokenAsync(user).Result;
                    var url = Url.Action("ResetPassword", "Account", new { email = forgetPasswordViewModel.Email ,token }, Request.Scheme);
                    var email = new Email()
                    {
                        To = forgetPasswordViewModel.Email,
                        Subject = "Reset your password",
                        //baseurl/Account/Resetpassword?Email=MAriam@gmail.com&token=xndjndxj
                        //Body= //url == > reset password [form ] [new password , confirm password]
                        Body = url

                    };
                    //send email [shared]
                    _emailSender.SendEmail(email);
                    return RedirectToAction("CheckYourInbox");
                }
                else
                {
                    ModelState.AddModelError("", "invalid operation");
                }
            }

            return View(forgetPasswordViewModel);
           
        }

        [HttpGet]
        public IActionResult CheckYourInbox() => View();

        #endregion


        #region Ressetpassword

        #endregion
    }
}
