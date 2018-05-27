using eSchool.Data.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eSchoolSemi.Web.Areas.AdministratorModul.ViewModels
{
    public class RoditeljDodajViewModel
    {
        [Remote("ProvjeriTelefon", "RoditeljController", "AdministratorModul")]
        public Roditelj Roditelj { get; set; }

        public List<Grad> Gradovi { get; set; }
    }
}
