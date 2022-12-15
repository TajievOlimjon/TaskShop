using Domain.DTOs.ProductDTOs;
using Domain.Entities;

namespace Services.EntitiesServices.ProductServices
{
    public  interface IProductService
    {
        Task<List<Product>> GetProductsAsync();
        Task<Product> GetProductByIdAsync(int id);
        Task<int> AddProductAsync(Product product);
        Task<int> UpdateProductAsync(Product product);
        Task<int> DeleteProductAsync(int id);
        Task<Product> GetCategoryProductById(int id);
        Task<List<GetProductsByJoinCategories>> GetProductsByJoinCategories();
    }
}
