using System;

namespace Gorgias.Business.DataTransferObjects.Report
{
    public class AlbumResult
    {        
        public Int64 Likes { get; set; }
        public int Albums { get; set; }
        public int AlbumViews { get; set; }
        public int ProfileID { get; set; }
        public string getProfileImage { get { return "https://gorgiasasia.blob.core.windows.net/images/profile-" + ProfileID + ".jpg?timestamp=" + System.DateTime.Now.Second.ToString() ; } }
    }
}