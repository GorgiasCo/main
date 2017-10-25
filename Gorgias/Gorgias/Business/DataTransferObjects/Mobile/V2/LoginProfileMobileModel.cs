using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gorgias.Business.DataTransferObjects.Mobile.V2
{
    public class LoginProfileMobileModel
    {
        public string ProfileFullname { get; set; }
        public string ProfileFullnameEnglish { get; set; }
        public int ProfileID { get; set; }
        public int? CityID { get; set; }
        public string CityName { get; set; }
        public string CountryName { get; set; }
        public int? IndustryID { get; set; }
        public string IndustryName { get; set; }
        public int ProfileTypeID { get; set; }
        public string ProfileTypeName { get; set; }
        public string ProfileShortDescription { get; set; }
        public DateTime? ProfileBirthday { get; set; }
        public string ProfileEmail { get; set; }
        public string ProfilePassword { get; set; }
        public string ProfileLanguageApp { get; set; }
        public bool? isFirstRegistration { get; set; }
        public bool? ProfileIsConfirmed { get; set; }
        public bool? ProfileIsPeople { get; set; }
        public string ProfileImage { get; set; }
        public string ProfileURL { get; set; }
        public int UserID { get; set; }

        public string ProfileBirthdayTitle
        {
            get
            {
                return ProfileBirthday.HasValue ? ProfileBirthday.Value.ToString("MMMM dd, yyyy") : null;
            }
        }

        //Manage Joint Accounts ;)
        public IEnumerable<UserProfileMobileModel> Accounts { get; set; }
    }
}