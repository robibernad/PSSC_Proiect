using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class NewMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Uid",
                table: "Products",
                newName: "ProductId");

            migrationBuilder.RenameColumn(
                name: "ProductUid",
                table: "OrderLines",
                newName: "ProductId");

            migrationBuilder.RenameColumn(
                name: "OrderHeaderUid",
                table: "OrderLines",
                newName: "OrderHeaderId");

            migrationBuilder.RenameColumn(
                name: "Uid",
                table: "OrderLines",
                newName: "OrderLineId");

            migrationBuilder.RenameColumn(
                name: "Uid",
                table: "OrderHeaders",
                newName: "OrderHeaderId");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "OrderHeaders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ClientId",
                table: "OrderHeaders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Date",
                table: "OrderHeaders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<long>(
                name: "PhoneNumber",
                table: "OrderHeaders",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "OrderHeaders");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "OrderHeaders");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "OrderHeaders");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "OrderHeaders");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "Products",
                newName: "Uid");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "OrderLines",
                newName: "ProductUid");

            migrationBuilder.RenameColumn(
                name: "OrderHeaderId",
                table: "OrderLines",
                newName: "OrderHeaderUid");

            migrationBuilder.RenameColumn(
                name: "OrderLineId",
                table: "OrderLines",
                newName: "Uid");

            migrationBuilder.RenameColumn(
                name: "OrderHeaderId",
                table: "OrderHeaders",
                newName: "Uid");
        }
    }
}
