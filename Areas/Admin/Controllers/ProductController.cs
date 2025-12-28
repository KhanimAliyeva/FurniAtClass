
using FurniMpa201.Context;
using FurniMpa201.Helpers;
using FurniMpa201.Models;
using FurniMpa201.ViewModels;
using FurniMpa201.ViewModels.ProductViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Furni.App.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        readonly AppDbContext _context;
        private readonly IWebHostEnvironment _environment;


        public ProductController(AppDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public IActionResult Index()
        {
            var products = _context.Products.ToList();
            return View(products);
        }

        // ================= CREATE =================
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProductCreateVM vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            if (!vm.MainImage.CheckType("image/") || !vm.MainImage.CheckSize(2))
            {
                ModelState.AddModelError("MainImage", "Image must be jpg/png and max 2MB");
                return View(vm);
            }

            string folder = Path.Combine(_environment.WebRootPath, "assets", "images", "products");

            string fileName = vm.MainImage.SaveFile(folder);

            Product product = new()
            {
                Name = vm.Name,
                Price = vm.Price,
                ImageUrl = fileName,
                ImageName = fileName,
                CreatedDate = DateTime.UtcNow.AddHours(4),
                IsDeleted = false
            };

            _context.Products.Add(product);
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





        [HttpGet]
        public IActionResult Update(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null) return NotFound();

            ProductUpdateVM vm = new()
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                ImageUrl = product.ImageUrl
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(ProductUpdateVM vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var product = _context.Products.Find(vm.Id);
            if (product == null) return NotFound();

            string folder = Path.Combine(_environment.WebRootPath, "assets", "images", "products");

            if (vm.MainImage != null)
            {
                if (!vm.MainImage.CheckType("image/") || !vm.MainImage.CheckSize(2))
                {
                    ModelState.AddModelError("MainImage", "Invalid image");
                    return View(vm);
                }

                if (!string.IsNullOrEmpty(product.ImageUrl))
                {
                    string oldPath = Path.Combine(folder, product.ImageUrl);
                    if (System.IO.File.Exists(oldPath))
                    {
                        System.IO.File.Delete(oldPath);
                    }
                }

                string newImage = vm.MainImage.SaveFile(folder);
                product.ImageUrl = newImage;
                product.ImageName = newImage;
            }


            product.Name = vm.Name;
            product.Price = vm.Price;
            product.UpdatedDate = DateTime.UtcNow.AddHours(4);

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