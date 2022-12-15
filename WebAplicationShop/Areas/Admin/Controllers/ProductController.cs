using Domain.DTOs.ProductDTOs;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Services.EntitiesServices.CategoryServices;
using Services.EntitiesServices.FileServices;
using Services.EntitiesServices.ProductServices;

namespace WebAplicationShop.Areas.Admin.Controllers
{
    public class ProductController : BaseController
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IFileService _fileService;
        public ProductController(IProductService productService,ICategoryService categoryService,IFileService service)
        {
            _productService = productService;
            _categoryService = categoryService;
            _fileService = service;
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var products = await _productService.GetProductsByJoinCategories();
       
            return View(products);

        }

        [HttpGet]
        // GET: ProductController/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = await _categoryService.GetCategories();

            return View(new CreateProductDto());
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateProductDto  dto)
        {
            ViewBag.Categories = await _categoryService.GetCategories();

            try
            {
                if (ModelState.IsValid)
                {
                    var product = new Product
                    {
                        Name = dto.Name,
                        Price = dto.Price,
                        CategoryId = dto.CategoryId,
                        Image = _fileService.AddFile(dto.Image)
                    };
                    await _productService.AddProductAsync(product);
                    return RedirectToAction(nameof(Index));
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
            ViewBag.Categories= 
                await _categoryService.GetCategories();

            var p =
                await _productService.GetProductByIdAsync(id);

            var item = new UpdateProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                CategoryId = p.CategoryId,
                Image = p.Image
            };
            return View(item);
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateProductDto dto)
        {
            ViewBag.Categories = 
                await _categoryService.GetCategories();

            string imgPath="";

            if (dto.Img != null)
            {
                imgPath = _fileService.UpdateFile(dto.Img);
            }
            else
            {
                imgPath = dto.Image;
            }
            try
            {
                if (ModelState.IsValid)
                {
                    var product = new Product
                    {
                        Name = dto.Name,
                        Price = dto.Price,
                        CategoryId = dto.CategoryId,
                        Image = imgPath
                    };

                    await _productService.UpdateProductAsync(product);
                    return RedirectToAction(nameof(Index));
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
            var p = await _productService.DeleteProductAsync(id);

            return RedirectToAction(nameof(Index));
        }
      
    }
}
