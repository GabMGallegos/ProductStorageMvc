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
                .AsNoTracking()
                .OrderBy(product => product.Id)
                .ToListAsync();

            ProductListViewModel viewModel = new ProductListViewModel();

            foreach (Product product in products)
            {
                decimal totalValue = product.Price * product.Quantity;

                ProductRowViewModel row = new ProductRowViewModel
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    Quantity = product.Quantity,
                    TotalValue = totalValue
                };

                viewModel.Products.Add(row);
            }

            viewModel.TotalQuantity = viewModel.Products.Sum(product => product.Quantity);
            viewModel.TotalInventoryValue = viewModel.Products.Sum(product => product.TotalValue);

            return viewModel;
        }

        public async Task<ProductEditViewModel?> GetProductForEditAsync(int id)
        {
            Product? product = await _context.Products
                .AsNoTracking()
                .FirstOrDefaultAsync(product => product.Id == id);

            if (product == null)
            {
                return null;
            }

            ProductEditViewModel viewModel = new ProductEditViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Quantity = product.Quantity
            };

            return viewModel;
        }

        public async Task<bool> UpdateProductAsync(ProductEditViewModel viewModel)
        {
            Product? product = await _context.Products
                .FirstOrDefaultAsync(product => product.Id == viewModel.Id);

            if (product == null)
            {
                return false;
            }

            product.Name = viewModel.Name.Trim();
            product.Price = viewModel.Price;
            product.Quantity = viewModel.Quantity;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            Product? product = await _context.Products
                .FirstOrDefaultAsync(product => product.Id == id);

            if (product == null)
            {
                return false;
            }

            _context.Products.Remove(product);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<ProductRowViewModel?> GetProductForDeleteAsync(int id)
        {
            Product? product = await _context.Products
                .AsNoTracking()
                .FirstOrDefaultAsync(product => product.Id == id);

            if (product == null)
            {
                return null;
            }

            ProductRowViewModel viewModel = new ProductRowViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Quantity = product.Quantity,
                TotalValue = product.Price * product.Quantity
            };

            return viewModel;
        }
    }
}