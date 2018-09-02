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
    public class NastavniPlanController : Controller
    {
        private MojContext _context;
        public NastavniPlanController(MojContext context) => _context = context;

        public IActionResult Index()
        {
           

            List<NastavniPlan> lista = _context._NastavniPlan.ToList();
            return View(lista);
        }

        public IActionResult DodajNastavniPlan()
        {
            NastavniPlan novi = new NastavniPlan();

            return View(novi);
        }

        [HttpPost]
        public IActionResult DodajNastavniPlan(NastavniPlan plan)
        {

            _context._NastavniPlan.Add(plan);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }



        public IActionResult DodajPredmet()
        {
            Predmet noviPredmet = new Predmet();

            return View(noviPredmet);
        }

        [HttpPost]
        public IActionResult DodajPredmet(Predmet novi)
        {
            _context._Predmet.Add(novi);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Obrisi(int PredmetID)
        {
            _context._Predmet.Remove(_context._Predmet.Find(PredmetID));
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        //Koje sve predmete ima nastavni plan i program
        public IActionResult Detalji(int nastavniPlanId)
        {
            ListaPredmetaNastavniPlanVM temp = new ListaPredmetaNastavniPlanVM();
            temp.Naziv = _context._NastavniPlan.FirstOrDefault(x => x.NastavniPlanId == nastavniPlanId).Naziv;

            temp.Npp = _context._NastavniPlanPredmet.Where(x => x.NastavniPlanId == nastavniPlanId).ToList();

            temp.Predmeti = _context._Predmet.Select(x => new SelectListItem
            {

                Value = x.PredmetId.ToString(),
                Text = x.Naziv
            }).ToList();



            return View(temp);
        }


        //Opcija da angazuje Nastavnika na predmete koji su na listi
        //Sada da li je naziv predmeta i zvanje isto? dali da mi to bude provjera?

        public async Task<IActionResult> AnagazujNastavnika(int nastavniPlanPredmetID)
        {
            AngazujNastavnikVM nastavnikNaPredmetu =new AngazujNastavnikVM { NastavniPlanPredmetID = nastavniPlanPredmetID };

            NastavniPlanPredmet temp = await _context._NastavniPlanPredmet.FirstOrDefaultAsync(x => x.NastavniPlanPredmetId == nastavniPlanPredmetID);

            //Daj mi sva odjeljenja koja imaju ovaj nastavni plan i predmet tako kad azuriram nastavnika za neko odjeljenje,zelim da ima izbor
            //odjeljenja koji imaju taj nastavni plan i program
            List<Odjeljenje> odjeljenjeNPP = await _context._Odjeljenje.Where(x => x.NastavniPlanId == temp.NastavniPlanId).ToListAsync();


            nastavnikNaPredmetu.Odjeljenja = odjeljenjeNPP.Select(x => new SelectListItem
            {
                Value = x.OdjeljenjeId.ToString(),
                Text = x.Oznaka
            }).ToList();

            nastavnikNaPredmetu.Nastavnici =  _context._Nastavnik.Select(x => new SelectListItem
            {
                Value = x.KorisnikId.ToString(),
                Text = x.Titula + "." + x.Ime + " " + x.Prezime
            }).ToList();


            return View(nastavnikNaPredmetu);
        }

        [HttpPost]
        public IActionResult AnagazujNastavnika(AngazujNastavnikVM angazuj)
        {
            Angazovan angazovan = new Angazovan();
            angazovan.NastavnikId = angazuj.NastavnikID;
            angazovan.NastavniPlanPredmetId = angazuj.NastavniPlanPredmetID;
            angazovan.OdjeljenjeId = angazuj.OdjeljenjeID;
          



            _context._Angazovan.Add(angazovan);
            _context.SaveChanges();


            return RedirectToAction("Index");
        }

        public IActionResult ObrisiNastavniPlan(int nastavniPlanId)
        {
            //Brisanje citavog nastavnog programa sto znaci sve predmete na njemu sve azurirane nastavnike na tim predmetima i nulliranje 
            //OdljenjaID tako da ne pravi problem prilikom brisanja(sada bit trebao dodati uredivanja odljenja sto se tice nastavnog plana)
            NastavniPlan nastavniPlan = _context._NastavniPlan.FirstOrDefault(x => x.NastavniPlanId == nastavniPlanId);

            if (nastavniPlan == null) { return Content("Nema tog nastavnog plana"); }

            List<NastavniPlanPredmet> predmeti = _context._NastavniPlanPredmet.Where(x => x.NastavniPlanId == nastavniPlan.NastavniPlanId).ToList();

            if (predmeti !=null)
            {
                List<Angazovan> nastavnici = new List<Angazovan>();
                foreach (var item in predmeti)
                {
                    nastavnici.Add(_context._Angazovan.FirstOrDefault(x => x.NastavniPlanPredmetId == item.NastavniPlanPredmetId));
                }

                if (nastavnici!=null)
                {
                    foreach (var item in nastavnici)
                    {
                        _context._Angazovan.Remove(_context._Angazovan.Find(item.AngazovanId));
                        
                    }
                    _context.SaveChanges();
                }

                foreach (var item in predmeti)
                {
                    _context._NastavniPlanPredmet.Remove(_context._NastavniPlanPredmet.Find(item.NastavniPlanPredmetId));
                }
                _context.SaveChanges();
            }
            
            List<Odjeljenje> Odljeljnja = _context._Odjeljenje.Where(x => x.NastavniPlanId == nastavniPlanId).ToList();
            if (Odljeljnja != null) {
                foreach (var item in Odljeljnja)
                {
                    _context._Odjeljenje.Find(item.OdjeljenjeId).NastavniPlanId = null;

                }
                _context.SaveChanges();
            }
           

            _context._NastavniPlan.Remove(_context._NastavniPlan.Find(nastavniPlanId));
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult ObrisiPredmetIzNPP(int nastavniPlanProgramID)
        {

            NastavniPlanPredmet obrisiPredmet = _context._NastavniPlanPredmet.FirstOrDefault(x => x.NastavniPlanPredmetId == nastavniPlanProgramID);

            Angazovan angazovanNastavnikPredmet = _context._Angazovan.FirstOrDefault(x => x.NastavniPlanPredmetId == obrisiPredmet.NastavniPlanPredmetId);

            if (angazovanNastavnikPredmet!=null)
            {
                _context._Angazovan.Remove(_context._Angazovan.Find(angazovanNastavnikPredmet.AngazovanId));
                _context.SaveChanges();
            }

            int? nastavniPlanId = obrisiPredmet.NastavniPlanId;

            _context._NastavniPlanPredmet.Remove(_context._NastavniPlanPredmet.Find(obrisiPredmet.NastavniPlanPredmetId));
            _context.SaveChanges();
            
            //trebam skontat kako pass value prema akciji a da ne bude null kad gore dodje ne znam sad zasto se to desava
            return View("Index");
        }
    }
}