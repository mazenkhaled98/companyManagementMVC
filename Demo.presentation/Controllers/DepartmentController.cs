
using Demo.BusinessLogic.DTOS;
using Demo.BusinessLogic.DTOS.DepartmentDtos;
using Demo.BusinessLogic.Services.Interfaces;
using Demo.DataAccess.Models;
using Demo.presentation.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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
                        return RedirectToAction("ErrorView",ex);
                    }
                }
            }
            return View(departmentDto);


        }
        #endregion

        #region Details

        [HttpGet]
        public IActionResult Details(int? id)
        {
           if(!id.HasValue || id <= 0)
            {
                return BadRequest();
            }
            var department = _departmentService.GetDepartmentById(id.Value);
            if(department == null)
            {
                return NotFound();
            }
            return View(department);
        }
        #endregion


        #region Edit
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (!id.HasValue || id <= 0)
            {
                return BadRequest();
            }
            var department = _departmentService.GetDepartmentById(id.Value);
            if (department == null)
            {
                return NotFound();
            }
            var departmentVM = new DepartmentEditViewModel
            {
               
                Name = department.Name,
                Code = department.Code,
                Description = department.Description,
                Createdon = department.CreatedOn.HasValue ? department.CreatedOn.Value :default

            };
            return View(departmentVM);
        }

        [HttpPost]
        public IActionResult Edit([FromRoute]int? id,DepartmentEditViewModel departmentVM)
        {
           if(ModelState.IsValid)
            {
                try
                {
                    if(!id.HasValue || id <= 0)
                    {
                        return BadRequest();
                    }
                    var departmentDto = new UpdateDepartmentDto
                    {
                        Id = id.Value,
                        Name = departmentVM.Name,
                        Code = departmentVM.Code,
                        Description = departmentVM.Description,
                        DateOfCreation = departmentVM.Createdon
                    };
                    int result = _departmentService.UpdateDepartment(departmentDto);
                    if (result > 0)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Failed to update department");
                    }
                }
                catch (Exception ex)
                {
                    if (_env.IsDevelopment())
                    {
                        _logger.LogError(ex, "Error occurred while updating department");
                    }
                    else
                    {
                        _logger.LogError(ex, "Error occurred while updating department");
                        return RedirectToAction("ErrorView",ex);
                    }
                }
            }
            return View(departmentVM);
        }

        #endregion

        #region Delete

        //[HttpGet]
        //public IActionResult Delete(int? id)
        //{
        //    if (!id.HasValue || id <= 0)
        //    {
        //        return BadRequest();
        //    }
        //    var department = _departmentService.GetDepartmentById(id.Value);
        //    if (department == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(department);
        //}

        [HttpPost]
        public IActionResult Delete([FromRoute]int id)
        {
            if ( id == 0)
            {
                return BadRequest();
            }
            try
            {
                bool isDeleted = _departmentService.DeleteDepartment(id);
                if (isDeleted)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Failed to delete department");
               
                }
            }
            catch (Exception ex)
            {
                if (_env.IsDevelopment())
                {
                    _logger.LogError(ex, "Error occurred while deleting department");
                }
                else
                {
                    _logger.LogError(ex, "Error occurred while deleting department");
                    return RedirectToAction("ErrorView", ex);
                }
            }
            return RedirectToAction(nameof(Delete), new { id });
        }

        #endregion
    }
}
