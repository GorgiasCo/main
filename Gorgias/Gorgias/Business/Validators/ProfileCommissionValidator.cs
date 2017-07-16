using Gorgias.Business.DataTransferObjects;
using FluentValidation;
namespace Gorgias.Business.Validators
{   
        
    public class ProfileCommissionValidator : AbstractValidator<ProfileCommissionDTO>
    {
        public ProfileCommissionValidator() {            
            RuleFor(profileCommission => profileCommission.ProfileCommissionRate).NotNull().WithMessage("ProfileCommissionRate is required");
            RuleFor(profileCommission => profileCommission.ProfileCommissionDateCreated).NotNull().WithMessage("ProfileCommissionDateCreated is required");
            RuleFor(profileCommission => profileCommission.ProfileCommissionStatus).NotNull().WithMessage("ProfileCommissionStatus is required");
            RuleFor(profileCommission => profileCommission.ProfileID).NotNull().WithMessage("Profile is required");
            RuleFor(profileCommission => profileCommission.UserID).NotNull().WithMessage("User is required");
            RuleFor(profileCommission => profileCommission.UserRoleID).NotNull().WithMessage("UserRole is required");
        }
    }  
}   

