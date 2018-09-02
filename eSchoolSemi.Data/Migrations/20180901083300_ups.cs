using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace eSchoolSemi.Data.Migrations
{
    public partial class ups : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "KorisnickiNalogID",
                table: "_Korisnik",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX__Korisnik_KorisnickiNalogID",
                table: "_Korisnik",
                column: "KorisnickiNalogID");

            migrationBuilder.AddForeignKey(
                name: "FK__Korisnik_korisnickiNalogs_KorisnickiNalogID",
                table: "_Korisnik",
                column: "KorisnickiNalogID",
                principalTable: "korisnickiNalogs",
                principalColumn: "KorisnickiNalogID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Korisnik_korisnickiNalogs_KorisnickiNalogID",
                table: "_Korisnik");

            migrationBuilder.DropIndex(
                name: "IX__Korisnik_KorisnickiNalogID",
                table: "_Korisnik");

            migrationBuilder.DropColumn(
                name: "KorisnickiNalogID",
                table: "_Korisnik");
        }
    }
}
