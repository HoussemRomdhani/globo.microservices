using Catalog.API.Models;
using Catalog.API.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Catalog.API.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService) =>
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));

        [HttpGet]
        public ActionResult Get(Guid? categoryId)
        {
            var result = _productService.Get(categoryId);
            return Ok(result);
        }

        [HttpGet("{id}", Name = "GetProduct")]
        public ActionResult Get(Guid id)
        {
            var result = _productService.GetById(id);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpPost]
        public ActionResult Create(ProductInputDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var createdModel = _productService.Create(model);
            return CreatedAtRoute("GetProduct", new { id = createdModel.Id.ToString() }, createdModel);
        }

        [HttpPut("{id}")]
        public IActionResult Update(Guid id, ProductInputDto modelIn)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            _productService.Update(id, modelIn);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            _productService.Delete(id);

            return NoContent();
        }
    }
}
