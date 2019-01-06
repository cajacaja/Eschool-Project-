using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eSchoolSemi.Web.Areas.AdministratorModul.ViewModels
{
    public class RasporedDodajVM
    {
        public string RasporedNaziv { get; set; }

        public int OdljenjeID { get; set; }

        //Mozda provjerit imal vec raspored uradjen za neko odjeljenje
        public IEnumerable<SelectListItem> Odjeljenja { get; set; }


    }
}
