using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gorgias.Business.DataTransferObjects.Mobile.V2
{
    public class AlbumMobileModel
    {
        public int AlbumID { get; set; }
        public int ProfileID { get; set; }
        public string AlbumName { get; set; }

        //Album Statistic Section
        public int AlbumContents { get; set; }
        public int AlbumView { get; set; }
        public int? AlbumLike { get; set; }

        public string AlbumAvailabilityName { get; set; }

        public string AlbumCover { get; set; }

        //Album Created, Publish & Expiring Date
        [Newtonsoft.Json.JsonIgnore]
        public DateTime AlbumDateCreated { get; set; }
        
        //[Newtonsoft.Json.JsonIgnore]
        public DateTime AlbumDatePublish { get; set; }

        //[Newtonsoft.Json.JsonIgnore]
        public DateTime AlbumDateExpire { get; set; }


        public double AlbumExpiring
        {
            get
            {
                if(AlbumDateExpire > DateTime.UtcNow)
                {
                    return AlbumDateExpire.Subtract(DateTime.UtcNow).TotalDays;
                }
                return 0;
            }
        }

        public string AlbumExpiringTitle
        {
            get
            {
                return Infrastruture.Core.DateToMoment.TimeLeft(AlbumDateExpire);
            }
        }

        public bool canRepost
        {
            get
            {
                return !isValidate ? true : false;
            }
        }

        public int AlbumAvailability { get; set; }

        //Comment Section
        public bool? AlbumHasComment { get; set; }
        public int? AlbumComments { get; set; }

        //Reading Language Section
        public string AlbumReadingLanguageCode { get; set; }

        //Repost Section
        public int? AlbumRepostValue { get; set; }
        public int? AlbumRepostRequest { get; set; }
        public int? AlbumRepostAttempt { get; set; }

        //Price & Token Section
        public decimal? AlbumPrice { get; set; }
        public bool? AlbumIsTokenAvailable { get; set; }
        public int? AlbumPriceToken { get; set; }

        //Content Rating Section
        public int? ContentRatingID { get; set; }
        
        //Category Section
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }

        public bool? isFelt { get; set; }
        public bool? isSubscribed { get; set; }

        public double ExpiringTime
        {
            get
            {
                TimeSpan v = AlbumDateExpire - AlbumDatePublish;
                return v.TotalMinutes;
            }
        }

        public string cdnAlbumCover
        {
            get
            {
                //return "ddd";
                return AlbumCover.Replace("https://gorgiasasia.blob.core.windows.net/", "https://gorgiascdn.azureedge.net/");
                //return AlbumCover.Replace("https://gorgiasasia.blob.core.windows.net/", "https://gorgiasresizer.azurewebsites.net/");
            }
        }

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
                //Because of album cover like, we added a album cover in Content table as a independent photo ;)

                //Contents.Insert(0, new ContentMobileModel { ContentURL = AlbumCover });
                //if (isValidate)
                //{
                //    Contents = Contents.All(t => t.ContentURL.Replace(",", ""));
                //}
                return Contents;
            }
        }

        public IList<ContentLikeMobileModel> ContentLikes
        {
            get
            {
                //Because of album cover like, we added a album cover in Content table as a independent photo ;)
                IList<ContentLikeMobileModel> result = new List<ContentLikeMobileModel>();
                if(Images != null)
                {
                    if (Images.Count > 0)
                    {
                        foreach (ContentMobileModel obj in Images)
                        {
                            result.Add(new ContentLikeMobileModel { ContentID = obj.ContentID, ContentLikes = 0 });
                        }
                    }
                }               
                //Contents.Insert(0, new ContentMobileModel { ContentURL = AlbumCover });
                //if (isValidate)
                //{
                //    Contents = Contents.All(t => t.ContentURL.Replace(",", ""));
                //}
                return result;
            }
        }
    }
}