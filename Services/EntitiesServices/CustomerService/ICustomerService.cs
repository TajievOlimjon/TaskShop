using Domain.Entities;

namespace Services.EntitiesServices.CustomerService
{
    public interface ICustomerService
    {
        Task<int> AddCustomer(Customer customer);
        Task<List<Customer>> GetCustomers();
        Task<Customer> GetCustomerByEmail(string email);
        Task<int> DeleteCustomer(int customerId);
    }
}
