using Domain.Entities;
using Vonage.Messaging;

namespace Services.EntitiesServices.OrderServices
{
    public interface IOrderService
    {
        Task<List<Order>> GetOrders();
        Task<int> AddToOrder(Customer customer);
    }
}
