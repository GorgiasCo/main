using Gorgias.Business.DataTransferObjects;
using FluentValidation;
namespace Gorgias.Business.Validators
{

    public class ProfileSettingValidator : AbstractValidator<ProfileSettingDTO>
    {
        public ProfileSettingValidator()
        {
            RuleFor(profileSetting => profileSetting.ProfileLanguageApp).NotEmpty().Length(1, 10).WithMessage("ProfileLanguageApp is required");
            RuleFor(profileSetting => profileSetting.ProfileCityID).NotNull().WithMessage("ProfileCityID is required");
            RuleFor(profileSetting => profileSetting.ProfileBirthday).NotNull().WithMessage("ProfileBirthday is required");
        }
    }
}

