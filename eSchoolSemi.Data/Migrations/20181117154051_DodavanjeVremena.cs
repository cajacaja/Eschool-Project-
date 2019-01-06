using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace eSchoolSemi.Data.Migrations
{
    public partial class DodavanjeVremena : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "KrajCasa",
                table: "RasporedDetalj");

            migrationBuilder.DropColumn(
                name: "PocetakCasa",
                table: "RasporedDetalj");

            migrationBuilder.AddColumn<int>(
                name: "PocetakCasaID",
                table: "RasporedDetalj",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PocetakCasaID1",
                table: "RasporedDetalj",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PocetakCasa",
                columns: table => new
                {
                    PocetakCasaID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Pocetak = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PocetakCasa", x => x.PocetakCasaID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RasporedDetalj_PocetakCasaID",
                table: "RasporedDetalj",
                column: "PocetakCasaID");

            migrationBuilder.CreateIndex(
                name: "IX_RasporedDetalj_PocetakCasaID1",
                table: "RasporedDetalj",
                column: "PocetakCasaID1");

            migrationBuilder.AddForeignKey(
                name: "FK_RasporedDetalj_PocetakCasa_PocetakCasaID",
                table: "RasporedDetalj",
                column: "PocetakCasaID",
                principalTable: "PocetakCasa",
                principalColumn: "PocetakCasaID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RasporedDetalj_PocetakCasa_PocetakCasaID1",
                table: "RasporedDetalj",
                column: "PocetakCasaID1",
                principalTable: "PocetakCasa",
                principalColumn: "PocetakCasaID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RasporedDetalj_PocetakCasa_PocetakCasaID",
                table: "RasporedDetalj");

            migrationBuilder.DropForeignKey(
                name: "FK_RasporedDetalj_PocetakCasa_PocetakCasaID1",
                table: "RasporedDetalj");

            migrationBuilder.DropTable(
                name: "PocetakCasa");

            migrationBuilder.DropIndex(
                name: "IX_RasporedDetalj_PocetakCasaID",
                table: "RasporedDetalj");

            migrationBuilder.DropIndex(
                name: "IX_RasporedDetalj_PocetakCasaID1",
                table: "RasporedDetalj");

            migrationBuilder.DropColumn(
                name: "PocetakCasaID",
                table: "RasporedDetalj");

            migrationBuilder.DropColumn(
                name: "PocetakCasaID1",
                table: "RasporedDetalj");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "KrajCasa",
                table: "RasporedDetalj",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "PocetakCasa",
                table: "RasporedDetalj",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }
    }
}
