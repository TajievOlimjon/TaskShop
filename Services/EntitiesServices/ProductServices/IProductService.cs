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
        Task<List<Product>> GetProducts();
        Task<List<Product>> GetCategorySmartphones();
        Task<List<Product>> GetCategoryComputers();
        Task<List<Product>> GetCategoryTelevisions();
        Task<ProductDto> GetProductById(int id);
        Task<int> AddProduct(ProductDto product);
        Task<int> UpdateProduct(ProductDto product);
        Task<int> DeleteProduct(int id);
        IEnumerable<Product> GetProduct { get; }


    }
}
