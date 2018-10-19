using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using eCommerce.cs.Data.Interfaces;
using eCommerce.cs.Models;
using eCommerce.cs.Utilities;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.cs.Areas.Management.Controllers
{
    [Area("Management")]
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductTypeRepository _productTypeRepository;
        private readonly ISpecialTagRepository _specialTagRepository;
        private readonly HostingEnvironment _hostingEnvironment;

        [BindProperty]
        public ProductViewModel ProductViewModel { get; set; }

        public ProductController(IProductRepository productRepository, IProductTypeRepository productTypeRepository, ISpecialTagRepository specialTagRepository, HostingEnvironment hostingEnvironment)
        {
            _productRepository = productRepository;
            _productTypeRepository = productTypeRepository;
            _specialTagRepository = specialTagRepository;
            _hostingEnvironment = hostingEnvironment;

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

        public IActionResult Create()
        {
            return View(ProductViewModel);
        }

        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public IActionResult Store()
        {
            if (!ModelState.IsValid) return View(ProductViewModel);

            _productRepository.Create(ProductViewModel.Product);

            string webRootPath = _hostingEnvironment.WebRootPath;
            var files = HttpContext.Request.Form.Files;
            var product = _productRepository.GetByID(ProductViewModel.Product.ProductID);

            if (files.Count != 0)
            {
                var uploads = Path.Combine(webRootPath, StaticDetails.ImagePath);
                var extension = Path.GetExtension(files[0].FileName);

                using (var fileStream = new FileStream(Path.Combine(uploads, ProductViewModel.Product.ProductID + extension), FileMode.Create))
                {
                    files[0].CopyTo(fileStream);
                }

                product.Image = @"\" + StaticDetails.ImagePath + @"\" + ProductViewModel.Product.ProductID + extension;
            }
            else
            {
                var uploads = Path.Combine(webRootPath, StaticDetails.ImagePath + @"\" + StaticDetails.DefaultProductImage);
                System.IO.File.Copy(uploads, webRootPath + @"\" + StaticDetails.ImagePath + @"\" + ProductViewModel.Product.ProductID + ".jpg");
                product.Image = @"\" + StaticDetails.ImagePath + @"\" + ProductViewModel.Product.ProductID + ".jpg";
            }

            _productRepository.Save();

            return RedirectToAction(nameof(Index));
        }
    }
}