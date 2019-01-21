using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eSchool.Data.Models;
using eSchoolSemi.Data;
using eSchoolSemi.Web.Areas.AdministratorModul.ViewModels;
using eSchoolSemi.Web.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace eSchoolSemi.Web.Areas.AdministratorModul.Controllers
{
    [Area("AdministratorModul")]
    [Autorizacija(false, false, false, true)]
    public class AjaxOdjeljenjeController : Controller
    {
        private MojContext _context;
        public AjaxOdjeljenjeController(MojContext context) => _context = context;

        public IActionResult Index(int odjeljenjeId)
        {
            Odjeljenje prikazOdljenje = _context._Odjeljenje.FirstOrDefault(x => x.OdjeljenjeId == odjeljenjeId);

            List<UpisUOdjeljenje> upisi = _context._UpisUOdjeljenje.Where(x => x.OdjeljenjeId == prikazOdljenje.OdjeljenjeId).Include(x => x.Ucenik).ToList();

            AjaxPrikazVM uceniciOdljenja = new AjaxPrikazVM
            {
                OdjeljenjeID= odjeljenjeId,

                Ucenici = _context._UpisUOdjeljenje.Where(x => x.OdjeljenjeId == prikazOdljenje.OdjeljenjeId).Select(x => new AjaxPrikazVM.Row
                {

                    BrojUDnevniku = x.BrojUDnevniku,
                    ImePrezimeUcenika = x.Ucenik.Ime + " " + x.Ucenik.Prezime,
                    UcenikID = x.UcenikId
                }).ToList()
            };

            uceniciOdljenja.Ucenici = uceniciOdljenja.Ucenici.OrderBy(x => x.BrojUDnevniku);


            return PartialView(uceniciOdljenja);
        }

        [HttpGet]
        public IActionResult DetaljiUcenik(int UcenikId) {

            Ucenik nekiUcenik= _context._Ucenik.Where(x => x.KorisnikId == UcenikId).Include(x => x.Roditelj).Include(x => x.MjestoRodenja).FirstOrDefault();
            string ImePrezimRoditelja = "Prazno";
            if (nekiUcenik.RoditeljId!=null)
            {
                ImePrezimRoditelja = nekiUcenik.Roditelj.Ime + " " + nekiUcenik.Roditelj.Prezime;
            }

            UcenikDetalj ucenik = new UcenikDetalj {

                Slika=nekiUcenik.KorisnickaSlika,
                ImeIprezime=nekiUcenik.Ime+" "+nekiUcenik.Prezime,
                DatumRodjenja=nekiUcenik.DatumRodenja.ToShortDateString(),
                Roditelj=ImePrezimRoditelja,
                MjestoPrebivalista=nekiUcenik.MjestoRodenja.Naziv
            };

            return PartialView(ucenik);
        }

        public IActionResult PromjeniOdjeljenje(int OdjeljenjeId,int UcenikId) {


            var odjeljenje = _context._Odjeljenje.First(x => x.OdjeljenjeId == OdjeljenjeId);
            var ucenik = _context._Ucenik.First(x => x.KorisnikId == UcenikId);

            var Odjejlenja = _context._Odjeljenje.Where(x =>x.GodinaStudijaId==odjeljenje.GodinaStudijaId && x.OdjeljenjeId!=odjeljenje.OdjeljenjeId).ToList();

            Odjejlenja = Odjejlenja.OrderBy(x => x.Oznaka).ToList();

            AjaxUcenikOdjeljenjeVM spisakOdjeljenja = new AjaxUcenikOdjeljenjeVM {
                OdjeljenjeID = odjeljenje.OdjeljenjeId,
                UcenikId=ucenik.KorisnikId,
                Naziv=ucenik.Prezime+" "+ucenik.Ime,
                Odjeljenja= Odjejlenja.Select(x=>new SelectListItem {

                    Value=x.OdjeljenjeId.ToString(),
                    Text=x.Oznaka

                }).ToList()

            };




            return PartialView(spisakOdjeljenja);
        }

        public IActionResult SnimiPrebaceno(AjaxUcenikOdjeljenjeVM ucenik) {

            var upisUcenik = _context._UpisUOdjeljenje.First(x => x.UcenikId == ucenik.UcenikId && x.OdjeljenjeId == ucenik.OdjeljenjeID);

            upisUcenik.OdjeljenjeId = ucenik.OdjeljenjePrebacenoId;
            _context.Update(upisUcenik);

            List<UpisUOdjeljenje> listaUcenika = _context._UpisUOdjeljenje.Where(x => x.OdjeljenjeId == ucenik.OdjeljenjePrebacenoId).ToList();


            int brojac = 1;

            foreach (var temp in listaUcenika)
            {
                temp.BrojUDnevniku = brojac;
                brojac++;
            }

            brojac = 1;

            _context.SaveChanges();


            return RedirectToAction(nameof(Index),new { odjeljenjeId=ucenik.OdjeljenjeID });

        }


    }
}