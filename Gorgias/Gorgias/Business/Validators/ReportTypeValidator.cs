using Gorgias.Business.DataTransferObjects;
using FluentValidation;
namespace Gorgias.Business.Validators
{
    public class ReportTypeValidator : AbstractValidator<ReportTypeDTO>
    {
        public ReportTypeValidator()
        {
            RuleFor(reportType => reportType.ReportTypeName).NotEmpty().Length(1, 150).WithMessage("ReportTypeName is required");
            RuleFor(reportType => reportType.ReportTypeIsCountable).NotNull().WithMessage("ReportTypeIsCountable is required");
            RuleFor(reportType => reportType.ReportTypeStatus).NotNull().WithMessage("ReportTypeStatus is required");
            RuleFor(reportType => reportType.ReportTypeID).NotNull().WithMessage("ReportTypeID is required");
        }
    }
}

