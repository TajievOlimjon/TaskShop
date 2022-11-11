using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.EntitiesServices.CustomerService
{
    public interface ICustomerService
    {
        Task<int> AddCustomer(Customer customer);
        Customer GetCustomer(Customer customer);
    }
}
