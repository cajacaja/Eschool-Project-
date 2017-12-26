using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace eSchoolSemi.Data.Migrations
{
    public partial class eSchoolSemi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "_GodinaStudija",
                columns: table => new
                {
                    GodinaStudijaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Godina = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__GodinaStudija", x => x.GodinaStudijaId);
                });

            migrationBuilder.CreateTable(
                name: "_Grad",
                columns: table => new
                {
                    GradId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Naziv = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Grad", x => x.GradId);
                });

            migrationBuilder.CreateTable(
                name: "_NastavniPlan",
                columns: table => new
                {
                    NastavniPlanId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Naziv = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__NastavniPlan", x => x.NastavniPlanId);
                });

            migrationBuilder.CreateTable(
                name: "_Predmet",
                columns: table => new
                {
                    PredmetId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Naziv = table.Column<string>(nullable: true),
                    Oznaka = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Predmet", x => x.PredmetId);
                });

            migrationBuilder.CreateTable(
                name: "_SastanakTip",
                columns: table => new
                {
                    SastanakTipId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Naziv = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__SastanakTip", x => x.SastanakTipId);
                });

            migrationBuilder.CreateTable(
                name: "_TipObavijesti",
                columns: table => new
                {
                    TipObavijestiId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Tip = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TipObavijesti", x => x.TipObavijestiId);
                });

            migrationBuilder.CreateTable(
                name: "_Korisnik",
                columns: table => new
                {
                    KorisnikId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DatumRodenja = table.Column<DateTime>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    GradId = table.Column<int>(nullable: true),
                    Ime = table.Column<string>(nullable: true),
                    KorisnickoIme = table.Column<string>(nullable: true),
                    Lozinka = table.Column<string>(nullable: true),
                    Prezime = table.Column<string>(nullable: true),
                    Telefon = table.Column<string>(nullable: true),
                    DatumZaposlenja = table.Column<DateTime>(nullable: true),
                    Titula = table.Column<string>(nullable: true),
                    Zvanje = table.Column<string>(nullable: true),
                    RoditeljId = table.Column<int>(nullable: true),
                    Vladanje = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Korisnik", x => x.KorisnikId);
                    table.ForeignKey(
                        name: "FK__Korisnik__Grad_GradId",
                        column: x => x.GradId,
                        principalTable: "_Grad",
                        principalColumn: "GradId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__Korisnik__Korisnik_RoditeljId",
                        column: x => x.RoditeljId,
                        principalTable: "_Korisnik",
                        principalColumn: "KorisnikId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "_NastavniPlanPredmet",
                columns: table => new
                {
                    NastavniPlanPredmetId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BrojSati = table.Column<int>(nullable: false),
                    GodinaStudiranja = table.Column<string>(nullable: true),
                    NastavniPlanId = table.Column<int>(nullable: true),
                    PredmetId = table.Column<int>(nullable: true),
                    Sifra = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__NastavniPlanPredmet", x => x.NastavniPlanPredmetId);
                    table.ForeignKey(
                        name: "FK__NastavniPlanPredmet__NastavniPlan_NastavniPlanId",
                        column: x => x.NastavniPlanId,
                        principalTable: "_NastavniPlan",
                        principalColumn: "NastavniPlanId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__NastavniPlanPredmet__Predmet_PredmetId",
                        column: x => x.PredmetId,
                        principalTable: "_Predmet",
                        principalColumn: "PredmetId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "_Sastanak",
                columns: table => new
                {
                    SastanakId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Naziv = table.Column<string>(nullable: true),
                    SastanakTipId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Sastanak", x => x.SastanakId);
                    table.ForeignKey(
                        name: "FK__Sastanak__SastanakTip_SastanakTipId",
                        column: x => x.SastanakTipId,
                        principalTable: "_SastanakTip",
                        principalColumn: "SastanakTipId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "_Odjeljenje",
                columns: table => new
                {
                    OdjeljenjeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GodinaStudijaId = table.Column<int>(nullable: true),
                    Kapacitet = table.Column<int>(nullable: false),
                    NastavniPlanId = table.Column<int>(nullable: true),
                    Oznaka = table.Column<string>(nullable: true),
                    PredstavnikId = table.Column<int>(nullable: true),
                    RazrednikId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Odjeljenje", x => x.OdjeljenjeId);
                    table.ForeignKey(
                        name: "FK__Odjeljenje__GodinaStudija_GodinaStudijaId",
                        column: x => x.GodinaStudijaId,
                        principalTable: "_GodinaStudija",
                        principalColumn: "GodinaStudijaId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__Odjeljenje__NastavniPlan_NastavniPlanId",
                        column: x => x.NastavniPlanId,
                        principalTable: "_NastavniPlan",
                        principalColumn: "NastavniPlanId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__Odjeljenje__Korisnik_PredstavnikId",
                        column: x => x.PredstavnikId,
                        principalTable: "_Korisnik",
                        principalColumn: "KorisnikId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__Odjeljenje__Korisnik_RazrednikId",
                        column: x => x.RazrednikId,
                        principalTable: "_Korisnik",
                        principalColumn: "KorisnikId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "_Materijal",
                columns: table => new
                {
                    MaterijalId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DatumObjave = table.Column<DateTime>(nullable: false),
                    NastavniPlanPredmetId = table.Column<int>(nullable: true),
                    NastavnikId = table.Column<int>(nullable: true),
                    Naziv = table.Column<string>(nullable: true),
                    Sadrzaj = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Materijal", x => x.MaterijalId);
                    table.ForeignKey(
                        name: "FK__Materijal__NastavniPlanPredmet_NastavniPlanPredmetId",
                        column: x => x.NastavniPlanPredmetId,
                        principalTable: "_NastavniPlanPredmet",
                        principalColumn: "NastavniPlanPredmetId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__Materijal__Korisnik_NastavnikId",
                        column: x => x.NastavnikId,
                        principalTable: "_Korisnik",
                        principalColumn: "KorisnikId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "_Obavjestenje",
                columns: table => new
                {
                    ObavjestenjeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DatumObavjestenja = table.Column<DateTime>(nullable: false),
                    NastavniPlanPredmetId = table.Column<int>(nullable: true),
                    Sadrzaj = table.Column<string>(nullable: true),
                    TipObavijestiId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Obavjestenje", x => x.ObavjestenjeId);
                    table.ForeignKey(
                        name: "FK__Obavjestenje__NastavniPlanPredmet_NastavniPlanPredmetId",
                        column: x => x.NastavniPlanPredmetId,
                        principalTable: "_NastavniPlanPredmet",
                        principalColumn: "NastavniPlanPredmetId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__Obavjestenje__TipObavijesti_TipObavijestiId",
                        column: x => x.TipObavijestiId,
                        principalTable: "_TipObavijesti",
                        principalColumn: "TipObavijestiId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "_SastanakRoditelj",
                columns: table => new
                {
                    SastanakRoditeljId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DatumSastanka = table.Column<DateTime>(nullable: false),
                    Komentar = table.Column<string>(nullable: true),
                    NastavnikId = table.Column<int>(nullable: true),
                    RoditeljId = table.Column<int>(nullable: true),
                    SastanakId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__SastanakRoditelj", x => x.SastanakRoditeljId);
                    table.ForeignKey(
                        name: "FK__SastanakRoditelj__Korisnik_NastavnikId",
                        column: x => x.NastavnikId,
                        principalTable: "_Korisnik",
                        principalColumn: "KorisnikId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__SastanakRoditelj__Korisnik_RoditeljId",
                        column: x => x.RoditeljId,
                        principalTable: "_Korisnik",
                        principalColumn: "KorisnikId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__SastanakRoditelj__Sastanak_SastanakId",
                        column: x => x.SastanakId,
                        principalTable: "_Sastanak",
                        principalColumn: "SastanakId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "_Angazovan",
                columns: table => new
                {
                    AngazovanId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NastavniPlanPredmetId = table.Column<int>(nullable: true),
                    NastavnikId = table.Column<int>(nullable: true),
                    OdjeljenjeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Angazovan", x => x.AngazovanId);
                    table.ForeignKey(
                        name: "FK__Angazovan__NastavniPlanPredmet_NastavniPlanPredmetId",
                        column: x => x.NastavniPlanPredmetId,
                        principalTable: "_NastavniPlanPredmet",
                        principalColumn: "NastavniPlanPredmetId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__Angazovan__Korisnik_NastavnikId",
                        column: x => x.NastavnikId,
                        principalTable: "_Korisnik",
                        principalColumn: "KorisnikId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__Angazovan__Odjeljenje_OdjeljenjeId",
                        column: x => x.OdjeljenjeId,
                        principalTable: "_Odjeljenje",
                        principalColumn: "OdjeljenjeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "_UpisUOdjeljenje",
                columns: table => new
                {
                    UpisUOdjeljenjeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BrojUDnevniku = table.Column<int>(nullable: false),
                    OdjeljenjeId = table.Column<int>(nullable: true),
                    UcenikId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__UpisUOdjeljenje", x => x.UpisUOdjeljenjeId);
                    table.ForeignKey(
                        name: "FK__UpisUOdjeljenje__Odjeljenje_OdjeljenjeId",
                        column: x => x.OdjeljenjeId,
                        principalTable: "_Odjeljenje",
                        principalColumn: "OdjeljenjeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__UpisUOdjeljenje__Korisnik_UcenikId",
                        column: x => x.UcenikId,
                        principalTable: "_Korisnik",
                        principalColumn: "KorisnikId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "_OdrzanCas",
                columns: table => new
                {
                    OdrzanCasId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AngazovanId = table.Column<int>(nullable: true),
                    DatumOdrzavanja = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__OdrzanCas", x => x.OdrzanCasId);
                    table.ForeignKey(
                        name: "FK__OdrzanCas__Angazovan_AngazovanId",
                        column: x => x.AngazovanId,
                        principalTable: "_Angazovan",
                        principalColumn: "AngazovanId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "_UcenikPredmet",
                columns: table => new
                {
                    UcenikPredmetId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DatumOcjenjivanja = table.Column<DateTime>(nullable: false),
                    Napomena = table.Column<string>(nullable: true),
                    NastavniPlanPredmetId = table.Column<int>(nullable: true),
                    OpisOcjene = table.Column<string>(nullable: true),
                    UpisUOdjeljenjeId = table.Column<int>(nullable: true),
                    ZakljucniaOcjena = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__UcenikPredmet", x => x.UcenikPredmetId);
                    table.ForeignKey(
                        name: "FK__UcenikPredmet__NastavniPlanPredmet_NastavniPlanPredmetId",
                        column: x => x.NastavniPlanPredmetId,
                        principalTable: "_NastavniPlanPredmet",
                        principalColumn: "NastavniPlanPredmetId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__UcenikPredmet__UpisUOdjeljenje_UpisUOdjeljenjeId",
                        column: x => x.UpisUOdjeljenjeId,
                        principalTable: "_UpisUOdjeljenje",
                        principalColumn: "UpisUOdjeljenjeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "_OdrzanCasDetalji",
                columns: table => new
                {
                    OdrzanCasDetaljiId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Ocjena = table.Column<int>(nullable: false),
                    OdrzanCasId = table.Column<int>(nullable: true),
                    Odsutan = table.Column<bool>(nullable: false),
                    Opravdano = table.Column<bool>(nullable: false),
                    UpisUOdjeljenjeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__OdrzanCasDetalji", x => x.OdrzanCasDetaljiId);
                    table.ForeignKey(
                        name: "FK__OdrzanCasDetalji__OdrzanCas_OdrzanCasId",
                        column: x => x.OdrzanCasId,
                        principalTable: "_OdrzanCas",
                        principalColumn: "OdrzanCasId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__OdrzanCasDetalji__UpisUOdjeljenje_UpisUOdjeljenjeId",
                        column: x => x.UpisUOdjeljenjeId,
                        principalTable: "_UpisUOdjeljenje",
                        principalColumn: "UpisUOdjeljenjeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "_ZakljucnaOcjena",
                columns: table => new
                {
                    ZakljucnaOcjenaid = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DadumZakljucivanja = table.Column<DateTime>(nullable: false),
                    OdrzanCasDetaljiId = table.Column<int>(nullable: true),
                    UcenikPredmetId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ZakljucnaOcjena", x => x.ZakljucnaOcjenaid);
                    table.ForeignKey(
                        name: "FK__ZakljucnaOcjena__OdrzanCasDetalji_OdrzanCasDetaljiId",
                        column: x => x.OdrzanCasDetaljiId,
                        principalTable: "_OdrzanCasDetalji",
                        principalColumn: "OdrzanCasDetaljiId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__ZakljucnaOcjena__UcenikPredmet_UcenikPredmetId",
                        column: x => x.UcenikPredmetId,
                        principalTable: "_UcenikPredmet",
                        principalColumn: "UcenikPredmetId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX__Angazovan_NastavniPlanPredmetId",
                table: "_Angazovan",
                column: "NastavniPlanPredmetId");

            migrationBuilder.CreateIndex(
                name: "IX__Angazovan_NastavnikId",
                table: "_Angazovan",
                column: "NastavnikId");

            migrationBuilder.CreateIndex(
                name: "IX__Angazovan_OdjeljenjeId",
                table: "_Angazovan",
                column: "OdjeljenjeId");

            migrationBuilder.CreateIndex(
                name: "IX__Korisnik_GradId",
                table: "_Korisnik",
                column: "GradId");

            migrationBuilder.CreateIndex(
                name: "IX__Korisnik_RoditeljId",
                table: "_Korisnik",
                column: "RoditeljId");

            migrationBuilder.CreateIndex(
                name: "IX__Materijal_NastavniPlanPredmetId",
                table: "_Materijal",
                column: "NastavniPlanPredmetId");

            migrationBuilder.CreateIndex(
                name: "IX__Materijal_NastavnikId",
                table: "_Materijal",
                column: "NastavnikId");

            migrationBuilder.CreateIndex(
                name: "IX__NastavniPlanPredmet_NastavniPlanId",
                table: "_NastavniPlanPredmet",
                column: "NastavniPlanId");

            migrationBuilder.CreateIndex(
                name: "IX__NastavniPlanPredmet_PredmetId",
                table: "_NastavniPlanPredmet",
                column: "PredmetId");

            migrationBuilder.CreateIndex(
                name: "IX__Obavjestenje_NastavniPlanPredmetId",
                table: "_Obavjestenje",
                column: "NastavniPlanPredmetId");

            migrationBuilder.CreateIndex(
                name: "IX__Obavjestenje_TipObavijestiId",
                table: "_Obavjestenje",
                column: "TipObavijestiId");

            migrationBuilder.CreateIndex(
                name: "IX__Odjeljenje_GodinaStudijaId",
                table: "_Odjeljenje",
                column: "GodinaStudijaId");

            migrationBuilder.CreateIndex(
                name: "IX__Odjeljenje_NastavniPlanId",
                table: "_Odjeljenje",
                column: "NastavniPlanId");

            migrationBuilder.CreateIndex(
                name: "IX__Odjeljenje_PredstavnikId",
                table: "_Odjeljenje",
                column: "PredstavnikId");

            migrationBuilder.CreateIndex(
                name: "IX__Odjeljenje_RazrednikId",
                table: "_Odjeljenje",
                column: "RazrednikId");

            migrationBuilder.CreateIndex(
                name: "IX__OdrzanCas_AngazovanId",
                table: "_OdrzanCas",
                column: "AngazovanId");

            migrationBuilder.CreateIndex(
                name: "IX__OdrzanCasDetalji_OdrzanCasId",
                table: "_OdrzanCasDetalji",
                column: "OdrzanCasId");

            migrationBuilder.CreateIndex(
                name: "IX__OdrzanCasDetalji_UpisUOdjeljenjeId",
                table: "_OdrzanCasDetalji",
                column: "UpisUOdjeljenjeId");

            migrationBuilder.CreateIndex(
                name: "IX__Sastanak_SastanakTipId",
                table: "_Sastanak",
                column: "SastanakTipId");

            migrationBuilder.CreateIndex(
                name: "IX__SastanakRoditelj_NastavnikId",
                table: "_SastanakRoditelj",
                column: "NastavnikId");

            migrationBuilder.CreateIndex(
                name: "IX__SastanakRoditelj_RoditeljId",
                table: "_SastanakRoditelj",
                column: "RoditeljId");

            migrationBuilder.CreateIndex(
                name: "IX__SastanakRoditelj_SastanakId",
                table: "_SastanakRoditelj",
                column: "SastanakId");

            migrationBuilder.CreateIndex(
                name: "IX__UcenikPredmet_NastavniPlanPredmetId",
                table: "_UcenikPredmet",
                column: "NastavniPlanPredmetId");

            migrationBuilder.CreateIndex(
                name: "IX__UcenikPredmet_UpisUOdjeljenjeId",
                table: "_UcenikPredmet",
                column: "UpisUOdjeljenjeId");

            migrationBuilder.CreateIndex(
                name: "IX__UpisUOdjeljenje_OdjeljenjeId",
                table: "_UpisUOdjeljenje",
                column: "OdjeljenjeId");

            migrationBuilder.CreateIndex(
                name: "IX__UpisUOdjeljenje_UcenikId",
                table: "_UpisUOdjeljenje",
                column: "UcenikId");

            migrationBuilder.CreateIndex(
                name: "IX__ZakljucnaOcjena_OdrzanCasDetaljiId",
                table: "_ZakljucnaOcjena",
                column: "OdrzanCasDetaljiId");

            migrationBuilder.CreateIndex(
                name: "IX__ZakljucnaOcjena_UcenikPredmetId",
                table: "_ZakljucnaOcjena",
                column: "UcenikPredmetId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "_Materijal");

            migrationBuilder.DropTable(
                name: "_Obavjestenje");

            migrationBuilder.DropTable(
                name: "_SastanakRoditelj");

            migrationBuilder.DropTable(
                name: "_ZakljucnaOcjena");

            migrationBuilder.DropTable(
                name: "_TipObavijesti");

            migrationBuilder.DropTable(
                name: "_Sastanak");

            migrationBuilder.DropTable(
                name: "_OdrzanCasDetalji");

            migrationBuilder.DropTable(
                name: "_UcenikPredmet");

            migrationBuilder.DropTable(
                name: "_SastanakTip");

            migrationBuilder.DropTable(
                name: "_OdrzanCas");

            migrationBuilder.DropTable(
                name: "_UpisUOdjeljenje");

            migrationBuilder.DropTable(
                name: "_Angazovan");

            migrationBuilder.DropTable(
                name: "_NastavniPlanPredmet");

            migrationBuilder.DropTable(
                name: "_Odjeljenje");

            migrationBuilder.DropTable(
                name: "_Predmet");

            migrationBuilder.DropTable(
                name: "_GodinaStudija");

            migrationBuilder.DropTable(
                name: "_NastavniPlan");

            migrationBuilder.DropTable(
                name: "_Korisnik");

            migrationBuilder.DropTable(
                name: "_Grad");
        }
    }
}
