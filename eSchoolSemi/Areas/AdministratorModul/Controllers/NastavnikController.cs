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

        public IActionResult Index()
        {

            
            


            return View();
        }

        public async Task<IActionResult> tabelaNastavnici(string sortOrder, string search, int? page)
        {

            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "ime_desc" : "";

            var Nastavnici = _context._Nastavnik.Include(x => x.MjestoRodenja).ToList();
            IQueryable<Nastavnik> ListaNastavnika = from x in _context._Nastavnik
                                                 from s in _context._Grad
                                                 where x.GradId == s.GradId
                                                 select new Nastavnik
                                                 {
                                                     KorisnikId = x.KorisnikId,
                                                     Titula=x.Titula,
                                                     Zvanje = x.Zvanje,
                                                     DatumZaposlenja = x.DatumZaposlenja,
                                                     KorisnickoIme = x.KorisnickoIme,
                                                     Lozinka = x.Lozinka,
                                                     Ime = x.Ime,
                                                     Prezime = x.Prezime,
                                                     GradId = x.GradId,
                                                     MjestoRodenja = s
                                                 };

            if (!String.IsNullOrEmpty(search))
            {

                ListaNastavnika = ListaNastavnika.Where(x => (x.Ime +x.Prezime).Contains(search) || (x.Prezime + " " + x.Ime).Contains(search));
            }

            switch (sortOrder)
            {
                case "ime_desc":
                    ListaNastavnika = ListaNastavnika.OrderByDescending(x => x.Prezime);
                    break;
                default:
                    ListaNastavnika = ListaNastavnika.OrderBy(x => x.Prezime);
                    break;

            }

            int pageSize = 10;
            return PartialView(await PaginatedList<Nastavnik>.CreateAsync(ListaNastavnika.AsNoTracking(), page ?? 1, pageSize));

        }

        public JsonResult nazivNastavnika(string Prefix)
        {

            List<SelectListItem> nastavnik = _context._Nastavnik.Where(x => (x.Ime + " " + x.Prezime).StartsWith(Prefix) || (x.Prezime + " " + x.Ime).StartsWith(Prefix)).Select(x => new SelectListItem
            {

                Value = x.KorisnikId.ToString(),
                Text = x.Prezime + " " + x.Ime
            }).ToList();




            return Json(nastavnik);
        }

        public IActionResult DodajNastavnika()
        {
            NastavnikGradViewModel vm = new NastavnikGradViewModel();



            return PartialView(GetDefaultVM(vm));
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
            if (!ModelState.IsValid)
            {

                return View("DodajNastavnika", GetDefaultVM(obj));
            }

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

            Angazovan angazovanNastavnik = _context._Angazovan.FirstOrDefault(x => x.NastavnikId == NastavnikID);

            if (angazovanNastavnik!=null)
            {




                angazovanNastavnik.NastavnikId = null;
                _context._Angazovan.Update(angazovanNastavnik);                
                _context.SaveChanges();
                
            }


            Odjeljenje odjeljenje = _context._Odjeljenje.Where(x => x.RazrednikId == NastavnikID).FirstOrDefault();

            if (odjeljenje != null)
            {
                odjeljenje.RazrednikId = null;
                _context.SaveChanges();
            }


            _context._Nastavnik.Remove(_context._Nastavnik.Find(NastavnikID));
            _context.SaveChanges();

            return RedirectToAction(nameof(tabelaNastavnici));
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

            
            return PartialView(novi);

        }

        public IActionResult UrediNastavnika(NastavnikGradViewModel vm)
        {

            if (!ModelState.IsValid)
            {

                return View("Uredi", GetDefaultVM(vm));
            }

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

        public IActionResult Detalji(int nastavnikID) {

            var Nastavnik = _context._Nastavnik.FirstOrDefault(x => x.KorisnikId == nastavnikID);

            if (Nastavnik==null)
            {
                return NotFound();
            }

            NastavnikGradViewModel detaljiNastavnik = new NastavnikGradViewModel {


                Ime=Nastavnik.Ime,
                Prezime=Nastavnik.Prezime,
                Email=Nastavnik.Email,
                Telefon=Nastavnik.Telefon,
                DatumRodjenja=Nastavnik.DatumRodenja,
                DatumZaposljenja=Nastavnik.DatumZaposlenja,
                MjestoRodjenja=_context._Grad.First(x=>x.GradId==Nastavnik.GradId).Naziv,
                Zvanje=Nastavnik.Zvanje            
                
            };

            return PartialView(detaljiNastavnik);
        }

        public IActionResult Angazuj(int nastavnikID)
        {
            Angazovan angazujNastavnika = new Angazovan { NastavnikId = nastavnikID };

            return View(angazujNastavnika);
        }

       
            public NastavnikGradViewModel GetDefaultVM(NastavnikGradViewModel vm) {

                if (vm.Gradovi==null)
                {
                vm.Gradovi = _context._Grad.Select(x => new SelectListItem
                {
                    Value = x.GradId.ToString(),
                    Text = x.Naziv
                }).ToList();
                }

            return vm;
            }

        //public IActionResult DodajAngazman()
        //{


        //}
    }
}