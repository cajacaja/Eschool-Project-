using eSchool.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eSchoolSemi.Web.Areas.AdministratorModul.ViewModels
{
    public class NastavnikGradViewModel
    {
        public Nastavnik Nastavnik { get; set; }
        public List<Grad> Gradovi { get; set; }
    }
}
