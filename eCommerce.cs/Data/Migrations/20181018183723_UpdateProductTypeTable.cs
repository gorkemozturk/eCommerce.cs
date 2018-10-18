using Microsoft.EntityFrameworkCore.Migrations;

namespace eCommerce.cs.Data.Migrations
{
    public partial class UpdateProductTypeTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProductName",
                table: "ProductTypes",
                newName: "ProductTypeName");

            migrationBuilder.RenameColumn(
                name: "ProductID",
                table: "ProductTypes",
                newName: "ProductTypeID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProductTypeName",
                table: "ProductTypes",
                newName: "ProductName");

            migrationBuilder.RenameColumn(
                name: "ProductTypeID",
                table: "ProductTypes",
                newName: "ProductID");
        }
    }
}
