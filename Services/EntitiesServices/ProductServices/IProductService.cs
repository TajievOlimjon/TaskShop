using Domain.DTOs.ProductDTOs;
using Domain.EntitesDto;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.EntitiesServices.ProductServices
{
    public  interface IProductService
    {
        Task<List<Product>> GetProductsAsync();
        Task<Product> GetProductByIdAsync(int id);
        Task<int> AddProductAsync(Product product);
        Task<int> UpdateProductAsync(Product product);
        Task<int> DeleteProductAsync(int id);
        IEnumerable<Product> GetProduct { get; }
        Task<List<GetProductByJoinCategory>> GetProductsByJoinCategories();
    }
}
