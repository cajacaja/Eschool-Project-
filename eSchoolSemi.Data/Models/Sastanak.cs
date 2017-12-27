
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace eSchool.Data.Models
{
    public class Sastanak
    {
        [Key]
        public int SastanakId { get; set; }
        public string Naziv { get; set; }
        public int? SastanakTipId { get; set; }
        public SastanakTip SastanakTip { get; set; }
        public int? OdjeljenjeId { get; set; }
        public Odjeljenje Odjeljenje { get; set; }
    }
}
