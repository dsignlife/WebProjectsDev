using System.Collections.Generic;
using Webshop.Models;

namespace Webshop.ViewModels
{
    public class ProductsListViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public string CurrentCategory { get; set; }
    }
}