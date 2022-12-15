using Microsoft.AspNetCore.Mvc;
using Services.EntitiesServices.CartServices;
using Services.EntitiesServices.ProductServices;

namespace WebAplicationShop.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly IProductService _productService;
        public CartController(ICartService service,IProductService productService)
        {
            _cartService = service;
            _productService = productService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var carts = await _cartService.GetAllCarts();
            return View(carts);
        }

        [HttpGet]
        public async Task<IActionResult> AddToCart(int id)
        {
            var item = await _productService.GetCategoryProductById(id);
            return View();
        }

        [HttpPost,ActionName("AddToCart")]
        public async Task<IActionResult> AddToCart(int id,int range)
        {
            if (id != 0)
            {
                await _cartService.AddToCart(id, range);

                return RedirectToAction(nameof(Index));
            }
            return View(id);
        }

    }
}
