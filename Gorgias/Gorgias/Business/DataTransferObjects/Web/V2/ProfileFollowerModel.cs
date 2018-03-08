using System;

namespace Gorgias.Business.DataTransferObjects.Web.V2
{
    public class ProfileFollowerModel
    {
        public string ProfileFullname { get; set; }
        public int ProfileID { get; set; }
        public int RequestTypeID { get; set; }
        public bool ConnectionStatus { get; set; }
        public DateTime ConnectionDateCreate { get; set; }
        public string ProfileImage
        {
            get
            {
                return "https://gorgiasasia.blob.core.windows.net/images/profile-"+ ProfileID +".jpg" + "?timestamp=" + DateTime.Now.ToLongTimeString();
            }
        }
    }
}