using System;
using System.Collections.Generic;
using System.Linq;

namespace Gorgias.Business.DataTransferObjects.Mobile
{
    public class AlbumMobileModel
    {
        public int AlbumID { get; set; }
        public string AlbumName { get; set; }
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

        public bool? AlbumHasComment { get; set; }

        public int? AlbumComments { get; set; }

        public string blurAlbumCover
        {
            get
            {
                if (isValidate)
                {
                    return AlbumCover;
                } else
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
                //Because of album cover like, we added a album cover in Content table as a independent photo ;)

                //Contents.Insert(0, new ContentMobileModel { ContentURL = AlbumCover });
                //if (isValidate)
                //{
                //    Contents = Contents.All(t => t.ContentURL.Replace(",", ""));
                //}
                return Contents;
            }
        }
    }
}