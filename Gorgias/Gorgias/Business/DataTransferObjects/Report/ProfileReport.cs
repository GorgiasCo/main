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
        public long? ProfileView { get; set; }
        public long? AlbumView { get; set; }
        public long? AlbumLikes { get; set; }
        public long? AlbumComments { get; set; }
        public long? StayOnConnection { get; set; }
        public long? Subscription { get; set; }
        public double? ConnectedUserShare { get; set; }
        public double? UserCommission { get; set; }

        public double? OverAllRevenue { get; set; }
        public long? OverAllView { get; set; }
        public long? OverAllEngagement { get; set; }
        public long? OverAllSubscription { get; set; }

        public double EstimatedRPM { get; set; }

        public double? UserShareEstimateAmount
        {
            get
            {
                double result = 0;
                if (UserCommission.HasValue)
                {
                    result = UserCommission.Value;
                    if (EstimatedRevenue.HasValue)
                    {
                        result = ((EstimatedRevenue.Value * UserCommission.Value) / 100);
                    }
                    else
                    {
                        return 0;
                    }
                }
                return result;
            }
        }

        public double? UserShareAmount
        {
            get
            {
                double result = 0;
                if (UserCommission.HasValue)
                {
                    result = UserCommission.Value;
                    if (OverAllRevenue.HasValue)
                    {
                        result = ((OverAllRevenue.Value * UserCommission.Value) / 100);
                    }
                    else
                    {
                        return 0;
                    }
                }
                return result;
            }
        }

        public double? ConnectedUserShareAmount
        {
            get
            {
                double result = 0;
                if (ConnectedUserShare.HasValue)
                {
                    result = ConnectedUserShare.Value;
                    if (OverAllRevenue.HasValue)
                    {
                        result = ((OverAllRevenue.Value * ConnectedUserShare.Value) / 100);
                    }
                    else
                    {
                        return 0;
                    }
                }
                return result;
            }
        }

        public double? ProfileNetAmount
        {
            get
            {
                double result = 0;
                if (OverAllRevenue.HasValue)
                {
                    result = OverAllRevenue.Value;
                    if (ConnectedUserShare.HasValue)
                    {
                        result = OverAllRevenue.Value - ((OverAllRevenue.Value * ConnectedUserShare.Value) / 100);
                    }
                }
                return result;
            }
        }

        public long? OverAllTotalView
        {
            get
            {
                long result = 0;
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

        public long? OverAllTotalEngagement
        {
            get
            {
                long result = 0;
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

        public long? OverAllTotalSubscription
        {
            get
            {
                long result = 0;
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

        public long? TotalView
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

        public double? EstimatedRevenue
        {
            get
            {
                if (AlbumView.HasValue)
                {
                    return ((ProfileView + AlbumView.Value) / 1000) * EstimatedRPM;
                }
                else
                {
                    return (ProfileView / 1000) * EstimatedRPM;
                }
            }
        }

        public long? TotalEngagement
        {
            get
            {
                long result = 0;
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

        public long? TotalSubscription
        {
            get
            {
                long result = 0;
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