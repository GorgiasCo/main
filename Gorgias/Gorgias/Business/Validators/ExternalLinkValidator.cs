using Gorgias.Business.DataTransferObjects;
using FluentValidation;
namespace Gorgias.Business.Validators
{   
        
    public class ExternalLinkValidator : AbstractValidator<ExternalLinkDTO>
    {
        public ExternalLinkValidator() {            
            RuleFor(externalLink => externalLink.ProfileID).NotNull().WithMessage("ProfileID is required");
            RuleFor(externalLink => externalLink.ExternalLinkURL).NotEmpty().Length(1, 250).WithMessage("ExternalLinkURL is required");
        }
    }  
}   

