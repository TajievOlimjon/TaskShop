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
        public double SumInstallment { get; set; }
       /* public int Range { get; set; }*/
        public DateTimeOffset OrderDate { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }






    }
}
