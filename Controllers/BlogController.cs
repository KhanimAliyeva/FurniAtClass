using FurniMpa201.Context;
using Microsoft.AspNetCore.Mvc;

namespace FurniMpa201.Controllers
{
    public class BlogController : Controller
    {
        private readonly AppDbContext _context;

        public BlogController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var blogs = _context.Blogs
                                    .Where(e => !e.IsDeleted)
                                    .ToList();

           ViewBag.Blogs = blogs;
            return View();
        }
    }
}
