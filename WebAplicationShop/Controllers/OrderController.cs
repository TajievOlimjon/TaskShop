using Domain.Entities;
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
