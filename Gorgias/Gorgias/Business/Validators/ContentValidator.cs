using Gorgias.Business.DataTransferObjects;
using FluentValidation;
namespace Gorgias.Business.Validators
{   
        
    public class ContentValidator : AbstractValidator<ContentDTO>
    {
        public ContentValidator() {            
            RuleFor(content => content.ContentTitle).Length(1, 250).WithMessage("ContentTitle is required");
            RuleFor(content => content.ContentURL).NotEmpty().Length(1, 250).WithMessage("ContentURL is required");
            RuleFor(content => content.ContentType).NotNull().WithMessage("ContentType is required");
            RuleFor(content => content.ContentStatus).NotNull().WithMessage("ContentStatus is required");
            RuleFor(content => content.ContentIsDeleted).NotNull().WithMessage("ContentIsDeleted is required");
            RuleFor(content => content.AlbumID).NotNull().WithMessage("Album is required");
        }
    }  
}   

