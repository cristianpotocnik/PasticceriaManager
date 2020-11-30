using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Pasticceria.Data.Migrations
{
    public partial class UpdateEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_DolceIngrediente",
                table: "DolceIngrediente");

            migrationBuilder.AddColumn<string>(
                name: "Value",
                table: "Ingredienti",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "DolceIngrediente",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<double>(
                name: "Quantity",
                table: "DolceIngrediente",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_DolceIngrediente",
                table: "DolceIngrediente",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_DolceIngrediente",
                table: "DolceIngrediente");

            migrationBuilder.DropColumn(
                name: "Value",
                table: "Ingredienti");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "DolceIngrediente");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "DolceIngrediente");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DolceIngrediente",
                table: "DolceIngrediente",
                column: "DolceId");
        }
    }
}
