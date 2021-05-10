using BackOffice.Client.Models;
using BackOffice.Client.Services;
using BackOffice.Client.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BackOffice.Client.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));
        }

        public ActionResult Index()
        {
            var result = _categoryService.Get();

            return View(result);
        }

        public ActionResult Create()
        {
            var model = new CategoryInputDto();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CategoryInputDto model)
        {
            if (!ModelState.IsValid)
                return View();

            var created = _categoryService.Create(model);

            return created == null ? View() : RedirectToAction(nameof(Index));
        }

        public ActionResult Edit(Guid id)
        {
            var model = _categoryService.GetById(id);
            if (model == null)
                RedirectToAction(nameof(Index));

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, Category model)
        {
            if (!ModelState.IsValid)
                return View();

            var modelToUpdate = new CategoryInputDto(model.Name);

            bool isUpdated = _categoryService.Update(id, modelToUpdate);

            return !isUpdated ? View() : RedirectToAction(nameof(Index));
        }


        public ActionResult Delete(Guid id)
        {
            var model = _categoryService.GetById(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, Category model)
        {
            var isDeleted = _categoryService.Delete(id);
            return !isDeleted ? View() : RedirectToAction(nameof(Index));
        }

    }
}
