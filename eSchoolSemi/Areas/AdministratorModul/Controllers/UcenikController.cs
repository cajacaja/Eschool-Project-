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


            vm.Ucenik.KorisnickoIme = vm.Ucenik.Ime + "." + vm.Ucenik.Prezime;
            vm.Ucenik.KorisnickoIme = vm.Ucenik.Ime + "." + vm.Ucenik.Prezime + vm.Ucenik.DatumRodenja.Month.ToString() + vm.Ucenik.DatumRodenja.Day.ToString();
            _context._Ucenik.Add(vm.Ucenik);
            _context.SaveChanges();

            UpisUOdjeljenje upisNovi = new UpisUOdjeljenje
            {
                UcenikId=vm.Ucenik.KorisnikId,
                OdjeljenjeId=vm.OdjeljenjeID,
                BrojUDnevniku=vm.BrojUDnevniku
                
                
            };

           

            _context._UpisUOdjeljenje.Add(upisNovi);
            _context.SaveChanges();


           


            return RedirectToAction(nameof(Index));

        }


        public UcenikDodajVM GetDefaultVM(UcenikDodajVM vm)
        {

            
            if (vm.Ucenik==null)
            {
                vm.Ucenik = new Ucenik { Vladanje = "Primjerno" };
            }

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
            _context._Ucenik.Remove(_context._Ucenik.Find(UcenikID));
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Uredi(int? id)
        {
            if (id == null)
                return NotFound();

            UcenikDodajVM novi = new UcenikDodajVM();

            //novi.Ucenik = _context._Ucenik.Where(x => x.KorisnikId == id).FirstOrDefault();
            novi.Ucenik = _context._Ucenik.FirstOrDefault(u => u.KorisnikId == id);
            GetDefaultVM(novi);
           

            return View(novi);

        }

        public IActionResult UrediUcenika(UcenikDodajVM vm)
        {
            
            _context.Update(vm.Ucenik);
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