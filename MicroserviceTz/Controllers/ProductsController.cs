using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using MicroserviceTz.Models;
using System.Threading.Tasks;

namespace MicroserviceTz.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly ProductService _productService;

        public ProductsController(ProductService productService)
        {
            _productService = productService;
        }

     
        [HttpGet]
        public IActionResult GetAllProducts()
        {
            var products = _productService.GetAllProducts();

            if (products == null)
                return NotFound();

            return Ok(products);
        }

       
        [HttpPost]
        public async Task<IActionResult> CreateProduct(Product product,string name,double price)
        {
            product.Name = name;
            product.Price = price;
            var createdProduct = await _productService.CreateProduct(product);
            
               
            return CreatedAtAction(nameof(GetProductById), new { productId = createdProduct.Id }, createdProduct);
        }

        [HttpGet("{productId}")]
        public async Task<IActionResult> GetProductById(int productId)
        {
            var product = await _productService.GetProductById(productId);

            if (product == null)
                return BadRequest(new { message = "Данные отсутствуют" });

            return Ok(product);
        }

        [HttpPut("{productId}")]
        public async Task<IActionResult> UpdateProduct(int productId, string name, double price, Product updatedProduct)
        {
            updatedProduct.Name = name;
            updatedProduct.Price = price;
            var product = await _productService.UpdateProduct(productId, updatedProduct);

            if (product == null)
                return BadRequest(new { message = "Данный продукт отсутствует" });

            return Ok(product);
        }

        [HttpDelete("{productId}")]
        public async Task<IActionResult> DeleteProduct(int productId)
        {
            var result = await _productService.DeleteProduct(productId);

            if (!result)
                return BadRequest(new { message = "Данный продукт отсутствует" });

            return NoContent();
        }
    }
}
