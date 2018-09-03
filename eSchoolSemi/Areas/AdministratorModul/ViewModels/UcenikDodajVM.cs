﻿using eSchool.Data.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eSchoolSemi.Web.Areas.AdministratorModul.ViewModels
{
    public class UcenikDodajVM
    {

        public int UcenikID { get; set; }

        public string Ime { get; set; }

        public string Prezime { get; set; }

        public DateTime DatumRodjenja { get; set; }

        public string Email { get; set; }

        public string Telefon { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public int BrojUDnevniku { get; set; }

        public int? OdjeljenjeID{ get; set; }     
        public List<SelectListItem> Odjeljenja { get; set; }

        public int? RoditeljID { get; set; }
        public List<SelectListItem> Roditleji { get; set; }

        public int? GradID { get; set; }
        public List<SelectListItem> Gradovi { get; set; }
    }
}
