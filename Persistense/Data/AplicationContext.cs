using Domain;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistense.Data
{
    public  class AplicationContext:DbContext
    {
        public AplicationContext(DbContextOptions<AplicationContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            SeedCategory(modelBuilder);
            base.OnModelCreating(modelBuilder);
            
        }
        private  void SeedCategory(ModelBuilder builder)
        {
                builder.Entity<Category>().HasData(
                   new Category { Id = 1,
                       Name = "Компьютер",
                       Description= "Компьютер  устройство," +
                       " которое выполняет логические операции и обработку данных, " +
                       "может использовать устройства ввода и вывода информации" +
                       " на дисплей и обычно включает в себя центральный процессор " +
                       "(CPU) для выполнения операций. " 
                   },
                   new Category { Id = 2,
                       Name = "Смартфон" ,
                       Description= " мобильный телефон (современный — как правило, " +
                       "с сенсорным экраном), дополненный функциональностью умного устройства."
                   },
                   new Category { 
                       Id = 3,
                       Name = "Телевизор",
                       Description= " телевизионный приёмник (новолат. televisorium " +
                       "«дальновидец»; от др.-греч. τῆλε «далеко» + лат. vīsio «зрение;" +
                       " видение») — приёмник телевизионных сигналов изображения и звука, " +
                       "отображающий их на экране и с помощью динамиков."
                   }
                  

                );
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }


    }         
}
