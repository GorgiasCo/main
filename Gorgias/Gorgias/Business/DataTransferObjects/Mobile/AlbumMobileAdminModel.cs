using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gorgias.Business.DataTransferObjects.Mobile
{
    public class AlbumMobileAdminModel
    {
        public int AlbumID { get; set; }
        public string AlbumName { get; set; }
        public string AlbumCategoryName { get; set; }
        public int AlbumContents { get; set; }
        public string AlbumAvailabilityName { get; set; }
        public int? AlbumLike { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        public DateTime AlbumDateCreated { get; set; }
        public string AlbumCover { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        public DateTime AlbumDatePublish { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        public DateTime AlbumDateExpire { get; set; }
        public int AlbumAvailability { get; set; }

        public int AlbumComments { get; set; }
        public bool? AlbumHasComment { get; set; }

        public string blurAlbumCover
        {
            get
            {
                if (isValidate)
                {
                    return AlbumCover;
                }
                else
                {
                    return AlbumCover.Replace("hottest-album-", "blur-hottest-album-");
                }
            }
        }

        public bool isValidate
        {
            get
            {
                if (AlbumDateExpire > DateTime.UtcNow)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool isUpdated
        {
            get
            {
                if (AlbumDatePublish < AlbumDateCreated)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        [Newtonsoft.Json.JsonIgnore]
        public IList<ContentMobileModel> Contents { get; set; }

        public string MomentDate
        {
            get
            {
                return Infrastruture.Core.DateToMoment.TimeAgo(AlbumDateCreated);
            }
        }

        public IList<ContentMobileModel> Images
        {
            get
            {
                //We added Album Cover into Content
                //Contents.Insert(0, new ContentMobileModel { ContentURL = AlbumCover });
                return Contents;
            }
        }
    }
}