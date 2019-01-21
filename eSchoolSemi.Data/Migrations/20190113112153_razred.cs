using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace eSchoolSemi.Data.Migrations
{
    public partial class razred : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RazredID",
                table: "_Odjeljenje",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Razred",
                columns: table => new
                {
                    RazredId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BrojRazreda = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Razred", x => x.RazredId);
                });

            migrationBuilder.CreateIndex(
                name: "IX__Odjeljenje_RazredID",
                table: "_Odjeljenje",
                column: "RazredID");

            migrationBuilder.AddForeignKey(
                name: "FK__Odjeljenje_Razred_RazredID",
                table: "_Odjeljenje",
                column: "RazredID",
                principalTable: "Razred",
                principalColumn: "RazredId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Odjeljenje_Razred_RazredID",
                table: "_Odjeljenje");

            migrationBuilder.DropTable(
                name: "Razred");

            migrationBuilder.DropIndex(
                name: "IX__Odjeljenje_RazredID",
                table: "_Odjeljenje");

            migrationBuilder.DropColumn(
                name: "RazredID",
                table: "_Odjeljenje");
        }
    }
}
