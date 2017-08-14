using System;
using FluentValidation.Attributes;
using Gorgias.Business.Validators;
namespace Gorgias.Business.DataTransferObjects
{   

    [Validator(typeof(ProfileReportValidator))]
    public class ProfileReportDTO
    {
            public int ProfileReportID{get; set;}
            public long ProfileReportActivityCount{get; set;}
            public double ProfileReportRevenue{get; set;}
            public int ReportTypeID{get; set;}
            public int ProfileID{get; set;}
            public int RevenueID{get; set;}
        
            public virtual ReportTypeDTO ReportType {get; set;}
            public virtual ProfileDTO Profile {get; set;}
            public virtual RevenueDTO Revenue {get; set;}
        
    }
}   

