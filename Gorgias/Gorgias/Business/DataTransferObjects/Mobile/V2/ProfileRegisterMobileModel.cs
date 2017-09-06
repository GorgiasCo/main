using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gorgias.Business.DataTransferObjects.Mobile.V2
{
    public class ProfileRegisterMobileModel
    {
        public int ProfileID { get; set; }
        public string ProfileFullname { get; set; }
        public string ProfileFullnameEnglish { get; set; }
        public string ProfileEmail { get; set; }
        public int ProfileTypeID { get; set; }
        /// <summary>
        /// Industry uses as Profession, student, teacher, dr, badminton player
        /// </summary>
        public int IndustryID { get; set; }
        public int CityID { get; set; }
        public string ProfileShortDescription { get; set; }
        public string ProfilePassword { get; set; }
    }
}