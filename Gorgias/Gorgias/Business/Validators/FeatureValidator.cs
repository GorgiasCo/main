using Gorgias.Business.DataTransferObjects;
using FluentValidation;
namespace Gorgias.Business.Validators
{   
        
    public class FeatureValidator : AbstractValidator<FeatureDTO>
    {
        public FeatureValidator() {            
            RuleFor(feature => feature.FeatureTitle).NotEmpty().Length(1, 250).WithMessage("FeatureTitle is required");
            RuleFor(feature => feature.FeatureDateCreated).NotNull().WithMessage("FeatureDateCreated is required");
            RuleFor(feature => feature.FeatureDateExpired).NotNull().WithMessage("FeatureDateExpired is required");
            RuleFor(feature => feature.FeatureStatus).NotNull().WithMessage("FeatureStatus is required");
            RuleFor(feature => feature.FeatureIsDeleted).NotNull().WithMessage("FeatureIsDeleted is required");
            RuleFor(feature => feature.FeatureImage).NotEmpty().Length(1, 150).WithMessage("FeatureImage is required");
            RuleFor(feature => feature.FeatureImage).NotEmpty().Length(1, 150).WithMessage("FeatureImage is required");
            RuleFor(feature => feature.ProfileID).NotNull().WithMessage("ProfileID is required");
        }
    }  
}   

