using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Catalog.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CatalogController : ControllerBase
    {
        private static List<Product> Products = new List<Product>();

        private readonly ILogger<CatalogController> _logger;

        public CatalogController(ILogger<CatalogController> logger)
        {
            _logger = logger;
            Products.Clear();
            Products.Add(new Product { Id = "001", Name = "chair", Price = 295, Quantity = 1 });
        }

        /// <summary>
        /// Get Catalog Size
        /// Success: Return number of products in the Catalog
        /// Failed: Return 501, if there is no product in the Catalog
        /// </summary>
        /// <returns></returns>
        [Route("size")]
        [HttpGet]
        public IActionResult Size()
        {
            var count = Products.Count;
            if(count > 0)
            {
                var result = new CatalogSize
                {
                    Count = count
                };
                return Ok(result);
            }
            else
            {
                return StatusCode(501);
            }
            
        }

        /// <summary>
        /// Get Product from Catalog by Product ID
        /// Success: Return requested product details
        /// Failed: Return 404, if there is no product with the given ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("{id}")]
        [HttpGet]
        public IActionResult Get(string id)
        {
            var product = Products.FirstOrDefault(x => x.Id == id);
            if(product != null)
            {
                return Ok(product);
            }
            else
            {
                return StatusCode(404);
            }
        }
    }
}