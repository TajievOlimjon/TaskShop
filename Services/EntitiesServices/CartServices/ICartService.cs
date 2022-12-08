using Domain.Entities;

namespace Services.EntitiesServices.CartServices
{
    public  interface ICartService
    {
        string CartId { get; set; }
        Task<int> AddToCart(int id,int range);
        Task<List<Cart>> GetAllCarts();
    }
}
