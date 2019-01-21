using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace eSchoolSemi.Data.Migrations
{
    public partial class prebaciNazad : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Odjeljenje_Razred_RazredID",
                table: "_Odjeljenje");

            migrationBuilder.AlterColumn<int>(
                name: "RazredID",
                table: "_Odjeljenje",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK__Odjeljenje_Razred_RazredID",
                table: "_Odjeljenje",
                column: "RazredID",
                principalTable: "Razred",
                principalColumn: "RazredId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Odjeljenje_Razred_RazredID",
                table: "_Odjeljenje");

            migrationBuilder.AlterColumn<int>(
                name: "RazredID",
                table: "_Odjeljenje",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK__Odjeljenje_Razred_RazredID",
                table: "_Odjeljenje",
                column: "RazredID",
                principalTable: "Razred",
                principalColumn: "RazredId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
