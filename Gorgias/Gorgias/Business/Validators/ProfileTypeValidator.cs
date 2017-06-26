using Gorgias.Business.DataTransferObjects;
using FluentValidation;
namespace Gorgias.Business.Validators
{   
        
    public class ProfileTypeValidator : AbstractValidator<ProfileTypeDTO>
    {
        public ProfileTypeValidator() {            
            RuleFor(profileType => profileType.ProfileTypeName).NotEmpty().Length(1, 100).WithMessage("ProfileTypeName is required");
            RuleFor(profileType => profileType.ProfileTypeStatus).NotNull().WithMessage("ProfileTypeStatus is required");
        }
    }  
}   

