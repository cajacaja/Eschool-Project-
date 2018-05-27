using eSchool.Data.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eSchoolSemi.Web.Areas.AdministratorModul.ViewModels
{
    public class UcenikDodajVM
    {

        public Ucenik Ucenik { get; set; }

        public int BrojUDnevniku { get; set; }

        public int OdjeljenjeID{ get; set; }
        public List<SelectListItem> Odjeljenja { get; set; }

        
        public List<SelectListItem> Roditleji { get; set; }


        public List<SelectListItem> Gradovi { get; set; }
    }
}
