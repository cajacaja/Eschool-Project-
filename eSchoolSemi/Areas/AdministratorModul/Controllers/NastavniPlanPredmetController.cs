using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eSchool.Data.Models;
using eSchoolSemi.Data;
using eSchoolSemi.Web.Areas.AdministratorModul.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace eSchoolSemi.Web.Areas.AdministratorModul.Controllers
{
    [Area("AdministratorModul")]
    public class NastavniPlanPredmetController : Controller
    {
        private MojContext _context;
        public NastavniPlanPredmetController(MojContext context) => _context = context;



        public IActionResult Index()
        {
            return View();
        }


        public IActionResult DodajPlanPredmet()
        {
            NastPPVM ppViewModel = new NastPPVM();

            //Lista za nastavni plan
            ppViewModel.nastavniPlan = _context._NastavniPlan.Select(x => new SelectListItem
            {

                Value = x.NastavniPlanId.ToString(),
                Text = x.Naziv
            }).ToList();

            //Lista za predmete
            ppViewModel.predmet = _context._Predmet.Select(x => new SelectListItem
            {

                Value = x.PredmetId.ToString(),
                Text = x.Naziv
            }).ToList();

            return View(ppViewModel);
        }

        [HttpPost]
        public IActionResult DodajPlanPredmet(NastPPVM obj)
        {
            //Upisi informacije u novi obj NastavniPlanPredmet
            NastavniPlanPredmet noviObj = new NastavniPlanPredmet
            {
                Sifra = obj.sifra,
                BrojSati = obj.brojSati,
                GodinaStudiranja = obj.godinaStudiranja,
                NastavniPlanId = obj.nastavniPlanID,
                PredmetId = obj.predmetID
            };

            _context._NastavniPlanPredmet.Add(noviObj);
            _context.SaveChanges();

            //sad treba angazovat nastavnika na ovja predmet
            //prvo cu napravit index za ovaj kontroler
            //pa cu stavit angazman akciju mozda
            return RedirectToAction("Index","NastavniPlan");
         }
    }
   
}