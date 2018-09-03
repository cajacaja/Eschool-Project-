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

namespace eSchoolSemi.Web.Areas.AdministratorModul.Controllers
{
    [Area("AdministratorModul")]
    [Autorizacija(false, false, false, true)]
    public class UcenikController : Controller
    {
        private MojContext _context;


        public UcenikController(MojContext context) => _context = context;

        public IActionResult Index()
        {
           

            List<Ucenik> lista = _context._Ucenik.ToList();
            return View(lista);
        }

        public IActionResult DodajUcenika()
        {
            UcenikDodajVM vm = new UcenikDodajVM();

            return View(GetDefaultVM(vm));
        }

        [HttpPost]
        public IActionResult DodajUcenika(UcenikDodajVM vm)
        {

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
                RoditeljId=vm.RoditeljID,
                Vladanje="Primjerno"
               

            };
           
            _context._Ucenik.Add(noviUcenik);
            _context.SaveChanges();

            if (vm.OdjeljenjeID != null)
            {
                UpisUOdjeljenje upisNovi = new UpisUOdjeljenje
                {
                    UcenikId = noviUcenik.KorisnikId,
                    OdjeljenjeId = vm.OdjeljenjeID,
                    BrojUDnevniku = vm.BrojUDnevniku


                };

                _context._UpisUOdjeljenje.Add(upisNovi);
                _context.SaveChanges();
            }
           

           

            


           


            return RedirectToAction(nameof(Index));

        }


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
                    Text = x.Ime+" "+x.Prezime
                }).ToList();
            }

            if (vm.Odjeljenja == null)
            {
                vm.Odjeljenja = _context._Odjeljenje.Select(x => new SelectListItem
                {
                    Value = x.OdjeljenjeId.ToString(),
                    Text = x.Oznaka
                }).ToList();
            }

            return vm;
        }

        public IActionResult Obrisi(int UcenikID)
        {


            Ucenik izbrisi = _context._Ucenik.FirstOrDefault(x => x.KorisnikId == UcenikID);
            KorisnickiNalog korisnicki = _context.korisnickiNalogs.FirstOrDefault(x => x.KorisnickiNalogID == izbrisi.KorisnickiNalogID);

            _context.korisnickiNalogs.Remove(korisnicki);

            _context._Ucenik.Remove(izbrisi);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Uredi(int? id)
        {
            if (id == null)
                return NotFound();

            

            //novi.Ucenik = _context._Ucenik.Where(x => x.KorisnikId == id).FirstOrDefault();
            //novi.Ucenik = _context._Ucenik.FirstOrDefault(u => u.KorisnikId == id);
            //GetDefaultVM(novi);

            Ucenik urediUcenik = _context._Ucenik.FirstOrDefault(x => x.KorisnikId == id);

            UcenikDodajVM novi = new UcenikDodajVM
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

            



            return View(GetDefaultVM(novi));

        }

        public IActionResult UrediUcenika(UcenikDodajVM vm)
        {
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
            

            _context.Update(noviUcenik);
            _context.SaveChanges();


            return RedirectToAction("Index");

        }

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


            return View(ucenik);
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

            return RedirectToAction(nameof(Index));
        }
    }

    

    


}