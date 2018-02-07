using System.Data.Entity.Spatial;
using System.Linq;

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

        public string[] Dimension
        {
            get
            {
                if (ContentDimension != null)
                {
                    return ContentDimension.Split('-');
                }
                else
                {
                    return null;
                }
            }
        }

        public int ContentWidth
        {
            get
            {
                if (Dimension != null)
                {
                    if (Dimension.Count() > 0)
                    {
                        return int.Parse(Dimension[0]);
                    }
                    else
                    {
                        return 0;
                    }
                }
                else
                {
                    return 0;
                }
            }
        }

        public int ContentHeight
        {
            get
            {
                if (Dimension != null)
                {
                    if (Dimension.Count() > 0)
                    {
                        return int.Parse(Dimension[1]);
                    }
                    else
                    {
                        return 0;
                    }
                }
                else
                {
                    return 0;
                }
            }
        }
    }
}