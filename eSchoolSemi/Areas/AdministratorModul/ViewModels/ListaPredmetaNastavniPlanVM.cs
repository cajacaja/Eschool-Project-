using eSchool.Data.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eSchoolSemi.Web.Areas.AdministratorModul.ViewModels
{
    public class ListaPredmetaNastavniPlanVM
    {
        public string Naziv { get; set; }

        public List<NastavniPlanPredmet> Npp { get; set; }
        public List<SelectListItem> Predmeti{ get; set; }


    }
}
