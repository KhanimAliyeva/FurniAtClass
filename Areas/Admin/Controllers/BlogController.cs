using FurniMpa201.Context;
using FurniMpa201.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FurniMpa201.Areas.Admin.Controllers
{
    [Area("Admin")]


    public class BlogController : Controller
    {
        private readonly AppDbContext _context;
        public BlogController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var blogs = _context.Blogs.Include(b => b.Employee).ToList();
            return View(blogs);
        }
        public IActionResult Update(int id)
        {
            var blog = _context.Blogs.FirstOrDefault(b => b.Id == id);
            if (blog == null) return NotFound();
            return View(blog);
        }
        [HttpPost]
        public IActionResult Update(Blog blog)
        {
            if (!ModelState.IsValid)
                return View(blog);
            var existingBlog = _context.Blogs.FirstOrDefault(b => b.Id == blog.Id);
            if (existingBlog == null) return NotFound();
            existingBlog.Title = blog.Title;
            existingBlog.Text = blog.Text;
            existingBlog.EmployeeId = blog.EmployeeId;
            existingBlog.ImageName = blog.ImageName;
            existingBlog.ImageUrl = blog.ImageUrl;
            existingBlog.CreatedDate = blog.CreatedDate;
            existingBlog.UpdatedDate = DateTime.UtcNow.AddHours(4);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var blog = _context.Blogs.FirstOrDefault(b => b.Id == id);
            if (blog == null) return NotFound();
            _context.Blogs.Remove(blog);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public IActionResult SoftDelete(int id)
        {
            var blog = _context.Blogs.FirstOrDefault(b => b.Id == id);
            if (blog == null) return NotFound();
            blog.IsDeleted = !blog.IsDeleted;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public IActionResult Restore(int id)
        {
            var blog = _context.Blogs.FirstOrDefault(b => b.Id == id);
            if (blog == null) return NotFound("Blog is not found!");
            blog.IsDeleted = false;
            blog.UpdatedDate = DateTime.UtcNow.AddHours(4);
            _context.Update(blog);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Blog blog)
        {
            if (!ModelState.IsValid)
                return View(blog);
            blog.CreatedDate = DateTime.UtcNow.AddHours(4);
            blog.UpdatedDate = DateTime.UtcNow.AddHours(4);
            blog.IsDeleted = false;
            _context.Blogs.Add(blog);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]

        public IActionResult Create()
        {
            ViewBag.Employees = _context.Employees
                .Select(e => new SelectListItem
                {
                    Value = e.Id.ToString(),
                    Text = e.FirstName+" "+ e.LastName   
                })
                .ToList();

            return View();
        }

    }
}

