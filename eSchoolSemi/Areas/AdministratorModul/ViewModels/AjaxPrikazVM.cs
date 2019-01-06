using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eSchoolSemi.Web.Areas.AdministratorModul.ViewModels
{
    public class AjaxPrikazVM
    {

        public IEnumerable<Row> Ucenici { get; set; }

        public class Row {

            public int BrojUDnevniku { get; set; }

            public string  ImePrezimeUcenika { get; set; }

            public int UcenikID { get; set; }
        }
    }
}
