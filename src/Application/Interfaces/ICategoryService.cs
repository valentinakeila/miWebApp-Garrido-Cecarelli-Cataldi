using Application.Models.Request;
using Application.Models;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface ICategoryService
    {
        CategoryDto? GetCategoryById(int id);

        List<CategoryDto?> GetAllCategories();

        CategoryDto CreateNewCategory(CategoryCreateRequest categoryCreateRequest);

        void ModifyCategoryData(int id, CategoryUpdateRequest categoryUpdateRequest);

        void DeleteCategory(int id);

        CategoryDto? GetCategoryByName(string name);

    }
}
