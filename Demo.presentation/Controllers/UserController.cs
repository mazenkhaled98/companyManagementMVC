using Demo.DataAccess.Models.IdentityModule;
using Demo.presentation.ViewModels.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Demo.presentation.Controllers
{
    [Authorize]
    public class UserController(UserManager<ApplicationUser> _userManager , IWebHostEnvironment _env) : Controller
    {
        //service ==> usermanager
        //index ,details ,edit ,delete ,[create user register]

        #region Index
        [HttpGet]
        public IActionResult Index(string searchValue)
        {
            var usersQuery = _userManager.Users.AsQueryable();
            if (!string.IsNullOrEmpty(searchValue))
            {
                usersQuery = usersQuery.Where(u => u.Email.ToLower().Contains(searchValue.ToLower()));
            }
            var users = usersQuery.Select(u => new UserViewModel()
            {
                Id = u.Id,
                Email = u.Email,
                FirstName = u.FirstName,
                LastName = u.LastName,
                //Roles
            }).ToList();

            foreach (var user in users)
            {
                // Handle roles for each user
                user.Roles = _userManager.GetRolesAsync(_userManager.FindByIdAsync(user.Id).Result).Result;
            }

            return View(users);
        }
        #endregion

        #region Details

        [HttpGet]
        public IActionResult Details(string? id)
        {
            if (id is null) return BadRequest();
            var user = _userManager.FindByIdAsync(id).Result;
            if (user is null) return NotFound();
            var userViewModel = new UserViewModel()
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Roles = _userManager.GetRolesAsync(user).Result
            };
            return View(userViewModel);
        }

        #endregion
     
        #region Edit

        public IActionResult Edit(string? id)
        {
            if (id is null) return BadRequest();
            var user = _userManager.FindByIdAsync(id).Result;
            if (user is null) return NotFound();
            var userViewModel = new UserViewModel()
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Roles = _userManager.GetRolesAsync(user).Result
            };
            return View(userViewModel);
        }
        [HttpPost]
        public IActionResult Edit(UserViewModel userViewModel, string id)
        {
            if (!ModelState.IsValid) return View(userViewModel);
            if (userViewModel.Id != id) return BadRequest();
            string message = string.Empty;
            try
            {
                var user = _userManager.FindByIdAsync(id).Result;
                if (user is null) return NotFound();
                user.FirstName = userViewModel.FirstName;
                user.LastName = userViewModel.LastName;
                user.Email = userViewModel.Email;
                var result = _userManager.UpdateAsync(user).Result;
                if (result.Succeeded) return RedirectToAction(nameof(Index));
                else
                    message = "User can't be updated!";
            }
            catch (Exception ex)
            {
                message = _env.IsDevelopment() ? ex.Message : "Some error occurred!";
            }
            ModelState.AddModelError(string.Empty, message);
            return View(userViewModel);
        }

        #endregion

        #region Delete

        [HttpPost]
        public IActionResult Delete(string id)
        {
            var user = _userManager.FindByIdAsync(id).Result;
            if (user is null) return NotFound();
            string message = string.Empty;
            try
            {
                var result = _userManager.DeleteAsync(user).Result;
                if (result.Succeeded) return RedirectToAction(nameof(Index));
                else
                    message = "User can't be deleted!";
            }
            catch (Exception ex)
            {
                message = _env.IsDevelopment() ? ex.Message : "Some error occurred!";
            }
            ModelState.AddModelError(string.Empty, message);
            return RedirectToAction(nameof(Index));
        }

        #endregion
    }
}
