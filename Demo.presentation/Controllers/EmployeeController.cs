using Demo.BusinessLogic.DTOS.EmployeeDtos;
//using Demo.BusinessLogic.DTOS.EmployeeModule;
using Demo.BusinessLogic.Services.Interfaces;
using Demo.DataAccess.Models.EmployeeModule;
using Demo.DataAccess.Models.Shared;
using Demo.presentation.Controllers;
using Demo.presentation.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Presentation.Controllers
{
    public class EmployeeController(IEmployeeService _employeeService ,IWebHostEnvironment _env, ILogger<DepartmentController> _logger) : Controller
    {
        #region Index

        //Master action
        //BaseUrl / Employee / Index ==> Send data [ Controller --> View ]
        [HttpGet]
        public IActionResult Index(string? EmployeeSearchName)
        {
            var employees = _employeeService.GetAllEmployees(EmployeeSearchName);
            return View(employees);
        }

        #endregion

        #region Create

        [HttpGet]
        public IActionResult Create() // CLR Action Injection
        {
           
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(EmployeeViewModel employeeViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    int result = _employeeService.CreateEmployee(new CreateEmployeeDto
                    {
                        Name = employeeViewModel.Name,
                        Age = employeeViewModel.Age,
                        Address = employeeViewModel.Address,
                        IsActive = employeeViewModel.IsActive,
                        Email = employeeViewModel.Email,
                        Salary = employeeViewModel.Salary,
                        PhoneNumber = employeeViewModel.PhoneNumber,
                        HiringDate = employeeViewModel.HiringDate,
                        DepartmentId = employeeViewModel.DepartmentId,
                        EmployeeType = employeeViewModel.EmployeeType,
                        Gender = employeeViewModel.Gender
                    });
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
            return View(employeeViewModel);
        }

        #endregion
        #region Details

        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var employee = _employeeService.GetEmployeeById(id.Value);
            if (employee is null) return NotFound();
            return View(employee);
        }

        #endregion

        #region Edit

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var employee = _employeeService.GetEmployeeById(id.Value);
            if (employee is null) return NotFound();
            var employeeViewModel = new EmployeeViewModel() //deptid
            {
             
                Name = employee.Name,
                Age = employee.Age,
                Address = employee.Address,
                IsActive = employee.IsActive,
                Email = employee.Email,
                Salary = employee.Salary,
                PhoneNumber = employee.PhoneNumber,
                HiringDate = employee.HiringDate,
                Gender = Enum.Parse<Gender>(employee.Gender),
                EmployeeType = Enum.Parse<EmployeeType>(employee.EmployeeType),
                DepartmentId = employee.DepartmentId,

            };
            return View(employeeViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int? id, EmployeeViewModel employeeViewModel)
        {
            if (!id.HasValue ) return BadRequest();
            if (!ModelState.IsValid) return View(employeeViewModel);
            try
            {
                int result = _employeeService.UpdateEmployee(new UpdateEmployeeDto
                {
               
                    Name = employeeViewModel.Name,
                    Age = employeeViewModel.Age,
                    Address = employeeViewModel.Address,
                    IsActive = employeeViewModel.IsActive,
                    Email = employeeViewModel.Email,
                    Salary = employeeViewModel.Salary,
                    PhoneNumber = employeeViewModel.PhoneNumber,
                    HiringDate = employeeViewModel.HiringDate,
                    DepartmentId = employeeViewModel.DepartmentId,
                    EmployeeType = employeeViewModel.EmployeeType,
                    Gender =employeeViewModel.Gender,
                    Id = id.Value
                });
                if (result > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("", "Employee can't be updated");
                    return View(employeeViewModel);
                }
            }
            catch (Exception ex)
            {
                if (_env.IsDevelopment())
                {
                    // Log error in file/DB
                    _logger.LogError($"Employee can't be updated because : {ex.Message}");
                    return View(employeeViewModel);
                }
                else
                {
                    // Log error in file/DB
                    _logger.LogError($"Employee can't be updated because : {ex}");
                    return View("Error", ex); // Error.cshtml
                }
            }
        }
        #region Delete

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            if (id == 0) return BadRequest(); // 400
            try
            {
                bool isDeleted = _employeeService.DeleteEmployee(id);
                if (isDeleted)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Employee can't be deleted!");
                }
            }
            catch (Exception ex)
            {
                // Development ==> action , Log error in console , View
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
                // Deployment ==> Log error in file/DB , return view [ Error.cshtml ]
            }
            return RedirectToAction(nameof(Delete), new { id = id });
        }

        #endregion


        #endregion






    }
}