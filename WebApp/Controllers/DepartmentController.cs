using DAL;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class DepartmentController : Controller
    {
        AppDbContext _db;
        public DepartmentController(AppDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var departments = _db.Departments.Select(d => new DepartmentViewModel
            {
                DepartmentId = d.DepartmentId,
                Name = d.Name,
            }).ToList();
            return View(departments);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(DepartmentViewModel model)
        {
            ModelState.Remove("DepartmentId");
            if (ModelState.IsValid)
            {
                Department department = new Department();
                department.Name = model.Name;
                _db.Departments.Add(department);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Edit(int id)
        {
            DepartmentViewModel model = new DepartmentViewModel();
            Department data = _db.Departments.Find(id);
            if (data != null)
            {
                model = new DepartmentViewModel
                {
                    DepartmentId = data.DepartmentId,
                    Name = data.Name
                };
            }
            return View("Create", model);
        }

        [HttpPost]
        public IActionResult Edit(DepartmentViewModel model)
        {
            if (ModelState.IsValid)
            {
                Department department = new Department();
                department.DepartmentId = model.DepartmentId;
                department.Name = model.Name;
                
                _db.Departments.Update(department);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Delete(int id)
        {
            Department model = _db.Departments.Find(id);
            if (model != null)
            {
                _db.Departments.Remove(model);
                _db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

    }
}
