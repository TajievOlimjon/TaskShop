using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistense.Data;

namespace Services.EntitiesServices.CustomerService
{
    public class CustomerService:ICustomerService
    {
        private readonly ApplicationContext _context;

        public CustomerService(ApplicationContext aplicationContext)
        {
            _context = aplicationContext;
        }

        public async Task<int> AddCustomer(Customer customer)
        {
            await _context.Customers.AddAsync(customer);
            var x = await _context.SaveChangesAsync();

            if (x == 0) return 0;
            return x;
        }

        public async Task<int> DeleteCustomer(int customerId)
        {
            var customer = await _context.Customers.FindAsync(customerId);
            if(customer == null) return 0;
            _context.Customers.Remove(customer);
            var x = await _context.SaveChangesAsync();

            if (x == 0) return 0;
            return x;
        }

        public async Task<Customer> GetCustomerByEmail(string email)
        {
            var customerEmail = await _context.Customers.FindAsync(email);
            if (customerEmail == null) return null;
            return customerEmail;
        }

        public async Task<List<Customer>> GetCustomers()
        {
            var items = await _context.Customers.ToListAsync();
            return items;
        }
    }
}
