using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace eSchool.Data.Models
{
    public class SastanakRoditelj
    {
        [Key]
        public int SastanakRoditeljId { get; set; }
        public int? RoditeljId { get; set; }
        public Roditelj Roditelj { get; set; }
        public int? NastavnikId { get; set; }
        public Nastavnik Nastavnik { get; set; }
        public int? SastanakId { get; set; }
        public Sastanak Sastanak { get; set; }
        public string Komentar { get; set; }
        [DataType(DataType.Date)]
        public DateTime DatumSastanka { get; set; }
    }
}
