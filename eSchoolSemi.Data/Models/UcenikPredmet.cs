using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace eSchool.Data.Models
{
    public class UcenikPredmet
    {
        [Key]
        public int UcenikPredmetId { get; set; }
        public int ZakljucniaOcjena { get; set; }
        public string OpisOcjene { get; set; }
        public string Napomena { get; set; }
        [DataType(DataType.Date)]
        public DateTime DatumOcjenjivanja { get; set; }
        public int? NastavniPlanPredmetId { get; set; }
        public NastavniPlanPredmet NastavniPlanPredmet { get; set; }
        public int? UpisUOdjeljenjeId { get; set; }
        public UpisUOdjeljenje UpisUOdjeljenje { get; set; }

    }
}
