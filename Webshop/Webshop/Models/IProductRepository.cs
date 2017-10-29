using System.Collections.Generic;

namespace Webshop.Models
{
    public interface IProductRepository
    {
        IEnumerable<Product> Products { get; }
        IEnumerable<Product> ProductsOfTheWeek { get; }

        Product GetProductById(int productId);

        void CreateProduct(Product product);

        void UpdateProduct(Product product);
    }
}