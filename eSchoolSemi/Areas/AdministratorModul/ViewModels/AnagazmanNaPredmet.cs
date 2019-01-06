using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eSchoolSemi.Web.Areas.AdministratorModul.ViewModels
{
    public class AnagazmanNaPredmet
    {

        public string GodinaStudiranja { get; set; }

        public int BrojCasova { get; set; }

        public int NastavniPlanProgramID { get; set; }
        public List<SelectListItem> nastavniPlanProgram { get; set; }

        public int PredmetID { get; set; }
        public List<SelectListItem> predmet { get; set; }

        public int NastavnikID { get; set; }
        public List<SelectListItem> nastavnik { get; set; }
    }
}
