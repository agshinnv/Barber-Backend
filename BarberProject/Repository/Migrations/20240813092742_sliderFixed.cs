using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    public partial class sliderFixed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SliderImages_Sliders_SliderId",
                table: "SliderImages");

            migrationBuilder.DropIndex(
                name: "IX_SliderImages_SliderId",
                table: "SliderImages");

            migrationBuilder.DropColumn(
                name: "SliderId",
                table: "SliderImages");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SliderId",
                table: "SliderImages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_SliderImages_SliderId",
                table: "SliderImages",
                column: "SliderId");

            migrationBuilder.AddForeignKey(
                name: "FK_SliderImages_Sliders_SliderId",
                table: "SliderImages",
                column: "SliderId",
                principalTable: "Sliders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
