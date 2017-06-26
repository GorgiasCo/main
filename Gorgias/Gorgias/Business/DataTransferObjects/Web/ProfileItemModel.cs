using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gorgias.Business.DataTransferObjects.Web
{
    public class ProfileItemModel
    {
        public int ProfileID { get; set; }
        public string ProfileImage { get; set; }
        public string ProfileFullname { get; set; }
        public string ProfileURL { get; set; }
        public int ProfileLike { get; set; }
        public int ProfileView { get; set; }
        public double? Distance { get; set; }
        public int SubscriptionTypeID { get; set; }
    }
}