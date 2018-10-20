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

namespace eCommerce.cs.Controllers
{
    [Area("Store")]
    public class HomeController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductTypeRepository _productTypeRepository;
        private readonly ISpecialTagRepository _specialTagRepository;
        public ProductViewModel ProductViewModel { get; }

        public HomeController(IProductRepository productRepository, IProductTypeRepository productTypeRepository, ISpecialTagRepository specialTagRepository)
        {
            _productRepository = productRepository;
            _productTypeRepository = productTypeRepository;
            _specialTagRepository = specialTagRepository;

            ProductViewModel = new ProductViewModel()
            {
                ProductTypes = _productTypeRepository.GetAllWithProductTypes(),
                SpecialTags = _specialTagRepository.GetAllWithSpecialTags(),
                Product = new Product()
            };
        }

        public IActionResult Index()
        {
            var products = _productRepository.GetAllWithProductTypesAndSpecialTags();

            return View(products);
        }

        public IActionResult Show(int id)
        {
            ProductViewModel.Product = _productRepository.FindWithProductTypesAndSpecialTags(id);

            return View(ProductViewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
