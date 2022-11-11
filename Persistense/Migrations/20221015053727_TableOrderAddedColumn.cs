using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistense.Migrations
{
    public partial class TableOrderAddedColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "SummaInstallmentRange",
                table: "Orders",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SummaInstallmentRange",
                table: "Orders");
        }
    }
}
