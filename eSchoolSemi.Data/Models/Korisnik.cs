using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace eSchool.Data.Models
{
    public class Korisnik
    {
        [Key]
        public int KorisnikId { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        [DataType(DataType.Date)]
        public DateTime DatumRodenja { get; set; }
        public string KorisnickoIme { get; set; }
        public string Lozinka { get; set; }

        public string Email { get; set; }

        public string  Telefon { get; set; }
        public Grad MjestoRodenja { get; set; }
        public int? GradId { get; set; }
    }
}
