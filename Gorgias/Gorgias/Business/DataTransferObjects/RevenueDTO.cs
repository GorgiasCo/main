using System;
using FluentValidation.Attributes;
using Gorgias.Business.Validators;
using System.Collections.Generic;

namespace Gorgias.Business.DataTransferObjects
{

    [Validator(typeof(RevenueValidator))]
    public class RevenueDTO
    {
        public int RevenueID { get; set; }
        public DateTime RevenueDateCreated { get; set; }
        public double RevenueAmount { get; set; }
        public Int64 RevenueTotalViews { get; set; }
        public double RevenueMemberShare { get; set; }
        //public virtual IEnumerable<ProfileReportDTO> ProfileReports { get; set; }
        public double ProfileShare
        {
            get
            {
                return (RevenueAmount * RevenueMemberShare) / 100;
            }
        }
    }
}

