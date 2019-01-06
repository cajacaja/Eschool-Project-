using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using eSchoolSemi.Models;
using eSchoolSemi.Data;
using eSchoolSemi.Web.Helper;

namespace eSchoolSemi.Controllers
{
    [Autorizacija(true, true, true, true)]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            
            return View();
        }

       
    }
}
