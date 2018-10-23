using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using eCommerce.cs.Data.Interfaces;
using eCommerce.cs.Models;

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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
