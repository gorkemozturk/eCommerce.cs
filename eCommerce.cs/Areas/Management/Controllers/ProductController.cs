using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerce.cs.Data.Interfaces;
using eCommerce.cs.Models;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.cs.Areas.Management.Controllers
{
    [Area("Management")]
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductTypeRepository _productTypeRepository;
        private readonly ISpecialTagRepository _specialTagRepository;

        [BindProperty]
        public ProductViewModel ProductViewModel { get; set; }

        public ProductController(IProductRepository productRepository, IProductTypeRepository productTypeRepository, ISpecialTagRepository specialTagRepository)
        {
            _productRepository = productRepository;
            _productTypeRepository = productTypeRepository;
            _specialTagRepository = specialTagRepository;

            ProductViewModel = new ProductViewModel()
            {
                ProductTypes = _productTypeRepository.GetAllWithProductTypes(),
                SpecialTags = _specialTagRepository.GetAllWithSpecialTags(),
                Product = new Data.Entities.Product()
            };
        }

        public IActionResult Index()
        {
            var products = _productRepository.GetAllWithProductTypesAndSpecialTags();

            return View(products);
        }
    }
}