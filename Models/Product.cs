using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductStorageMvc.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The product name is required.")]
        [StringLength(100, ErrorMessage = "The product name cannot exceed 100 characters.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "The product price is required.")]
        [Range(0.01, 999999.99, ErrorMessage = "The price must be greater than 0.")]
        [Column(TypeName = "numeric(10,2)")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "The product quantity is required.")]
        [Range(1, 1000000, ErrorMessage = "The quantity must be at least 1.")]
        public int Quantity { get; set; }

        [NotMapped]
        public decimal TotalValue
        {
            get
            {
                return Price * Quantity;
            }
        }
    }
}
