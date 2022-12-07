using Domain.Entities;

namespace Services.EntitiesServices.CustomerService
{
    public interface ICustomerService
    {
        Task<int> AddCustomer(Customer customer);
        Customer GetCustomer(Customer customer);
    }
}
