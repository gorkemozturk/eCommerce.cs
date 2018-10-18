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

            return View();
        }

        public IActionResult Empty()
        {
            return View();
        }
    }
}