using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Webshop.Models
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _appDbContext;

        public ProductRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<Product> Products {
            get { return _appDbContext.Products.Include(c => c.Category); }
        }

        public IEnumerable<Product> ProductsOfTheWeek {
            get { return _appDbContext.Products.Include(c => c.Category).Where(p => p.IsProductOfTheWeek); }
        }

        public Product GetProductById(int productId)
        {
            return _appDbContext.Products.FirstOrDefault(p => p.ProductId == productId);
        }
    }
}