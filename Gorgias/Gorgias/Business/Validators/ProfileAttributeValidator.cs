using Gorgias.Business.DataTransferObjects;
using FluentValidation;
namespace Gorgias.Business.Validators
{   
        
    public class ProfileAttributeValidator : AbstractValidator<ProfileAttributeDTO>
    {
        public ProfileAttributeValidator() {            
            RuleFor(profileAttribute => profileAttribute.ProfileID).NotNull().WithMessage("ProfileID is required");
            RuleFor(profileAttribute => profileAttribute.ProfileID).NotNull().WithMessage("ProfileID is required");
        }
    }  
}   

