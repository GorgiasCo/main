using Gorgias.Business.DataTransferObjects;
using FluentValidation;
namespace Gorgias.Business.Validators
{   
        
    public class IndustryValidator : AbstractValidator<IndustryDTO>
    {
        public IndustryValidator() {            
            RuleFor(industry => industry.IndustryName).NotEmpty().Length(1, 100).WithMessage("IndustryName is required");
            RuleFor(industry => industry.IndustryStatus).NotNull().WithMessage("IndustryStatus is required");
            RuleFor(industry => industry.IndustryParentID).NotNull().WithMessage("IndustryParentID is required");
            RuleFor(industry => industry.IndustryImage).Length(1, 150).WithMessage("IndustryImage is required");
            RuleFor(industry => industry.IndustryImage).Length(1, 150).WithMessage("IndustryImage is required");
        }
    }  
}   

