using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pri.Identity.Api.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pri.Identity.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        List<ProductDto> products = new List<ProductDto>();

        public ProductsController()
        {
            GenerateSomeProducts();
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(products);
        }

        [HttpGet("loyalmembers")]
        [Authorize(Policy = "OnlyLoyalMembers")]
        public async Task<IActionResult> GetProductsForLoyalMembers()
        {
            return Ok("A list of products for our loyal members! Enjoy :-)");
        }

        [HttpPost]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Post([FromBody] ProductDto productDto)
        {
            productDto.Id = Guid.NewGuid();
            products.Add(productDto);
            return Ok("Product was added");
        }

        private void GenerateSomeProducts()
        {
            products.Add(new ProductDto { Id = Guid.NewGuid(), Name = "Product 1", Price = 23.45m });
            products.Add(new ProductDto { Id = Guid.NewGuid(), Name = "Product 2", Price = 43.45m });
            products.Add(new ProductDto { Id = Guid.NewGuid(), Name = "Product 3", Price = 63.45m });
        }
    }
}
