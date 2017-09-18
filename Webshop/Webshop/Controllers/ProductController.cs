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
        private ICategoryRepository _categoryRepository;
        private IProductRepository _productRepository;

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public ProductController(IProductRepository productRepository, ICategoryRepository categoryRepository)

        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        public ViewResult List()
        {
           ProductsListViewModel productsListViewModel = new ProductsListViewModel();
            productsListViewModel.Products = _productRepository.Products;

            productsListViewModel.CurrentCategory = "cheese cake";
            return View(productsListViewModel);
        }
    }
}
