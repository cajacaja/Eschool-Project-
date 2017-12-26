using eSchool.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace eSchoolSemi.Data
{
    public class MojContext:DbContext
    {
        public MojContext(DbContextOptions<MojContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }



        public DbSet<Angazovan> _Angazovan { get; set; }
        public DbSet<GodinaStudija> _GodinaStudija { get; set; }
        public DbSet<Grad> _Grad { get; set; }
        public DbSet<Korisnik> _Korisnik { get; set; }
        public DbSet<Materijal> _Materijal { get; set; }
        public DbSet<Nastavnik> _Nastavnik { get; set; }
        public DbSet<NastavniPlan> _NastavniPlan { get; set; }
        public DbSet<NastavniPlanPredmet> _NastavniPlanPredmet { get; set; }
        public DbSet<Obavjestenje> _Obavjestenje { get; set; }
        public DbSet<Odjeljenje> _Odjeljenje { get; set; }
        public DbSet<OdrzanCas> _OdrzanCas { get; set; }
        public DbSet<OdrzanCasDetalji> _OdrzanCasDetalji { get; set; }
        public DbSet<Predmet> _Predmet { get; set; }
        public DbSet<Roditelj> _Roditelj { get; set; }
        public DbSet<Sastanak> _Sastanak { get; set; }
        public DbSet<SastanakRoditelj> _SastanakRoditelj { get; set; }
        public DbSet<SastanakTip> _SastanakTip { get; set; }
        public DbSet<TipObavijesti> _TipObavijesti { get; set; }
        public DbSet<Ucenik> _Ucenik { get; set; }
        public DbSet<UcenikPredmet> _UcenikPredmet { get; set; }
        public DbSet<UpisUOdjeljenje> _UpisUOdjeljenje { get; set; }
        public DbSet<ZakljucnaOcjena> _ZakljucnaOcjena { get; set; }
    }
}
