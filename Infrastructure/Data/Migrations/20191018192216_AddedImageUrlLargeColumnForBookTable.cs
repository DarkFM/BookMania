using Microsoft.EntityFrameworkCore.Migrations;

namespace BookMania.Infrastructure.Data.Migrations
{
    public partial class AddedImageUrlLargeColumnForBookTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrlLarge",
                table: "Books",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrlLarge",
                table: "Books");
        }
    }
}
