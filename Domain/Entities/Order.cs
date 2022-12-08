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
        public DateTimeOffset OrderDate { get; set; }
        public int CartId { get; set; }
        public Cart? Cart { get; set; }
        public int CustomerId { get; set; }
        public Customer? Customer { get; set; }
    }
}
