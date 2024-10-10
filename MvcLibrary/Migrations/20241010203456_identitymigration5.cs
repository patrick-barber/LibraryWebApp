using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MvcLibrary.Migrations
{
    /// <inheritdoc />
    public partial class identitymigration5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DueDate",
                table: "Book",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UserCheckedOut",
                table: "Book",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DueDate",
                table: "Book");

            migrationBuilder.DropColumn(
                name: "UserCheckedOut",
                table: "Book");
        }
    }
}
