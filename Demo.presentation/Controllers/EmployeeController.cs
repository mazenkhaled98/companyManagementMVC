using Demo.BusinessLogic.DTOS.EmployeeDtos;
//using Demo.BusinessLogic.DTOS.EmployeeModule;
using Demo.BusinessLogic.Services.Interfaces;
using Demo.DataAccess.Models.EmployeeModule;
using Demo.DataAccess.Models.Shared;
using Demo.presentation.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Presentation.Controllers
{
    public class EmployeeController(IEmployeeService _employeeService, IWebHostEnvironment _env, ILogger<DepartmentController> _logger) : Controller
    {
        #region Index

        //Master action
        //BaseUrl / Employee / Index ==> Send data [ Controller --> View ]
        [HttpGet]
        public IActionResult Index()
        {
            var employees = _employeeService.GetAllEmployees();
            return View(employees);
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
        public IActionResult Create(CreateEmployeeDto employeeDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    int result = _employeeService.CreateEmployee(employeeDto);
                    if (result > 0)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ModelState.AddModelError("", "Employee can't be created");
                    }
                }
                catch (Exception ex)
                {
                    if (_env.IsDevelopment())
                    {
                        // Log error in file/DB
                        _logger.LogError($"Employee can't be created because : {ex.Message}");
                    }
                    else
                    {
                        // Log error in file/DB
                        _logger.LogError($"Employee can't be created because : {ex}");
                        return View("Error", ex); // Error.cshtml
                    }
                }
            }
            return View(employeeDto);
        }

        #endregion

    }
}