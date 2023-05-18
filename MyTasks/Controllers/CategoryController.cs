using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyTasks.Core.Models.Domains;
using MyTasks.Core.Service;
using MyTasks.Core.ViewModels;
using MyTasks.Persistence.Extentions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyTasks.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        private ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public IActionResult Categories()
        {
            var userId = User.GetUserId();
            var categories = _categoryService.Get(userId);

            return View(categories);
        }

        [HttpPost]
        public IActionResult Categories(CategoriesViewModel viewModel)
        {
            var userId = User.GetUserId();
            var categories = _categoryService.Get(userId);

            return View("Categories", categories);
        }

        public IActionResult Category(int id = 0)
        {
            var userId = User.GetUserId();
            var category = id == 0 ?
                new Category { Id = 0, UserId = userId} :
               _categoryService.Get(id, userId);

            var vm = new CategoryViewModel
            {
                Category = category,
                Heading = id == 0 ?
                "Dodawanie nowej kategorii" : "Edycja kategorii"
            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Category(Category category)
        {
            var userId = User.GetUserId();
            category.UserId = userId;
            ModelState.Remove("category.UserId");

            if (!ModelState.IsValid)
            {
                var vm = new CategoryViewModel
                {
                    Category = category,
                    Heading = category.Id == 0 ?
                    "Dodawanie nowej kategorii" : "Edycja kategorii"
                };
                return View("Category", vm);
            }

            if (category.Id == 0)
                _categoryService.Add(category);
            else
                _categoryService.Update(category);

            return RedirectToAction("Categories");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            try
            {
                var userId = User.GetUserId();
                _categoryService.Delete(id, userId);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
            return Json(new { success = true });

        }
    }
}
