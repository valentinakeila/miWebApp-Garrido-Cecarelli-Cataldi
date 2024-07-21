using Application.Interfaces;
using Application.Models;
using Application.Models.Request;
using Application.Services;
using Domain.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }


        [HttpGet("[action]")]
        public ActionResult<List<CategoryDto?>> GetAllCategory()
        {
            return _categoryService.GetAllCategories();
        }

        [HttpGet("[action]/{id}")]
        public ActionResult<CategoryDto?> GetCategoryById([FromRoute] int id)
        {
            try
            {
                return _categoryService.GetCategoryById(id);
            }
            catch (NotFoundException)
            {
                return NotFound("El Id especificado no existe");
            }
        }



        [HttpGet("[action]/{name}")]
        public ActionResult<CategoryDto?> GetCategoryByName([FromRoute] string name)
        {
            try
            {
                return _categoryService.GetCategoryByName(name);
            }
            catch (NotFoundException)
            {
                return NotFound("El nombre especificado no existe");
            }
            
        }

        [HttpPost("[action]")]
        public ActionResult<CategoryDto> CreateNewCategory([FromBody] CategoryCreateRequest categoryCreateRequest)
        {
            return _categoryService.CreateNewCategory(categoryCreateRequest);
        }

        [HttpPut("[action]/{id}")]
        public ActionResult ModifyCategoryData([FromRoute] int id, [FromBody] CategoryUpdateRequest categoryUpdateRequest)
        {

            try
            {
                _categoryService.ModifyCategoryData(id, categoryUpdateRequest);
                return Ok();
            }
            catch (NotFoundException)
            {
                return NotFound("El Id especificado no existe");
            }
        }

        [HttpDelete("[action]/{id}")]
        public ActionResult DeleteCategory([FromRoute] int id)
        {
            
            try
            {
                _categoryService.DeleteCategory(id);
                return Ok();
            }
            catch (NotFoundException)
            {
                return NotFound("El Id especificado no existe");
            }
        }
    }
}
