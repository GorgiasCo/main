using System;

namespace Gorgias.Business.DataTransferObjects.Report
{
    public class AlbumLike
    {
        public int AlbumID { get; set; }
        public int CategoryID { get; set; }
        public int AlbumView { get; set; }
        public int AlbumAvailability { get; set; }
        public int ProfileID { get; set; }
        public string CategoryName { get; set; }
        public string AlbumName { get; set; }
        public DateTime AlbumDatePublish { get; set; }
        public Int64 Likes { get; set; }
        public string getProfileImage { get { return "https://gorgiasasia.blob.core.windows.net/images/profile-" + ProfileID + ".jpg?timestamp=" + System.DateTime.Now.Second.ToString(); } }
    }
}