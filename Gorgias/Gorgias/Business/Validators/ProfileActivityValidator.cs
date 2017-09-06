using Gorgias.Business.DataTransferObjects;
using FluentValidation;
namespace Gorgias.Business.Validators
{

    public class ProfileActivityValidator : AbstractValidator<ProfileActivityDTO>
    {
        public ProfileActivityValidator()
        {
            RuleFor(profileActivity => profileActivity.AlbumID).NotNull().WithMessage("AlbumID is required");
            RuleFor(profileActivity => profileActivity.ProfileID).NotNull().WithMessage("ProfileID is required");
            RuleFor(profileActivity => profileActivity.ActivityTypeID).NotNull().WithMessage("ActivityType is required");
            RuleFor(profileActivity => profileActivity.ProfileActivityCount).NotNull().WithMessage("ProfileActivityCount is required");            
        }
    }
}

