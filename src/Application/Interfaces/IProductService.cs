using Application.Models;
using Application.Models.Request;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IProductService
    {
        List<ProductDto?> GetAllProducts();
        ProductDto? GetProductById(int id);
        List<ProductDto?> GetProductsByCategory(int categoryId);
        List<ProductDto?> GetProductsByName(string name);
        ProductDto CreateNewProduct(ProductCreateRequest productCreateRequest);

        void ModifyProductData(int id, ProductUpdateRequest productUpdateRequest);

        void DeleteProduct(int id);
    }
}
