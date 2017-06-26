using Gorgias.Business.DataTransferObjects;
using FluentValidation;
namespace Gorgias.Business.Validators
{   
        
    public class LinkTypeValidator : AbstractValidator<LinkTypeDTO>
    {
        public LinkTypeValidator() {            
            RuleFor(linkType => linkType.LinkTypeName).NotEmpty().Length(1, 70).WithMessage("LinkTypeName is required");
            RuleFor(linkType => linkType.LinkTypeStatus).NotNull().WithMessage("LinkTypeStatus is required");
            RuleFor(linkType => linkType.LinkTypeImage).NotEmpty().Length(1, 150).WithMessage("LinkTypeImage is required");
            RuleFor(linkType => linkType.LinkTypeImage).NotEmpty().Length(1, 150).WithMessage("LinkTypeImage is required");
        }
    }  
}   

