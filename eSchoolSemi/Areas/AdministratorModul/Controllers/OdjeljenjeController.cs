using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eSchool.Data.Models;
using eSchoolSemi.Data;
using eSchoolSemi.Web.Areas.AdministratorModul.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace eSchoolSemi.Web.Areas.AdministratorModul.Controllers
{
    [Area("AdministratorModul")]
    public class OdjeljenjeController : Controller
    {

        private MojContext _context;
        public OdjeljenjeController(MojContext context) => _context = context;

        public IActionResult Index()
        {
            List<Odjeljenje> novi = _context._Odjeljenje.ToList();
            return View(novi);
        }

        public IActionResult DodajOdjeljenje()
        {
            OdjenjenjeVM vm = new OdjenjenjeVM();


            return View(GetDefaultVM(vm));
        }

        public IActionResult Uredi(int id)
        {
            OdjenjenjeVM odjeljenjeUredi = new OdjenjenjeVM();

            odjeljenjeUredi.Odjejlenje = _context._Odjeljenje.Where(x=>x.OdjeljenjeId== id).FirstOrDefault();
            GetDefaultVM(odjeljenjeUredi);

            List<UpisUOdjeljenje> upisi = _context._UpisUOdjeljenje.Where(x => x.OdjeljenjeId == id).ToList();
            List<Ucenik> ucenici = new List<Ucenik>();

            foreach (var item in upisi)
            {
                ucenici.Add(_context._Ucenik.Where(x => x.KorisnikId == item.UcenikId).FirstOrDefault());
            }

             odjeljenjeUredi.Predstavnik = ucenici.Select(x => new SelectListItem
            { Value=x.KorisnikId.ToString(),Text=x.Ime+" "+x.Prezime }).ToList();
          



            return View(odjeljenjeUredi);
        }

        public IActionResult SnimiUredjeno(OdjenjenjeVM uredi)
        {
            _context._Odjeljenje.Update(uredi.Odjejlenje);
            _context.SaveChanges();
         

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Detalji(int id)
        {
            OdjenjenjeVM odjeljenjeUredi = new OdjenjenjeVM();

            odjeljenjeUredi.Odjejlenje = _context._Odjeljenje.Where(x => x.OdjeljenjeId == id).FirstOrDefault();
            GetDefaultVM(odjeljenjeUredi);

            List<UpisUOdjeljenje> upisi = _context._UpisUOdjeljenje.Where(x => x.OdjeljenjeId == id).ToList();
            List<Ucenik> ucenici = new List<Ucenik>();

            foreach (var item in upisi)
            {
                ucenici.Add(_context._Ucenik.Where(x => x.KorisnikId == item.UcenikId).FirstOrDefault());
            }

            odjeljenjeUredi.Predstavnik = ucenici.Select(x => new SelectListItem
            { Value = x.KorisnikId.ToString(), Text = x.Ime + " " + x.Prezime }).ToList();




            return View(odjeljenjeUredi);
        }


        private OdjenjenjeVM GetDefaultVM(OdjenjenjeVM vm)
        {

            if (vm.Odjejlenje == null)
                vm.Odjejlenje = new Odjeljenje();

            if(vm.Razrednik==null)
            vm.Razrednik = _context._Nastavnik.Select(x => new SelectListItem
            {
                Value = x.KorisnikId.ToString(),
                Text = x.Ime + " " + x.Prezime
            }).ToList();
            
            return vm;
        }


        [HttpPost]
        public IActionResult Snimi(OdjenjenjeVM vm )
        {

           
            _context._Odjeljenje.Add(vm.Odjejlenje);
            _context.SaveChanges();


            return RedirectToAction(nameof(Index));

        }

        public IActionResult Obrisi(int OdjeljenjeID)
        {
           List<UpisUOdjeljenje> upisi = _context._UpisUOdjeljenje.Where(x => x.OdjeljenjeId == OdjeljenjeID).ToList();
            foreach (var item in upisi)
            {
                _context._UpisUOdjeljenje.Remove(_context._UpisUOdjeljenje.Find(item.UpisUOdjeljenjeId));
                _context.SaveChanges();
            }

            _context._Odjeljenje.Remove(_context._Odjeljenje.Find(OdjeljenjeID));
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }


    }
}