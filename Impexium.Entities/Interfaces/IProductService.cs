using Impexium.Entities.Models;
using System.Collections.Generic;

namespace Impexium.Entities.Interfaces
{
    public interface IProductService
    {
        IEnumerable<Product> GetAllProducts();
    }
}
