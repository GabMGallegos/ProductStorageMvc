using Microsoft.AspNetCore.Mvc;
using ProductStorageMvc.Services;
using ProductStorageMvc.ViewModels;

namespace ProductStorageMvc.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ProductService _productService;

        public ProductsController(ProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public IActionResult Create()
        {
            ProductCreateViewModel viewModel = new ProductCreateViewModel();

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductCreateViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            await _productService.CreateProductAsync(viewModel);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ProductListViewModel viewModel = await _productService.GetProductListAsync();

            return View(viewModel);
        }
    }
}
