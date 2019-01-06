using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace eSchoolSemi.Data.Migrations
{
    public partial class Obavjesti : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Obavjestenje__NastavniPlanPredmet_NastavniPlanPredmetId",
                table: "_Obavjestenje");

            migrationBuilder.RenameColumn(
                name: "NastavniPlanPredmetId",
                table: "_Obavjestenje",
                newName: "NastavnikID");

            migrationBuilder.RenameIndex(
                name: "IX__Obavjestenje_NastavniPlanPredmetId",
                table: "_Obavjestenje",
                newName: "IX__Obavjestenje_NastavnikID");

            migrationBuilder.AddColumn<int>(
                name: "AdministatorID",
                table: "_Obavjestenje",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AdministratorKorisnikId",
                table: "_Obavjestenje",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Naslov",
                table: "_Obavjestenje",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX__Obavjestenje_AdministratorKorisnikId",
                table: "_Obavjestenje",
                column: "AdministratorKorisnikId");

            migrationBuilder.AddForeignKey(
                name: "FK__Obavjestenje__Korisnik_AdministratorKorisnikId",
                table: "_Obavjestenje",
                column: "AdministratorKorisnikId",
                principalTable: "_Korisnik",
                principalColumn: "KorisnikId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK__Obavjestenje__Korisnik_NastavnikID",
                table: "_Obavjestenje",
                column: "NastavnikID",
                principalTable: "_Korisnik",
                principalColumn: "KorisnikId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Obavjestenje__Korisnik_AdministratorKorisnikId",
                table: "_Obavjestenje");

            migrationBuilder.DropForeignKey(
                name: "FK__Obavjestenje__Korisnik_NastavnikID",
                table: "_Obavjestenje");

            migrationBuilder.DropIndex(
                name: "IX__Obavjestenje_AdministratorKorisnikId",
                table: "_Obavjestenje");

            migrationBuilder.DropColumn(
                name: "AdministatorID",
                table: "_Obavjestenje");

            migrationBuilder.DropColumn(
                name: "AdministratorKorisnikId",
                table: "_Obavjestenje");

            migrationBuilder.DropColumn(
                name: "Naslov",
                table: "_Obavjestenje");

            migrationBuilder.RenameColumn(
                name: "NastavnikID",
                table: "_Obavjestenje",
                newName: "NastavniPlanPredmetId");

            migrationBuilder.RenameIndex(
                name: "IX__Obavjestenje_NastavnikID",
                table: "_Obavjestenje",
                newName: "IX__Obavjestenje_NastavniPlanPredmetId");

            migrationBuilder.AddForeignKey(
                name: "FK__Obavjestenje__NastavniPlanPredmet_NastavniPlanPredmetId",
                table: "_Obavjestenje",
                column: "NastavniPlanPredmetId",
                principalTable: "_NastavniPlanPredmet",
                principalColumn: "NastavniPlanPredmetId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
