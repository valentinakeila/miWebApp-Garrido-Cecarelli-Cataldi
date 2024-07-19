using Application.Interfaces;
using Application.Models;
using Application.Models.Request;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using System.Collections.Generic;

namespace Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public List<CategoryDto> GetAllCategories()
        {
            var categories = _categoryRepository.GetAllCategories();
            return CategoryDto.CreateList(categories);
        }

        public CategoryDto GetCategoryById(int id)
        {
            var category = _categoryRepository.GetById(id);
            if (category == null)
                throw new NotFoundException(nameof(Category), id);

            return CategoryDto.Create(category);
        }

        public CategoryDto CreateCategory(CreateCategoryRequest createCategoryRequest)
        {
            var category = new Category
            {
                Name = createCategoryRequest.Name,
                ImageUrl = createCategoryRequest.ImageUrl
            };

            _categoryRepository.Add(category);
            return CategoryDto.Create(category);
        }

        public void UpdateCategory(int id, UpdateCategoryRequest updateCategoryRequest)
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
    }
}
