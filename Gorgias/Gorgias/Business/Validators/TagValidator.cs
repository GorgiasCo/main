using Gorgias.Business.DataTransferObjects;
using FluentValidation;
namespace Gorgias.Business.Validators
{   
        
    public class TagValidator : AbstractValidator<TagDTO>
    {
        public TagValidator() {            
            RuleFor(tag => tag.TagName).NotEmpty().Length(1, 200).WithMessage("TagName is required");
            RuleFor(tag => tag.TagStatus).NotNull().WithMessage("TagStatus is required");
            RuleFor(tag => tag.TagIsPrimary).NotNull().WithMessage("TagIsPrimary is required");
            RuleFor(tag => tag.TagWeight).NotNull().WithMessage("TagWeight is required");
        }
    }  
}   

