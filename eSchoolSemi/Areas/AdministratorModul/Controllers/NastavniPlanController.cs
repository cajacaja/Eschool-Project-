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


        public IActionResult DodajPredmetUProgram() {

            AnagazmanNaPredmet angazovanje = new AnagazmanNaPredmet
            {
                nastavniPlanProgram=_context._NastavniPlan.Select(x=>new SelectListItem {

                    Value=x.NastavniPlanId.ToString(),
                    Text=x.Naziv
                }).ToList(),

                predmet=_context._Predmet.Select(x=>new SelectListItem {

                    Value = x.PredmetId.ToString(),
                    Text = x.Naziv
                }).ToList(),

                nastavnik= _context._Nastavnik.Select(x => new SelectListItem
                {

                    Value = x.KorisnikId.ToString(),
                    Text = x.Ime+" "+x.Prezime
                }).ToList()

            };


            return View(angazovanje);


        }

        public IActionResult SnimiPlanPredmet(AnagazmanNaPredmet viewModel) {

            NastavniPlanPredmet noviNastavniPredmet = new NastavniPlanPredmet {

                GodinaStudiranja=viewModel.GodinaStudiranja,
                BrojCasova=viewModel.BrojCasova,
                NastavniPlanId=viewModel.NastavniPlanProgramID,
                PredmetId=viewModel.PredmetID

            };

            _context._NastavniPlanPredmet.Add(noviNastavniPredmet);


            Angazovan angazovanNastavnik = new Angazovan
            {

                NastavnikId = viewModel.NastavnikID,
                NastavniPlanPredmetId = noviNastavniPredmet.NastavniPlanPredmetId
            };

            _context._Angazovan.Add(angazovanNastavnik);

            _context.SaveChanges();


            return RedirectToAction("Index");
        }

     


            //Koje sve predmete ima nastavni plan i program
            public IActionResult Detalji(int nastavniPlanId)
        {
            ListaPredmetaNastavniPlanVM temp = new ListaPredmetaNastavniPlanVM();

            temp.Naziv = _context._NastavniPlan.FirstOrDefault(x => x.NastavniPlanId == nastavniPlanId).Naziv;

            List<NastavniPlanPredmet> nastavniPredmeti = _context._NastavniPlanPredmet.Where(x => x.NastavniPlanId == nastavniPlanId).ToList();
                     

            temp.Angazovani = new List<ListaPredmetaNastavniPlanVM.Row>();

            foreach (var item in nastavniPredmeti)
            {
                temp.Angazovani.Add
                (
                    _context._Angazovan.Where(x => x.NastavniPlanPredmetId == item.NastavniPlanPredmetId).Select
                    (x => new ListaPredmetaNastavniPlanVM.Row
                    {
                        AngazovanID = x.AngazovanId,
                        NazivPredmeta = x.NastavniPlanPredmet.Predmet.Naziv,
                        BrojCasova = x.NastavniPlanPredmet.BrojCasova,
                        NazivNastavnika = (x.Nastavnik != null) ? x.Nastavnik.Ime + " " + x.Nastavnik.Prezime : "Nastavnik izbrisan"

                    }).First()
                
                );
            }

           



            return View(temp);
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
                        if(item!=null)
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



        public IActionResult ObrisiPredmet(int id) {

            Angazovan obrisiAngazovanog = _context._Angazovan.FirstOrDefault(x => x.AngazovanId == id);

            NastavniPlanPredmet obrisiPlanPredmet = _context._NastavniPlanPredmet.FirstOrDefault(x => x.NastavniPlanPredmetId == obrisiAngazovanog.NastavniPlanPredmetId);

            _context._NastavniPlanPredmet.Remove(obrisiPlanPredmet);

            _context._Angazovan.Remove(obrisiAngazovanog);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}