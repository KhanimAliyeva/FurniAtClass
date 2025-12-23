using Microsoft.AspNetCore.Mvc;
using FurniMpa201.Models;
using FurniMpa201.Context;

namespace FurniMpa201.Controllers
{
    public class ShopController : Controller
    {
        private readonly AppDbContext _context;

        public ShopController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var products = _context.Products
                                   .Where(p => !p.IsDeleted)
                                   .ToList();

            ViewBag.Products = products;
            return View();
        }
    }
}
