using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eSchoolSemi.Web.Areas.AdministratorModul.ViewModels
{
    public class AdministratorIndexVM
    {


        public List<Row> Administratori { get; set; }

        public class Row {

            public string ImePrezime { get; set; }

            public string Username { get; set; }

            public string Password { get; set; }
        }
        
    }
}
