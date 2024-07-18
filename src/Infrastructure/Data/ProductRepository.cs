using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class ProductRepository : EfRepository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationContext context) : base(context)
        {
        }

        public Product? GetProductById(int id)
        {
            var query = _context.Products.Where(u => u.Id == id);
            return query.FirstOrDefault();
        }

        public List<Product?> GetProductByCategory( int categoryId)
        {
            IQueryable<Product?> query;

            query = _context.Products.Where(u => u.Category.Id == categoryId);

            return query.ToList();
        }
        public List<Product?> GetProductByName(string name)
        {
            IQueryable<Product?> query;

            query = _context.Products.Where(u => u.Name == name);

            return query.ToList();
        }

    }
}
