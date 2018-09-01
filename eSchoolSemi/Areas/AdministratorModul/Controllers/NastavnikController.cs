using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eSchool.Data.Models;
using eSchoolSemi.Data;
using eSchoolSemi.Web.Areas.AdministratorModul.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eSchoolSemi.Web.Areas.AdministratorModul.Controllers
{
    [Area("AdministratorModul")]
    public class NastavnikController : Controller
    {
        private MojContext _context;
        public NastavnikController(MojContext context) => _context = context;

        public async Task<IActionResult> Index()
        {
            List<Nastavnik> nav = await _context._Nastavnik.AsNoTracking().ToListAsync();
            return View(nav);
        }


        public IActionResult DodajNastavnika()
        {
            NastavnikGradViewModel vm = new NastavnikGradViewModel();

            return View("DodajNastavnika", GetDefaultVM(vm));
        }

        private NastavnikGradViewModel GetDefaultVM(NastavnikGradViewModel vm)
        {
            if (vm.Nastavnik == null)
                vm.Nastavnik = new Nastavnik();

            if (vm.Gradovi == null)
                vm.Gradovi = _context._Grad.ToList();

            return vm;
        }


        [HttpPost]
        public IActionResult DodajNastavnika(NastavnikGradViewModel vm)
        {

            vm.Nastavnik.KorisnickoIme = vm.Nastavnik.Ime + "." + vm.Nastavnik.Prezime;
            vm.Nastavnik.Lozinka = vm.Nastavnik.Prezime + vm.Nastavnik.Ime + vm.Nastavnik.DatumRodenja.Year.ToString();
            _context._Nastavnik.Add(vm.Nastavnik);
            _context.SaveChanges();


            return RedirectToAction(nameof(Index));

        }

        public IActionResult Obrisi(int NastavnikID)
        {
            _context._Nastavnik.Remove(_context._Nastavnik.Find(NastavnikID));
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Uredi(int? id)
        {
            if (id == null)
                return NotFound();

            NastavnikGradViewModel novi = new NastavnikGradViewModel();

            Nastavnik korisnik = _context._Nastavnik.Where(x => x.KorisnikId == id).FirstOrDefault();
            novi.Nastavnik = korisnik;
            novi.Gradovi = _context._Grad.ToList();

            return View(novi);

        }

        public IActionResult UrediNastavnika(NastavnikGradViewModel vm)
        {

            _context.Update(vm.Nastavnik);
            _context.SaveChanges();

            return RedirectToAction("Index");

        }

        //public IActionResult Angazuj(int nastavnikID)
        //{
        //    Angazovan angazujNastavnika = new Angazovan {NastavnikId=nastavnikID};

        //   return View(angazujNastavnika);
        //}

        //public IActionResult DodajAngazman()
        //{


        //}
    }
}