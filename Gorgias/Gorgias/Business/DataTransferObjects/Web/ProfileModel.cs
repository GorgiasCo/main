using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gorgias.Business.DataTransferObjects.Web
{
    public class ProfileModel
    {
        public string CountryName { get; set; }
        public string CountryShortName { get; set; }
        public int ProfileID { get; set; }
        public string ProfileEmail { get; set; }
        public string ProfileFullname { get; set; }
        public bool ProfileIsPeople { get; set; }
        public int ProfileLike { get; set; }
        public string ProfileDescription { get; set; }
        public string ProfileURL { get; set; }
        public string ProfileShortDescription { get; set; }
        public string ProfileImage { get; set; }
        public string Expr1 { get; set; }
        public string ProfileTypeName { get; set; }
        public string ThemeClassCode { get; set; }
        public string IndustryName { get; set; }
        public string SubscriptionTypeName { get; set; }
        public int SubscriptionTypeID { get; set; }
        public int ProfileTypeID { get; set; }
        public int? RequestedProfileID { get; set; }
    }
}