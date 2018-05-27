using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace eSchoolSemi.Data.Migrations
{
    public partial class nesto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Odjeljenje__Korisnik_PredstavnikId",
                table: "_Odjeljenje");

            migrationBuilder.RenameColumn(
                name: "PredstavnikId",
                table: "_Odjeljenje",
                newName: "UcenikID");

            migrationBuilder.RenameIndex(
                name: "IX__Odjeljenje_PredstavnikId",
                table: "_Odjeljenje",
                newName: "IX__Odjeljenje_UcenikID");

            migrationBuilder.AlterColumn<string>(
                name: "Prezime",
                table: "_Korisnik",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Ime",
                table: "_Korisnik",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK__Odjeljenje__Korisnik_UcenikID",
                table: "_Odjeljenje",
                column: "UcenikID",
                principalTable: "_Korisnik",
                principalColumn: "KorisnikId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Odjeljenje__Korisnik_UcenikID",
                table: "_Odjeljenje");

            migrationBuilder.RenameColumn(
                name: "UcenikID",
                table: "_Odjeljenje",
                newName: "PredstavnikId");

            migrationBuilder.RenameIndex(
                name: "IX__Odjeljenje_UcenikID",
                table: "_Odjeljenje",
                newName: "IX__Odjeljenje_PredstavnikId");

            migrationBuilder.AlterColumn<string>(
                name: "Prezime",
                table: "_Korisnik",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Ime",
                table: "_Korisnik",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddForeignKey(
                name: "FK__Odjeljenje__Korisnik_PredstavnikId",
                table: "_Odjeljenje",
                column: "PredstavnikId",
                principalTable: "_Korisnik",
                principalColumn: "KorisnikId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
