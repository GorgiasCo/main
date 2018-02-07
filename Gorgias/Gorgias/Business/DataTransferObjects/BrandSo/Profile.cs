using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gorgias.Business.DataTransferObjects.BrandSo
{
    public class Profile
    {
        public int ProfileID { get; set; }
        public string ProfileFullname { get; set; }
        public bool ProfileIsConfirmed { get; set; }
        public string ProfileImage { get; set; }       
        public string ProfileEmail { get; set; } 
        public string Country { get; set; }
        public string CustomURL { get; set; }        
        public string CurrencyCode { get; set; }
    }
}