using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Data
{
    public class CategoryRepository(ApplicationContext context) : EfRepository<Category>(context), ICategoryRepository
    {
        public Category? GetCategoryByName(string name)
        {
            var query = _context.Categories.Where(c => c.Name == name);
            return query.FirstOrDefault();
        }
    }
}
