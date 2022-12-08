using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public  class Cart
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int Range { get; set; }
        [Required]
        public decimal SumInstallment { get; set; }
        [Required]
        public string CartId { get; set; }
        public Product? Product { get; set; }
    }
}
