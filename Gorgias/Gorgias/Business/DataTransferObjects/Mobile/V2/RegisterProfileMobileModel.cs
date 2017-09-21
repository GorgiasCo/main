using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gorgias.Business.DataTransferObjects.Mobile.V2
{
    public class RegisterProfileMobileModel
    {
        public string ProfileFullname { get; set; }
        public string ProfileFullnameEnglish { get; set; }
        public int ProfileID { get; set; }
        public int CityID { get; set; }
        public int IndustryID { get; set; }
        public int ProfileTypeID { get; set; }
        public string ProfileShortDescription { get; set; }
        public DateTime ProfileBirthday { get; set; }
        public string ProfileEmail { get; set; }
        public string ProfilePassword { get; set; }
        public string ProfileLanguageApp { get; set; }
        public bool isFirstRegistration { get; set; }
    }
}