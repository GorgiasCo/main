using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gorgias.Models
{
    public class vCountry
    {
        public int CityID { get; set; }
        public int CountryID { get; set; }

        public string CityName { get; set; }
        public bool CityStatus { get; set; }

        public string CountryName { get; set; }
    }
}