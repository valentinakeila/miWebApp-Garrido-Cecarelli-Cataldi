using Application.Interfaces;
using Application.Models;
using Application.Models.Request;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using System.Collections.Generic;
using System.Security.Claims;

namespace Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

       

        public List<CategoryDto?> GetAllCategories()
        {
            var categories = _categoryRepository.List();

            return CategoryDto.CreateList(categories);
        }

        public CategoryDto GetCategoryById(int id)
        {
            var category = _categoryRepository.GetById(id);
            if (category == null)
                throw new NotFoundException(nameof(Category), id);

            return CategoryDto.Create(category);
        }

        public CategoryDto CreateNewCategory(CategoryCreateRequest categoryCreateRequest)
        {
            var category = new Category
            {
                Name = categoryCreateRequest.Name,
                ImageUrl = categoryCreateRequest.ImageUrl
            };

            _categoryRepository.Add(category);
            return CategoryDto.Create(category);
        }

        public void ModifyCategoryData(int id, CategoryUpdateRequest updateCategoryRequest)
        {
            var category = _categoryRepository.GetById(id);
            if (category == null)
                throw new NotFoundException(nameof(Category), id);

            category.Name = updateCategoryRequest.Name;
            category.ImageUrl = updateCategoryRequest.ImageUrl;

            _categoryRepository.Update(category);
        }

        public void DeleteCategory(int id)
        {
            var category = _categoryRepository.GetById(id);
            if (category == null)
                throw new NotFoundException(nameof(Category), id);

            _categoryRepository.Delete(category);
        }

        public CategoryDto GetCategoryByName(string name)
        {
            var category = _categoryRepository.GetCategoryByName(name);
            if (category == null)
                throw new NotFoundException(nameof(Category));

            return CategoryDto.Create(category);
        }
    }
}

//int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "");
//var userRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
