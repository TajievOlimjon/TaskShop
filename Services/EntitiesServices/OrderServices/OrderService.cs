using AutoMapper;
using Domain.EntitesDto;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Persistense.Data;
using System.Security.Cryptography.X509Certificates;
using Vonage.Messaging;
namespace Services.EntitiesServices.OrderServices
{
    public class OrderService : IOrderService
    {
        private readonly IMapper mapper;
        private readonly AplicationContext context;

        public OrderService(IMapper mapper, AplicationContext context)
        {
            this.mapper = mapper;
            this.context = context;
        }




        public async Task<List<Orders>> GetOrders()
        {
            var query = await (from orders in context.Orders
                               join product in context.Products on orders.ProductId equals product.Id
                               join category in context.Categories on product.CategoryId equals category.Id
                               join customer in context.Customers on orders.CustomerId equals customer.Id
                               orderby orders.Id descending
                               select new Orders
                               {
                                   ProductName=orders.ProductName,
                                   CategoryName=category.Name,
                                   Price=orders.Price,
                                   SummaInstallmentRange=orders.SummaInstallmentRange,
                                   ProductRange=orders.ProductRange,
                                   OrderCreated=orders.OrderCreated,
                                   CustomerName=customer.FirstName,
                                   LastName=customer.LastName,
                                   Email=customer.Email,
                                   Phone=customer.PhoneNumber

                               }).ToListAsync();
            return query;
        }


        public SendSmsRequest AddSmartfonOrder(OrderDto dto)
        {
            Order order = GetSmartphoneByInstallments(dto);

            //var mapped = mapper.Map<Order>(order);
           
            context.Orders.Add(order);            
            context.SaveChanges(); 

            SendSmsRequest smsRequest = GetSendSmsRequestSmartfon(order);
            //SendSmsResponse smsResponse = client.SmsClient.SendAnSms(smsRequest);

           
            if (smsRequest == null)
            {
                return null;//new SendSmsResponse();
            }
            return smsRequest;

        }       
        public SendSmsRequest AddKomputerOrder(OrderDto dto)
        {
            OrderDto order= GetComputerByInstallments(dto);

            var mapped = mapper.Map<Order>(order);
            context.Orders.Add(mapped);
            context.SaveChanges();

            SendSmsRequest smsRequest = GetSendSmsRequestComputer(mapped);
            //SendSmsResponse smsResponse = client.SmsClient.SendAnSms(smsRequest);


            if (smsRequest == null)
            {
                return null;//new SendSmsResponse();
            }
            return smsRequest;
        }
        public SendSmsRequest AddTvOrder(OrderDto dto)
        {
            OrderDto order =GetTvByInstallments(dto);

            var mapped = mapper.Map<Order>(order);
            context.Orders.Add(mapped);
            context.SaveChanges();

            SendSmsRequest smsRequest = GetSendSmsRequestTv(mapped);
            //SendSmsResponse smsResponse = client.SmsClient.SendAnSms(smsRequest);


            if (smsRequest == null)
            {
                return null;//new SendSmsResponse();
            }
            return smsRequest;
        }

