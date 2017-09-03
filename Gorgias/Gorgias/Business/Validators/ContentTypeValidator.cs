using Gorgias.Business.DataTransferObjects;
using FluentValidation;
namespace Gorgias.Business.Validators
{   
        
    public class ContentTypeValidator : AbstractValidator<ContentTypeDTO>
    {
        public ContentTypeValidator() {            
            RuleFor(contentType => contentType.ContentTypeName).NotEmpty().Length(1, 150).WithMessage("ContentTypeName is required");
            RuleFor(contentType => contentType.ContentTypeOrder).NotNull().WithMessage("ContentTypeOrder is required");
            RuleFor(contentType => contentType.ContentTypeStatus).NotNull().WithMessage("ContentTypeStatus is required");
            RuleFor(contentType => contentType.ContentTypeLanguageCode).NotEmpty().Length(1, 10).WithMessage("ContentTypeLanguageCode is required");
            RuleFor(contentType => contentType.ContentTypeExpression).Length(1, 100).WithMessage("ContentTypeExpression is required");
        }
    }  
}   

