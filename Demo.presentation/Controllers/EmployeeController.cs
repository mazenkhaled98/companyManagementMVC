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

        //#region Create

        //[HttpGet]
        //public IActionResult Create()
        //{
        //    return View();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Create(CreatedEmployeeDto employeeDto)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            int result = _employeeService.CreateEmployee(employeeDto);
        //            if (result > 0)
        //            {
        //                return RedirectToAction(nameof(Index));
        //            }
        //            else
        //            {
        //                ModelState.AddModelError("", "Employee can't be created");
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            if (_env.IsDevelopment())
        //            {
        //                // Log error in file/DB
        //                _logger.LogError($"Employee can't be created because : {ex.Message}");
        //            }
        //            else
        //            {
        //                // Log error in file/DB
        //                _logger.LogError($"Employee can't be created because : {ex}");
        //                return View("Error", ex); // Error.cshtml
        //            }
        //        }
        //    }
        //    return View(employeeDto);
        //}

        //#endregion

        //#region Details

        //[HttpGet]
        //public IActionResult Details(int? id)
        //{
        //    if (!id.HasValue) return BadRequest();
        //    var employee = _employeeService.GetEmployeeById(id.Value);
        //    if (employee is null) return NotFound();
        //    return View(employee);
        //}

        //#endregion

        //#region Edit

        //[HttpGet]
        //public IActionResult Edit(int? id)
        //{
        //    if (!id.HasValue) return BadRequest();
        //    var employee = _employeeService.GetEmployeeById(id.Value);
        //    if (employee is null) return NotFound();
        //    var employeeDto = new UpdatedEmployeeDto()
        //    {
        //        Id = employee.Id,
        //        Name = employee.Name,
        //        Age = employee.Age,
        //        Address = employee.Address,
        //        IsActive = employee.IsActive,
        //        Email = employee.Email,
        //        Salary = employee.Salary,
        //        PhoneNumber = employee.PhoneNumber,
        //        HiringDate = employee.HiringDate,
        //        Gender = Enum.Parse<Gender>(employee.Gender),
        //        EmployeeType = Enum.Parse<EmployeeType>(employee.EmployeeType)
        //    };
        //    return View(employeeDto);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Edit([FromRoute] int? id, UpdatedEmployeeDto employeeDto)
        //{
        //    if (!id.HasValue || id != employeeDto.Id) return BadRequest();
        //    if (!ModelState.IsValid) return View(employeeDto);
        //    try
        //    {
        //        int result = _employeeService.UpdateEmployee(employeeDto);
        //        if (result > 0)
        //        {
        //            return RedirectToAction(nameof(Index));
        //        }
        //        else
        //        {
        //            ModelState.AddModelError("", "Employee can't be updated");
        //            return View(employeeDto);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        if (_env.IsDevelopment())
        //        {
        //            // Log error in file/DB
        //            _logger.LogError($"Employee can't be updated because : {ex.Message}");
        //            return View(employeeDto);
        //        }
        //        else
        //        {
        //            // Log error in file/DB
        //            _logger.LogError($"Employee can't be updated because : {ex}");
        //            return View("Error", ex); // Error.cshtml
        //        }
        //    }
        //}


        //#endregion

        //#region Delete

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Delete(int id)
        //{
        //    if (id == 0) return BadRequest(); // 400
        //    try
        //    {
        //        bool isDeleted = _employeeService.DeleteEmployee(id);
        //        if (isDeleted)
        //        {
        //            return RedirectToAction(nameof(Index));
        //        }
        //        else
        //        {
        //            ModelState.AddModelError(string.Empty, "Employee can't be deleted!");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Development ==> action , Log error in console , View
        //        if (_env.IsDevelopment())
        //        {
        //            // Log error in file/DB
        //            _logger.LogError($"Employee can't be created because : {ex.Message}");
        //        }
        //        else
        //        {
        //            // Log error in file/DB
        //            _logger.LogError($"Employee can't be created because : {ex}");
        //            return View("Error", ex); // Error.cshtml
        //        }
        //        // Deployment ==> Log error in file/DB , return view [ Error.cshtml ]
        //    }
        //    return RedirectToAction(nameof(Delete), new { id = id });
        //}

        //#endregion

    }
}