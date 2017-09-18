using System.Collections;
using System.Collections.Generic;

namespace Webshop.Models
{
    public interface IProductRepository
    {
        IEnumerable<Product> Products { get; }
        IEnumerable<Product> ProductsOfTheWeek { get; }


        Product GetProducById(int productId);
    }
}