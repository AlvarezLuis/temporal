using Impexium.Entities.Interfaces;
using Impexium.Entities.Models;
using Impexium.Persistence;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Impexium.Repositories.DataAcces
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext context) : base(context)
        {

        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _context.Products.ToList();
        }
    }
}
