using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ModelBilletterSystem.Migrations
{
    /// <inheritdoc />
    public partial class ChangeDecimalDatatype : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Ticket_Price",
                table: "Tickets",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Events",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Events");

            migrationBuilder.AlterColumn<int>(
                name: "Ticket_Price",
                table: "Tickets",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");
        }
    }
}
