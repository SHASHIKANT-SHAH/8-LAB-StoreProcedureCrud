using DAL;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class EmployeeController : Controller
    {
        AppDbContext _db;

        public OutputParameter<bool?> Status { get; private set; }

        public EmployeeController(AppDbContext db)
        {
            _db = db;
        }
        

        public async Task<IActionResult> Index()
        {
            IList<EmployeeViewModel> model = new List<EmployeeViewModel>();
            var data = await _db.Procedures.usp_getemployeesAsync();
            if (data != null)
            {
                foreach (var item in data)
                {
                    EmployeeViewModel employee = new EmployeeViewModel();
                    employee.EmployeeId = item.EmployeeId;
                    employee.Name = item.Name;
                    employee.Address = item.Address;
                    employee.DepartmentId = item.DepartmentId;
                    employee.DepartmentName = item.DepartmentName;
                    model.Add(employee);
                }
            }
            return View(model);
        }

        public IActionResult Create()
        {
            ViewBag.Departments = _db.Departments.ToList();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(EmployeeViewModel model)
        {
            ModelState.Remove("EmployeeId");
            if (ModelState.IsValid)
            {
                Employee employee = new Employee
                {
                    Name = model.Name,
                    Address = model.Address,
                    DepartmentId = model.DepartmentId
                };
                await _db.Procedures.usp_addemployeeAsync(employee.Name, employee.Address, employee.DepartmentId, Status);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Departments = _db.Departments.ToList();
            return View();
        }


        public async Task<IActionResult> Edit(int id)
        {
            EmployeeViewModel employee = new EmployeeViewModel();
            var data = await _db.Procedures.usp_getemployeeAsync(id);
           
            if (data != null)
            {
                foreach (var item in data)
                {
                    employee.EmployeeId = item.EmployeeId;
                    employee.Name = item.Name;
                    employee.Address = item.Address;
                    employee.DepartmentId = item.DepartmentId;
                }
            }
            ViewBag.Departments = _db.Departments.ToList();
            return View("Create", employee);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EmployeeViewModel model)
        {
            if (ModelState.IsValid)
            {
                Employee employee = new Employee
                {
                    EmployeeId = model.EmployeeId,
                    Name = model.Name,
                    Address = model.Address,
                    DepartmentId = model.DepartmentId
                };
                await _db.Procedures.usp_updateemployeeAsync(employee.EmployeeId,employee.Name, employee.Address, employee.DepartmentId, Status);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Departments = _db.Departments.ToList();
            return View("Create");

        }
        public async Task<IActionResult> Delete(int id)
        {
            var data = await _db.Procedures.usp_getemployeeAsync(id);
            if (data != null) {
                await _db.Procedures.usp_deleteemployeeAsync(id, Status);
                _db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
      
    }
}
