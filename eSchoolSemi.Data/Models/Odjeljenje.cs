using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace eSchool.Data.Models
{
    public class Odjeljenje
    {
        [Key]
        public int OdjeljenjeId { get; set; }
        public string Oznaka { get; set; }
        public int Kapacitet { get; set; }

        public int? RazrednikId { get; set; }
        public Nastavnik Razrednik { get; set; }

        public int? PredstavnikId { get; set; }
        public Ucenik Predstavnik { get; set; }

        public int? NastavniPlanId { get; set; }
        public NastavniPlan NastavniPlan { get; set; }

        public int? GodinaStudijaId { get; set; }
        public GodinaStudija GodinaStudija  { get; set; }

    }
}
