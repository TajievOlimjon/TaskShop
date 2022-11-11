using Domain.EntitesDto;
using Vonage.Messaging;

namespace Services.EntitiesServices.OrderServices
{
    public interface IOrderService
    {
        Task<List<Orders>> GetOrders();
        SendSmsRequest AddSmartfonOrder(OrderDto order);
        SendSmsRequest AddKomputerOrder(OrderDto dto);
        SendSmsRequest AddTvOrder(OrderDto dto);
    }
}
