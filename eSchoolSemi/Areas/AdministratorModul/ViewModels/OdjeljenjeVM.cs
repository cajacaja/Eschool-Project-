using eSchool.Data.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eSchoolSemi.Web.Areas.AdministratorModul.ViewModels
{
    public class OdjenjenjeVM
    {



        public Odjeljenje Odjejlenje { get; set; }

        public string PredstavnikOdabrani { get; set; }

        public List<SelectListItem> Predstavnik { get; set; }

        public List<SelectListItem> Razrednik { get; set; }

        public List<SelectListItem> NastavniPlan { get; set; }

    }
}
