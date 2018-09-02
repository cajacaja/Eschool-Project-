using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eSchoolSemi.Data;
using eSchoolSemi.Data.Models;
using eSchoolSemi.Web.Areas.AdministratorModul.ViewModels;
using eSchoolSemi.Web.Helper;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace eSchoolSemi.Web.Areas.AdministratorModul.Controllers
{
    [Area("AdministratorModul")]
    [Autorizacija(false,false,false,true)]
    public class AdministratorController : Controller
    {
        private MojContext _context;


        public AdministratorController(MojContext context) => _context = context;

        public IActionResult Index()
        {
            KorisnickiNalog korisnik = HttpContext.GetLogiraniKorisnik();
            if (korisnik==null)
            {
                TempData["error_poruka"] = "Nemate pravo pristupa!";
                return RedirectToAction("Index", "Autentifikacija", new { area = "" });
            }

            AdministratorIndexVM admini = new AdministratorIndexVM {

                Administratori=_context.Administrators.Select(x=>new AdministratorIndexVM.Row {

                    ImePrezime=x.Ime+" "+x.Prezime,
                    Username=x.KorisnickoIme,
                    Password=x.Lozinka
                } ).ToList()
            };

            return View(admini);
        }

        public IActionResult Dodaj()
        {

            AdministratorDodajVM dodajVM = new AdministratorDodajVM
            {
                Gradovi = _context._Grad.Select(x => new SelectListItem
                {
                    Value = x.GradId.ToString(),
                    Text = x.Naziv
                }).ToList()
            };

            return View(dodajVM);
        }

        public IActionResult Snimi(AdministratorDodajVM obj)
        {
            KorisnickiNalog korisnik = new KorisnickiNalog
            {
                Username = obj.Username,
                Password = obj.Password,
                Zapamti = false
            };

            _context.korisnickiNalogs.Add(korisnik);
            _context.SaveChanges();

            Administrator noviAdmin = new Administrator
            {
                Ime = obj.Ime,
                Prezime = obj.Prezime,
                DatumRodenja = obj.DatumRodjenja,
                Email = obj.Email,
                Telefon = obj.Telefon,
                KorisnickoIme = obj.Username,
                Lozinka = obj.Password,
                GradId = obj.GradID,
                KorisnickiNalogID = _context.korisnickiNalogs.Where(x=>x.Username==obj.Username).First().KorisnickiNalogID
            };

            _context.Administrators.Add(noviAdmin);

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Obrisi()
        {
            //ovo je samo testiranje treba doradit posto sve korisnicke naloge brise i administratore

            List<KorisnickiNalog> nalozi = _context.korisnickiNalogs.ToList();

            foreach (var item in nalozi)
            {
                _context.korisnickiNalogs.Remove(item);
            }

            List<Administrator> administrators = _context.Administrators.ToList();

            foreach (var item in administrators)
            {
                _context.Administrators.Remove(item);
            }

            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}