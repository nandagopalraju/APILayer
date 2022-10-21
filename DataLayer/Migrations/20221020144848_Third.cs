using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    public partial class Third : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "image",
                table: "Employees");

            migrationBuilder.AddColumn<string>(
                name: "imagePath",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "imagePath",
                table: "Employees");

            migrationBuilder.AddColumn<byte[]>(
                name: "image",
                table: "Employees",
                type: "varbinary(max)",
                nullable: true);
        }
    }
}
