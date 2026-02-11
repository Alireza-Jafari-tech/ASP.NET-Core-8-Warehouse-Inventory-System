using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updatedSupplierModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_ItemsStatus_ItemStatusId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_QuantitiesStatus_QuantityStatusId",
                table: "Items");

            migrationBuilder.DropTable(
                name: "QuantitiesStatus");

            migrationBuilder.DropIndex(
                name: "IX_Items_ItemStatusId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "ItemStatusId",
                table: "Items");

            migrationBuilder.RenameColumn(
                name: "QuantityStatusId",
                table: "Items",
                newName: "StatusId");

            migrationBuilder.RenameIndex(
                name: "IX_Items_QuantityStatusId",
                table: "Items",
                newName: "IX_Items_StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_ItemsStatus_StatusId",
                table: "Items",
                column: "StatusId",
                principalTable: "ItemsStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_ItemsStatus_StatusId",
                table: "Items");

            migrationBuilder.RenameColumn(
                name: "StatusId",
                table: "Items",
                newName: "QuantityStatusId");

            migrationBuilder.RenameIndex(
                name: "IX_Items_StatusId",
                table: "Items",
                newName: "IX_Items_QuantityStatusId");

            migrationBuilder.AddColumn<int>(
                name: "ItemStatusId",
                table: "Items",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "QuantitiesStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CssClassName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuantitiesStatus", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Items_ItemStatusId",
                table: "Items",
                column: "ItemStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_ItemsStatus_ItemStatusId",
                table: "Items",
                column: "ItemStatusId",
                principalTable: "ItemsStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Items_QuantitiesStatus_QuantityStatusId",
                table: "Items",
                column: "QuantityStatusId",
                principalTable: "QuantitiesStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
