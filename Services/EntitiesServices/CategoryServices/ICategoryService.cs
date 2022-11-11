using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.EntitiesServices.CategoryServices
{
    public  interface ICategoryService
    {
        Task<List<Category>> GetCategories();
        Task<Category> GetCategoryById(int id);
        
    }
}
