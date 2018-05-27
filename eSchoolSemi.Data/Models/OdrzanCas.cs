using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace eSchool.Data.Models
{
    public class OdrzanCas
    {
        [Key]
        public int OdrzanCasId { get; set; }
        [DataType(DataType.Date)]
        public DateTime DatumOdrzavanja { get; set; }

        public int? AngazovanId { get; set; }
        public Angazovan Angazovan { get; set; }
    }
}
