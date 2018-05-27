using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace eSchool.Data.Models
{
    public class NastavniPlanPredmet
    {
        [Key]
        public int NastavniPlanPredmetId { get; set; }

        public string Sifra { get; set; }
        public string GodinaStudiranja { get; set; }
        public int BrojSati { get; set; }

        public int? NastavniPlanId { get; set; }
        public NastavniPlan NastavniPlan { get; set; }

        public int? PredmetId { get; set; }
        public Predmet Predmet { get; set; }

        public List<Angazovan> Angazovani { get; set; }
    }
}
