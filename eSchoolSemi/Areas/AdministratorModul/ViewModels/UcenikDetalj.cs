﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eSchoolSemi.Web.Areas.AdministratorModul.ViewModels
{
    public class UcenikDetalj
    {

        public byte[] Slika { get; set; }

        public string ImeIprezime { get; set; }

        public string DatumRodjenja { get; set; }

        public string Roditelj { get; set; }

        public string MjestoPrebivalista { get; set; }
    }
}
