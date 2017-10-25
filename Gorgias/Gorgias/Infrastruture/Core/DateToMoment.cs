using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gorgias.Infrastruture.Core
{
    public static class DateToMoment
    {
        public static string TimeAgo(DateTime dt)
        {
            TimeSpan span = DateTime.UtcNow - dt;
            if (span.Days > 365)
            {
                //int years = (span.Days / 365);
                //if (span.Days % 365 != 0)
                //    years += 1;
                //return String.Format("about {0} {1} ago",
                //years, years == 1 ? "year" : "years");
                return dt.ToString("MMMM dd, yyyy");
            }
            if (span.Days > 30)
            {
                //int months = (span.Days / 30);
                //if (span.Days % 31 != 0)
                //    months += 1;
                //return String.Format("about {0} {1} ago",
                //months, months == 1 ? "month" : "months");
                return dt.ToString("MMMM dd, yyyy");
            }
            if (span.Days > 0)
                //return String.Format("about {0} {1} ago",
                //span.Days, span.Days == 1 ? "day" : "days");
                return dt.ToString("MMMM dd, yyyy");
            if (span.Hours > 0)
                return String.Format("{0} {1} ago",
                span.Hours, span.Hours == 1 ? "hour" : "hours");
            if (span.Minutes > 0)
                return String.Format("{0} {1} ago",
                span.Minutes, span.Minutes == 1 ? "minute" : "minutes");
            if (span.Seconds > 5)
                return String.Format("{0} seconds ago", span.Seconds);
            if (span.Seconds <= 5)
                return "just now";
            return string.Empty;
        }

        public static string TimeLeft(DateTime dt)
        {
            TimeSpan span = dt - DateTime.UtcNow;
            //if (span.Days > 365)
            //{
            //    //int years = (span.Days / 365);
            //    //if (span.Days % 365 != 0)
            //    //    years += 1;
            //    //return String.Format("about {0} {1} ago",
            //    //years, years == 1 ? "year" : "years");
            //    return dt.ToString("MMMM dd, yyyy");
            //}
            //if (span.Days > 30)
            //{
            //    //int months = (span.Days / 30);
            //    //if (span.Days % 31 != 0)
            //    //    months += 1;
            //    //return String.Format("about {0} {1} ago",
            //    //months, months == 1 ? "month" : "months");
            //    return dt.ToString("MMMM dd, yyyy");
            //}
            if (span.Days > 0)
                return String.Format("{0}",
                span.Days);
                //return dt.ToString("MMMM dd, yyyy");
            if (span.Hours > 0)
                return String.Format("{0}",
                span.Hours);
            if (span.Minutes > 0)
                return String.Format("{0}",
                span.Minutes);
            if (span.Seconds > 5)
                return String.Format("{0}", span.Seconds);
            if (span.Seconds <= 5)
                return "";
            return string.Empty;
        }

        public static string TimeLeftTitleOnly(DateTime dt)
        {
            TimeSpan span = dt - DateTime.UtcNow;
            //if (span.Days > 365)
            //{
            //    //int years = (span.Days / 365);
            //    //if (span.Days % 365 != 0)
            //    //    years += 1;
            //    //return String.Format("about {0} {1} ago",
            //    //years, years == 1 ? "year" : "years");
            //    return dt.ToString("MMMM dd, yyyy");
            //}
            //if (span.Days > 30)
            //{
            //    //int months = (span.Days / 30);
            //    //if (span.Days % 31 != 0)
            //    //    months += 1;
            //    //return String.Format("about {0} {1} ago",
            //    //months, months == 1 ? "month" : "months");
            //    return dt.ToString("MMMM dd, yyyy");
            //}
            if (span.Days > 0)
                return String.Format("{0}",
                       span.Days == 1 ? "d" : "d");
                           //return dt.ToString("MMMM dd, yyyy");
            if (span.Hours > 0)
                return String.Format("{0}",
                       span.Hours == 1 ? "h" : "hs");
            if (span.Minutes > 0)
                return String.Format("{0}",
                       span.Minutes == 1 ? "min" : "mins");
            if (span.Seconds > 5)
                return "sec";// String.Format("{0} sec", span.Seconds);
            if (span.Seconds <= 5)
                return "expired";
            return string.Empty;
        }
    }
}