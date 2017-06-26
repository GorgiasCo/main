using Gorgias.Business.DataTransferObjects;
using FluentValidation;
namespace Gorgias.Business.Validators
{   
        
    public class SocialNetworkValidator : AbstractValidator<SocialNetworkDTO>
    {
        public SocialNetworkValidator() {            
            RuleFor(socialNetwork => socialNetwork.SocialNetworkName).NotEmpty().Length(1, 50).WithMessage("SocialNetworkName is required");
            RuleFor(socialNetwork => socialNetwork.SocialNetworkStatus).NotNull().WithMessage("SocialNetworkStatus is required");
            RuleFor(socialNetwork => socialNetwork.SocialNetworkURL).NotEmpty().Length(1, 150).WithMessage("SocialNetworkURL is required");
            RuleFor(socialNetwork => socialNetwork.SocialNetworkImage).NotEmpty().Length(1, 150).WithMessage("SocialNetworkImage is required");
            RuleFor(socialNetwork => socialNetwork.SocialNetworkImage).NotEmpty().Length(1, 150).WithMessage("SocialNetworkImage is required");
        }
    }  
}   

