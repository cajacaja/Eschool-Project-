using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace eSchoolSemi.Data.Migrations
{
    public partial class odbaciNUll : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__UpisUOdjeljenje__Odjeljenje_OdjeljenjeId",
                table: "_UpisUOdjeljenje");

            migrationBuilder.DropForeignKey(
                name: "FK__UpisUOdjeljenje__Korisnik_UcenikId",
                table: "_UpisUOdjeljenje");

            migrationBuilder.AlterColumn<int>(
                name: "UcenikId",
                table: "_UpisUOdjeljenje",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "OdjeljenjeId",
                table: "_UpisUOdjeljenje",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK__UpisUOdjeljenje__Odjeljenje_OdjeljenjeId",
                table: "_UpisUOdjeljenje",
                column: "OdjeljenjeId",
                principalTable: "_Odjeljenje",
                principalColumn: "OdjeljenjeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK__UpisUOdjeljenje__Korisnik_UcenikId",
                table: "_UpisUOdjeljenje",
                column: "UcenikId",
                principalTable: "_Korisnik",
                principalColumn: "KorisnikId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__UpisUOdjeljenje__Odjeljenje_OdjeljenjeId",
                table: "_UpisUOdjeljenje");

            migrationBuilder.DropForeignKey(
                name: "FK__UpisUOdjeljenje__Korisnik_UcenikId",
                table: "_UpisUOdjeljenje");

            migrationBuilder.AlterColumn<int>(
                name: "UcenikId",
                table: "_UpisUOdjeljenje",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "OdjeljenjeId",
                table: "_UpisUOdjeljenje",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK__UpisUOdjeljenje__Odjeljenje_OdjeljenjeId",
                table: "_UpisUOdjeljenje",
                column: "OdjeljenjeId",
                principalTable: "_Odjeljenje",
                principalColumn: "OdjeljenjeId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK__UpisUOdjeljenje__Korisnik_UcenikId",
                table: "_UpisUOdjeljenje",
                column: "UcenikId",
                principalTable: "_Korisnik",
                principalColumn: "KorisnikId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
