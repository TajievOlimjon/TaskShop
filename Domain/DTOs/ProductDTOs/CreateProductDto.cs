using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.ProductDTOs
{
    public  class CreateProductDto
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public IFormFile Image { get; set; }
        public int CategoryId { get; set; }
    }
}
