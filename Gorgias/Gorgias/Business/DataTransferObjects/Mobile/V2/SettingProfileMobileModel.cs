using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gorgias.Business.DataTransferObjects.Mobile.V2
{
    public class SettingProfileMobileModel
    {
        public int ProfileID { get; set; }
        public string ProfileFullname { get; set; }
        public string ProfileFullnameEnglish { get; set; }
        public string ProfileShortDescription { get; set; }
        public string ProfileImage { get; set; }

        public string ProfileURL { get; set; }
        public bool ProfileIsPeople { get; set; }
        public bool ProfileIsConfirmed { get; set; }

        public Int64? TotalConnections { get; set; }
        public Int64? TotalViews { get; set; }
        public Int64? TotalEngagements { get; set; }

        public Int64? TotalShares { get; set; }
        public Int64? TotalLikes { get; set; }
        public Int64? TotalFeel { get; set; }

    }
}