using System.Data.Entity.Spatial;

namespace Gorgias.Business.DataTransferObjects.Mobile.V2
{
    public class ContentUpdateMobileModel
    {
        public int ContentID { get; set; }
        public string ContentTitle { get; set; }
        public string ContentURL { get; set; }
       
        //Content Extra info
        //public bool ContentStatus { get; set; }
        //public bool ContentIsDeleted { get; set; }
        //public int AlbumID { get; set; }

        //Content GeoLocation info
        public DbGeography ContentGeoLocation { get; set; }

        //Content Dimension Info
        public string ContentDimension { get; set; }
        
        //Content Type Info
        public int ContentTypeID { get; set; }
    }
}