using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace eSchool.Data.Models
{
    public class ZakljucnaOcjena
    {
        [Key]
        public int ZakljucnaOcjenaid { get; set; }
        public int? OdrzanCasDetaljiId { get; set; }
        public OdrzanCasDetalji OdrzanCasDetalji { get; set; }
        public int? UcenikPredmetId { get; set; }
        public UcenikPredmet UcenikPredmet { get; set; }
        [DataType(DataType.Date)]
        public DateTime DadumZakljucivanja { get; set; }
    }
}
