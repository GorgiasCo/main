using Gorgias.Business.DataTransferObjects;
using FluentValidation;
namespace Gorgias.Business.Validators
{   
        
    public class ProfileValidator : AbstractValidator<ProfileDTO>
    {
        public ProfileValidator() {            
            RuleFor(profile => profile.ProfileFullname).NotEmpty().Length(1, 200).WithMessage("ProfileFullname is required");
            RuleFor(profile => profile.ProfileIsPeople).NotNull().WithMessage("ProfileIsPeople is required");
            RuleFor(profile => profile.ProfileIsDeleted).NotNull().WithMessage("ProfileIsDeleted is required");
            RuleFor(profile => profile.ProfileDateCreated).NotNull().WithMessage("ProfileDateCreated is required");
            RuleFor(profile => profile.ProfileDateCreated).NotNull().WithMessage("ProfileDateCreated is required");
            RuleFor(profile => profile.ProfileView).NotNull().WithMessage("ProfileView is required");
            RuleFor(profile => profile.ProfileLike).NotNull().WithMessage("ProfileLike is required");
            RuleFor(profile => profile.ProfileURL).Length(1, 150).WithMessage("ProfileURL is required");
            RuleFor(profile => profile.ProfileShortDescription).Length(1, 250).WithMessage("ProfileShortDescription is required");
            //Ignore because of seperate update after insert
            //RuleFor(profile => profile.ProfileImage).NotEmpty().Length(1, 150).WithMessage("ProfileImage is required");
            RuleFor(profile => profile.ProfileEmail).NotEmpty().Length(1, 150).EmailAddress().WithMessage("ProfileEmail is required");
            RuleFor(profile => profile.ProfileStatus).NotNull().WithMessage("ProfileStatus is required");
            RuleFor(profile => profile.ProfileIsConfirmed).NotNull().WithMessage("ProfileIsConfirmed is required");
            
            //Remove because of Profile Industries requirement ;)
            //RuleFor(profile => profile.IndustryID).NotNull().WithMessage("Industry is required");
            RuleFor(profile => profile.ProfileTypeID).NotNull().WithMessage("ProfileType is required");
            RuleFor(profile => profile.ThemeID).NotNull().WithMessage("Theme is required");
            RuleFor(profile => profile.SubscriptionTypeID).NotNull().WithMessage("SubscriptionType is required");
        }
    }  
}   

