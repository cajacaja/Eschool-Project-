using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace eSchoolSemi.Data.Migrations
{
    public partial class MigracijaPonvo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Angazovan__Odjeljenje_OdjeljenjeId",
                table: "_Angazovan");

            migrationBuilder.DropForeignKey(
                name: "FK__OdrzanCas__Angazovan_AngazovanId",
                table: "_OdrzanCas");

            migrationBuilder.DropForeignKey(
                name: "FK__OdrzanCasDetalji__TipOcjene_TipOcjeneId",
                table: "_OdrzanCasDetalji");

            migrationBuilder.DropIndex(
                name: "IX__OdrzanCasDetalji_TipOcjeneId",
                table: "_OdrzanCasDetalji");

            migrationBuilder.DropIndex(
                name: "IX__OdrzanCas_AngazovanId",
                table: "_OdrzanCas");

            migrationBuilder.DropIndex(
                name: "IX__Angazovan_OdjeljenjeId",
                table: "_Angazovan");

            migrationBuilder.DropColumn(
                name: "TipOcjeneId",
                table: "_OdrzanCasDetalji");

            migrationBuilder.DropColumn(
                name: "AngazovanId",
                table: "_OdrzanCas");

            migrationBuilder.DropColumn(
                name: "Sifra",
                table: "_NastavniPlanPredmet");

            migrationBuilder.DropColumn(
                name: "OdjeljenjeId",
                table: "_Angazovan");

            migrationBuilder.RenameColumn(
                name: "BrojSati",
                table: "_NastavniPlanPredmet",
                newName: "BrojCasova");

            migrationBuilder.AlterColumn<int>(
                name: "Ocjena",
                table: "_OdrzanCasDetalji",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "NastavniPlanPredmetId",
                table: "_OdrzanCas",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Naziv",
                table: "_OdrzanCas",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OdjeljenjeID",
                table: "_OdrzanCas",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ZakljucnaOcjena",
                columns: table => new
                {
                    ZakljucnaOcjenaID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DatumZakljucivanja = table.Column<DateTime>(nullable: false),
                    Ocjena = table.Column<int>(nullable: false),
                    PredmetID = table.Column<int>(nullable: false),
                    UpisUOdjeljenjeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZakljucnaOcjena", x => x.ZakljucnaOcjenaID);
                    table.ForeignKey(
                        name: "FK_ZakljucnaOcjena__Predmet_PredmetID",
                        column: x => x.PredmetID,
                        principalTable: "_Predmet",
                        principalColumn: "PredmetId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ZakljucnaOcjena__UpisUOdjeljenje_UpisUOdjeljenjeId",
                        column: x => x.UpisUOdjeljenjeId,
                        principalTable: "_UpisUOdjeljenje",
                        principalColumn: "UpisUOdjeljenjeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX__OdrzanCas_NastavniPlanPredmetId",
                table: "_OdrzanCas",
                column: "NastavniPlanPredmetId");

            migrationBuilder.CreateIndex(
                name: "IX__OdrzanCas_OdjeljenjeID",
                table: "_OdrzanCas",
                column: "OdjeljenjeID");

            migrationBuilder.CreateIndex(
                name: "IX_ZakljucnaOcjena_PredmetID",
                table: "ZakljucnaOcjena",
                column: "PredmetID");

            migrationBuilder.CreateIndex(
                name: "IX_ZakljucnaOcjena_UpisUOdjeljenjeId",
                table: "ZakljucnaOcjena",
                column: "UpisUOdjeljenjeId");

            migrationBuilder.AddForeignKey(
                name: "FK__OdrzanCas__NastavniPlanPredmet_NastavniPlanPredmetId",
                table: "_OdrzanCas",
                column: "NastavniPlanPredmetId",
                principalTable: "_NastavniPlanPredmet",
                principalColumn: "NastavniPlanPredmetId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK__OdrzanCas__Odjeljenje_OdjeljenjeID",
                table: "_OdrzanCas",
                column: "OdjeljenjeID",
                principalTable: "_Odjeljenje",
                principalColumn: "OdjeljenjeId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__OdrzanCas__NastavniPlanPredmet_NastavniPlanPredmetId",
                table: "_OdrzanCas");

            migrationBuilder.DropForeignKey(
                name: "FK__OdrzanCas__Odjeljenje_OdjeljenjeID",
                table: "_OdrzanCas");

            migrationBuilder.DropTable(
                name: "ZakljucnaOcjena");

            migrationBuilder.DropIndex(
                name: "IX__OdrzanCas_NastavniPlanPredmetId",
                table: "_OdrzanCas");

            migrationBuilder.DropIndex(
                name: "IX__OdrzanCas_OdjeljenjeID",
                table: "_OdrzanCas");

            migrationBuilder.DropColumn(
                name: "NastavniPlanPredmetId",
                table: "_OdrzanCas");

            migrationBuilder.DropColumn(
                name: "Naziv",
                table: "_OdrzanCas");

            migrationBuilder.DropColumn(
                name: "OdjeljenjeID",
                table: "_OdrzanCas");

            migrationBuilder.RenameColumn(
                name: "BrojCasova",
                table: "_NastavniPlanPredmet",
                newName: "BrojSati");

            migrationBuilder.AlterColumn<int>(
                name: "Ocjena",
                table: "_OdrzanCasDetalji",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TipOcjeneId",
                table: "_OdrzanCasDetalji",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AngazovanId",
                table: "_OdrzanCas",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Sifra",
                table: "_NastavniPlanPredmet",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OdjeljenjeId",
                table: "_Angazovan",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX__OdrzanCasDetalji_TipOcjeneId",
                table: "_OdrzanCasDetalji",
                column: "TipOcjeneId");

            migrationBuilder.CreateIndex(
                name: "IX__OdrzanCas_AngazovanId",
                table: "_OdrzanCas",
                column: "AngazovanId");

            migrationBuilder.CreateIndex(
                name: "IX__Angazovan_OdjeljenjeId",
                table: "_Angazovan",
                column: "OdjeljenjeId");

            migrationBuilder.AddForeignKey(
                name: "FK__Angazovan__Odjeljenje_OdjeljenjeId",
                table: "_Angazovan",
                column: "OdjeljenjeId",
                principalTable: "_Odjeljenje",
                principalColumn: "OdjeljenjeId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK__OdrzanCas__Angazovan_AngazovanId",
                table: "_OdrzanCas",
                column: "AngazovanId",
                principalTable: "_Angazovan",
                principalColumn: "AngazovanId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK__OdrzanCasDetalji__TipOcjene_TipOcjeneId",
                table: "_OdrzanCasDetalji",
                column: "TipOcjeneId",
                principalTable: "_TipOcjene",
                principalColumn: "TipOcjeneId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
