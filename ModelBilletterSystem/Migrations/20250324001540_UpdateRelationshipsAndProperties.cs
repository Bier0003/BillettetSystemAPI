using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ModelBilletterSystem.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRelationshipsAndProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Categories_categoryId",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_categoryId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "categoryId",
                table: "Events");

            migrationBuilder.RenameColumn(
                name: "Event_Time",
                table: "Events",
                newName: "Create_At");

            migrationBuilder.AddColumn<int>(
                name: "EventId",
                table: "Categories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_EventId",
                table: "Categories",
                column: "EventId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Events_EventId",
                table: "Categories",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id_event",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Events_EventId",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_EventId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "EventId",
                table: "Categories");

            migrationBuilder.RenameColumn(
                name: "Create_At",
                table: "Events",
                newName: "Event_Time");

            migrationBuilder.AddColumn<int>(
                name: "categoryId",
                table: "Events",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Events_categoryId",
                table: "Events",
                column: "categoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Categories_categoryId",
                table: "Events",
                column: "categoryId",
                principalTable: "Categories",
                principalColumn: "Id_Category",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
