using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistense.Migrations
{
    public partial class TableCustomerAddedColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InstallmentRange",
                table: "Customers",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InstallmentRange",
                table: "Customers");
        }
    }
}
