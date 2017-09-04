using Gorgias.Business.DataTransferObjects;
using FluentValidation;
namespace Gorgias.Business.Validators
{

    public class ProfileReadingValidator : AbstractValidator<ProfileReadingDTO>
    {
        public ProfileReadingValidator()
        {
            RuleFor(profileReading => profileReading.ProfileReadingLanguageCode).Length(1, 10).WithMessage("ProfileReadingLanguageCode is required");
            RuleFor(profileReading => profileReading.ProfileReadingDatetime).NotNull().WithMessage("ProfileReadingDatetime is required");
            RuleFor(profileReading => profileReading.ProfileID).NotNull().WithMessage("Profile is required");
        }
    }
}

