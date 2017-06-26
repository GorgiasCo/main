using Gorgias.Infrastruture.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gorgias.Business.DataTransferObjects.Web.List
{
    public class ContentItemModel
    {
        public string ContentURL { get; set; }
        public int AlbumID { get; set; }
        public string ContentTitle { get; set; }
        public int ContentType { get; set; }
        public string CategoryName { get; set; }
        public DateTime? ContentCreatedDate { get; set; }
        public int ProfileID { get; set; }
        public int CategoryID { get; set; }
        public int ContentID { get; set; }

        public string MomentDate
        {
            get
            {
                if (ContentCreatedDate.HasValue)
                {
                    return DateToMoment.TimeAgo(ContentCreatedDate.Value);
                }
                else
                {
                    return "";
                }
            }
        }
    }
}