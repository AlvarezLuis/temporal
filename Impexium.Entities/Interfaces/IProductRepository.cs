using Impexium.Entities.Models;
using System.Collections.Generic;

namespace Impexium.Entities.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        IEnumerable<Product> GetAllProducts();
    }
}
