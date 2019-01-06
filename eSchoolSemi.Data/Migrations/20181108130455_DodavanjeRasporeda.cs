using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace eSchoolSemi.Data.Migrations
{
    public partial class DodavanjeRasporeda : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Dan",
                columns: table => new
                {
                    DanID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Naziv = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dan", x => x.DanID);
                });

            migrationBuilder.CreateTable(
                name: "Raspored",
                columns: table => new
                {
                    RasporedID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Naziv = table.Column<string>(nullable: true),
                    OdjeljenjeID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Raspored", x => x.RasporedID);
                    table.ForeignKey(
                        name: "FK_Raspored__Odjeljenje_OdjeljenjeID",
                        column: x => x.OdjeljenjeID,
                        principalTable: "_Odjeljenje",
                        principalColumn: "OdjeljenjeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RasporedDetalj",
                columns: table => new
                {
                    RasporedDetaljID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DanID = table.Column<int>(nullable: false),
                    KrajCasa = table.Column<TimeSpan>(nullable: false),
                    PocetakCasa = table.Column<TimeSpan>(nullable: false),
                    PredmetID = table.Column<int>(nullable: false),
                    RasporedID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RasporedDetalj", x => x.RasporedDetaljID);
                    table.ForeignKey(
                        name: "FK_RasporedDetalj_Dan_DanID",
                        column: x => x.DanID,
                        principalTable: "Dan",
                        principalColumn: "DanID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RasporedDetalj__Predmet_PredmetID",
                        column: x => x.PredmetID,
                        principalTable: "_Predmet",
                        principalColumn: "PredmetId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RasporedDetalj_Raspored_RasporedID",
                        column: x => x.RasporedID,
                        principalTable: "Raspored",
                        principalColumn: "RasporedID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Raspored_OdjeljenjeID",
                table: "Raspored",
                column: "OdjeljenjeID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RasporedDetalj_DanID",
                table: "RasporedDetalj",
                column: "DanID");

            migrationBuilder.CreateIndex(
                name: "IX_RasporedDetalj_PredmetID",
                table: "RasporedDetalj",
                column: "PredmetID");

            migrationBuilder.CreateIndex(
                name: "IX_RasporedDetalj_RasporedID",
                table: "RasporedDetalj",
                column: "RasporedID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RasporedDetalj");

            migrationBuilder.DropTable(
                name: "Dan");

            migrationBuilder.DropTable(
                name: "Raspored");
        }
    }
}
