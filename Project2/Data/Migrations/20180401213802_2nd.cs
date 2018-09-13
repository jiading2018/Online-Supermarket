using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Project2.Data.Migrations
{
    public partial class _2nd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProfileEmail",
                table: "Items",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Profiles",
                columns: table => new
                {
                    Email = table.Column<string>(nullable: false),
                    FirstName = table.Column<string>(maxLength: 50, nullable: false),
                    Last = table.Column<string>(maxLength: 50, nullable: false),
                    State = table.Column<string>(maxLength: 2, nullable: false),
                    Street = table.Column<string>(nullable: false),
                    Username = table.Column<string>(nullable: false),
                    Zip = table.Column<string>(maxLength: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profiles", x => x.Email);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Items_ProfileEmail",
                table: "Items",
                column: "ProfileEmail");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Profiles_ProfileEmail",
                table: "Items",
                column: "ProfileEmail",
                principalTable: "Profiles",
                principalColumn: "Email",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Profiles_ProfileEmail",
                table: "Items");

            migrationBuilder.DropTable(
                name: "Profiles");

            migrationBuilder.DropIndex(
                name: "IX_Items_ProfileEmail",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "ProfileEmail",
                table: "Items");
        }
    }
}
