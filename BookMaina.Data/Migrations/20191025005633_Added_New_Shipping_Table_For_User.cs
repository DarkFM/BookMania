using Microsoft.EntityFrameworkCore.Migrations;

namespace BookMania.Data.Migrations
{
    public partial class Added_New_Shipping_Table_For_User : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShippingAddress_AddressLine1",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ShippingAddress_AddressLine2",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ShippingAddress_AddressLine3",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ShippingAddress_Country",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ShippingAddress_CountryProvince",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ShippingAddress_ZipOrPostCode",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "UserAddress",
                columns: table => new
                {
                    ApplicationUserId = table.Column<int>(nullable: false),
                    Country = table.Column<string>(maxLength: 100, nullable: true),
                    CountryProvince = table.Column<string>(maxLength: 100, nullable: true),
                    ZipOrPostCode = table.Column<string>(maxLength: 15, nullable: true),
                    AddressLine1 = table.Column<string>(maxLength: 255, nullable: true),
                    AddressLine2 = table.Column<string>(maxLength: 255, nullable: true),
                    AddressLine3 = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAddress", x => x.ApplicationUserId);
                    table.ForeignKey(
                        name: "FK_UserAddress_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserAddress");

            migrationBuilder.AddColumn<string>(
                name: "ShippingAddress_AddressLine1",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShippingAddress_AddressLine2",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShippingAddress_AddressLine3",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShippingAddress_Country",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShippingAddress_CountryProvince",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShippingAddress_ZipOrPostCode",
                table: "AspNetUsers",
                nullable: true);
        }
    }
}
