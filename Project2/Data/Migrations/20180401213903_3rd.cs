using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Project2.Data.Migrations
{
    public partial class _3rd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Profiles_ProfileEmail",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_ProfileEmail",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "ProfileEmail",
                table: "Items");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProfileEmail",
                table: "Items",
                nullable: true);

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
    }
}
