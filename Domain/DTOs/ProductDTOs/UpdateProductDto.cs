using Microsoft.AspNetCore.Http;

namespace Domain.DTOs.ProductDTOs
{
    public class UpdateProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string Image { get; set; }
        public IFormFile? Img { get; set; }
        public int CategoryId { get; set; }
    }
}
