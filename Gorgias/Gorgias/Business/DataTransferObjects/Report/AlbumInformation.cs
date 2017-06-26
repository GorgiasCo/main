using System;

namespace Gorgias.Business.DataTransferObjects.Report
{
    public class AlbumInformation
    {        
        public Int64 Likes { get; set; }
        public int ProfileID { get; set; }
        public string AlbumCover { get; set; }
        public string AlbumName { get; set; }
        public int AlbumView { get; set; }
        public string getProfileImage { get { return "https://gorgiasasia.blob.core.windows.net/images/profile-" + ProfileID + ".jpg?timestamp=" + System.DateTime.Now.Second.ToString(); } }        
    }
}