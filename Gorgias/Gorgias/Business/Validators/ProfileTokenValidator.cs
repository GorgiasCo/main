using Gorgias.Business.DataTransferObjects;
using FluentValidation;
namespace Gorgias.Business.Validators
{

    public class ProfileTokenValidator : AbstractValidator<ProfileTokenDTO>
    {
        public ProfileTokenValidator()
        {
            RuleFor(profileToken => profileToken.ProfileTokenString).NotEmpty().Length(1, 100).WithMessage("ProfileTokenString is required");
            RuleFor(profileToken => profileToken.ProfileTokenRegistration).NotNull().WithMessage("ProfileTokenRegistration is required");
            RuleFor(profileToken => profileToken.ProfileID).NotNull().WithMessage("Profile is required");
        }
    }
}

