using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectGSO.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDataBaseContentAdd2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "PublishDateTime",
                table: "Content",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PublishDateTime",
                table: "Content");
        }
    }
}
