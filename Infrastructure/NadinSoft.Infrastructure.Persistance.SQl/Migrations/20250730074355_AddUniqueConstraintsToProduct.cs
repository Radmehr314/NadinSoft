using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NadinSoft.Infrastructure.Persistance.SQl.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueConstraintsToProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ManufactureEmail",
                table: "Products",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ManufactureEmail",
                table: "Products",
                column: "ManufactureEmail",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProducedDate",
                table: "Products",
                column: "ProducedDate",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Products_ManufactureEmail",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_ProducedDate",
                table: "Products");

            migrationBuilder.AlterColumn<string>(
                name: "ManufactureEmail",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
