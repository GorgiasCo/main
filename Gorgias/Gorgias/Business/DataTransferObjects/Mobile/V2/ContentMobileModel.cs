using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;

namespace Gorgias.Business.DataTransferObjects.Mobile.V2
{
    public class ContentMobileModel
    {
        public int ContentID { get; set; }
        public string ContentTitle { get; set; }
        public string ContentURL { get; set; }

        public string cdnContentURL
        {
            get
            {
                return ContentURL.Replace("https://gorgiasasia.blob.core.windows.net/", "https://gorgiascdn.azureedge.net/");
                //return AlbumCover.Replace("https://gorgiasasia.blob.core.windows.net/", "https://gorgiasresizer.azurewebsites.net/");
            }
        }

        //Content Extra info
        public bool ContentStatus { get; set; }
        public bool ContentIsDeleted { get; set; }
        public DateTime ContentCreatedDate { get; set; }
        public int AlbumID { get; set; }

        //Content Statistic
        public int? ContentLike { get; set; }
        public int? ContentComments { get; set; }

        public virtual IList<ContentCommentMobileModel> TopComments { get; set; }

        //Content GeoLocation info
        public DbGeography ContentGeoLocation { get; set; }

        //Content Dimension Info
        public string ContentDimension { get; set; }

        public string[] Dimension
        {
            get
            {
                if (ContentDimension != null)
                {
                    return ContentDimension.Split('-');
                } else
                {
                    return null;
                }
            }
        }
        public int ContentWidth
        {
            get
            {
                if(Dimension != null)
                {
                    if (Dimension.Count() > 0)
                    {
                        return int.Parse(Dimension[0]);
                    }
                    else
                    {
                        return 0;
                    }
                } else
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

        //Content Type Info
        public int ContentTypeID { get; set; }
        public string ContentTypeExpression { get; set; }
        //public virtual ContentTypeDTO ContentType { get; set; }
    }
}