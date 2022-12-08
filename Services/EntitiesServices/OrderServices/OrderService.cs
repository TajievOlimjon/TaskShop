
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistense.Data;
using Services.EntitiesServices.CartServices;
using Services.EntitiesServices.CustomerService;

namespace Services.EntitiesServices.OrderServices
{
    public class OrderService : IOrderService
    {
        private readonly ICustomerService _customerService;
        private readonly ApplicationContext _context;
        private readonly ICartService _cartService;

        public OrderService(ICustomerService service , ApplicationContext context,ICartService cartService)
        {
            _customerService=service;
            _context = context;
            _cartService = cartService;
        }

        public async Task<int> AddToOrder(Customer customer)
        {
            customer.OrderDate = DateTimeOffset.UtcNow;
            await _customerService.AddCustomer(customer);

            var entity = await _customerService.GetCustomerByEmail(customer.Email);

            var cartItems = await _cartService.GetAllCarts();

            foreach (var item in cartItems)
            {
                if (!item.Equals(null))
                {
                    var order = new Order
                    {
                        CustomerId = entity.Id,
                        CartId = item.Id,
                        OrderDate = DateTimeOffset.UtcNow
                    };
                    await _context.Orders.AddRangeAsync(order);
                }
            }
            var x = await _context.SaveChangesAsync();

            if (x != 0) return x;

            await _customerService.DeleteCustomer(entity.Id);
            return 0;
        }

        public async Task<List<Order>> GetOrders()
        {
            var orders = await _context.Orders
                        .Include(x => x.Cart)
                        .Include(c => c.Customer)
                        .ToListAsync();
           return orders;
        }
    }

}
