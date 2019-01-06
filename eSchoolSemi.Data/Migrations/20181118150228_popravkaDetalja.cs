using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace eSchoolSemi.Data.Migrations
{
    public partial class popravkaDetalja : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RasporedDetalj_PocetakCasa_PocetakCasaID",
                table: "RasporedDetalj");

            migrationBuilder.DropForeignKey(
                name: "FK_RasporedDetalj_PocetakCasa_PocetakCasaID1",
                table: "RasporedDetalj");

            migrationBuilder.DropIndex(
                name: "IX_RasporedDetalj_PocetakCasaID1",
                table: "RasporedDetalj");

            migrationBuilder.DropColumn(
                name: "PocetakCasaID1",
                table: "RasporedDetalj");

            migrationBuilder.RenameColumn(
                name: "PocetakCasaID",
                table: "RasporedDetalj",
                newName: "PocetakCasaId");

            migrationBuilder.RenameIndex(
                name: "IX_RasporedDetalj_PocetakCasaID",
                table: "RasporedDetalj",
                newName: "IX_RasporedDetalj_PocetakCasaId");

            migrationBuilder.AlterColumn<int>(
                name: "PocetakCasaId",
                table: "RasporedDetalj",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_RasporedDetalj_PocetakCasa_PocetakCasaId",
                table: "RasporedDetalj",
                column: "PocetakCasaId",
                principalTable: "PocetakCasa",
                principalColumn: "PocetakCasaID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RasporedDetalj_PocetakCasa_PocetakCasaId",
                table: "RasporedDetalj");

            migrationBuilder.RenameColumn(
                name: "PocetakCasaId",
                table: "RasporedDetalj",
                newName: "PocetakCasaID");

            migrationBuilder.RenameIndex(
                name: "IX_RasporedDetalj_PocetakCasaId",
                table: "RasporedDetalj",
                newName: "IX_RasporedDetalj_PocetakCasaID");

            migrationBuilder.AlterColumn<int>(
                name: "PocetakCasaID",
                table: "RasporedDetalj",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "PocetakCasaID1",
                table: "RasporedDetalj",
                nullable: true);

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
    }
}
