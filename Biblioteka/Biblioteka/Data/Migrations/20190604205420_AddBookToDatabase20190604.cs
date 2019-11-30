using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Biblioteka.Data.Migrations
{
    public partial class AddBookToDatabase20190604 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Comment",
                table: "Book");

            migrationBuilder.AlterColumn<int>(
                name: "YearPublic",
                table: "Book",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "Tom",
                table: "Book",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "Page",
                table: "Book",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfReading",
                table: "Book",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AddColumn<string>(
                name: "IdUser",
                table: "Book",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Book",
                maxLength: 2000,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdUser",
                table: "Book");

            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Book");

            migrationBuilder.AlterColumn<int>(
                name: "YearPublic",
                table: "Book",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Tom",
                table: "Book",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Page",
                table: "Book",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfReading",
                table: "Book",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Comment",
                table: "Book",
                maxLength: 1000,
                nullable: true);
        }
    }
}
