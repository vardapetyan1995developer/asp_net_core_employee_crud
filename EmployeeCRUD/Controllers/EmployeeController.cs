using EmployeeCRUD.Data;
using EmployeeCRUD.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeCRUD.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmployeeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            IEnumerable<Employee> objcatList = _context.Employees;

            return View(objcatList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Employee empObj)
        {
            if(ModelState.IsValid)
            {
                _context.Employees.Add(empObj);
                _context.SaveChanges();

                TempData["ResultOk"] = "Record Added Successfully!";

                return RedirectToAction("Index");
            }

            return View(empObj);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var empfromdb = _context.Employees.Find(id);

            if (empfromdb == null)
            {
                return NotFound();
            }

            return View(empfromdb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Employee empobj)
        {
            if (ModelState.IsValid)
            {
                _context.Employees.Update(empobj);
                _context.SaveChanges();

                TempData["ResultOk"] = "Data Updated Successfully!";

                return RedirectToAction("Index");
            }

            return View(empobj);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var empfromdb = _context.Employees.Find(id);

            if (empfromdb == null)
            {
                return NotFound();
            }

            return View(empfromdb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteEmp(int? id)
        {
            var deleterecord = _context.Employees.Find(id);

            if (deleterecord == null)
            {
                return NotFound();
            }

            _context.Employees.Remove(deleterecord);
            _context.SaveChanges();

            TempData["ResultOk"] = "Data Deleted Successfully!";

            return RedirectToAction("Index");
        }
    }
}
