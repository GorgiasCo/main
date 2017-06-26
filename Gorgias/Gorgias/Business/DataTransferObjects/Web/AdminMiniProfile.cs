using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gorgias.Business.DataTransferObjects.Web
{
    public class AdminMiniProfile
    {
        public int ProfileID { get; set; }
        public string ProfileFullname { get; set; }
        public string ProfileImage { get; set; }
        public string CountryName { get; set; }
        public string IndustryName { get; set; }
        public string ProfileTypeName { get; set; }
    }
}