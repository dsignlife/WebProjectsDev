using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Template;
using Microsoft.AspNetCore.Server.Kestrel.Internal.System.Runtime;
using Webshop.Models;
using Webshop.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Webshop.Controllers
{
    [Route("api/[controller]")]
    public class ProductDataController : Controller
    {
        private IProductRepository _productRepository;

        
        // GET: /<controller>/
        public ProductDataController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public IEnumerable<ProductViewModel> LoadMoreProducts()
        {
            IEnumerable<Product> dbProducts = null;

            dbProducts = _productRepository.Products.OrderBy(p => p.ProductId).Take(10);

            List<ProductViewModel> products = new List<ProductViewModel>();

            foreach (var dbProduct in dbProducts)
            {
                products.Add(MapDbProductToProductViewModel(dbProduct));
            }
            return products;
        }

        
        private ProductViewModel MapDbProductToProductViewModel(Product dbProduct)
        {
            return new ProductViewModel()
            {
                ProductId = dbProduct.ProductId,
                Name = dbProduct.Name,
                Price = dbProduct.Price,
                ShortDesc = dbProduct.ShortDesc,
                ImageThumbnailUrl = dbProduct.ImageThumbnailUrl
            };
        }
    }
}
