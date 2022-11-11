using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistense.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.EntitiesServices.CategoryServices
{
    public  class CategoryService:ICategoryService
    {
        private readonly AplicationContext _context;

        public CategoryService(AplicationContext context)
        {
            _context = context;
        }

        public async Task<List<Category>> GetCategories()
        {
            var list = await _context.Categories.ToListAsync();

            return list;
            
        }

        public Task<Category> GetCategoryById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
