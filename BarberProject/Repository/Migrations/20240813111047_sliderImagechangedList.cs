using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    public partial class sliderImagechangedList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "SliderImages");

            migrationBuilder.AddColumn<int>(
                name: "SliderImageId",
                table: "SliderImages",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SliderImages_SliderImageId",
                table: "SliderImages",
                column: "SliderImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_SliderImages_SliderImages_SliderImageId",
                table: "SliderImages",
                column: "SliderImageId",
                principalTable: "SliderImages",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SliderImages_SliderImages_SliderImageId",
                table: "SliderImages");

            migrationBuilder.DropIndex(
                name: "IX_SliderImages_SliderImageId",
                table: "SliderImages");

            migrationBuilder.DropColumn(
                name: "SliderImageId",
                table: "SliderImages");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "SliderImages",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
