using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gorgias.Business.DataTransferObjects.Report
{
    public class ProfileReport
    {
        public int ProfileID { get; set; }
        public string ProfileFullname { get; set; }
        public int? ProfileView { get; set; }
        public int? AlbumView { get; set; }
        public int? AlbumLikes { get; set; }
        public int? AlbumComments { get; set; }
        public int? StayOnConnection { get; set; }
        public int? Subscription { get; set; }

        public double? OverAllRevenue { get; set; }
        public int? OverAllView { get; set; }
        public int? OverAllEngagement { get; set; }
        public int? OverAllSubscription { get; set; }

        public int? OverAllTotalView
        {
            get
            {
                int result = 0;
                if (TotalView.HasValue)
                {
                    result = TotalView.Value;
                }
                if (OverAllView.HasValue)
                {
                    result = result + OverAllView.Value;
                }
                return result;
            }
        }

        public int? OverAllTotalEngagement
        {
            get
            {
                int result = 0;
                if (TotalEngagement.HasValue)
                {
                    result = TotalEngagement.Value;
                }
                if (OverAllEngagement.HasValue)
                {
                    result = result + OverAllEngagement.Value;
                }
                return result;
            }
        }

        public int? OverAllTotalSubscription
        {
            get
            {
                int result = 0;
                if (TotalSubscription.HasValue)
                {
                    result = TotalSubscription.Value;
                }
                if (OverAllSubscription.HasValue)
                {
                    result = result + OverAllSubscription.Value;
                }
                return result;
            }
        }

        public int? TotalView
        {
            get
            {
                if (AlbumView.HasValue)
                {
                    return ProfileView + AlbumView.Value;
                } else
                {
                    return ProfileView;
                }
            }
        }

        public int? TotalEngagement
        {
            get
            {
                int result = 0;
                if (AlbumLikes.HasValue)
                {
                    result = AlbumLikes.Value;
                }
                if (AlbumComments.HasValue)
                {
                    result = result + AlbumComments.Value;
                }
                return result;
            }
        }

        public int? TotalSubscription
        {
            get
            {
                int result = 0;
                if (Subscription.HasValue)
                {
                    result = Subscription.Value;
                }
                if (StayOnConnection.HasValue)
                {
                    result = result + StayOnConnection.Value;
                }
                return result;
            }
        }

    }
}