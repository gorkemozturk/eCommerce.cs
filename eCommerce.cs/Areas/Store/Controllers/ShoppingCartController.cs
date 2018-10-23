using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerce.cs.Data.Entities;
using eCommerce.cs.Data.Interfaces;
using eCommerce.cs.Extensions;
using eCommerce.cs.Models;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.cs.Areas.Store.Controllers
{
    [Area("Store")]
    public class ShoppingCartController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderDetailRepository _orderDetailRepository;

        [BindProperty]
        public ShoppingCartViewModel ShoppingCartViewModel { get; set; }

        public ShoppingCartController(IProductRepository productRepository, IOrderRepository orderRepository, IOrderDetailRepository orderDetailRepository)
        {
            _productRepository = productRepository;
            _orderRepository = orderRepository;
            _orderDetailRepository = orderDetailRepository;
            ShoppingCartViewModel = new ShoppingCartViewModel()
            {
                Products = new List<Product>()
            };
        }

        public IActionResult Index()
        {
            List<int> shoppingCart = HttpContext.Session.Get<List<int>>("ShoppingCart");

            if (shoppingCart.Count > 0)
            {
                foreach (var item in shoppingCart)
                {
                    Product product = _productRepository.FindWithProductTypesAndSpecialTags(item);
                    ShoppingCartViewModel.Products.Add(product);
                }
            }

            return View(ShoppingCartViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddToCart(int id)
        {
            List<int> shoppingCart = HttpContext.Session.Get<List<int>>("ShoppingCart");

            if (shoppingCart == null) shoppingCart = new List<int>();

            shoppingCart.Add(id);
            HttpContext.Session.Set("ShoppingCart", shoppingCart);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteFromCart(int id)
        {
            List<int> shoppingCart = HttpContext.Session.Get<List<int>>("ShoppingCart");

            if (shoppingCart.Count > 0)
            {
                if (shoppingCart.Contains(id)) shoppingCart.Remove(id);
            }

            HttpContext.Session.Set("ShoppingCart", shoppingCart);

            return RedirectToAction(nameof(Index));
        }
    }
}