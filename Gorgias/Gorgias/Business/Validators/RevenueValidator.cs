using Gorgias.Business.DataTransferObjects;
using FluentValidation;
namespace Gorgias.Business.Validators
{

    public class RevenueValidator : AbstractValidator<RevenueDTO>
    {
        public RevenueValidator()
        {
            RuleFor(revenue => revenue.RevenueDateCreated).NotNull().WithMessage("RevenueDateCreated is required");
            RuleFor(revenue => revenue.RevenueAmount).NotNull().WithMessage("RevenueAmount is required");
            RuleFor(revenue => revenue.RevenueTotalViews).NotNull().WithMessage("RevenueTotalViews is required");
            RuleFor(revenue => revenue.RevenueMemberShare).NotNull().WithMessage("RevenueMemberShare is required");
        }
    }
}

