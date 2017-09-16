using System;

namespace Gorgias.Business.DataTransferObjects.Mobile.V2
{
    public class MiniProfileMobileModel
    {
        public int ProfileID { get; set; }
        public string ProfileFullname { get; set; }
        public string ProfileFullnameEnglish { get; set; }
        public string CityName { get; set; }
        public string ProfileShortDescription { get; set; }
        public string IndustryName { get; set; }
        public string ProfileTypeName { get; set; }
        public string ProfileImage { get; set; }

        public bool isSubscribed { get; set; }

        public Int64? TotalConnections { get; set; }
        public Int64? TotalViews { get; set; }
        public Int64? TotalEngagements { get; set; }
    }
}