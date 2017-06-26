using Gorgias.Business.DataTransferObjects;
using FluentValidation;
namespace Gorgias.Business.Validators
{   
        
    public class ProfileIndustryValidator : AbstractValidator<ProfileIndustryDTO>
    {
        public ProfileIndustryValidator() {            
            RuleFor(profileIndustry => profileIndustry.ProfileID).NotNull().WithMessage("ProfileID is required");
        }
    }  
}   

