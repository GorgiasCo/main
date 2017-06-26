using Gorgias.Business.DataTransferObjects;
using FluentValidation;
namespace Gorgias.Business.Validators
{   
        
    public class FeaturedSponsorValidator : AbstractValidator<FeaturedSponsorDTO>
    {
        public FeaturedSponsorValidator() {            
            RuleFor(featuredSponsor => featuredSponsor.ProfileID).NotNull().WithMessage("ProfileID is required");
            RuleFor(featuredSponsor => featuredSponsor.FeaturedSponsorMode).NotNull().WithMessage("FeaturedSponsorMode is required");
            RuleFor(featuredSponsor => featuredSponsor.FeaturedRole).NotNull().WithMessage("FeaturedRole is required");
        }
    }  
}   

