using Microsoft.EntityFrameworkCore;
using ProductStorageMvc.Data;
using ProductStorageMvc.Models;
using ProductStorageMvc.ViewModels;

namespace ProductStorageMvc.Services
{
    public class ProductService
    {
        private readonly ApplicationDbContext _context;

        public ProductService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateProductAsync(ProductCreateViewModel viewModel)
        {
            Product product = new Product
            {
                Name = viewModel.Name.Trim(),
                Price = viewModel.Price,
                Quantity = viewModel.Quantity
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }

        public async Task<ProductListViewModel> GetProductListAsync()
        {
            List<Product> products = await _context.Products
                .OrderBy(product => product.Id)
                .ToListAsync();

            List<ProductRowViewModel> productRows = products
                .Select(product => new ProductRowViewModel
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    Quantity = product.Quantity,
                    TotalValue = product.Price * product.Quantity
                })
                .ToList();

            ProductListViewModel viewModel = new ProductListViewModel
            {
                Products = productRows,
                TotalQuantity = productRows.Sum(product => product.Quantity),
                TotalInventoryValue = productRows.Sum(product => product.TotalValue)
            };

            return viewModel;
        }
    }
}
