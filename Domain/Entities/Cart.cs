using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [Column(TypeName = "decimal(18, 2)")]
        public decimal SumInstallment { get; set; }
        [Required]
        public string CartId { get; set; }
        public Product? Product { get; set; }
    }
}
