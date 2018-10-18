using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerce.cs.Data.Entities;
using eCommerce.cs.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.cs.Areas.Management.Controllers
{
    [Area("Management")]
    public class SpecialTagController : Controller
    {
        private readonly ISpecialTagRepository _context;

        public SpecialTagController(ISpecialTagRepository context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var specialTags = _context.GetAll();

            if (specialTags.Count() == 0) return View(nameof(Empty));

            return View(specialTags);
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
        public IActionResult Create(SpecialTag  specialTag)
        {
            if (ModelState.IsValid)
            {
                _context.Create(specialTag);
                return RedirectToAction(nameof(Index));
            }

            return View(specialTag);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null) return NotFound();

            SpecialTag specialTag = _context.GetByID(id);

            if (specialTag == null) return NotFound();

            return View(specialTag);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, SpecialTag specialTag)
        {
            if (id != specialTag.TagID) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(specialTag);
                return RedirectToAction(nameof(Index));
            }

            return View(specialTag);
        }

        public IActionResult Show(int? id)
        {
            if (id == null) return NotFound();

            SpecialTag specialTag = _context.GetByID(id);

            if (specialTag == null) return NotFound();

            return View(specialTag);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();

            SpecialTag specialTag = _context.GetByID(id);

            if (specialTag == null) return NotFound();

            return View(specialTag);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var specialTag = _context.GetByID(id);
            _context.Delete(specialTag);

            return RedirectToAction(nameof(Index));
        }
    }
}