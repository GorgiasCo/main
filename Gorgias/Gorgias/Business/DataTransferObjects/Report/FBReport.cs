using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gorgias.Business.DataTransferObjects.Report
{
    public class FBReport
    {
        public decimal TotalRevenue { get; set; }
        public int TotalView { get; set; }
        public DateTime CurrentDate { get; set; }
    }
}