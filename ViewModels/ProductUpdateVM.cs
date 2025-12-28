using System.ComponentModel.DataAnnotations;

namespace FurniMpa201.ViewModels
{
    public class ProductUpdateVM
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public double Price { get; set; }

        public string ImageUrl { get; set; }

        public IFormFile MainImage { get; set; }
    }
}
