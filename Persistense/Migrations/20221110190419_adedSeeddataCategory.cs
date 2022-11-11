using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistense.Migrations
{
    public partial class adedSeeddataCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Компьютер  устройство, которое выполняет логические операции и обработку данных, может использовать устройства ввода и вывода информации на дисплей и обычно включает в себя центральный процессор (CPU) для выполнения операций. ", "Компьютер" },
                    { 2, " мобильный телефон (современный — как правило, с сенсорным экраном), дополненный функциональностью умного устройства.", "Смартфон" },
                    { 3, " телевизионный приёмник (новолат. televisorium «дальновидец»; от др.-греч. τῆλε «далеко» + лат. vīsio «зрение; видение») — приёмник телевизионных сигналов изображения и звука, отображающий их на экране и с помощью динамиков.", "Телевизор" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
