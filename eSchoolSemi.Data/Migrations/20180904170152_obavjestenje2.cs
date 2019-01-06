using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace eSchoolSemi.Data.Migrations
{
    public partial class obavjestenje2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ObavjestenjeOdjeljenje",
                columns: table => new
                {
                    ObavjestenjeOdjeljenjeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ObavjestenjeID = table.Column<int>(nullable: false),
                    OdjeljenjeID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObavjestenjeOdjeljenje", x => x.ObavjestenjeOdjeljenjeID);
                    table.ForeignKey(
                        name: "FK_ObavjestenjeOdjeljenje__Obavjestenje_ObavjestenjeID",
                        column: x => x.ObavjestenjeID,
                        principalTable: "_Obavjestenje",
                        principalColumn: "ObavjestenjeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ObavjestenjeOdjeljenje__Odjeljenje_OdjeljenjeID",
                        column: x => x.OdjeljenjeID,
                        principalTable: "_Odjeljenje",
                        principalColumn: "OdjeljenjeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ObavjestenjeOdjeljenje_ObavjestenjeID",
                table: "ObavjestenjeOdjeljenje",
                column: "ObavjestenjeID");

            migrationBuilder.CreateIndex(
                name: "IX_ObavjestenjeOdjeljenje_OdjeljenjeID",
                table: "ObavjestenjeOdjeljenje",
                column: "OdjeljenjeID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ObavjestenjeOdjeljenje");
        }
    }
}
