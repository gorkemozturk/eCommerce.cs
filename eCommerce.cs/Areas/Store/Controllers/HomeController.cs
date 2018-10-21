using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using eCommerce.cs.Data;
using eCommerce.cs.Data.Interfaces;
using eCommerce.cs.Models;
using eCommerce.cs.Data.Entities;
using eCommerce.cs.Extensions;

namespace eCommerce.cs.Controllers
{
    [Area("Store")]
    public class HomeController : Controller
    {
        private readonly IProductRepository _productRepository;

        public HomeController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public IActionResult Index()
        {
            var products = _productRepository.GetAllWithProductTypesAndSpecialTags();

            return View(products);
        }

        public IActionResult Show(int id)
        {
            var product = _productRepository.FindWithProductTypesAndSpecialTags(id);

            return View(product);
        }

        [HttpPost, ActionName("Show")]
        [ValidateAntiForgeryToken]
        public IActionResult AddToCart(int id)
        {
            List<int> shoppingCart = HttpContext.Session.Get<List<int>>("ShoppingCart");

            if (shoppingCart == null) shoppingCart = new List<int>();

            shoppingCart.Add(id);
            HttpContext.Session.Set("ShoppingCart", shoppingCart);

            return RedirectToAction(nameof(Index));
        }

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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
