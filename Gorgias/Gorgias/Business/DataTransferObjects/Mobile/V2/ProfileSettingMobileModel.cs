using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gorgias.Business.DataTransferObjects.Mobile.V2
{
    public class ProfileSettingMobileModel
    {
        public int ProfileID { get; set; }
        public string ProfileLanguageApp { get; set; }
        public int? ProfileCityID { get; set; }
        public DateTime? ProfileBirthday { get; set; }
    }
}