
using FurniMpa201.Context;
using FurniMpa201.Models;
using Microsoft.AspNetCore.Mvc;

namespace Furni.App.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        readonly AppDbContext _context;

        public ProductController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var products = _context.Products.ToList();
            return View(products);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            if (!ModelState.IsValid) return View(product);
            product.CreatedDate = DateTime.UtcNow.AddHours(4);
            product.IsDeleted = false;
            _context.Add(product);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost("product/{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == id);
            if (product == null) return NotFound("Poduct is not found!");
            _context.Remove(product);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Update(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == id);
            if (product == null) return NotFound("Product is not found!");
            return View(product);
        }

        [HttpPost]
        public IActionResult Update(Product product)
        {
            var existProduct = _context.Products.FirstOrDefault(p => p.Id == product.Id);
            if (existProduct == null) return NotFound("Product is not found!");
            if (!ModelState.IsValid) return View(product);
            existProduct.Name = product.Name;
            existProduct.Price = product.Price;
            existProduct.ImageUrl = product.ImageUrl;
            existProduct.ImageName = product.ImageName;
            existProduct.UpdatedDate = DateTime.UtcNow.AddHours(4);
            _context.Update(existProduct);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult SoftDelete(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == id);
            if (product == null) return NotFound();

            product.IsDeleted = !product.IsDeleted; 
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
        
        [HttpPost]
        public IActionResult Restore(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == id);
            if (product == null) return NotFound("Product is not found!");
            product.IsDeleted = false;
            product.UpdatedDate = DateTime.UtcNow.AddHours(4);
            _context.Update(product);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }


    }
}