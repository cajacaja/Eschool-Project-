using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eSchool.Data.Models;
using eSchoolSemi.Data;
using Microsoft.AspNetCore.Mvc;

namespace eSchoolSemi.Web.Areas.AdministratorModul.Controllers
{

    [Area("AdministratorModul")]
    public class DodajController : Controller
    {
        private MojContext _context;

        public DodajController(MojContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<Grad> model = _context._Grad.ToList();                       
            return View("Index",model);
        }
    }
}