using AutoMapper;
using Domain.EntitesDto;
using Domain.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Persistense.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace Services.EntitiesServices.ProductServices
{
    public class ProductService : IProductService
    {
        private readonly AplicationContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductService(AplicationContext context,IMapper mapper,IWebHostEnvironment webHostEnvironment)
        {
            _mapper = mapper;
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public  IEnumerable<Product> GetProduct =>  _context.Products.Include(x=>x.Category).ToList();

        public async Task<int> AddProduct(ProductDto product)
        {  
            var fileName = Guid.NewGuid() + "_" + Path.GetFileName(product.Image.FileName);
            var path = Path.Combine(_webHostEnvironment.WebRootPath, "Images", fileName);
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await product.Image.CopyToAsync(stream);
            }

            var mapped = _mapper.Map<Product>(product);
            mapped.Image = fileName;
            await _context.Products.AddAsync(mapped);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
             _context.Products.Remove(product);
            return await _context.SaveChangesAsync();
        }

        public async Task<List<Product>> GetCategoryComputers()
        {
            var query = await (from p in _context.Products
                               join c in _context.Categories on p.CategoryId equals c.Id
                               where c.Name.Contains("Компьютер")
                               select p).ToListAsync();
            return query;
        }

        public async Task<List<Product>> GetCategorySmartphones()
        {
            var query = await(from p in _context.Products
                              join c in _context.Categories on p.CategoryId equals c.Id
                              where c.Name.Contains("Смартфон")
                              select p).ToListAsync();
            return query;
        }

        public async Task<List<Product>> GetCategoryTelevisions()
        {
            var query = await(from p in _context.Products
                              join c in _context.Categories on p.CategoryId equals c.Id
                              where c.Name.Contains("Телевизор")
                              select p).ToListAsync();
            return query;
        }

        public async Task<ProductDto> GetProductById(int id)
        {
            var product = await (from p in _context.Products
                                 select new ProductDto
                                 {
                                     Id=p.Id,
                                     Name=p.Name,
                                     Price=p.Price,
                                     GetImage=p.Image,
                                     CategoryId=p.CategoryId
                                 }).FirstOrDefaultAsync();
            return product;
        }

        public async Task<List<Product>> GetProducts()
        {
            return await _context.Products.Include(x=>x.Category).ToListAsync();
        }

        public async Task<int> UpdateProduct(ProductDto product)
        {
            if (product.Image != null)
            {
                var fileName = Guid.NewGuid() + "_" + Path.GetFileName(product.Image.FileName);
                var path = Path.Combine(_webHostEnvironment.WebRootPath, "Images", fileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await product.Image.CopyToAsync(stream);
                }
                var p = await _context.Products.FindAsync(product.Id);
                p.Name = product.Name;
                p.Price = product.Price;
                p.Image = fileName;
                p.CategoryId = product.CategoryId;
            }
            else
            {
                var p = await _context.Products.FindAsync(product.Id);
                p.Name = product.Name;
                p.Price = product.Price;
                p.CategoryId = product.CategoryId;
               

            }
            return await _context.SaveChangesAsync();
        }
    }
}
