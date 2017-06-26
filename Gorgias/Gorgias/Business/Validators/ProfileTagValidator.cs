using Gorgias.Business.DataTransferObjects;
using FluentValidation;
namespace Gorgias.Business.Validators
{   
        
    public class ProfileTagValidator : AbstractValidator<ProfileTagDTO>
    {
        public ProfileTagValidator() {            
            RuleFor(profileTag => profileTag.ProfileID).NotNull().WithMessage("ProfileID is required");
            RuleFor(profileTag => profileTag.ProfileTagStatus).NotNull().WithMessage("ProfileTagStatus is required");
        }
    }  
}   

