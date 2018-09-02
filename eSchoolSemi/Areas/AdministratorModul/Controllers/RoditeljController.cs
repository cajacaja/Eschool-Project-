using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eSchool.Data.Models;
using eSchoolSemi.Data;
using eSchoolSemi.Data.Models;
using eSchoolSemi.Web.Areas.AdministratorModul.ViewModels;
using eSchoolSemi.Web.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eSchoolSemi.Web.Areas.AdministratorModul.Controllers
{

    [Area("AdministratorModul")]
    [Autorizacija(false, false, false, true)]
    public class RoditeljController : Controller
    {
        private MojContext _context;
        public RoditeljController(MojContext context) => _context = context;

        //Dodat ViewModel da se vidie i gradovi
        public IActionResult Index()
        {
            

            List<Roditelj> model = _context._Roditelj.ToList();
            return View("Index", model);
        }

        #region DodavanjeRoditelja
        public IActionResult DodajRoditelja()
        {
            RoditeljDodajViewModel vm = new RoditeljDodajViewModel();

            return View("DodajRoditelja",GetDefaultVM(vm));
        }


        [HttpPost]
        public IActionResult DodajRoditelja(RoditeljDodajViewModel vm)
        {
           

        
            if (!ModelState.IsValid)
            {

                return View(GetDefaultVM(vm));
            }
            vm.Roditelj.KorisnickoIme=vm.Roditelj.Ime+"."+vm.Roditelj.Prezime;
            vm.Roditelj.Lozinka = vm.Roditelj.Prezime + vm.Roditelj.Ime + vm.Roditelj.DatumRodenja.Year.ToString();
            _context._Roditelj.Add(vm.Roditelj);
            _context.SaveChanges();

            
            return RedirectToAction(nameof(Index));

        }


        private RoditeljDodajViewModel GetDefaultVM(RoditeljDodajViewModel vm)
        {
            if (vm.Roditelj == null)
                vm.Roditelj = new Roditelj();

            if (vm.Gradovi == null)
                vm.Gradovi = _context._Grad.ToList();

            return vm;
        }
        #endregion

        public IActionResult Obrisi(int RoditeljID)
        {
            _context._Roditelj.Remove(_context._Roditelj.Find(RoditeljID));
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Uredi(int? id)
        {
            if (id == null)
                return NotFound();

            RoditeljDodajViewModel novi = new RoditeljDodajViewModel();

            Roditelj korisnik = _context._Roditelj.Where(x => x.KorisnikId == id).FirstOrDefault();
            novi.Roditelj = korisnik;
            novi.Gradovi = _context._Grad.ToList();

            return View(novi);

        }

        public IActionResult UrediRoditelja(RoditeljDodajViewModel vm)
        {
            
            _context.Update(vm.Roditelj);
            _context.SaveChanges();

            return RedirectToAction("Index");

        }

        public IActionResult Detalji(int? id)
        {
            Roditelj novi = _context._Roditelj.Where(s => s.KorisnikId == id).FirstOrDefault();

            return View(novi);

        }

        public IActionResult ProvjeriTelefon(Roditelj roditelj)
        {
            if (_context._Korisnik.Any(x => x.Telefon == roditelj.Telefon))
                return Json($"Broj{roditelj.Telefon} je zauzet!");
            return Json(true);
        }

       
    }
}