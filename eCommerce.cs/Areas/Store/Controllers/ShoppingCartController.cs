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
        [ActionName("Index")]
        public IActionResult Store()
        {
            List<int> shoppingCart = HttpContext.Session.Get<List<int>>("ShoppingCart");

            Order order = ShoppingCartViewModel.Order;
            _orderRepository.Create(order);

            int orderID = order.OrderID;

            foreach (int productID in shoppingCart)
            {
                OrderDetail orderDetail = new OrderDetail() {
                    OrderID = orderID,
                    ProductID = productID
                };

                _orderDetailRepository.Create(orderDetail);
            }

            _orderDetailRepository.Save();
            shoppingCart = new List<int>();
            HttpContext.Session.Set("ShoppingCart", shoppingCart);

            return RedirectToAction(nameof(Index));
        }
    }
}