        public SendSmsRequest GetSendSmsRequestSmartfon(Order order)
        {
            SendSmsRequest? sendSmsRequest = null;
            try
            {
                if (order.ProductRange <= 9)
                {
                    SendSmsRequest? smsRequest = null;

                    if (order.ProductRange.Equals(3))
                    {
                        smsRequest = new SendSmsRequest
                        {
                            To = order.PhoneNumber,
                            From = " Alif ",
                            Text = $" Здравствуйте вы купили мобильный телефон {order.ProductName} " +
                                   $" с рассрочкой {order.ProductRange} месяцев . " +
                                   $" Вы дольжны платить каждый месяц {(Double)order.SummaInstallmentRange / 3} сомонов. " +
                                   $" Обшая сумма составляет {order.SummaInstallmentRange} сомон. " +
                                   $" С уважением Алиф Бонк   "
                        };

                    }
                    if (order.ProductRange.Equals(6))
                    {
                        smsRequest = new SendSmsRequest
                        {
                            To = order.PhoneNumber,
                            From = " Alif ",
                            Text = $" Здравствуйте вы купили мобильный телефон {order.ProductName} " +
                                $" с рассрочкой {order.ProductRange} месяцев . " +
                                $" Вы дольжны платить каждый месяц {(Double)order.SummaInstallmentRange / 6} сомонов. " +
                                $" Обшая сумма составляет {order.SummaInstallmentRange} сомон. " +
                                $" С уважением Алиф Бонк   "
                        };


                    }
                    if (order.ProductRange.Equals(9))
                    {
                        smsRequest = new SendSmsRequest
                        {
                            To = order.PhoneNumber,
                            From = " Alif ",
                            Text = $" Здравствуйте вы купили мобильный телефон {order.ProductName} " +
                                $" с рассрочкой {order.ProductRange} месяцев . " +
                                $" Вы дольжны платить каждый месяц {(Double)order.SummaInstallmentRange / 9} сомонов. " +
                                $" Обшая сумма составляет {order.SummaInstallmentRange} сомон. " +
                                $" С уважением Алиф Бонк   "
                        };


                    }

                    sendSmsRequest = smsRequest;
                };



                if (order.ProductRange > 9 && order.ProductRange <= 12)
                {
                    sendSmsRequest = new SendSmsRequest
                    {
                        To = order.PhoneNumber,
                        From = "Alif",
                        Text = $" Здравствуйте вы купили мобильный телефон {order.ProductName} " +
                                $" с рассрочкой {order.ProductRange} месяцев и вам добавляется 3 % от обшей суммы. " +
                                $" Вы дольжны платить каждый месяц {(Double)order.SummaInstallmentRange / 12} сомонов. " +
                                $" Обшая сумма составляет {order.SummaInstallmentRange} сомон. " +
                                $" С уважением Алиф Бонк   "
                    };
                }
                if (order.ProductRange > 12 && order.ProductRange <= 18)
                {
                    sendSmsRequest = new SendSmsRequest
                    {
                        To = order.PhoneNumber,
                        From = " Alif ",
                        Text = $" Здравствуйте вы купили мобильный телефон {order.ProductName} " +
                                $" с рассрочкой {order.ProductRange} месяцев и вам добавляется 6 % от обшей суммы. " +
                                $" Вы дольжны платить каждый месяц {(Double)order.SummaInstallmentRange / 18} сомонов. " +
                                $" Обшая сумма составляет {order.SummaInstallmentRange} сомон. " +
                                $" С уважением Алиф Бонк   "
                    };
                }
                if (order.ProductRange > 18 && order.ProductRange <= 24)
                {
                    sendSmsRequest = new SendSmsRequest
                    {
                        To = order.PhoneNumber,
                        From = " Alif ",
                        Text = $" Здравствуйте вы купили мобильный телефон {order.ProductName} " +
                                $" с рассрочкой {order.ProductRange} месяцев и вам добавляется 9 % от обшей суммы. " +
                                $" Вы дольжны платить каждый месяц {(Double)order.SummaInstallmentRange / 24} сомонов. " +
                                $" Обшая сумма составляет {order.SummaInstallmentRange} сомон. " +
                                $" С уважением Алиф Бонк   "
                    };
                }


            }

            catch (VonageSmsResponseException ex)
            {
                ex.Message.ToString();
            }

            if (sendSmsRequest == null) return new SendSmsRequest();
            return sendSmsRequest;
        }
        public SendSmsRequest GetSendSmsRequestComputer(Order order)
        {
            SendSmsRequest? sendSmsRequest = null;
            try
            {
                if (order.ProductRange <= 12)
                {
                    SendSmsRequest? smsRequest = null;
                    if (order.ProductRange.Equals(3))
                    {
                        smsRequest = new SendSmsRequest
                        {
                            To = order.PhoneNumber,
                            From = " Alif ",
                            Text = $" Здравствуйте вы купили компьютер  {order.ProductName} " +
                                   $" с рассрочкой {order.ProductRange} месяцев . " +
                                   $" Вы дольжны платить каждый месяц {(Double)order.SummaInstallmentRange / 3} сомонов. " +
                                   $" Обшая сумма составляет {order.SummaInstallmentRange} сомон. " +
                                   $" С уважением Алиф Бонк   "
                        };

                    }
                    if (order.ProductRange.Equals(6))
                    {
                        smsRequest = new SendSmsRequest
                        {
                            To = order.PhoneNumber,
                            From = " Alif ",
                            Text = $" Здравствуйте вы купили компьютер {order.ProductName} " +
                                   $" с рассрочкой {order.ProductRange} месяцев . " +
                                   $" Вы дольжны платить каждый месяц {(Double)order.SummaInstallmentRange / 6} сомонов. " +
                                   $" Обшая сумма составляет {order.SummaInstallmentRange} сомон. " +
                                   $" С уважением Алиф Бонк   "
                        };


                    }
                    if (order.ProductRange.Equals(9))
                    {
                        smsRequest = new SendSmsRequest
                        {
                            To = order.PhoneNumber,
                            From = " Alif ",
                            Text = $" Здравствуйте вы купили компьютер {order.ProductName} " +
                                $" с рассрочкой {order.ProductRange} месяцев . " +
                                $" Вы дольжны платить каждый месяц {(Double)order.SummaInstallmentRange / 9} сомонов. " +
                                $" Обшая сумма составляет {order.SummaInstallmentRange} сомон. " +
                                $" С уважением Алиф Бонк   "
                        };

                        if (order.ProductRange.Equals(9))
                        {
                            smsRequest = new SendSmsRequest
                            {
                                To = order.PhoneNumber,
                                From = "Alif",
                                Text = $" Здравствуйте вы купили компьютер {order.ProductName} " +
                                        $" с рассрочкой {order.ProductRange} месяцев . " +
                                        $" Вы дольжны платить каждый месяц {(Double)order.SummaInstallmentRange / 12} сомонов. " +
                                        $" Обшая сумма составляет {order.SummaInstallmentRange} сомон. " +
                                        $" С уважением Алиф Бонк   "
                            };
                        }
                    }
                    sendSmsRequest = smsRequest;
                };

                if (order.ProductRange > 12 && order.ProductRange <= 18)
                {
                    sendSmsRequest = new SendSmsRequest
                    {
                        To = order.PhoneNumber,
                        From = " Alif ",
                        Text = $" Здравствуйте вы купили компьютер {order.ProductName} " +
                                $" с рассрочкой {order.ProductRange} месяцев и вам добавляется 4 % от обшей суммы. " +
                                $" Вы дольжны платить каждый месяц {(Double)order.SummaInstallmentRange / 18} сомонов. " +
                                $" Обшая сумма составляет {order.SummaInstallmentRange} сомон. " +
                                $" С уважением Алиф Бонк   "
                    };
                }
                if (order.ProductRange > 18 && order.ProductRange <= 24)
                {
                    sendSmsRequest = new SendSmsRequest
                    {
                        To = order.PhoneNumber,
                        From = " Alif ",
                        Text = $" Здравствуйте вы купили компьютер {order.ProductName} " +
                                $" с рассрочкой {order.ProductRange} месяцев и вам добавляется 8 % от обшей суммы. " +
                                $" Вы дольжны платить каждый месяц {(Double)order.SummaInstallmentRange / 24} сомонов. " +
                                $" Обшая сумма составляет {order.SummaInstallmentRange} сомон. " +
                                $" С уважением Алиф Бонк   "
                    };
                }


            }

            catch (VonageSmsResponseException ex)
            {
                ex.Message.ToString();
            }

            if (sendSmsRequest == null) return new SendSmsRequest();
            return sendSmsRequest;
        }
        public SendSmsRequest GetSendSmsRequestTv(Order order)
        {
            SendSmsRequest? sendSmsRequest = null;
            try
            {
                if (order.ProductRange <= 12)
                {
                    SendSmsRequest? smsRequest = null;
                    if (order.ProductRange.Equals(3))
                    {
                        smsRequest = new SendSmsRequest
                        {
                            To = order.PhoneNumber,
                            From = " Alif ",
                            Text = $" Здравствуйте вы купили телевизор  {order.ProductName} " +
                                   $" с рассрочкой {order.ProductRange} месяцев . " +
                                   $" Вы дольжны платить каждый месяц {(Double)order.SummaInstallmentRange / 3} сомонов. " +
                                   $" Обшая сумма составляет {order.SummaInstallmentRange} сомон. " +
                                   $" С уважением Алиф Бонк   "
                        };

                    }
                    if (order.ProductRange.Equals(6))
                    {
                        smsRequest = new SendSmsRequest
                        {
                            To = order.PhoneNumber,
                            From = " Alif ",
                            Text = $" Здравствуйте вы купили телевизор {order.ProductName} " +
                                   $" с рассрочкой {order.ProductRange} месяцев . " +
                                   $" Вы дольжны платить каждый месяц {(Double)order.SummaInstallmentRange / 6} сомонов. " +
                                   $" Обшая сумма составляет {order.SummaInstallmentRange} сомон. " +
                                   $" С уважением Алиф Бонк   "
                        };


                    }
                    if (order.ProductRange.Equals(9))
                    {
                        smsRequest = new SendSmsRequest
                        {
                            To = order.PhoneNumber,
                            From = " Alif ",
                            Text = $" Здравствуйте вы купили телевизор {order.ProductName} " +
                                $" с рассрочкой {order.ProductRange} месяцев . " +
                                $" Вы дольжны платить каждый месяц {(Double)order.SummaInstallmentRange / 9} сомонов. " +
                                $" Обшая сумма составляет {order.SummaInstallmentRange} сомон. " +
                                $" С уважением Алиф Бонк   "
                        };

                        if (order.ProductRange.Equals(9))
                        {
                            smsRequest = new SendSmsRequest
                            {
                                To = order.PhoneNumber,
                                From = "Alif",
                                Text = $" Здравствуйте вы купили телевизор {order.ProductName} " +
                                        $" с рассрочкой {order.ProductRange} месяцев . " +
                                        $" Вы дольжны платить каждый месяц {(Double)order.SummaInstallmentRange / 12} сомонов. " +
                                        $" Обшая сумма составляет {order.SummaInstallmentRange} сомон. " +
                                        $" С уважением Алиф Бонк   "
                            };
                        }
                        if (order.ProductRange.Equals(18))
                        {

                            smsRequest = new SendSmsRequest
                            {
                                To = order.PhoneNumber,
                                From = " Alif ",
                                Text = $" Здравствуйте вы купили телевизор {order.ProductName} " +
                                        $" с рассрочкой {order.ProductRange} месяцев . " +
                                        $" Вы дольжны платить каждый месяц {(Double)order.SummaInstallmentRange / 18} сомонов. " +
                                        $" Обшая сумма составляет {order.SummaInstallmentRange} сомон. " +
                                        $" С уважением Алиф Бонк   "
                            };
                        }
                    }
                    sendSmsRequest = smsRequest;
                };


                if (order.ProductRange.Equals(24))
                {
                    sendSmsRequest = new SendSmsRequest
                    {
                        To = order.PhoneNumber,
                        From = " Alif ",
                        Text = $" Здравствуйте вы купили телевизор {order.ProductName} " +
                                $" с рассрочкой {order.ProductRange} месяцев и вам добавляется 5 процент от обшей суммы. " +
                                $" Вы дольжны платить каждый месяц {(Double)order.SummaInstallmentRange / 24} сомонов. " +
                                $" Обшая сумма составляет {order.SummaInstallmentRange} сомон. " +
                                $" С уважением Алиф Бонк   "
                    };
                }


            }

            catch (VonageSmsResponseException ex)
            {
                ex.Message.ToString();
            }

            if (sendSmsRequest == null) return new SendSmsRequest();
            return sendSmsRequest;
        }
        public Order GetSmartphoneByInstallments(OrderDto dto)
        {
            Order? query = null;

            if (dto.ProductRange <= 9)
            {
                query = (from o in context.Orders
                         where dto.ProductRange <= 9
                         let r = dto.Price
                         select new Order
                         {
                             ProductId = dto.ProductId,
                             ProductName = dto.ProductName,
                             CustomerId = dto.CustomerId,
                             Price = dto.Price,
                             PhoneNumber = dto.PhoneNumber,
                             ProductRange = dto.ProductRange,
                             OrderCreated = dto.OrderCreated,
                             SummaInstallmentRange = r
                         }).FirstOrDefault();
                
            }


            if (dto.ProductRange > 9 && dto.ProductRange <= 12)
            {
                query = (from o in context.Orders
                         where dto.ProductRange == 12
                         let range = (dto.Price * 3) / 100
                         let res = dto.Price + range
                         select new Order
                         {
                             ProductId = dto.ProductId,
                             ProductName = dto.ProductName,
                             CustomerId = dto.CustomerId,
                             Price = dto.Price,
                             PhoneNumber = dto.PhoneNumber,
                             ProductRange = dto.ProductRange,
                             OrderCreated = dto.OrderCreated,
                             SummaInstallmentRange = res
                         }).FirstOrDefault();
                //var res = 0;
                //if (dto.ProductRange.Equals(12))
                //{
                //    res = (dto.ProductRange * 3) / 100;
                //}
                //query = new Order
                //{
                //    ProductId = dto.ProductId,
                //    ProductName = dto.ProductName,
                //    CustomerId = dto.CustomerId,
                //    Price = dto.Price,
                //    PhoneNumber = dto.PhoneNumber,
                //    ProductRange = dto.ProductRange,
                //    OrderCreated = dto.OrderCreated,
                //    SummaInstallmentRange = res+dto.Price
                //};
            }
            if (dto.ProductRange > 12)
            {
                 query = (from o in context.Orders
                         where dto.ProductRange==18
                         let range = (dto.Price * 6) / 100
                         let r = dto.Price + range
                         select new Order
                         {
                             ProductId = dto.ProductId,
                             ProductName = dto.ProductName,
                             CustomerId = dto.CustomerId,
                             Price = dto.Price,
                             PhoneNumber = dto.PhoneNumber,
                             ProductRange = dto.ProductRange,
                             OrderCreated = dto.OrderCreated,
                             SummaInstallmentRange=r
                         }).FirstOrDefault();

            }
            if (dto.ProductRange > 18)
            {
                 query = (from o in context.Orders
                         where dto.ProductRange == 24
                         let range = (dto.Price * 9) / 100
                         let r = dto.Price + range
                         select new Order
                         {
                             ProductId = dto.ProductId,
                             ProductName = dto.ProductName,
                             CustomerId = dto.CustomerId,
                             Price = dto.Price,
                             PhoneNumber = dto.PhoneNumber,
                             ProductRange = dto.ProductRange,
                             OrderCreated = dto.OrderCreated,
                             SummaInstallmentRange = r
                         }).FirstOrDefault();

            }
        
            return query;
        }
        public OrderDto GetComputerByInstallments(OrderDto dto)
        {

            OrderDto? query = null;
            if (dto.ProductRange<=12)
            {

                query = (from o in context.Orders
                         where dto.ProductRange<=12
                         let r = dto.Price
                         select new OrderDto
                         {
                             ProductId = dto.ProductId,
                             ProductName = dto.ProductName,
                             CustomerId = dto.CustomerId,
                             Price = dto.Price,
                             PhoneNumber = dto.PhoneNumber,
                             ProductRange = dto.ProductRange,
                             OrderCreated = dto.OrderCreated,
                             SummaInstallmentRange = r
                         }).FirstOrDefault();


            }
            if (dto.ProductRange >12 )
            {

                      query= (from o in context.Orders
                              where  dto.ProductRange==18
                              let range = (dto.Price * 4) / 100
                              let r = dto.Price + range
                              select new OrderDto
                              {
                                  ProductId = dto.ProductId,
                                  ProductName = dto.ProductName,
                                  CustomerId = dto.CustomerId,
                                  Price = dto.Price,
                                  PhoneNumber = dto.PhoneNumber,
                                  ProductRange = dto.ProductRange,
                                  OrderCreated = dto.OrderCreated,
                                  SummaInstallmentRange = r
                              }).FirstOrDefault();
                

            }
            if (dto.ProductRange > 18)
            {
                query = (from o in context.Orders
                         where dto.ProductRange==24
                         let range = (dto.Price * 8) / 100
                         let r = dto.Price + range
                         select new OrderDto
                         {
                             ProductId = dto.ProductId,
                             ProductName = dto.ProductName,
                             CustomerId = dto.CustomerId,
                             Price = dto.Price,
                             PhoneNumber = dto.PhoneNumber,
                             ProductRange = dto.ProductRange,
                             OrderCreated = dto.OrderCreated,
                             SummaInstallmentRange = r
                         }).FirstOrDefault();
            }
            return query;
        }
        public OrderDto GetTvByInstallments(OrderDto dto)
        {
            OrderDto? query = null;
          
            if (dto.ProductRange<=18)
            {
                query = (from o in context.Orders
                         where dto.ProductRange<=18
                         let r = dto.Price
                         select new OrderDto
                         {
                             ProductId = dto.ProductId,
                             ProductName = dto.ProductName,
                             CustomerId = dto.CustomerId,
                             Price = dto.Price,
                             PhoneNumber = dto.PhoneNumber,
                             ProductRange = dto.ProductRange,
                             OrderCreated = dto.OrderCreated,
                             SummaInstallmentRange = r
                         }).FirstOrDefault();

            }
            if (dto.ProductRange>18) {
                 query = (from o in context.Orders
                          where dto.ProductRange==24
                          let range = (dto.Price * 5) / 100
                          let r = dto.Price + range
                          select new OrderDto
                          {
                              ProductId = dto.ProductId,
                              ProductName = dto.ProductName,
                              CustomerId = dto.CustomerId,
                              Price = dto.Price,
                              PhoneNumber = dto.PhoneNumber,
                              ProductRange = dto.ProductRange,
                              OrderCreated = dto.OrderCreated,
                              SummaInstallmentRange = r
                          }).FirstOrDefault();
            
            }
            return query;
        }

    }

      
}
