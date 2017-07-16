using Gorgias.Business.DataTransferObjects;
using FluentValidation;
namespace Gorgias.Business.Validators
{   
        
    public class ProfileReportValidator : AbstractValidator<ProfileReportDTO>
    {
        public ProfileReportValidator() {            
            RuleFor(profileReport => profileReport.ProfileReportActivityCount).NotNull().WithMessage("ProfileReportActivityCount is required");
            RuleFor(profileReport => profileReport.ProfileReportRevenue).NotNull().WithMessage("ProfileReportRevenue is required");
            RuleFor(profileReport => profileReport.ReportTypeID).NotNull().WithMessage("ReportType is required");
            RuleFor(profileReport => profileReport.ProfileID).NotNull().WithMessage("Profile is required");
            RuleFor(profileReport => profileReport.RevenueID).NotNull().WithMessage("Revenue is required");
        }
    }  
}   

