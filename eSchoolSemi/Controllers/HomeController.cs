using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using eSchoolSemi.Models;
using eSchoolSemi.Data;
using eSchoolSemi.Web.Helper;
using eSchool.Data.Models;

namespace eSchoolSemi.Controllers
{
    [Autorizacija(true, true, true, true)]
    public class HomeController : Controller
    {

        private readonly MojContext _context;

        public HomeController(MojContext context) => _context = context;

        public IActionResult Index()
        {
            
            return View();
        }


        public JsonResult GetSearchValue(string search)
        {

            List<Grad> allsearch = _context._Grad.Where(x => x.Naziv.StartsWith(search)).Select(x => new Grad {

                GradId = x.GradId,
                Naziv = x.Naziv
            }).ToList();
            
            return new JsonResult(allsearch);
        }


    }
}
