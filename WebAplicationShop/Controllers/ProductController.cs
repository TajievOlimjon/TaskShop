using Domain.EntitesDto;
using Microsoft.AspNetCore.Mvc;
using Services.EntitiesServices.CategoryServices;
using Services.EntitiesServices.ProductServices;

namespace WebAplicationShop.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public ProductController(IProductService productService,ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }
        [HttpGet]
        public async Task<ActionResult> Index()
        {   
            var list = await _productService.GetProducts();
            return View(list);
        }

        public async Task<ActionResult> GetAll()
        {
            var list = await _productService.GetProducts();
            return View(list);
        }





        // GET: ProductController/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = await _categoryService.GetCategories();
            return View(new ProductDto());
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductDto  dto)
        {
            ViewBag.Categories = await _categoryService.GetCategories();
            try
            {
                if (ModelState.IsValid)
                {
                    await _productService.AddProduct(dto);
                    return RedirectToAction(nameof(GetAll));
                }
                if (dto.GetImage == null)
                {
                    await _productService.AddProduct(dto);
                    return RedirectToAction(nameof(GetAll));
                }
                return View(dto);
            }
            catch(Exception ex)
            {
                return View(ex.Message);
            }
        }

        // GET: ProductController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.Categories= await _categoryService.GetCategories();
            var p = await _productService.GetProductById(id);
            return View(p);
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductDto dto)
        {
            ViewBag.Categories = await _categoryService.GetCategories();
            try
            {
                if (ModelState.IsValid)
                {
                    await _productService.UpdateProduct(dto);
                    return RedirectToAction(nameof(GetAll));
                }
                if(dto.Image==null&& dto.GetImage == null)
                {
                    await _productService.UpdateProduct(dto);
                    return RedirectToAction(nameof(GetAll));
                }
                return View(dto);
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }

        // GET: ProductController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var p = await _productService.DeleteProduct(id);
            return RedirectToAction("GetAll");
        }

      
    }
}
