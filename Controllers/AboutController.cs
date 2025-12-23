using FurniMpa201.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FurniMpa201.Controllers
{
    public class AboutController : Controller
    {
        private readonly AppDbContext _context;

        public AboutController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var employees = _context.Employees
                                    .Where(e => !e.IsDeleted)
                                    .ToList();

            ViewBag.Employees = employees;

            var comments = _context.Comments
                        .Where(e => !e.IsDeleted)
                        .ToList();

            ViewBag.Comments = comments;
            return View();
        }

    }
}
