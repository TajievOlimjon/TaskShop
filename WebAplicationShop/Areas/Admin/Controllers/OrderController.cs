using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Services.EntitiesServices.OrderServices;

namespace WebAplicationShop.Areas.Admin.Controllers
{
    public class OrderController : BaseController
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
               _orderService = orderService;
        }
        [HttpGet]
        // GET: OrderController
        public async Task<IActionResult> Index()
        {
            var list = await _orderService.GetOrders();
            return View(list);
        }
        [HttpGet]
        public IActionResult AddToOrder()
        {
            return View(new Customer());
        }

        [HttpPost]
        public async Task<IActionResult> AddToOrder(Customer customer)
        {
            if (ModelState.IsValid)
            {
                await _orderService.AddToOrder(customer);

                return RedirectToAction(nameof(Result));
            }

            return View(customer);
        }
        [HttpPost]
        public IActionResult Result()
        {
            return View();
        }
    }
}
