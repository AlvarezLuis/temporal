using Impexium.Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Impexium.Entities.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
    }
}
