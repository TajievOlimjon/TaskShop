using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistense.Configurations.CategoryConfigurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            var categories = new List<Category>
            {
                new Category 
                {      
                    Id = 1,
                    Name = "Компьютер",
                    Description= "Компьютер  устройство," +
                    " которое выполняет логические операции и обработку данных, " +
                    "может использовать устройства ввода и вывода информации" +
                    " на дисплей и обычно включает в себя центральный процессор " +
                    "(CPU) для выполнения операций. "
                },
                new Category
                { 
                    Id = 2,
                    Name = "Смартфон" ,
                    Description= " мобильный телефон (современный — как правило, " +
                    "с сенсорным экраном), дополненный функциональностью умного устройства."
                },
                new Category
                {
                    Id = 3,
                    Name = "Телевизор",
                    Description= " телевизионный приёмник (новолат. televisorium " +
                    "«дальновидец»; от др.-греч. τῆλε «далеко» + лат. vīsio «зрение;" +
                    " видение») — приёмник телевизионных сигналов изображения и звука, " +
                    "отображающий их на экране и с помощью динамиков."
                }
            };
            builder.HasData(categories);
        }
    }
}
