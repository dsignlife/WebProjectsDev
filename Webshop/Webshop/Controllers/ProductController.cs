using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            var currentCategory = string.Empty;
            if (string.IsNullOrEmpty(category)) {
                products = _productRepository.Products.OrderBy(p => p.ProductId);
                currentCategory = "All Products";
            }
            else {
                products = _productRepository.Products
                                             .Where(p => p.Category.CategoryName == category)
                                             .OrderBy(p => p.ProductId);
                currentCategory = _categoryRepository.Categories.FirstOrDefault(c => c.CategoryName == category)
                                                     .CategoryName;
            }
            return View(new ProductsListViewModel {
                Products = products,
                CurrentCategory = currentCategory
            });
        }

        //public async Task OnGetAsync(string searchString)
        //{
        //    var products = _productRepository.Products.OrderBy(p => p.ProductId).ToList();

        //    if (!String.IsNullOrEmpty(searchString))
        //    {
        //        products = products.Where(s => s.Name.Contains(searchString)).OrderBy(p => p.ProductId).ToList();
        //        //products = (List<Product>) products.Where(s => s.Name.Contains(searchString));
        //    }
        //    var f = await products.AsQueryable().ToListAsync();

        //   // return View(f);
        //}


        //public async Task OnGetAsync(string searchString)
        //{
        //    var movies = from m in _productRepository.Products
        //                 select m;

        //    if (!String.IsNullOrEmpty(searchString))
        //    {
        //        movies = movies.Where(s => s.Name.Contains(searchString));
        //    }
        //    var f = await movies.AsQueryable().ToListAsync();

        //    Movie = await movies.ToListAsync();
        //}


        public IActionResult Details(int id)
        {
            var product = _productRepository.GetProductById(id);
            if (product == null)
                return NotFound();
            return View(product);
        }
    }
}