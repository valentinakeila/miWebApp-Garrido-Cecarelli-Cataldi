using Application.Interfaces;
using Application.Models;
using Application.Models.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [Authorize]
        [HttpGet("[action]")]
        public ActionResult<List<ProductDto?>> GetAllProducts()
        {
            return _productService.GetAllProducts(); 
        }

        [HttpGet("[action]/{id}")]
        public ActionResult<ProductDto?> GetProductById([FromRoute] int id)
        {
            return _productService.GetProductById(id);
        }

        [HttpGet("[action]/{categoryId}")]
        public ActionResult<List<ProductDto>> GetProductsByCategory([FromRoute] int categoryId)
        {
            return _productService.GetProductsByCategory(categoryId);
        }

        [HttpGet("[action]/{name}")]
        public ActionResult<List<ProductDto>> GetProductsByName([FromRoute] string name)
        {
            return _productService.GetProductsByName(name);
        }

        [HttpPost("[action]")]
        public ActionResult<ProductDto> CreateNewProduct([FromBody] ProductCreateRequest productCreateRequest)
        {
            return _productService.CreateNewProduct(productCreateRequest);
        }

        [HttpPut("[action]/{id}")]
        public ActionResult ModifyProductData([FromRoute] int id, [FromBody] ProductUpdateRequest productUpdateRequest)
        {
            _productService.ModifyProductData(id, productUpdateRequest);
            return Ok();
        }

        [HttpDelete("[action]/{id}")]
        public ActionResult DeleteProduct([FromRoute] int id)
        {
            _productService.DeleteProduct(id);
            return Ok();
        }
    }
}
