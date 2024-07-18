using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IProductRepository : IRepositoryBase<Product>
    {
        Product? GetProductById(int id);
        List<Product?> GetProductByCategory(int categoryId);
        List<Product?> GetProductByName(string name);
    }
}