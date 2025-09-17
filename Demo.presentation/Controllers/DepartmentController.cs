
using Demo.BusinessLogic.DTOS;
using Demo.BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Demo.presentation.Controllers
{
    public class DepartmentController(IDepartmentService _departmentService , IWebHostEnvironment _env , ILogger<DepartmentController> _logger) : Controller
    {
        #region Index
        public IActionResult Index()
        {
            var departments = _departmentService.GetAllDepartments();
            return View(departments);
        }

        #endregion


        #region Create
        //return view form
        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateDepartmentDto departmentDto)
        {
            if (ModelState.IsValid)//server side validation
            {
                try { 
                   int result= _departmentService.AddDepartment(departmentDto);
                     if (result > 0)
                     {
                          
                          return RedirectToAction(nameof(Index));
                     }
                     else
                     {
                          ModelState.AddModelError(string.Empty, "Failed to create department");
                          
                    }
                }

                catch (Exception ex)
                {
                    if(_env.IsDevelopment())
                    { 
                        
                        _logger.LogError(ex, "Error occurred while creating department");
                      
                    }
                    else
                    {
                                               _logger.LogError(ex, "Error occurred while creating department");
                        return RedirectToAction("ErrorView");
                    }
                }
            }
            return View(departmentDto);


        }
        #endregion
    }
}
