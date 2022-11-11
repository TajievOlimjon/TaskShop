using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.EntitesDto
{
    public  class Orders
    {
       
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
        public double Price { get; set; }
        public double SummaInstallmentRange { get; set; }
        public int ProductRange { get; set; }
        public DateTimeOffset OrderCreated { get; set; }
        public string CustomerName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
