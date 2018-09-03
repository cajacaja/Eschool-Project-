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
using Microsoft.AspNetCore.Mvc.Rendering;
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
            

            List<Roditelj> model = _context._Roditelj.Include(x=>x.MjestoRodenja).ToList();
            return View("Index", model);
        }

        #region DodavanjeRoditelja
        public IActionResult DodajRoditelja()
        {
            RoditeljDodajViewModel vm = new RoditeljDodajViewModel {

                Gradovi=_context._Grad.Select(x=>new SelectListItem {

                    Value=x.GradId.ToString(),
                    Text=x.Naziv
                }).ToList()
            };

            return View("DodajRoditelja",vm);
        }


        [HttpPost]
        public IActionResult DodajRoditelja(RoditeljDodajViewModel vm)
        {

            KorisnickiNalog korisnicki = new KorisnickiNalog
            {
                Username=vm.Username,
                Password=vm.Password,
                Zapamti=false
            };

            _context.korisnickiNalogs.Add(korisnicki);
            _context.SaveChanges();

            Roditelj noviRoditelj = new Roditelj
            {

                Ime = vm.Ime,
                Prezime = vm.Prezime,
                Telefon = vm.Telefon,
                Email = vm.Email,
                GradId = vm.GradID,
                KorisnickoIme = vm.Username,
                Lozinka = vm.Password,
                KorisnickiNalogID = _context.korisnickiNalogs.FirstOrDefault(x => x.Username == vm.Username && x.Password == vm.Password).KorisnickiNalogID,
                DatumRodenja = vm.DatumRodjenja

            };

            _context._Roditelj.Add(noviRoditelj);
            _context.SaveChanges();
        
           

            
            return RedirectToAction(nameof(Index));

        }


       
        #endregion

        public IActionResult Obrisi(int RoditeljID)
        {
            Roditelj izbrisi = _context._Roditelj.FirstOrDefault(x => x.KorisnikId == RoditeljID);
            KorisnickiNalog korisnicki = _context.korisnickiNalogs.FirstOrDefault(x => x.KorisnickiNalogID == izbrisi.KorisnickiNalogID);

            _context.korisnickiNalogs.Remove(korisnicki);
            
            _context._Roditelj.Remove(izbrisi);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Uredi(int? id)
        {
            if (id == null)
                return NotFound();

            Roditelj urediRoditelj = _context._Roditelj.FirstOrDefault(x => x.KorisnikId == id);

            RoditeljDodajViewModel novi = new RoditeljDodajViewModel
            {

                Ime= urediRoditelj.Ime,
                Prezime = urediRoditelj.Prezime,
                DatumRodjenja = urediRoditelj.DatumRodenja,
                Email = urediRoditelj.Email,
                Telefon = urediRoditelj.Telefon,
                GradID = urediRoditelj.GradId,
                Gradovi=_context._Grad.Select(x=>new SelectListItem {Value=x.GradId.ToString(),Text=x.Naziv }).ToList(),
                Username = urediRoditelj.KorisnickoIme,
                Password = urediRoditelj.Lozinka,
                RoditeljID=urediRoditelj.KorisnikId

            };

            

            return View(novi);

        }

        public IActionResult UrediRoditelja(RoditeljDodajViewModel vm)
        {

            Roditelj noviRoditelj = _context._Roditelj.FirstOrDefault(X => X.KorisnikId == vm.RoditeljID);

            KorisnickiNalog korisnik = _context.korisnickiNalogs.Where(x => x.KorisnickiNalogID == noviRoditelj.KorisnickiNalogID).FirstOrDefault();

            if (korisnik.Username!=vm.Username || korisnik.Password!=vm.Password)
            {
                korisnik.Password = vm.Password;
                korisnik.Username = vm.Username;

                _context.Update(korisnik);
            }


            noviRoditelj.Ime = vm.Ime;
            noviRoditelj.Prezime = vm.Prezime;
            noviRoditelj.Telefon = vm.Telefon;
            noviRoditelj.Email = vm.Email;
            noviRoditelj.GradId = vm.GradID;
            noviRoditelj.KorisnickoIme = vm.Username;
            noviRoditelj.Lozinka = vm.Password;
            noviRoditelj.DatumRodenja = vm.DatumRodjenja;

            

            _context.Update(noviRoditelj);
            _context.SaveChanges();

            return RedirectToAction("Index");

        }

        //TrebaDodaradit
        public IActionResult Detalji(int? id)
        {
            Roditelj novi = _context._Roditelj.Where(s => s.KorisnikId == id).FirstOrDefault();

            return View(novi);

        }

        //public IActionResult ProvjeriTelefon(Roditelj roditelj)
        //{
        //    if (_context._Korisnik.Any(x => x.Telefon == roditelj.Telefon))
        //        return Json($"Broj{roditelj.Telefon} je zauzet!");
        //    return Json(true);
        //}

       
    }
}