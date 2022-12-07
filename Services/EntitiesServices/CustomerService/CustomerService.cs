using Domain.Entities;
using Persistense.Data;

namespace Services.EntitiesServices.CustomerService
{
    public class CustomerService:ICustomerService
    {
        private readonly AplicationContext _context;

        public CustomerService(AplicationContext aplicationContext)
        {
            _context = aplicationContext;
        }

       

        public async Task<int> AddCustomer(Customer customer)
        {
            await _context.Customers.AddAsync(customer);
            return await _context.SaveChangesAsync();
        }

        public Customer GetCustomer(Customer customer)
        {
            return _context.Customers.Find(customer.Id);
        }
    }
}
