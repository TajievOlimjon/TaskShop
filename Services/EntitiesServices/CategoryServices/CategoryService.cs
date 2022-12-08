using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistense.Data;

namespace Services.EntitiesServices.CategoryServices
{
    public  class CategoryService:ICategoryService
    {
        private readonly ApplicationContext _context;

        public CategoryService(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<List<Category>> GetCategories()
        {
            var items = await _context.Categories.ToListAsync();

            return items;
            
        }

        public async Task<Category> GetCategoryById(int id)
        {
            var item = await _context.Categories.FindAsync(id);
            if (item == null) return null;
            return item;
        }
    }
}
