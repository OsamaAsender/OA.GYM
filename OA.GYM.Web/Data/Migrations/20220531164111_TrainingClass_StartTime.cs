using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OA.GYM.Web.Data.Migrations
{
    public partial class TrainingClass_StartTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "StartTime",
                table: "TrainingClasses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "TrainingClasses");
        }
    }
}
