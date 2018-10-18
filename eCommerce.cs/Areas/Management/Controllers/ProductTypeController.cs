using eCommerce.cs.Data.Entities;
using eCommerce.cs.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.cs.Areas.Management.Controllers
{
    [Area("Management")]
    public class ProductTypeController : Controller
    {
        private readonly IProductTypeRepository _context;

        public ProductTypeController(IProductTypeRepository context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var productTypes = _context.GetAll();

            if (productTypes.Count() == 0) return View(nameof(Empty));

            return View(productTypes);
        }

        public IActionResult Empty()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProductType productType)
        {
            if (ModelState.IsValid)
            {
                _context.Create(productType);
                return RedirectToAction(nameof(Index));
            }

            return View(productType);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null) return NotFound();

            ProductType productType = _context.GetByID(id);

            if (productType == null) return NotFound();

            return View(productType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, ProductType productType)
        {
            if (id != productType.ProductID) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(productType);
                return RedirectToAction(nameof(Index));
            }

            return View(productType);
        }

        public IActionResult Show(int? id)
        {
            if (id == null) return NotFound();

            ProductType productType = _context.GetByID(id);

            if (productType == null) return NotFound();

            return View(productType);
        }
    }
}