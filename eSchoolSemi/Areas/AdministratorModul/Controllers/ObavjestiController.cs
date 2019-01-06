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
    public class ObavjestiController : Controller
    {
        private MojContext _context;


        public ObavjestiController(MojContext context) => _context = context;

        public IActionResult Index()
        {
            

            ObavjestiIndexVM obavjestiIndexVM = new ObavjestiIndexVM {

                Obavjesti=_context._Obavjestenje.Select(x=>new ObavjestiIndexVM.Row {

                    ObavjestID=x.ObavjestenjeId,
                    Naslov=x.Naslov,
                    Sadrzaj=x.Sadrzaj,
                    TipObavjesti=x.TipObavijesti.Tip,
                    Autor=(x.AdministatorID==0)?x.Nastavnik.Ime+" "+x.Nastavnik.Prezime:"Administracija",                                                
                    DatumPostavljanja=x.DatumObavjestenja.ToLongDateString()


                }).ToList()
            };



            return View(obavjestiIndexVM);
        }

        public IActionResult DodajObavjest()
        {
            DodajObavjestVM novaObavijest = new DodajObavjestVM
            {

                NastavniPlanovi = _context._NastavniPlan.Select(x => new SelectListItem
                {

                    Value = x.NastavniPlanId.ToString(),
                    Text = x.Naziv
                }).ToList(),

                TipObavjesti = _context._TipObavijesti.Select(x => new SelectListItem
                {


                    Value = x.TipObavijestiId.ToString(),
                    Text = x.Tip

                }).ToList()
            };



            return View(novaObavijest);
        }

        public IActionResult Snimi(DodajObavjestVM obj)
        {
            KorisnickiNalog korisnik = HttpContext.GetLogiraniKorisnik();

            Administrator admin = _context.Administrators.FirstOrDefault(x => x.KorisnickiNalogID == korisnik.KorisnickiNalogID);

            Obavjestenje obavjest = new Obavjestenje {

                Naslov=obj.Naslov,
                Sadrzaj=obj.Sadrzaj,
                TipObavijestiId=obj.TipObavjestiID,
                AdministatorID=admin.KorisnikId,
                DatumObavjestenja=DateTime.Today
            };

            
            _context._Obavjestenje.Add(obavjest);
            _context.SaveChanges();


            Obavjestenje temp = _context._Obavjestenje.FirstOrDefault(x => x.Sadrzaj == obj.Sadrzaj && x.Naslov == obj.Naslov);

            List<Odjeljenje> odjeljenjea = new List<Odjeljenje>();

            if (obj.NastavniPlanovi == null) {

                odjeljenjea = _context._Odjeljenje.ToList();
            }

            else
            {
                odjeljenjea = _context._Odjeljenje.Where(x => x.NastavniPlanId == obj.NastavniPlanID).ToList();
            }

            if (odjeljenjea != null) {

                foreach (var item in odjeljenjea)
                {
                    ObavjestenjeOdjeljenje novo = new ObavjestenjeOdjeljenje
                    {

                        OdjeljenjeID = item.OdjeljenjeId,
                        ObavjestenjeID = temp.ObavjestenjeId
                    };

                    _context.ObavjestenjeOdjeljenje.Add(novo);
                }

                _context.SaveChanges();
            }
            

            return RedirectToAction("Index");
        }

        public IActionResult Detalji(int ObavjestID) {

            Obavjestenje obavjestenje = _context._Obavjestenje.FirstOrDefault(x => x.ObavjestenjeId == ObavjestID);

            Nastavnik nastavnik = _context._Nastavnik.FirstOrDefault(x => x.KorisnikId == obavjestenje.NastavnikID);

            ObavjestiIndexVM.Row obavjest = new ObavjestiIndexVM.Row {

                Naslov = obavjestenje.Naslov,
                Sadrzaj = obavjestenje.Sadrzaj,
                TipObavjesti = _context._TipObavijesti.First(x => x.TipObavijestiId == obavjestenje.TipObavijestiId).Tip,
                Autor = (nastavnik != null) ? nastavnik.Ime + " " + nastavnik.Prezime : "Administracija",
                DatumPostavljanja = obavjestenje.DatumObavjestenja.ToLongDateString()

            };   

            return View(obavjest);
        }

    }
}