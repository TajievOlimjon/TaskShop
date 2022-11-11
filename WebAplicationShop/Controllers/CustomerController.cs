using Domain.EntitesDto;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Services.EntitiesServices.CustomerService;
using Services.EntitiesServices.OrderServices;
using Services.EntitiesServices.ProductServices;
using Vonage;
using Vonage.Messaging;
using Vonage.Request;

namespace WebAplicationShop.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;
        private readonly IProductService _productService;
        private readonly IOrderService _orderService;
        public CustomerController(ICustomerService customerService,IProductService productService,IOrderService orderService )
        {
            _customerService = customerService;
            _productService = productService;
            _orderService = orderService;
        }

        public const string v = "ce937fa3";
        public const string VonageApiSecret = "eBIA47occA6Ye4QD";
        private static  Credentials credentials = Credentials.FromApiKeyAndSecret(v, VonageApiSecret);
        public VonageClient client = new VonageClient(credentials);

        public IActionResult Create()
        {
            return View(new Customer());
        }
        [HttpPost]
        public async Task<IActionResult> Create(Customer customer, int Id)
        {
           
            if (ModelState.IsValid)
            {
                customer.Id =0;
                await _customerService.AddCustomer(customer);




                SendSmsRequest? sendSmsRequest=null;
                Product? product = null;

                product = _productService.GetProduct.Where(x => x.Id == Id).FirstOrDefault();
                var getId = _customerService.GetCustomer(customer);
                if (product.Equals(null))
                {
                    ModelState.AddModelError("", "В системе что пошло не так пожолуйста попробуйте позже ?");
                }
                if (product.Category.Name.Equals("Смартфон"))
                {
                    sendSmsRequest = _orderService.AddSmartfonOrder(new OrderDto
                    {
                        ProductId = product.Id,
                        ProductName = product.Name,
                        CustomerId = getId.Id,
                        Price = product.Price,
                        PhoneNumber = customer.PhoneNumber,
                        ProductRange = customer.InstallmentRange,
                        OrderCreated = DateTimeOffset.UtcNow
                    });
                }                
                if (product.Category.Name.Equals("Компьютер"))
                {
                    sendSmsRequest= _orderService.AddKomputerOrder(new OrderDto
                    {
                        ProductId = product.Id,
                        ProductName = product.Name,
                        CustomerId = c.Id,
                        Price = product.Price,
                        PhoneNumber = customer.PhoneNumber,
                        ProductRange = customer.InstallmentRange,
                        OrderCreated = DateTimeOffset.UtcNow
                    });
                }
                if (product.Category.Name.Equals("Телевизор"))
                {
                    sendSmsRequest= _orderService.AddTvOrder(new OrderDto
                    {
                        ProductId = product.Id,
                        ProductName = product.Name,
                        CustomerId = c.Id,
                        Price = product.Price,
                        PhoneNumber = customer.PhoneNumber,
                        ProductRange = customer.InstallmentRange,
                        OrderCreated = DateTimeOffset.UtcNow
                    });
                }


                if (sendSmsRequest != null)
                {
                    try
                    {
                        SendSmsResponse smsResponse = client.SmsClient.SendAnSms(sendSmsRequest);
                        ViewBag.MessageId = smsResponse.Messages[0].MessageId;
                    }
                    catch (VonageSmsResponseException ex)
                    {
                        ViewBag.Error = ex.Message;
                    }
                }


                
                return RedirectToAction("Complete");
           }
            return View(customer);
            

        }
      
        public IActionResult Complete()
        {
            return View();
        }

       
    }
}
