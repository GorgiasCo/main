using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gorgias.Business.DataTransferObjects.Report
{
    public class FullProfileReport
    {
        public IList<ProfileReport> ProfileReports { get; set; }

        public double RevenueAmount { get; set; }
        public double? OverAllRevenue { get; set; }

        public Int64 TotalView { get; set; }
        public long ActualView { get; set; }
        public long ActualSubscription { get; set; }
        public long ActualEngagement { get; set; }
        public long OverAllView { get; set; }
        public long OverAllSubscription { get; set; }
        public long OverAllEngagement { get; set; }
        public long ProfileView { get; set; }
        public long AlbumView { get; set; }

        public long OverAllTotalView { get; set; }
        public long OverAllTotalSubscription { get; set; }
        public long OverAllTotalEngagement { get; set; }
        public double UserShareCommission { get; set; }

        public long TotalProfileVisit
        {
            get
            {
                return ProfileView + AlbumView;
            }
        }

        public double EstimatedRPM
        {
            get
            {
                return (RevenueAmount / TotalView) * 1000;
            }
        }

        public double EstimatedRevenue
        {
            get
            {
                return (ActualView / 1000) * EstimatedRPM;
            }
        }

        public double EstimatedUserRevenue
        {
            get
            {
                return (EstimatedRevenue * (UserShareCommission % ProfileReports.Count)) / 100;
            }
        }

        public double OverAllUserRevenue
        {
            get
            {
                if (OverAllRevenue.HasValue)
                {
                    if (ProfileReports.Count > 1)
                    {
                        return (OverAllRevenue.Value * (UserShareCommission % ProfileReports.Count)) / 100;
                    } else
                    {
                        return (OverAllRevenue.Value * UserShareCommission) / 100;
                    }                    
                }
                return 0;
            }
        }

    }
}