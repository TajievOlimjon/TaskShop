using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public  class Order
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }
        public double SummaInstallmentRange { get; set; }
        public int ProductRange { get; set; }
        public string PhoneNumber { get; set; }
        public DateTimeOffset OrderCreated { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }






    }
}
