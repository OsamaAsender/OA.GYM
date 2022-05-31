using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OA.GYM.Web.Data.Migrations
{
    public partial class TrainingClass_Price : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "TrainingClasses",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "TrainingClasses");
        }
    }
}
