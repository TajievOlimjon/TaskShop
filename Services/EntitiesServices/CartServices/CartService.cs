using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistense.Data;
using Services.EntitiesServices.ProductServices;

namespace Services.EntitiesServices.CartServices
{
    public  class CartService : ICartService
    {
        private readonly ApplicationContext _context;
        private readonly IProductService _service;
        public CartService(ApplicationContext context,IProductService service)
        {
            _context=context;
            _service=service;
        }
        public CartService(ApplicationContext context)
        {
            _context = context;
        }

        public string CartId { get; set;}

        public static CartService GetShopCart(IServiceProvider service)
        {
            ISession? session =
               service.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            var context = service.GetService<ApplicationContext>();
            string CartId = session.GetString("cartId") ?? Guid.NewGuid().ToString();
            session.SetString("cartId", CartId);

            return new CartService(context) { CartId = CartId };
        }
        public async Task<List<Cart>> GetAllCarts()
        {
            var carts = await _context.Carts
                        .Where(x => x.CartId.Equals(CartId))
                        .Include(x=>x.Product)
                        .ToListAsync();
            return carts;
        }
        public async Task<int> AddToCart(int id,int range)
        {
            if (id == 0) return 0;

            var item = await _service.GetCategoryProductById(id);

            if (item == null) return 0;

            var sumInstalment =  GetSumInstalmentByRange(range,item);

            var cart = new Cart
            {
                CartId = CartId,
                ProductId = item.Id,
                Range = range,
                SumInstallment=(decimal)sumInstalment
            };

            await _context.Carts.AddAsync(cart);
            var x = await _context.SaveChangesAsync();

            if (x == 0) return 0;
            return x;
            
            
        }
        private double GetSumInstalmentByRange(int range,Product product)
        {
            double sumInstalment = 0;
            if (product.Category.Name.Equals("Смартфон"))  //Phone
            {
                sumInstalment =  GetPhoneSumInstalment(range,product.Price);
            }
            else if (product.Category.Name.Equals("Компьютер")) //Computer
            {
                sumInstalment =  GetComputerSumInstalment(range,product.Price);
            }
            else if (product.Category.Name.Equals("Телевизор")) //Tv
            {
                sumInstalment =  GetTvSumInstalment(range,product.Price);
            }

            return sumInstalment;
        }

        private double GetComputerSumInstalment(int range,double price)
        {
            double query = 0;
            if (range <= 12)
            {

                query = price;


            }
            else if (range > 12)
            {

                query = price + (price * 4) / 100;


            }
           else if (range > 18)
            {
                query =price + (price * 8) / 100;
            }
            return query;
        }

        private double GetPhoneSumInstalment(int range,double price)
        {
            double query = 0;

            if (range <= 9)
            {
                query = price;

            }
            else if (range > 9 && range <= 12)
            {
                query = price + (price * 3) / 100;

            }
            else if (range > 12)
            {
                query = query = price + (price * 6) / 100;
            }
            else if (range > 18)
            {
                query = price + (price * 9) / 100;
            }

            return  query;
        }

        private double GetTvSumInstalment(int range,double price)
        {
            double query = 0;

            if (range <= 18)
            {
                query = price;
            }
            if (range > 18)
            {

                query = price + (price * 5) / 100;

            }
            return query;
        }
    }
}
