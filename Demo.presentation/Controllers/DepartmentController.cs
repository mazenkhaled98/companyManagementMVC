
using Demo.BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Demo.presentation.Controllers
{
    public class DepartmentController(IDepartmentService _departmentService) : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
