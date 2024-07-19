using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Models;
using Application.Models.Request;
using Domain.Entities;
using Domain.Enums;
using Domain.Exceptions;
using Domain.Interfaces;
using System.Collections.Generic;

namespace Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public List<ProductDto> GetAllProducts()
        {
            var productsList = _productRepository.GetAllProducts();

            if (productsList == null || !productsList.Any())
                throw new NotFoundException(nameof(Product), "All");

            return ProductDto.CreateList(productsList);
        } 

        public ProductDto? GetProductById(int id)
        {
            var product = _productRepository.GetById(id);

            if (product == null)
                throw new NotFoundException(nameof(Product), id);

            return ProductDto.Create(product);
        }

        public List<ProductDto?> GetProductsByCategory(int categoryId)
        {
            var productsList = _productRepository.GetProductByCategory(categoryId);

            if (productsList == null)
                throw new NotFoundException(nameof(Product), categoryId);

            return ProductDto.CreateList(productsList);
        }

        public List<ProductDto?> GetProductsByName(string name)
        {
            var productsList = _productRepository.GetProductByName(name);

            if (productsList == null)
                throw new NotFoundException(nameof(Product), name);

            return ProductDto.CreateList(productsList);
        }

        public ProductDto CreateNewProduct(ProductCreateRequest productCreateRequest)
        {
            var newProduct = new Product
            {
                Name = productCreateRequest.Name,
                Price = productCreateRequest.Price,
                Description = productCreateRequest.Description,
                Category = productCreateRequest.Category,
                ImageUrl = productCreateRequest.ImageUrl
            };

            return ProductDto.Create(_productRepository.Add(newProduct));
        }

        public void ModifyProductData(int id, ProductUpdateRequest productUpdateRequest)
        {
            var existingProduct = _productRepository.GetById(id);

            if (existingProduct == null)
                throw new NotFoundException(nameof(User), id);

            if (productUpdateRequest.Name != string.Empty) existingProduct.Name = productUpdateRequest.Name;

           if (productUpdateRequest.Price.HasValue)
                existingProduct.Price = productUpdateRequest.Price.Value;

            if (productUpdateRequest.Description != string.Empty) existingProduct.Description = productUpdateRequest.Description;

            if (productUpdateRequest.CategoryId.HasValue && productUpdateRequest.CategoryId != default(int))
            {
                
                var category = _categoryRepository.GetById(productUpdateRequest.CategoryId.Value);
                if (category != null)
                {
                    existingProduct.Category = category;
                }
            }

            if (productUpdateRequest.ImageUrl != string.Empty) existingProduct.ImageUrl = productUpdateRequest.ImageUrl;

            _productRepository.Update(existingProduct);
        }


        public void DeleteProduct(int id)
        {
            var product = _productRepository.GetById(id);

            if (product == null)
                throw new NotFoundException(nameof(Product), id);

            _productRepository.Delete(product);
        }

    }
}
