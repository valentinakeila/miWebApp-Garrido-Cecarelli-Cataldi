using Application.Interfaces;
using Application.Models;
using Application.Models.Request;
using Domain.Entities;
using Domain.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

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

       
        [HttpGet("[action]")]
        public ActionResult<List<ProductDto?>> GetAllProducts()
        {
            return _productService.GetAllProducts(); 
        }

        [HttpGet("[action]/{id}")]
        public ActionResult<ProductDto?> GetProductById([FromRoute] int id)
        {
            try
            {
                return _productService.GetProductById(id);
            }
            catch (NotFoundException)
            {

                return NotFound("El Id especificado no existe");
            }

            
        }

        [HttpGet("[action]/{categoryId}")]
        public ActionResult<List<ProductDto>> GetProductsByCategory([FromRoute] int categoryId) 
        {
            try
            {
                return _productService.GetProductsByCategory(categoryId);
            }
            catch (NotFoundException)
            {

                return NotFound("El Id de categoría especificado no existe");
            }
            
        }

        [HttpGet("[action]/{name}")]
        public ActionResult<List<ProductDto?>> GetProductsByName([FromRoute] string name)
        {
            try
            {
                return _productService.GetProductsByName(name);
            }
            catch (NotFoundException)
            {

                return NotFound("El nombre del producto especificado no existe");
            }
            
        }

        [Authorize]
        [HttpPost("[action]")]
        public ActionResult<ProductDto> CreateNewProduct([FromBody] ProductCreateRequest productCreateRequest)
        {
            return _productService.CreateNewProduct(productCreateRequest);
        }

        [Authorize]
        [HttpPut("[action]/{id}")]
        public ActionResult ModifyProductData([FromRoute] int id, [FromBody] ProductUpdateRequest productUpdateRequest)
        {
            try
            {
                _productService.ModifyProductData(id, productUpdateRequest);
                return Ok();
            }
            catch (NotFoundException)
            {

                return NotFound("El id especificado no existe");
            }
        }

        [Authorize]
        [HttpDelete("[action]/{id}")]
        public ActionResult DeleteProduct([FromRoute] int id)
        {
            try
            {
                _productService.DeleteProduct(id);
                return Ok();
            }
            catch (NotFoundException)
            {

                return NotFound("El id especificado no existe");
            }
        }
    }
}
