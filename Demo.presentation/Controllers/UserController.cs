using Demo.DataAccess.Models.IdentityModule;
using Demo.presentation.ViewModels.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Demo.presentation.Controllers
{
    [Authorize]
    public class UserController(UserManager<ApplicationUser> _userManager) : Controller
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
    }
}
