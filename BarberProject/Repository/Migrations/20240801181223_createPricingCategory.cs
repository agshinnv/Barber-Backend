using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    public partial class createPricingCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PricingImages");

            migrationBuilder.DropColumn(
                name: "Closed",
                table: "WorkTimes");

            migrationBuilder.DropColumn(
                name: "BackgroundImage",
                table: "Appointments");

            migrationBuilder.AddColumn<int>(
                name: "PricingCategoryId",
                table: "BarberPricings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "PricingCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SoftDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PricingCategories", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BarberPricings_PricingCategoryId",
                table: "BarberPricings",
                column: "PricingCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_BarberPricings_PricingCategories_PricingCategoryId",
                table: "BarberPricings",
                column: "PricingCategoryId",
                principalTable: "PricingCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BarberPricings_PricingCategories_PricingCategoryId",
                table: "BarberPricings");

            migrationBuilder.DropTable(
                name: "PricingCategories");

            migrationBuilder.DropIndex(
                name: "IX_BarberPricings_PricingCategoryId",
                table: "BarberPricings");

            migrationBuilder.DropColumn(
                name: "PricingCategoryId",
                table: "BarberPricings");

            migrationBuilder.AddColumn<bool>(
                name: "Closed",
                table: "WorkTimes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "BackgroundImage",
                table: "Appointments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PricingImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SoftDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PricingImages", x => x.Id);
                });
        }
    }
}
