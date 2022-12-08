
using Domain.DTOs.ProductDTOs;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistense.Data;

namespace Services.EntitiesServices.ProductServices
{
    public class ProductService : IProductService
    {
        private readonly ApplicationContext _context;
        public ProductService(ApplicationContext context)
        {
            _context = context;
        }

        public IEnumerable<Product> GetProduct => throw new NotImplementedException();

        public async Task<int> AddProductAsync(Product product)
        {
            await _context.AddAsync(product);

            var x = await _context.SaveChangesAsync();

            if (x == 0) return 0;
            return x;
        }

        public async Task<int> DeleteProductAsync(int id)
        {
            var product =
                await _context.Products.FindAsync(id);

            if (product == null) return 0;

            _context.Products.Remove(product);

            var x = await _context.SaveChangesAsync();

            if (x == 0) return 0;
            return x;
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            var product = 
                await _context.Products.FindAsync(id);

            if (product == null) return null;

            return product;
        }

        public async Task<List<GetProductByJoinCategory>> GetProductsByJoinCategories()
        {
            var items =
                       await(from p in _context.Products
                       join c in _context.Categories on p.CategoryId equals c.Id
                       select new GetProductByJoinCategory
                       {
                           Id = p.Id,
                           Name = p.Name,
                           Price = p.Price,
                           Image=p.Image,
                           CategoryName = c.Name,
                           Description=c.Description
                       }).ToListAsync();
            return items;
        }

        public async Task<List<Product>> GetProductsAsync()
        {
            var products =
                         await _context.Products
                               .Include(x => x.Category)
                               .ToListAsync();

            return products;
        }


        public async Task<int> UpdateProductAsync(Product product)
        {
            var p = await _context.Products.FindAsync(product.Id);

            if (p == null) return 0;

            p.Name = product.Name;
            p.Price = product.Price;
            p.Image = product.Image;
            p.CategoryId = product.CategoryId;

            var x = await _context.SaveChangesAsync();

            if (x == 0) return 0;
            return x;
        }

        public async Task<Product> GetCategoryProductById(int id)
        {
            var item = await _context.Products
                             .Include(x => x.Category)
                             .Where(x => x.Id.Equals(id))
                             .FirstOrDefaultAsync();

            return item;
        }
    }
}
