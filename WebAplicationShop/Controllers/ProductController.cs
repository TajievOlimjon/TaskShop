using Microsoft.AspNetCore.Mvc;
using Services.EntitiesServices.ProductServices;

namespace WebAplicationShop.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var products = await _productService.GetProductsByJoinCategories();
       
          /* var items =products.Join(_categoryService.GetCategories,
                                   product => product.CategoryId,
                                   category => category.Id,
                                   (product, category)
                                    => new GetProductsByJoinCategories
                                    {
                                        Id = product.Id,
                                        Name = product.Name,
                                        Price = product.Price,
                                        Image = product.Image,
                                        CategoryName = category.Name,
                                        Description = category.Description
                                    }).ToListAsync();*/
            return View(products);
        }
      
    }
}
