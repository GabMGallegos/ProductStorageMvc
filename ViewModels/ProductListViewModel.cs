using System.Collections.Generic;

namespace ProductStorageMvc.ViewModels
{
    public class ProductListViewModel
    {
        public List<ProductRowViewModel> Products { get; set; } = new List<ProductRowViewModel>();

        public int TotalQuantity { get; set; }

        public decimal TotalInventoryValue { get; set; }
    }
}
