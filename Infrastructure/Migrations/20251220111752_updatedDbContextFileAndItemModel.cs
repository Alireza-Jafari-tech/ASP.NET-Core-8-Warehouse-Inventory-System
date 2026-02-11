using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updatedDbContextFileAndItemModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemRelations_Items_ItemId1",
                table: "ItemRelations");

            migrationBuilder.DropIndex(
                name: "IX_ItemRelations_ItemId1",
                table: "ItemRelations");

            migrationBuilder.DropColumn(
                name: "ItemId1",
                table: "ItemRelations");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ItemId1",
                table: "ItemRelations",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ItemRelations_ItemId1",
                table: "ItemRelations",
                column: "ItemId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemRelations_Items_ItemId1",
                table: "ItemRelations",
                column: "ItemId1",
                principalTable: "Items",
                principalColumn: "Id");
        }
    }
}
