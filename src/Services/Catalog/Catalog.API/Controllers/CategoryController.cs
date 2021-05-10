using Catalog.API.Models;
using Catalog.API.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Catalog.API.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService ??
                throw new ArgumentNullException(nameof(categoryService));
        }

        [HttpGet]
        public ActionResult Get()
        {
            var result = _categoryService.Get();
            return Ok(result);
        }

        [HttpGet("{id}", Name = "GetCategory")]
        public ActionResult Get(Guid id)
        {
            var result = _categoryService.GetById(id);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpPost]
        public ActionResult Create(CategoryInputDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var createdModel = _categoryService.Create(model);
            return CreatedAtRoute("GetCategory", new { id = createdModel.Id.ToString() }, createdModel);
        }

        [HttpPut("{id}")]
        public IActionResult Update(Guid id, CategoryInputDto modelIn)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            _categoryService.Update(id, modelIn);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var result = _categoryService.GetById(id);

            if (result == null)
                return NotFound();

            _categoryService.Delete(result.Id);

            return NoContent();
        }
    }
}
