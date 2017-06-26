using Gorgias.Business.DataTransferObjects;
using FluentValidation;
namespace Gorgias.Business.Validators
{   
        
    public class UserProfileValidator : AbstractValidator<UserProfileDTO>
    {
        public UserProfileValidator() {            
            RuleFor(userProfile => userProfile.UserRoleID).NotNull().WithMessage("UserRoleID is required");
            RuleFor(userProfile => userProfile.UserID).NotNull().WithMessage("UserID is required");
        }
    }  
}   

