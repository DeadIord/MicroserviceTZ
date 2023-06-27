using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using ProducService.Models;
using System.Threading.Tasks;

namespace ProducService.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly ProductServices _ProducService;

        public ProductsController(ProductServices ProducService)
        {
            _ProducService = ProducService;
        }

     
        [HttpGet]
        public IActionResult GetAllProducts()
        {
            var products = _ProducService.GetAllProducts();

            if (products == null)
                return NotFound();

            return Ok(products);
        }

       
        [HttpPost]
        public async Task<IActionResult> CreateProduct(Product product,string name,double price)
        {
            product.Name = name;
            product.Price = price;
            var createdProduct = await _ProducService.CreateProduct(product);
            
               
            return CreatedAtAction(nameof(GetProductById), new { productId = createdProduct.Id }, createdProduct);
        }

        [HttpGet("{productId}")]
        public async Task<IActionResult> GetProductById(int productId)
        {
            var product = await _ProducService.GetProductById(productId);

            if (product == null)
                return BadRequest(new { message = "Данные отсутствуют" });

            return Ok(product);
        }

        [HttpPut("{productId}")]
        public async Task<IActionResult> UpdateProduct(int productId, string name, double price, Product updatedProduct)
        {
            updatedProduct.Name = name;
            updatedProduct.Price = price;
            var product = await _ProducService.UpdateProduct(productId, updatedProduct);

            if (product == null)
                return BadRequest(new { message = "Данный продукт отсутствует" });

            return Ok(product);
        }

        [HttpDelete("{productId}")]
        public async Task<IActionResult> DeleteProduct(int productId)
        {
            var result = await _ProducService.DeleteProduct(productId);

            if (!result)
                return BadRequest(new { message = "Данный продукт отсутствует" });

            return NoContent();
        }
    }
}
