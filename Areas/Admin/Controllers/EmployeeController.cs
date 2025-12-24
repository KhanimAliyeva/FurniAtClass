using FurniMpa201.Context;
using FurniMpa201.Models;
using Microsoft.AspNetCore.Mvc;

namespace FurniMpa201.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class EmployeeController : Controller
    {
        private readonly AppDbContext _context;

        public EmployeeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var employees = _context.Employees.ToList();
            return View(employees);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            if (!ModelState.IsValid)
                return View(employee);

            employee.CreatedDate = DateTime.UtcNow.AddHours(4);
            employee.IsDeleted = false;

            _context.Employees.Add(employee);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var employee = _context.Employees.FirstOrDefault(e => e.Id == id);
            if (employee == null) return NotFound();
            return View(employee);
        }

        [HttpPost]
        public IActionResult Edit(Employee employee)
        {
            if (!ModelState.IsValid)
                return View(employee);

            var existingEmployee = _context.Employees.FirstOrDefault(e => e.Id == employee.Id);
            if (existingEmployee == null) return NotFound();

            existingEmployee.FirstName = employee.FirstName;
            existingEmployee.LastName = employee.LastName;
            existingEmployee.Position = employee.Position;
            existingEmployee.Description = employee.Description;
            existingEmployee.ImageName = employee.ImageName;
            existingEmployee.ImageUrl = employee.ImageUrl;
            existingEmployee.UpdatedDate = DateTime.UtcNow.AddHours(4);

            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var employee = _context.Employees.FirstOrDefault(e => e.Id == id);
            if (employee == null) return NotFound();

            _context.Employees.Remove(employee);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Update(int id)
        {
            var employee = _context.Employees.FirstOrDefault(e => e.Id == id);
            if (employee == null) return NotFound("Employee is not found!");
            return View(employee);
        }
        [HttpPost]
        public IActionResult Update(Employee employee)
        {
            var existEmployee = _context.Employees.FirstOrDefault(e => e.Id == employee.Id);
            if (existEmployee == null) return NotFound("Employee is not found!");
            if (!ModelState.IsValid) return View(employee);
            existEmployee.FirstName = employee.FirstName;
            existEmployee.LastName = employee.LastName;
            existEmployee.Position = employee.Position;
            existEmployee.Description = employee.Description;
            existEmployee.ImageUrl = employee.ImageUrl;
            existEmployee.ImageName = employee.ImageName;
            existEmployee.UpdatedDate = DateTime.UtcNow.AddHours(4);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult SoftDelete(int id)
        {
            var employee = _context.Employees.FirstOrDefault(p => p.Id == id);
            if (employee == null) return NotFound();

            employee.IsDeleted = !employee.IsDeleted;
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Restore(int id)
        {
            var employee = _context.Employees.FirstOrDefault(p => p.Id == id);
            if (employee == null) return NotFound("employee is not found!");
            employee.IsDeleted = false;
            employee.UpdatedDate = DateTime.UtcNow.AddHours(4);
            _context.Update(employee);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

    }
}
