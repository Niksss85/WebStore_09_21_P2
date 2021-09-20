using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain;
using WebStore.Interfaces.Services;

namespace WebStore.WebAPI.Controllers
{

    [ApiController]
    [Route("api/products")]
    public class ProductsAPIController : ControllerBase
    {
        private readonly IProductData _ProductData;
        public ProductsAPIController(IProductData ProductData)
        {
            _ProductData = ProductData;
        }
        [HttpGet("sections")]
        public IActionResult GetSections()
        {
            var sections = _ProductData.GetSections();
            return Ok(sections);
        }
        [HttpGet("sections/{id:int}")]
        public IActionResult GetSections(int id)
        {
            var section = _ProductData.GetSection(id);
            if (section is null)
                return NotFound(id);
            return Ok(section);
        }
        [HttpGet("brands")]
        public IActionResult GetBrands()
        {
            var brands = _ProductData.GetBrands();
            return Ok(brands);
        }
        [HttpGet("brands/{id:int}")]
        public IActionResult GetBrand(int id)
        {
            var brand = _ProductData.GetBrand(id);
            if (brand is null)
                return NotFound(id);
            return Ok(brand);
        }
        [HttpPost]
        public IActionResult GetProducts(ProductFilter Filter =null)
        {
            var products = _ProductData.GetProducts(Filter ?? new());
            return Ok(products);
        }
        [HttpGet("{id:int}")]
        public IActionResult GetProduct(int id)
        {
            var product = _ProductData.GetProductById(id);
            if (product is null)
                return NotFound(id);
            return Ok(product);
        }
    }
}
