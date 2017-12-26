using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace eSchool.Data.Models
{
    public class NastavniPlan
    {
        [Key]
        public int NastavniPlanId { get; set; }
        public string Naziv { get; set; }
    }
}
