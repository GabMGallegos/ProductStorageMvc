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
                ViewBag.ErrorMessage = "The product request was sent, but the server could not confirm the result because the connection was slow. Please check the product list before trying again.";

                return View(viewModel);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ProductListViewModel viewModel = await _productService.GetProductListAsync();

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            ProductEditViewModel? viewModel = await _productService.GetProductForEditAsync(id);

            if (viewModel == null)
            {
                TempData["ErrorMessage"] = "The selected product was not found.";
                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductEditViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ErrorMessage = "Please check the entered data before updating the product.";
                return View(viewModel);
            }

            try
            {
                bool updated = await _productService.UpdateProductAsync(viewModel);

                if (!updated)
                {
                    ViewBag.ErrorMessage = "The selected product was not found.";
                    return View(viewModel);
                }

                TempData["SuccessMessage"] = "Product updated successfully.";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewBag.ErrorMessage = "The product could not be updated. Please try again.";
                return View(viewModel);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            ProductRowViewModel? viewModel = await _productService.GetProductForDeleteAsync(id);

            if (viewModel == null)
            {
                TempData["ErrorMessage"] = "The selected product was not found.";
                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                bool deleted = await _productService.DeleteProductAsync(id);

                if (!deleted)
                {
                    TempData["ErrorMessage"] = "The selected product was not found.";
                    return RedirectToAction(nameof(Index));
                }

                TempData["SuccessMessage"] = "Product deleted successfully.";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                TempData["ErrorMessage"] = "The product could not be deleted. Please try again.";
                return RedirectToAction(nameof(Index));
            }
        }
    }
}