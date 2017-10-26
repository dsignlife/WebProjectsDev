using Microsoft.AspNetCore.Mvc;
using Webshop.Models;
using Webshop.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Webshop.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductRepository _productRepository;

        public HomeController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            var homeViewModel = new HomeViewModel {
                ProductsOfTheWeek = _productRepository.ProductsOfTheWeek
            };
            return View(homeViewModel);
        }
    }
}