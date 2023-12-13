using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KelCVBajaDivaManufaktur.Migrations
{
    /// <inheritdoc />
    public partial class InitialUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NamaProduk",
                table: "Produk",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NamaProduk",
                table: "Produk");
        }
    }
}
