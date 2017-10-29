using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Webshop.Auth
{
    public static class WebShopClaimTypes
    {
        public static List<string> ClaimsList { get; set; } =
            new List<string> { "Delete Product", "Add Product", "Age for ordering" };
    }
}