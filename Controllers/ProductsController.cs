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
                ViewBag.ErrorMessage = "Please check the entered data before saving the product.";
                return View(viewModel);
            }

            try
            {
                await _productService.CreateProductAsync(viewModel);

                ModelState.Clear();

                ViewBag.SuccessMessage = "Product saved successfully.";

                ProductCreateViewModel cleanViewModel = new ProductCreateViewModel();

                return View(cleanViewModel);
            }
            catch
            {
                ViewBag.ErrorMessage = "The product could not be saved. Please try again.";

                return View(viewModel);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ProductListViewModel viewModel = await _productService.GetProductListAsync();

            return View(viewModel);
        }
    }
}