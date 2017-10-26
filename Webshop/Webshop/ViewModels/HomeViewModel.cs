using System.Collections.Generic;
using Webshop.Models;

namespace Webshop.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Product> ProductsOfTheWeek { get; set; }
    }
}