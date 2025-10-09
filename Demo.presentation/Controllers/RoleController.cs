using Demo.presentation.ViewModels.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Demo.presentation.Controllers
{
    public class RoleController(RoleManager<IdentityRole> _roleManager,IWebHostEnvironment _env) : Controller
    {

        #region Index
        [HttpGet]
        public IActionResult Index(string searchValue)
        {
            var rolesQuery = _roleManager.Roles.AsQueryable();
            if (!string.IsNullOrEmpty(searchValue))
            {
                rolesQuery = rolesQuery.Where(r => r.Name.ToLower().Contains(searchValue.ToLower()));
            }
            var roles = rolesQuery.Select(r => new RoleViewModel()
            {
                Id = r.Id,
                Name = r.Name,
            }).ToList();
            return View(roles);
        }
        #endregion

        #region Details

        [HttpGet]
        public IActionResult Details(string? id)
        {
            if (id is null) return BadRequest();
            var role = _roleManager.FindByIdAsync(id).Result;
            if (role is null) return NotFound();
            var roleViewModel = new RoleViewModel()
            {
                Id = role.Id,
                Name = role.Name,
            };
            return View(roleViewModel);
        }

        #endregion

        #region Create

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(RoleViewModel roleViewModel)
        {
            if (!ModelState.IsValid) return View(roleViewModel);
            string message = string.Empty;
            try
            {
                var role = new IdentityRole()
                {
                    Name = roleViewModel.Name,
                };
                var result = _roleManager.CreateAsync(role).Result;
                if (result.Succeeded) return RedirectToAction(nameof(Index));
                else
                    message = "Error while creating the role!";
            }
            catch (Exception ex)
            {
                message = _env.IsDevelopment() ? ex.Message : "Some error occurred!";
            }
            ModelState.AddModelError(string.Empty, message);
            return View(roleViewModel);
        }

        #endregion

        #region Edit

        [HttpGet]
        public IActionResult Edit(string? id)
        {
            if (id is null) return BadRequest();
            var role = _roleManager.FindByIdAsync(id).Result;
            if (role is null) return NotFound();
            var roleViewModel = new RoleViewModel()
            {
                Id = role.Id,
                Name = role.Name,
            };
            return View(roleViewModel);
        }

        [HttpPost]
        public IActionResult Edit(RoleViewModel roleViewModel, string id)
        {
            if (!ModelState.IsValid) return View(roleViewModel);
            if (roleViewModel.Id != id) return BadRequest();
            string message = string.Empty;
            try
            {
                var role = _roleManager.FindByIdAsync(id).Result;
                if (role is null) return NotFound();
                role.Name = roleViewModel.Name;
                var result = _roleManager.UpdateAsync(role).Result;
                if (result.Succeeded) return RedirectToAction(nameof(Index));
                else
                    message = "Error while updating the role!";
            }
            catch (Exception ex)
            {
                message = _env.IsDevelopment() ? ex.Message : "Some error occurred!";
            }
            ModelState.AddModelError(string.Empty, message);
            return View(roleViewModel);
        }




        #endregion

        #region Delete

        [HttpPost]
        public IActionResult Delete(string id)
        {
            var role = _roleManager.FindByIdAsync(id).Result;
            if (role is null) return NotFound();
            string message = string.Empty;
            try
            {
                var result = _roleManager.DeleteAsync(role).Result;
                if (result.Succeeded) return RedirectToAction(nameof(Index));
                else
                    message = "Error while deleting the role!";
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
