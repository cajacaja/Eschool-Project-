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
    public class NastavnikController : Controller
    {
        private MojContext _context;
        public NastavnikController(MojContext context) => _context = context;

        public async Task<IActionResult> Index()
        {

            NastavnikIndexVM obj = new NastavnikIndexVM
            {

                Nastavnici = _context._Nastavnik.Select(x => new NastavnikIndexVM.Row
                {

                    ImePrezime = x.Ime + " " + x.Prezime,
                    Zvanje = x.Zvanje,
                    Titula = x.Titula,
                    DatumZaposljenja=x.DatumZaposlenja.ToShortDateString(),
                    Username=x.KorisnickoIme,
                    Password=x.Lozinka,
                    NastavniID=x.KorisnikId
                    

                }).ToList()
            };




            return View(obj);
        }


        public IActionResult DodajNastavnika()
        {
            NastavnikGradViewModel vm = new NastavnikGradViewModel {

                Gradovi=_context._Grad.Select(x=>new SelectListItem {
                    Value=x.GradId.ToString(),
                    Text=x.Naziv
                }).ToList()
            };



            return View(vm);
        }

        //private NastavnikGradViewModel GetDefaultVM(NastavnikGradViewModel vm)
        //{
        //    if (vm.Nastavnik == null)
        //        vm.Nastavnik = new Nastavnik();

        //    if (vm.Gradovi == null)
        //        vm.Gradovi = _context._Grad.ToList();

        //    return vm;
        //}


        [HttpPost]
        public IActionResult DodajNastavnika(NastavnikGradViewModel obj)
        {

            KorisnickiNalog korisnik = new KorisnickiNalog
            {
                Username = obj.Username,
                Password = obj.Password,
                Zapamti = false
            };

            _context.korisnickiNalogs.Add(korisnik);
            _context.SaveChanges();

            Nastavnik noviNastavnik = new Nastavnik
            {
                Ime = obj.Ime,
                Prezime = obj.Prezime,
                DatumRodenja = obj.DatumRodjenja,
                Email = obj.Email,
                Telefon = obj.Telefon,
                KorisnickoIme = obj.Username,
                Lozinka = obj.Password,
                GradId = obj.GradID,
                KorisnickiNalogID = _context.korisnickiNalogs.Where(x => x.Username == obj.Username&&x.Password==obj.Password).First().KorisnickiNalogID,
                DatumZaposlenja=obj.DatumZaposljenja,
                Zvanje=obj.Zvanje,
                Titula=obj.Titula
                
            };

            _context._Nastavnik.Add(noviNastavnik);

            _context.SaveChanges();

            return RedirectToAction("Index");

        }

        public IActionResult Obrisi(int NastavnikID)
        {



            Odjeljenje odjeljenje = _context._Odjeljenje.Where(x => x.RazrednikId == NastavnikID).FirstOrDefault();

            if (odjeljenje != null)
            {
                odjeljenje.RazrednikId = null;
                _context.SaveChanges();
            }


            _context._Nastavnik.Remove(_context._Nastavnik.Find(NastavnikID));
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Uredi(int? id)
        {
            if (id == null)
                return NotFound();

            Nastavnik obj = _context._Nastavnik.Include(x=>x.MjestoRodenja).FirstOrDefault(o=>o.KorisnikId==id);

            NastavnikGradViewModel novi = new NastavnikGradViewModel {

                NastavniID=obj.KorisnikId,
                Ime = obj.Ime,
                Prezime = obj.Prezime,               
                Email = obj.Email,
                Telefon = obj.Telefon,
                Username = obj.KorisnickoIme,
                Password = obj.Lozinka,
                GradID = obj.GradId, 
                Gradovi=_context._Grad.Select(x=>new SelectListItem {Text=x.Naziv,Value=x.GradId.ToString() }).ToList(),
                Zvanje = obj.Zvanje,
                Titula = obj.Titula

            };

            
            return View(novi);

        }

        public IActionResult UrediNastavnika(NastavnikGradViewModel vm)
        {
            Nastavnik zamjena = _context._Nastavnik.FirstOrDefault(x => x.KorisnikId == vm.NastavniID);

            zamjena.Ime = vm.Ime;
            zamjena.Prezime = vm.Prezime;
           
            zamjena.Email = vm.Email;
            zamjena.Telefon = vm.Telefon;
            zamjena.KorisnickoIme = vm.Username;
            zamjena.Lozinka = vm.Password;
            zamjena.GradId = vm.GradID;
           
            zamjena.Zvanje = vm.Zvanje;
            zamjena.Titula = vm.Titula;

            _context.Update(zamjena);
            _context.SaveChanges();

            return RedirectToAction("Index");

        }

        public IActionResult Angazuj(int nastavnikID)
        {
            Angazovan angazujNastavnika = new Angazovan { NastavnikId = nastavnikID };

            return View(angazujNastavnika);
        }

        //public IActionResult DodajAngazman()
        //{


        //}
    }
}