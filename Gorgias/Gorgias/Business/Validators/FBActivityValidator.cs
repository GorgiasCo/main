using Gorgias.Business.DataTransferObjects;
using FluentValidation;
namespace Gorgias.Business.Validators
{   
        
    public class FBActivityValidator : AbstractValidator<FBActivityDTO>
    {
        public FBActivityValidator() {            
            RuleFor(fBActivity => fBActivity.FBActivityCount).NotNull().WithMessage("FBActivityCount is required");
            RuleFor(fBActivity => fBActivity.FBActivityDate).NotNull().WithMessage("FBActivityDate is required");
            RuleFor(fBActivity => fBActivity.FBActivityType).NotNull().WithMessage("FBActivityType is required");
        }
    }  
}   

