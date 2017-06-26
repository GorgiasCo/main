using Gorgias.Business.DataTransferObjects;
using FluentValidation;
namespace Gorgias.Business.Validators
{   
        
    public class ProfileSocialNetworkValidator : AbstractValidator<ProfileSocialNetworkDTO>
    {
        public ProfileSocialNetworkValidator() {            
            RuleFor(profileSocialNetwork => profileSocialNetwork.ProfileID).NotNull().WithMessage("ProfileID is required");
            RuleFor(profileSocialNetwork => profileSocialNetwork.ProfileSocialNetworkURL).NotEmpty().Length(1, 250).WithMessage("ProfileSocialNetworkURL is required");
        }
    }  
}   

