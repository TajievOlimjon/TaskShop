using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.EntitesDto
{
     public class ProductDto
     {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string? GetImage { get; set; }
        public IFormFile? Image { get; set; }
        public int CategoryId { get; set; }
     }
}
