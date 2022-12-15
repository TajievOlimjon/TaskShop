
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public  class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public string Image { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public  Category Category { get; set; }
    }
}
