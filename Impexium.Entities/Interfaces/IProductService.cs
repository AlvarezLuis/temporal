using Impexium.Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Impexium.Entities.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllProducts();
        Task AddRange(IEnumerable<Product> products);
        Task Add(Product product);
        Task Remove(Product product);
        Task RemoveRange(IEnumerable<Product> products);
        Task Update(Product product);
    }
}
