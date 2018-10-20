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
                var uploads = Path.Combine(webRootPath, ProductImageUtility.ImagePath);
                var extension = Path.GetExtension(files[0].FileName);

                using (var fileStream = new FileStream(Path.Combine(uploads, ProductViewModel.Product.ProductID + extension), FileMode.Create))
                {
                    files[0].CopyTo(fileStream);
                }

                product.Image = @"\" + ProductImageUtility.ImagePath + @"\" + ProductViewModel.Product.ProductID + extension;
            }
            else
            {
                var uploads = Path.Combine(webRootPath, ProductImageUtility.ImagePath + @"\" + ProductImageUtility.DefaultProductImage);
                System.IO.File.Copy(uploads, webRootPath + @"\" + ProductImageUtility.ImagePath + @"\" + ProductViewModel.Product.ProductID + ".jpg");
                product.Image = @"\" + ProductImageUtility.ImagePath + @"\" + ProductViewModel.Product.ProductID + ".jpg";
            }

            _productRepository.Save();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int? id)
        {
            if (id == null) return NotFound();

            ProductViewModel.Product = _productRepository.FindWithProductTypesAndSpecialTags(id);

            if (ProductViewModel == null) return NotFound();

            return View(ProductViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id)
        {
            if (ModelState.IsValid)
            {
                string webRootPath = _hostingEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;
                var product = _productRepository.Find(m => m.ProductID == ProductViewModel.Product.ProductID);

                if (files[0].Length > 0 && files[0] != null)
                {
                    var uploads = Path.Combine(webRootPath, ProductImageUtility.ImagePath);
                    var oldExtension = Path.GetExtension(files[0].FileName);
                    var newExtension = Path.GetExtension(product.Image);

                    if (System.IO.File.Exists(Path.Combine(uploads, ProductViewModel.Product.ProductID + oldExtension)))
                    {
                        System.IO.File.Delete(Path.Combine(uploads, ProductViewModel.Product.ProductID + oldExtension));
                    }

                    using (var fileStream = new FileStream(Path.Combine(uploads, ProductViewModel.Product.ProductID + newExtension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }

                    ProductViewModel.Product.Image = @"\" + ProductImageUtility.ImagePath + @"\" + ProductViewModel.Product.ProductID + newExtension;
                }

                if (ProductViewModel.Product.Image != null) product.Image = ProductViewModel.Product.Image;

                product.ProductName = ProductViewModel.Product.ProductName;
                product.Price = ProductViewModel.Product.Price;
                product.IsAvailable = ProductViewModel.Product.IsAvailable;
                product.ProductTypeID = ProductViewModel.Product.ProductTypeID;
                product.SpecialTagID = ProductViewModel.Product.SpecialTagID;

                _productRepository.Save();

                return RedirectToAction(nameof(Index));
            }

            return View(ProductViewModel);
        }
    }
}