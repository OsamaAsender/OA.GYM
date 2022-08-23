using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OA.GYM.Web.Data.Migrations
{
    public partial class AddedentitiestoCoach : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "TrainingClasses");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Trainees",
                newName: "FirstName");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Coaches",
                newName: "Profession");

            migrationBuilder.AddColumn<int>(
                name: "CoachingTitles",
                table: "Coaches",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "DOB",
                table: "Coaches",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Coaches",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Nationality",
                table: "Coaches",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Salary",
                table: "Coaches",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoachingTitles",
                table: "Coaches");

            migrationBuilder.DropColumn(
                name: "DOB",
                table: "Coaches");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Coaches");

            migrationBuilder.DropColumn(
                name: "Nationality",
                table: "Coaches");

            migrationBuilder.DropColumn(
                name: "Salary",
                table: "Coaches");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Trainees",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Profession",
                table: "Coaches",
                newName: "Name");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "TrainingClasses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
