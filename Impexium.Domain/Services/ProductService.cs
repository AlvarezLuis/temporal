using Impexium.Entities.Interfaces;
using Impexium.Entities.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Impexium.Domain.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task Add(Product product)
        {
            await _productRepository.Add(product);
        }

        public async Task AddRange(IEnumerable<Product> products)
        {
            await _productRepository.AddRange(products);
        }

        public async Task Update(Product product)
        {
            ValidateId(product);
            await _productRepository.UpdateAsync(product, product.Id);
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            return await _productRepository.GetAllProductsAsync();
        }

        public async Task Remove(Product product)
        {
            ValidateId(product);
            await _productRepository.Remove(product);
        }

        public async Task RemoveRange(IEnumerable<Product> products)
        {
            await _productRepository.RemoveRange(products);
        }

        private void ValidateId(Product product)
        {
            if (product.Id == default(int))
            {
                 throw new ArgumentException("Id cannot be null");
            }
        }
    }
}
