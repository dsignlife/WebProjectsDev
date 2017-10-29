using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Webshop.Models;

namespace Webshop.ViewModels
{
    public class ProductEditViewModel
    {
        public Product Product { get; set; }
        public List<SelectListItem> Categories { get; set; }
        public int CategoryId { get; set; }
    }
}
