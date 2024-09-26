using Company.Data.Models;
using Company.Service.Interfaces;
using Company.Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace Company.Web.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IDepartmentService _departmentService;

        public EmployeeController(IEmployeeService employeeService, IDepartmentService departmentService) 
        {
            _employeeService = employeeService;
            _departmentService = departmentService;
        }

        [HttpGet]   
        public IActionResult Index(string searchInp)
        {
            if(string.IsNullOrEmpty(searchInp)) 
            {
                var emp = _employeeService.GetAll();
                //ViewBag.Message = "This is message from ViewBag";
                //ViewData["TxtMessage"] = "This is message from ViewData";
                //TempData.Keep("TxtMessage2");
                //TempData["TxtMessage2"] = "This is message from TempData";
                return View(emp); 
            }
            else
            {
                var emp = _employeeService.GetEmployeeByName(searchInp);
                return View(emp);
            }


        }

        [HttpGet]
        public IActionResult Create()
        {
           ViewBag.Departments = _departmentService.GetAll();
            return View();
        }

        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _employeeService.Add(employee);
                    //TempData["TxtMessage2"] = "This is message from TempData";
                    return RedirectToAction(nameof(Index));
                }

                ModelState.AddModelError("EmployeeError", "ValidationErrors");

                return View(employee);

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("DepartmentError", ex.Message);
                return View(employee);
            }

        }
    }
}
