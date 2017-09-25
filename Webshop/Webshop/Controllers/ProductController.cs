using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Webshop.Models;
using Webshop.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Webshop.Controllers
{
    public class ProductController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductRepository _productRepository;


        public ProductController(IProductRepository productRepository, ICategoryRepository categoryRepository)

        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        public ViewResult List(string category)
        {
            IEnumerable<Product> products;
            string currentCategory = string.Empty;

            if (string.IsNullOrEmpty(category))
            {
                products = _productRepository.Products.OrderBy(p => p.ProductId);
                currentCategory = "All products";
            }
            else
            {
                products = _productRepository.Products.Where(p => p.Category.CategoryName == category)
                   .OrderBy(p => p.ProductId);
                currentCategory = _categoryRepository.Categories.FirstOrDefault(c => c.CategoryName == category).CategoryName;
            }
      

            return View(new ProductsListViewModel()
            {
                Products = products,
                CurrentCategory = currentCategory
                
            });
        }

        public IActionResult Details(int id)
        {
            var product = _productRepository.GetProductById(id);
            if (product == null)
                return NotFound();

            return View(product);
        }
    }
}
