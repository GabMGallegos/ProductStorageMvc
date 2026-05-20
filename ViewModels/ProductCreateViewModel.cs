using System.ComponentModel.DataAnnotations;

namespace ProductStorageMvc.ViewModels
{
    public class ProductCreateViewModel
    {
        [Required(ErrorMessage = "The product name is required.")]
        [StringLength(100, ErrorMessage = "The product name cannot exceed 100 characters.")]
        [Display(Name = "Product Name")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "The product price is required.")]
        [Range(0.01, 999999.99, ErrorMessage = "The price must be greater than 0.")]
        [Display(Name = "Price")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "The product quantity is required.")]
        [Range(1, 1000000, ErrorMessage = "The quantity must be at least 1.")]
        [Display(Name = "Quantity")]
        public int Quantity { get; set; }
    }
}
