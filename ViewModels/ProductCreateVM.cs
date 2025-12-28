using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace FurniMpa201.ViewModels.ProductViewModels
{
    public class ProductCreateVM
    {
        [Required]
        public string Name { get; set; }


        [Required]
        public double Price { get; set; }

        [Required]
        public IFormFile MainImage { get; set; }


    }
}
