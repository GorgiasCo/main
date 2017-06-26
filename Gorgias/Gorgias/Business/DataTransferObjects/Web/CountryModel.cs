using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gorgias.Business.DataTransferObjects.Web
{
    public class CountryModel
    {
        public int CountryID { get; set; }
        public string CountryName { get; set; }
        public string CountryShortName { get; set; }
    }
}