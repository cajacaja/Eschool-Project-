using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eSchoolSemi.Data;
using Microsoft.AspNetCore.Mvc;
using eSchool.Data.Models;
using eSchoolSemi.Web.Areas.AdministratorModul.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using eSchoolSemi.Web.Helper;
using eSchoolSemi.Data.Models;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace eSchoolSemi.Web.Areas.AdministratorModul.Controllers
{
    [Area("AdministratorModul")]
    [Autorizacija(false, false, false, true)]
    public class UcenikController : Controller
    {
        private MojContext _context;


        public UcenikController(MojContext context) => _context = context;

        public string sortFilter = "empty";

        public IActionResult Index()
        {
           
                     


            UcenikDodajVM vm = new UcenikDodajVM();
            ViewBag.Gradovi = GetDefaultVM(vm);
            


            return View();
        }

        public async Task<IActionResult> _tabela(string sortOrder, string currentFilter, string search, int? page) {

            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "ime_desc" : "";
            
            
            
           
            IQueryable<Ucenik> listaUcenika = from x in _context._Ucenik
                                              select x;
            

            if (!String.IsNullOrEmpty(search))
            {

                listaUcenika = listaUcenika.Where(x => x.Prezime.Contains(search) || x.Ime.Contains(search));
            }


            switch (sortOrder)
            {
                case "ime_desc":
                    listaUcenika = listaUcenika.OrderByDescending(x => x.Prezime);
                    break;
                default:
                    listaUcenika = listaUcenika.OrderBy(x => x.Prezime);
                    break;

            }

            

           
            int pageSize = 5;
            return PartialView(await PaginatedList<Ucenik>.CreateAsync(listaUcenika.AsNoTracking(), page ?? 1, pageSize));

        }

        public IActionResult DodajUcenika()
        {
            UcenikDodajVM vm = new UcenikDodajVM();

            

            return PartialView(GetDefaultVM(vm));
        }

        public IActionResult DodajFile()
        {
            Ucenik ucenik = _context._Ucenik.FirstOrDefault();

            return View(ucenik);
        }

        
        [HttpPost]
        public async Task<IActionResult> SnimiUcenika(UcenikDodajVM vm)
        {
           

            if (!ModelState.IsValid)
            {
                
                return View("DodajUcenika", GetDefaultVM(vm));
            }
            Roditelj roditelj = _context._Roditelj.FirstOrDefault(x => x.Prezime + " " + x.Ime == vm.Roditelj);
            

            KorisnickiNalog korisnicki = new KorisnickiNalog
            {
                Username = vm.Username,
                Password = vm.Password,
                Zapamti = false
            };

            _context.korisnickiNalogs.Add(korisnicki);
            _context.SaveChanges();

            Ucenik noviUcenik = new Ucenik
            {

                Ime = vm.Ime,
                Prezime = vm.Prezime,
                Telefon = vm.Telefon,
                Email = vm.Email,
                GradId = vm.GradID,
                KorisnickoIme = vm.Username,
                Lozinka = vm.Password,
                KorisnickiNalogID = _context.korisnickiNalogs.FirstOrDefault(x => x.Username == vm.Username && x.Password == vm.Password).KorisnickiNalogID,
                DatumRodenja = vm.DatumRodjenja,
                Vladanje = "Primjerno"


            };

            if (roditelj != null)
                noviUcenik.RoditeljId = roditelj.KorisnikId;

            if (vm.KorinickaSlika != null)
            {
                using (var memoryStream = new MemoryStream())
                {

                    await vm.KorinickaSlika.CopyToAsync(memoryStream);
                    noviUcenik.KorisnickaSlika = memoryStream.ToArray();
                }
            }





            _context._Ucenik.Add(noviUcenik);
            _context.SaveChanges();

            //if (vm.OdjeljenjeID != null)
            //{
            //    UpisUOdjeljenje upisNovi = new UpisUOdjeljenje
            //    {
            //        UcenikId = noviUcenik.KorisnikId,
            //        OdjeljenjeId = vm.OdjeljenjeID,
            //        BrojUDnevniku = vm.BrojUDnevniku


            //    };

            //    _context._UpisUOdjeljenje.Add(upisNovi);
            //    _context.SaveChanges();
            //}







           


            return RedirectToAction("Index");

        }




        #region glupeFunkcije

        public UcenikDodajVM GetDefaultVM(UcenikDodajVM vm)
        {

         

            if (vm.Gradovi==null)
            {
                vm.Gradovi = _context._Grad.Select(x => new SelectListItem
                {
                    Value = x.GradId.ToString(),
                    Text=x.Naziv
                }).ToList();
            }

            if (vm.Roditleji == null)
            {
                vm.Roditleji = _context._Roditelj.Select(x => new SelectListItem
                {
                    Value = x.KorisnikId.ToString(),
                    Text = x.Ime + " " + x.Prezime
                }).ToList();
            }

            

            return vm;
        }

        public UcenikUrediVM Popuni(UcenikUrediVM vm)
        {



            if (vm.Gradovi == null)
            {
                vm.Gradovi = _context._Grad.Select(x => new SelectListItem
                {
                    Value = x.GradId.ToString(),
                    Text = x.Naziv
                }).ToList();
            }

            if (vm.Roditleji == null)
            {
                vm.Roditleji = _context._Roditelj.Select(x => new SelectListItem
                {
                    Value = x.KorisnikId.ToString(),
                    Text = x.Ime + " " + x.Prezime
                }).ToList();
            }



            return vm;
        }
        #endregion

        public IActionResult Obrisi(int UcenikID)
        {


            Ucenik izbrisi = _context._Ucenik.FirstOrDefault(x => x.KorisnikId == UcenikID);
            KorisnickiNalog korisnicki = _context.korisnickiNalogs.FirstOrDefault(x => x.KorisnickiNalogID == izbrisi.KorisnickiNalogID);

            _context.korisnickiNalogs.Remove(korisnicki);

            _context._Ucenik.Remove(izbrisi);
            _context.SaveChanges();
            return RedirectToAction(nameof(_tabela));
        }

        public IActionResult Uredi(int? id)
        {
            if (id == null)
                return NotFound();

            

            //novi.Ucenik = _context._Ucenik.Where(x => x.KorisnikId == id).FirstOrDefault();
            //novi.Ucenik = _context._Ucenik.FirstOrDefault(u => u.KorisnikId == id);
            //GetDefaultVM(novi);

            Ucenik urediUcenik = _context._Ucenik.FirstOrDefault(x => x.KorisnikId == id);

            UcenikUrediVM novi = new UcenikUrediVM
            {

                Ime = urediUcenik.Ime,
                Prezime = urediUcenik.Prezime,
                DatumRodjenja = urediUcenik.DatumRodenja,
                Email = urediUcenik.Email,
                Telefon = urediUcenik.Telefon,
                GradID = urediUcenik.GradId,              
                Username = urediUcenik.KorisnickoIme,
                Password = urediUcenik.Lozinka,
                RoditeljID = urediUcenik.RoditeljId,
                UcenikID=urediUcenik.KorisnikId
                

            };

                
            



            return PartialView(Popuni(novi));

        }

        public async Task<IActionResult> UrediUcenika(UcenikUrediVM vm)
        {
            if (!ModelState.IsValid)
            {

                return PartialView("Uredi", Popuni(vm));
            }
            Ucenik noviUcenik = _context._Ucenik.FirstOrDefault(X => X.KorisnikId == vm.UcenikID);

            KorisnickiNalog korisnik = _context.korisnickiNalogs.Where(x => x.KorisnickiNalogID == noviUcenik.KorisnickiNalogID).FirstOrDefault();

            if (korisnik.Username != vm.Username || korisnik.Password != vm.Password)
            {
                korisnik.Password = vm.Password;
                korisnik.Username = vm.Username;

                _context.Update(korisnik);
            }


            noviUcenik.Ime = vm.Ime;
            noviUcenik.Prezime = vm.Prezime;
            noviUcenik.Telefon = vm.Telefon;
            noviUcenik.Email = vm.Email;
            noviUcenik.GradId = vm.GradID;
            noviUcenik.KorisnickoIme = vm.Username;
            noviUcenik.Lozinka = vm.Password;
            noviUcenik.DatumRodenja = vm.DatumRodjenja;
            noviUcenik.RoditeljId = vm.RoditeljID;

            if (vm.KorinickaSlika != null)
            {
                using (var memoryStream = new MemoryStream())
                {

                    await vm.KorinickaSlika.CopyToAsync(memoryStream);
                    noviUcenik.KorisnickaSlika = memoryStream.ToArray();
                }
            }

            _context.Update(noviUcenik);


            //Odjeljenje odjeljenje = _context._Odjeljenje.FirstOrDefault(x => x.OdjeljenjeId == vm.OdjeljenjeID);

            //if (odjeljenje != null) {

            //    UpisUOdjeljenje upisNovi = new UpisUOdjeljenje {

            //        UcenikId = noviUcenik.KorisnikId,
            //        OdjeljenjeId = odjeljenje.OdjeljenjeId,
            //        BrojUDnevniku = _context._UpisUOdjeljenje.Where(x => x.OdjeljenjeId == odjeljenje.OdjeljenjeId).Count() + 1

            //    };

            //    _context._UpisUOdjeljenje.Add(upisNovi);
                
            //}

            _context.SaveChanges();


            return RedirectToAction("_tabela");

        }

        public IActionResult Detalji(int id) {


            Ucenik ucenik = _context._Ucenik.Where(x => x.KorisnikId == id).Include(x => x.Roditelj).Include(x => x.MjestoRodenja).FirstOrDefault();

            UcenikDetalj detalji = new UcenikDetalj {

                Slika=ucenik.KorisnickaSlika,
                ImeIprezime=ucenik.Ime+" "+ucenik.Prezime,
                DatumRodjenja=ucenik.DatumRodenja.ToShortDateString(),
                Roditelj=ucenik.Roditelj.Ime+" "+ ucenik.Roditelj.Prezime,
                MjestoPrebivalista=ucenik.MjestoRodenja.Naziv

            };



            return View(detalji);
        }
        #region Dodavanje ucenika u odjeljenje
        public IActionResult DodajUOdjeljenje()
         {
            UcenikOdjeljenje ucenik = new UcenikOdjeljenje();

             ucenik.Ucenik =_context._Ucenik.Select(x => new SelectListItem
             {
                 Value = x.KorisnikId.ToString(),
                 Text = x.Prezime + " " + x.Ime
             }).ToList();


            //ucenik.Ucenik = _context._Ucenik.ToList();
           

            ucenik.Odjeljenja = _context._Odjeljenje.Select(x => new SelectListItem
             {
                Value = x.OdjeljenjeId.ToString(),
                Text = x.Oznaka
            }).ToList();

           


            return PartialView(ucenik);
        }

        [HttpPost]
        public IActionResult SnimiOdjeljenje(UcenikOdjeljenje vm)
        {

            
            
            UpisUOdjeljenje noviUpis = new UpisUOdjeljenje
            {
                BrojUDnevniku = vm.BrojUdnevniku,
                OdjeljenjeId=vm.OdjeljenjeId,
                UcenikId=vm.UcenikId
                
            };

            

            _context._UpisUOdjeljenje.Add(noviUpis);
            _context.SaveChanges();

            IEnumerable<UpisUOdjeljenje> listaUcenika = _context._UpisUOdjeljenje.Where(x => x.OdjeljenjeId == vm.OdjeljenjeId).Include(x=>x.Ucenik).ToList();

            listaUcenika = listaUcenika.OrderBy(x => x.Ucenik.Prezime);

            int brojac = 1;

            foreach (var ucenik in listaUcenika)
            {
                ucenik.BrojUDnevniku = brojac;
                brojac++;
            }

            brojac = 1;

            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region Provjera view modela za dodavanje ucenika
        public IActionResult ProvjeriRoditelj(string Roditelj)
        {
            Roditelj roditelj = _context._Roditelj.FirstOrDefault(x => x.Prezime + " " + x.Ime == Roditelj);
            if (roditelj==null)
            {
                return Json("Pogresno prezime i ime roditelja!");
            }

            return Json(true);
        }

        public IActionResult ProvjeriUsername(string Username,int UcenikID)
        {
            Korisnik provjera = _context._Korisnik.FirstOrDefault(x => x.KorisnickoIme == Username);

            if (provjera != null) {
                return Json("Username je vec zauzet");
            }

            return Json(true);
        }

        public IActionResult ProvjeriEmail(string Email)
        {
            if (Email == null)
                return Json(true);

            Korisnik roditelj = _context._Korisnik.FirstOrDefault(x => x.Email == Email);
            if (roditelj != null)
            {
                return Json("Email je vec zauzet!");
            }

            return Json(true);
        }


        public IActionResult ProvjeriTelefon(string Telefon)
        {
            

            Korisnik roditelj = _context._Korisnik.FirstOrDefault(x => x.Telefon == Telefon);
            if (roditelj != null)
            {
                return Json("Broj telefona je vec zauzet!");
            }

            return Json(true);
        }
        #endregion

        #region Provjera za uredjivanje ucenika
        public IActionResult UsernameCheck(string Username, int UcenikID)
        {

            Korisnik prviValue = _context._Korisnik.First(x => x.KorisnikId == UcenikID);
            Korisnik provjera = _context._Korisnik.FirstOrDefault(x => x.KorisnickoIme == Username && Username != prviValue.KorisnickoIme);

            if (provjera != null)
            {
                return Json("Username je vec zauzet");
            }

            return Json(true);
        }

        public IActionResult EmailCheck(string Email, int UcenikID)
        {

            Korisnik prviValue = _context._Korisnik.First(x => x.KorisnikId == UcenikID);
            Korisnik provjera = _context._Korisnik.FirstOrDefault(x => x.Email == Email && Email != prviValue.Email);

            if (provjera != null)
            {
                return Json("Email je vec zauzet");
            }

            return Json(true);
        }

        public IActionResult TelefonCheck(string Telefon, int UcenikID)
        {

            Korisnik prviValue = _context._Korisnik.First(x => x.KorisnikId == UcenikID);
            Korisnik provjera = _context._Korisnik.FirstOrDefault(x => x.Telefon == Telefon && Telefon != prviValue.Telefon);

            if (provjera != null)
            {
                return Json("Broj telefona je vec zauzet");
            }

            return Json(true);
        }
        #endregion
    }








}