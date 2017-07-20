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

        public int TotalView { get; set; }
        public int ActualView { get; set; }
        public int ActualSubscription { get; set; }
        public int ActualEngagement { get; set; }
        public int OverAllView { get; set; }
        public int OverAllSubscription { get; set; }
        public int OverAllEngagement { get; set; }
        public int ProfileView { get; set; }
        public int AlbumView { get; set; }

        public int OverAllTotalView { get; set; }
        public int OverAllTotalSubscription { get; set; }
        public int OverAllTotalEngagement { get; set; }
        public double UserShareCommission { get; set; }

        public int TotalProfileVisit
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