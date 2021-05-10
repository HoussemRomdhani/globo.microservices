using BackOffice.Client.Models;
using BackOffice.Client.Services;
using BackOffice.Client.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Linq;

namespace BackOffice.Client.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public ProductController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
            _categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));
        }

        public ActionResult Index()
        {
            var categories = _categoryService.Get();
            var products = _productService.Get();

            var vm = products.Select(x => new ProductForDisplayDto(x)
            {
                CategoryName = categories.FirstOrDefault(c => c.Id == x.CategoryId)?.Name
            });

            return View(vm);
        }

        public ActionResult Create()
        {
            var categories = _categoryService.Get();

            var model = new ProductForCreationInputDto
            {
                Categories = categories.Select(category => new SelectListItem { Value = category.Id.ToString(), Text = category.Name })
                                       .ToList()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductForCreationInputDto model)
        {
            if (!ModelState.IsValid)
                return View();

            var modelToCreate = new ProductInputDto(model);

            var createdModel = _productService.Create(modelToCreate);

            if (createdModel == null)
            {
                model.Categories = _categoryService.Get().Select(category => new SelectListItem { Value = category.Id.ToString(), Text = category.Name })
                                       .ToList();

                return View();
            }

            return RedirectToAction(nameof(Index));
        }

        public ActionResult Details(Guid id)
        {
            var model = _productService.GetById(id);
            if (model == null)
                RedirectToAction(nameof(Index));

            var vm = new ProductDetailsDto(model)
            {
                CategoryName = _categoryService.GetById(model.CategoryId)?.Name
            };

            return View(vm);
        }


        public ActionResult Edit(Guid id)
        {
            var model = _productService.GetById(id);
            if (model == null)
                RedirectToAction(nameof(Index));

            var categories = _categoryService.Get();

            var modelForUpdate = new ProductForUpdateInputDto(model)
            {
                Categories = categories.Select(category => new SelectListItem { Value = category.Id.ToString(), Text = category.Name })
                                       .ToList()
            };

            return View(modelForUpdate);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, ProductForUpdateInputDto model)
        {
            if (!ModelState.IsValid)
                return View();

            var modelForUpdate = new ProductInputDto(model);

            var isUpdated = _productService.Update(id, modelForUpdate);

            return !isUpdated ? View() : RedirectToAction(nameof(Index));
        }

        public ActionResult Delete(Guid id)
        {
            var result = _productService.GetById(id);
            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, Product model)
        {
            if (!ModelState.IsValid)
                return View();

            var isDeleted = _productService.Delete(id);

            return !isDeleted ? View() : RedirectToAction(nameof(Index));
        }

    }
}
