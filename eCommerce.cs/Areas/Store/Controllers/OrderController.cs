using System;
using System.Collections.Generic;
using eCommerce.cs.Data.Entities;
using eCommerce.cs.Data.Interfaces;
using eCommerce.cs.Extensions;
using eCommerce.cs.Models;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.cs.Areas.Store.Controllers
{
    [Area("Store")]
    public class OrderController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderDetailRepository _orderDetailRepository;

        [BindProperty]
        public ShoppingCartViewModel ShoppingCartViewModel { get; set; }

        public OrderController(IProductRepository productRepository, IOrderRepository orderRepository, IOrderDetailRepository orderDetailRepository)
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
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Store()
        {
            List<int> shoppingCart = HttpContext.Session.Get<List<int>>("ShoppingCart");

            ShoppingCartViewModel.Order.CreatedAt = DateTime.Now;

            Order order = ShoppingCartViewModel.Order;
            _orderRepository.Create(order);

            int orderID = order.OrderID;

            foreach (int productID in shoppingCart)
            {
                OrderDetail orderDetail = new OrderDetail()
                {
                    OrderID = orderID,
                    ProductID = productID
                };

                _orderDetailRepository.Create(orderDetail);
            }

            _orderDetailRepository.Save();
            shoppingCart = new List<int>();
            HttpContext.Session.Set("ShoppingCart", shoppingCart);

            return RedirectToAction("Summary", "Order", new { area = "Store", id = orderID });
        }

        public IActionResult Summary(int id)
        {
            ShoppingCartViewModel.Order = _orderRepository.FindOrder(id);
            List<OrderDetail> orderDetails = _orderDetailRepository.GetAllWithOrder(id);

            foreach (OrderDetail orderDetail in orderDetails)
            {
                ShoppingCartViewModel.Products.Add(_productRepository.FindWithProductTypesAndSpecialTags(orderDetail.ProductID));
            }

            return View(ShoppingCartViewModel);
        }
    }
}