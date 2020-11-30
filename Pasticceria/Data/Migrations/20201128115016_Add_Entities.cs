using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Pasticceria.Data.Migrations
{
    public partial class Add_Entities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DolceIngrediente",
                columns: table => new
                {
                    DolceId = table.Column<Guid>(nullable: false),
                    IngredienteId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DolceIngrediente", x => x.DolceId);
                });

            migrationBuilder.CreateTable(
                name: "Dolci",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    ProductionDate = table.Column<DateTime>(nullable: false),
                    UnitPrice = table.Column<double>(nullable: false),
                    IsValid = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dolci", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ingredienti",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredienti", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DolceIngrediente");

            migrationBuilder.DropTable(
                name: "Dolci");

            migrationBuilder.DropTable(
                name: "Ingredienti");
        }
    }
}
