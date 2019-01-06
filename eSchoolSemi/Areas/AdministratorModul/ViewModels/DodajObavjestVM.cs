using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eSchoolSemi.Web.Areas.AdministratorModul.ViewModels
{
    public class DodajObavjestVM
    {

        public int? NastavniPlanID { get; set; }
        public List<SelectListItem> NastavniPlanovi { get; set; }

        public int? TipObavjestiID { get; set; }
        public List<SelectListItem> TipObavjesti { get; set; }

        [StringLength(50)]
        public string Naslov { get; set; }

        public string Sadrzaj { get; set; }
    }
}
