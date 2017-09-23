using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Webshop.Models;
using Webshop.ViewModels;

namespace Webshop.Components
{
    public class ShoppingCartSummary : ViewComponent
    {

        private ShoppingCart _shoppingCart;


        public ShoppingCartSummary(ShoppingCart shoppingCart)
        {
            _shoppingCart = shoppingCart;
        }

        public IViewComponentResult Invoke()
        {
            
            var items = _shoppingCart.GetShoppingCartItems();
            //var items = new List<ShoppingCartItem>() {new ShoppingCartItem(), new ShoppingCartItem()}; //testdata
            _shoppingCart.ShoppingCartItems = items;

            var shoppingCartViewModel = new ShoppingCartViewModel
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()
            };
            return View(shoppingCartViewModel);
        }
    }
}
