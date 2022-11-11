using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.EntitiesServices.OrderServices;

namespace WebAplicationShop.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
               _orderService = orderService;
        }
        // GET: OrderController
        public async Task<IActionResult> Index()
        {
            var list = await _orderService.GetOrders();
            return View(list);
        }

      
    }
}
