using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ModelBilletterSystem.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id_Category = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id_Category);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id_event = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Event_Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Event_Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Event_Time = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    categoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id_event);
                    table.ForeignKey(
                        name: "FK_Events_Categories_categoryId",
                        column: x => x.categoryId,
                        principalTable: "Categories",
                        principalColumn: "Id_Category",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    Id_Ticket = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ticket_Amount = table.Column<int>(type: "int", nullable: false),
                    Ticket_Price = table.Column<int>(type: "int", nullable: false),
                    Ability = table.Column<bool>(type: "bit", nullable: false),
                    is_used = table.Column<bool>(type: "bit", nullable: false),
                    eventId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.Id_Ticket);
                    table.ForeignKey(
                        name: "FK_Tickets_Events_eventId",
                        column: x => x.eventId,
                        principalTable: "Events",
                        principalColumn: "Id_event",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Events_categoryId",
                table: "Events",
                column: "categoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_eventId",
                table: "Tickets",
                column: "eventId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
