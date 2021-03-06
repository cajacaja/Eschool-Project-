﻿// <auto-generated />
using eSchoolSemi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using System;

namespace eSchoolSemi.Data.Migrations
{
    [DbContext(typeof(MojContext))]
    [Migration("20180904170152_obavjestenje2")]
    partial class obavjestenje2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("eSchool.Data.Models.Angazovan", b =>
                {
                    b.Property<int>("AngazovanId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("NastavniPlanPredmetId");

                    b.Property<int?>("NastavnikId");

                    b.Property<int?>("OdjeljenjeId");

                    b.HasKey("AngazovanId");

                    b.HasIndex("NastavniPlanPredmetId");

                    b.HasIndex("NastavnikId");

                    b.HasIndex("OdjeljenjeId");

                    b.ToTable("_Angazovan");
                });

            modelBuilder.Entity("eSchool.Data.Models.GodinaStudija", b =>
                {
                    b.Property<int>("GodinaStudijaId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Godina");

                    b.HasKey("GodinaStudijaId");

                    b.ToTable("_GodinaStudija");
                });

            modelBuilder.Entity("eSchool.Data.Models.Grad", b =>
                {
                    b.Property<int>("GradId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Naziv");

                    b.HasKey("GradId");

                    b.ToTable("_Grad");
                });

            modelBuilder.Entity("eSchool.Data.Models.Korisnik", b =>
                {
                    b.Property<int>("KorisnikId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DatumRodenja");

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<string>("Email");

                    b.Property<int?>("GradId");

                    b.Property<string>("Ime")
                        .IsRequired();

                    b.Property<int>("KorisnickiNalogID");

                    b.Property<string>("KorisnickoIme");

                    b.Property<string>("Lozinka");

                    b.Property<string>("Prezime")
                        .IsRequired();

                    b.Property<string>("Telefon");

                    b.HasKey("KorisnikId");

                    b.HasIndex("GradId");

                    b.HasIndex("KorisnickiNalogID");

                    b.ToTable("_Korisnik");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Korisnik");
                });

            modelBuilder.Entity("eSchool.Data.Models.Materijal", b =>
                {
                    b.Property<int>("MaterijalId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DatumObjave");

                    b.Property<int?>("NastavniPlanPredmetId");

                    b.Property<int?>("NastavnikId");

                    b.Property<string>("Naziv");

                    b.Property<string>("Sadrzaj");

                    b.HasKey("MaterijalId");

                    b.HasIndex("NastavniPlanPredmetId");

                    b.HasIndex("NastavnikId");

                    b.ToTable("_Materijal");
                });

            modelBuilder.Entity("eSchool.Data.Models.NastavniPlan", b =>
                {
                    b.Property<int>("NastavniPlanId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Naziv");

                    b.HasKey("NastavniPlanId");

                    b.ToTable("_NastavniPlan");
                });

            modelBuilder.Entity("eSchool.Data.Models.NastavniPlanPredmet", b =>
                {
                    b.Property<int>("NastavniPlanPredmetId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BrojSati");

                    b.Property<string>("GodinaStudiranja");

                    b.Property<int?>("NastavniPlanId");

                    b.Property<int?>("PredmetId");

                    b.Property<string>("Sifra");

                    b.HasKey("NastavniPlanPredmetId");

                    b.HasIndex("NastavniPlanId");

                    b.HasIndex("PredmetId");

                    b.ToTable("_NastavniPlanPredmet");
                });

            modelBuilder.Entity("eSchool.Data.Models.Obavjestenje", b =>
                {
                    b.Property<int>("ObavjestenjeId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AdministatorID");

                    b.Property<int?>("AdministratorKorisnikId");

                    b.Property<DateTime>("DatumObavjestenja");

                    b.Property<string>("Naslov");

                    b.Property<int?>("NastavnikID");

                    b.Property<string>("Sadrzaj");

                    b.Property<int?>("TipObavijestiId");

                    b.HasKey("ObavjestenjeId");

                    b.HasIndex("AdministratorKorisnikId");

                    b.HasIndex("NastavnikID");

                    b.HasIndex("TipObavijestiId");

                    b.ToTable("_Obavjestenje");
                });

            modelBuilder.Entity("eSchool.Data.Models.Odjeljenje", b =>
                {
                    b.Property<int>("OdjeljenjeId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("GodinaStudijaId");

                    b.Property<int>("Kapacitet");

                    b.Property<int?>("NastavniPlanId");

                    b.Property<string>("Oznaka");

                    b.Property<int?>("RazrednikId");

                    b.Property<int?>("UcenikID");

                    b.HasKey("OdjeljenjeId");

                    b.HasIndex("GodinaStudijaId");

                    b.HasIndex("NastavniPlanId");

                    b.HasIndex("RazrednikId");

                    b.HasIndex("UcenikID");

                    b.ToTable("_Odjeljenje");
                });

            modelBuilder.Entity("eSchool.Data.Models.OdrzanCas", b =>
                {
                    b.Property<int>("OdrzanCasId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AngazovanId");

                    b.Property<DateTime>("DatumOdrzavanja");

                    b.HasKey("OdrzanCasId");

                    b.HasIndex("AngazovanId");

                    b.ToTable("_OdrzanCas");
                });

            modelBuilder.Entity("eSchool.Data.Models.OdrzanCasDetalji", b =>
                {
                    b.Property<int>("OdrzanCasDetaljiId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Ocjena");

                    b.Property<int?>("OdrzanCasId");

                    b.Property<bool>("Odsutan");

                    b.Property<bool>("Opravdano");

                    b.Property<int?>("TipOcjeneId");

                    b.Property<int?>("UpisUOdjeljenjeId");

                    b.HasKey("OdrzanCasDetaljiId");

                    b.HasIndex("OdrzanCasId");

                    b.HasIndex("TipOcjeneId");

                    b.HasIndex("UpisUOdjeljenjeId");

                    b.ToTable("_OdrzanCasDetalji");
                });

            modelBuilder.Entity("eSchool.Data.Models.Predmet", b =>
                {
                    b.Property<int>("PredmetId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Naziv");

                    b.Property<string>("Oznaka");

                    b.HasKey("PredmetId");

                    b.ToTable("_Predmet");
                });

            modelBuilder.Entity("eSchool.Data.Models.Sastanak", b =>
                {
                    b.Property<int>("SastanakId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Naziv");

                    b.Property<int?>("OdjeljenjeId");

                    b.Property<int?>("SastanakTipId");

                    b.HasKey("SastanakId");

                    b.HasIndex("OdjeljenjeId");

                    b.HasIndex("SastanakTipId");

                    b.ToTable("_Sastanak");
                });

            modelBuilder.Entity("eSchool.Data.Models.SastanakRoditelj", b =>
                {
                    b.Property<int>("SastanakRoditeljId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DatumSastanka");

                    b.Property<string>("Komentar");

                    b.Property<int?>("NastavnikId");

                    b.Property<int?>("RoditeljId");

                    b.Property<int?>("SastanakId");

                    b.HasKey("SastanakRoditeljId");

                    b.HasIndex("NastavnikId");

                    b.HasIndex("RoditeljId");

                    b.HasIndex("SastanakId");

                    b.ToTable("_SastanakRoditelj");
                });

            modelBuilder.Entity("eSchool.Data.Models.SastanakTip", b =>
                {
                    b.Property<int>("SastanakTipId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Naziv");

                    b.HasKey("SastanakTipId");

                    b.ToTable("_SastanakTip");
                });

            modelBuilder.Entity("eSchool.Data.Models.TipObavijesti", b =>
                {
                    b.Property<int>("TipObavijestiId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Tip");

                    b.HasKey("TipObavijestiId");

                    b.ToTable("_TipObavijesti");
                });

            modelBuilder.Entity("eSchool.Data.Models.TipOcjene", b =>
                {
                    b.Property<int>("TipOcjeneId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Tip");

                    b.HasKey("TipOcjeneId");

                    b.ToTable("_TipOcjene");
                });

            modelBuilder.Entity("eSchool.Data.Models.UpisUOdjeljenje", b =>
                {
                    b.Property<int>("UpisUOdjeljenjeId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BrojUDnevniku");

                    b.Property<int?>("OdjeljenjeId");

                    b.Property<int?>("UcenikId");

                    b.HasKey("UpisUOdjeljenjeId");

                    b.HasIndex("OdjeljenjeId");

                    b.HasIndex("UcenikId");

                    b.ToTable("_UpisUOdjeljenje");
                });

            modelBuilder.Entity("eSchoolSemi.Data.Models.AutorizacijskiToken", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("KorisnickiNalogID");

                    b.Property<string>("Vrijednost");

                    b.Property<DateTime>("VrijemeEvidentiranja");

                    b.HasKey("Id");

                    b.HasIndex("KorisnickiNalogID");

                    b.ToTable("AutorizacijskiToken");
                });

            modelBuilder.Entity("eSchoolSemi.Data.Models.KorisnickiNalog", b =>
                {
                    b.Property<int>("KorisnickiNalogID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Password");

                    b.Property<string>("Username");

                    b.Property<bool>("Zapamti");

                    b.HasKey("KorisnickiNalogID");

                    b.ToTable("korisnickiNalogs");
                });

            modelBuilder.Entity("eSchoolSemi.Data.Models.ObavjestenjeOdjeljenje", b =>
                {
                    b.Property<int>("ObavjestenjeOdjeljenjeID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ObavjestenjeID");

                    b.Property<int>("OdjeljenjeID");

                    b.HasKey("ObavjestenjeOdjeljenjeID");

                    b.HasIndex("ObavjestenjeID");

                    b.HasIndex("OdjeljenjeID");

                    b.ToTable("ObavjestenjeOdjeljenje");
                });

            modelBuilder.Entity("eSchool.Data.Models.Nastavnik", b =>
                {
                    b.HasBaseType("eSchool.Data.Models.Korisnik");

                    b.Property<DateTime>("DatumZaposlenja");

                    b.Property<string>("Titula");

                    b.Property<string>("Zvanje");

                    b.ToTable("Nastavnik");

                    b.HasDiscriminator().HasValue("Nastavnik");
                });

            modelBuilder.Entity("eSchool.Data.Models.Roditelj", b =>
                {
                    b.HasBaseType("eSchool.Data.Models.Korisnik");


                    b.ToTable("Roditelj");

                    b.HasDiscriminator().HasValue("Roditelj");
                });

            modelBuilder.Entity("eSchool.Data.Models.Ucenik", b =>
                {
                    b.HasBaseType("eSchool.Data.Models.Korisnik");

                    b.Property<int?>("RoditeljId");

                    b.Property<string>("Vladanje");

                    b.HasIndex("RoditeljId");

                    b.ToTable("Ucenik");

                    b.HasDiscriminator().HasValue("Ucenik");
                });

            modelBuilder.Entity("eSchoolSemi.Data.Models.Administrator", b =>
                {
                    b.HasBaseType("eSchool.Data.Models.Korisnik");


                    b.ToTable("Administrator");

                    b.HasDiscriminator().HasValue("Administrator");
                });

            modelBuilder.Entity("eSchool.Data.Models.Angazovan", b =>
                {
                    b.HasOne("eSchool.Data.Models.NastavniPlanPredmet", "NastavniPlanPredmet")
                        .WithMany("Angazovani")
                        .HasForeignKey("NastavniPlanPredmetId");

                    b.HasOne("eSchool.Data.Models.Nastavnik", "Nastavnik")
                        .WithMany()
                        .HasForeignKey("NastavnikId");

                    b.HasOne("eSchool.Data.Models.Odjeljenje", "Odjeljenje")
                        .WithMany()
                        .HasForeignKey("OdjeljenjeId");
                });

            modelBuilder.Entity("eSchool.Data.Models.Korisnik", b =>
                {
                    b.HasOne("eSchool.Data.Models.Grad", "MjestoRodenja")
                        .WithMany()
                        .HasForeignKey("GradId");

                    b.HasOne("eSchoolSemi.Data.Models.KorisnickiNalog", "KorisnickiNalog")
                        .WithMany()
                        .HasForeignKey("KorisnickiNalogID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("eSchool.Data.Models.Materijal", b =>
                {
                    b.HasOne("eSchool.Data.Models.NastavniPlanPredmet", "NastavniPlanPredmet")
                        .WithMany()
                        .HasForeignKey("NastavniPlanPredmetId");

                    b.HasOne("eSchool.Data.Models.Nastavnik", "Nastavnik")
                        .WithMany()
                        .HasForeignKey("NastavnikId");
                });

            modelBuilder.Entity("eSchool.Data.Models.NastavniPlanPredmet", b =>
                {
                    b.HasOne("eSchool.Data.Models.NastavniPlan", "NastavniPlan")
                        .WithMany()
                        .HasForeignKey("NastavniPlanId");

                    b.HasOne("eSchool.Data.Models.Predmet", "Predmet")
                        .WithMany()
                        .HasForeignKey("PredmetId");
                });

            modelBuilder.Entity("eSchool.Data.Models.Obavjestenje", b =>
                {
                    b.HasOne("eSchoolSemi.Data.Models.Administrator", "Administrator")
                        .WithMany()
                        .HasForeignKey("AdministratorKorisnikId");

                    b.HasOne("eSchool.Data.Models.Nastavnik", "Nastavnik")
                        .WithMany()
                        .HasForeignKey("NastavnikID");

                    b.HasOne("eSchool.Data.Models.TipObavijesti", "TipObavijesti")
                        .WithMany()
                        .HasForeignKey("TipObavijestiId");
                });

            modelBuilder.Entity("eSchool.Data.Models.Odjeljenje", b =>
                {
                    b.HasOne("eSchool.Data.Models.GodinaStudija", "GodinaStudija")
                        .WithMany()
                        .HasForeignKey("GodinaStudijaId");

                    b.HasOne("eSchool.Data.Models.NastavniPlan", "NastavniPlan")
                        .WithMany()
                        .HasForeignKey("NastavniPlanId");

                    b.HasOne("eSchool.Data.Models.Nastavnik", "Razrednik")
                        .WithMany()
                        .HasForeignKey("RazrednikId");

                    b.HasOne("eSchool.Data.Models.Ucenik", "Ucenik")
                        .WithMany()
                        .HasForeignKey("UcenikID");
                });

            modelBuilder.Entity("eSchool.Data.Models.OdrzanCas", b =>
                {
                    b.HasOne("eSchool.Data.Models.Angazovan", "Angazovan")
                        .WithMany()
                        .HasForeignKey("AngazovanId");
                });

            modelBuilder.Entity("eSchool.Data.Models.OdrzanCasDetalji", b =>
                {
                    b.HasOne("eSchool.Data.Models.OdrzanCas", "OdrzanCas")
                        .WithMany()
                        .HasForeignKey("OdrzanCasId");

                    b.HasOne("eSchool.Data.Models.TipOcjene", "TipOcjene")
                        .WithMany()
                        .HasForeignKey("TipOcjeneId");

                    b.HasOne("eSchool.Data.Models.UpisUOdjeljenje", "UpisUOdjeljenje")
                        .WithMany()
                        .HasForeignKey("UpisUOdjeljenjeId");
                });

            modelBuilder.Entity("eSchool.Data.Models.Sastanak", b =>
                {
                    b.HasOne("eSchool.Data.Models.Odjeljenje", "Odjeljenje")
                        .WithMany()
                        .HasForeignKey("OdjeljenjeId");

                    b.HasOne("eSchool.Data.Models.SastanakTip", "SastanakTip")
                        .WithMany()
                        .HasForeignKey("SastanakTipId");
                });

            modelBuilder.Entity("eSchool.Data.Models.SastanakRoditelj", b =>
                {
                    b.HasOne("eSchool.Data.Models.Nastavnik", "Nastavnik")
                        .WithMany()
                        .HasForeignKey("NastavnikId");

                    b.HasOne("eSchool.Data.Models.Roditelj", "Roditelj")
                        .WithMany()
                        .HasForeignKey("RoditeljId");

                    b.HasOne("eSchool.Data.Models.Sastanak", "Sastanak")
                        .WithMany()
                        .HasForeignKey("SastanakId");
                });

            modelBuilder.Entity("eSchool.Data.Models.UpisUOdjeljenje", b =>
                {
                    b.HasOne("eSchool.Data.Models.Odjeljenje", "Odjeljenje")
                        .WithMany()
                        .HasForeignKey("OdjeljenjeId");

                    b.HasOne("eSchool.Data.Models.Ucenik", "Ucenik")
                        .WithMany()
                        .HasForeignKey("UcenikId");
                });

            modelBuilder.Entity("eSchoolSemi.Data.Models.AutorizacijskiToken", b =>
                {
                    b.HasOne("eSchoolSemi.Data.Models.KorisnickiNalog", "KorisnickiNalog")
                        .WithMany()
                        .HasForeignKey("KorisnickiNalogID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("eSchoolSemi.Data.Models.ObavjestenjeOdjeljenje", b =>
                {
                    b.HasOne("eSchool.Data.Models.Obavjestenje", "Obavjestenje")
                        .WithMany()
                        .HasForeignKey("ObavjestenjeID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("eSchool.Data.Models.Odjeljenje", "Odjeljenje")
                        .WithMany()
                        .HasForeignKey("OdjeljenjeID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("eSchool.Data.Models.Ucenik", b =>
                {
                    b.HasOne("eSchool.Data.Models.Roditelj", "Roditelj")
                        .WithMany("Uceniks")
                        .HasForeignKey("RoditeljId");
                });
#pragma warning restore 612, 618
        }
    }
}
