using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Webshop.Models;
using Webshop.ViewModels;

namespace Webshop.Controllers
{
    [Authorize(Roles = "Admin"), Authorize(Policy = "DeleteProduct")]
    public class ProductManagementController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductRepository _productRepository;

        public ProductManagementController(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        public ViewResult Index()
        {
            var products = _productRepository.Products.OrderBy(p => p.ProductId);
            return View(products);
        }

        public IActionResult AddProduct()
        {
            var categories = _categoryRepository.Categories;
            var productEditViewModel = new ProductEditViewModel {
                Categories =
                    categories.Select(c => new SelectListItem {
                        Text = c.CategoryName,
                        Value = c.CategoryId.ToString()
                    }).ToList(),
                CategoryId = categories.FirstOrDefault().CategoryId
            };
            return View(productEditViewModel);
        }

        [HttpPost]
        public IActionResult AddProduct(ProductEditViewModel productEditViewModel)
        {
            //Basic validation
            if (ModelState.IsValid) {
                _productRepository.CreateProduct(productEditViewModel.Product);
                return RedirectToAction("Index");
            }
            return View(productEditViewModel);
        }

        public IActionResult EditProduct(int productId)
        {
            var categories = _categoryRepository.Categories;
            var product = _productRepository.Products.FirstOrDefault(p => p.ProductId == productId);
            var productEditViewModel = new ProductEditViewModel {
                Categories =
                    categories.Select(c => new SelectListItem {
                        Text = c.CategoryName,
                        Value = c.CategoryId.ToString()
                    }).ToList(),
                Product = product,
                CategoryId = product.CategoryId
            };
            var item = productEditViewModel.Categories.FirstOrDefault(c => c.Value == product.CategoryId.ToString());
            item.Selected = true;
            return View(productEditViewModel);
        }

        [HttpPost]
        public IActionResult EditProduct(ProductEditViewModel productEditViewModel)
        {
            productEditViewModel.Product.CategoryId = productEditViewModel.CategoryId;
            if (ModelState.IsValid) {
                _productRepository.UpdateProduct(productEditViewModel.Product);
                return RedirectToAction("Index");
            }
            return View(productEditViewModel);
        }

        [HttpPost]
        public IActionResult DeleteProduct(string productId)
        {
            return View();
        }
    }
}