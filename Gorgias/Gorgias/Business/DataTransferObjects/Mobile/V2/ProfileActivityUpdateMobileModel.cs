namespace Gorgias.Business.DataTransferObjects.Mobile.V2
{
    public class ProfileActivityUpdateMobileModel
    {
        public int ProfileID { get; set; }
        public int AlbumID { get; set; }
        public int ActivityTypeID { get; set; }
        public int ProfileActivityCount { get; set; }
        public int? ProfileActivityParentID { get; set; }
        public bool ProfileActivityIsFirst { get; set; }

        public System.Collections.Generic.IList<ContentLikeMobileModel> ContentLikes { get; set; }

        public string Share { get; set; }
        public string Location { get; set; }
    }
}