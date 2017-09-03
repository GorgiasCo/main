using Gorgias.Business.DataTransferObjects;
using FluentValidation;
namespace Gorgias.Business.Validators
{   
        
    public class ContentRatingValidator : AbstractValidator<ContentRatingDTO>
    {
        public ContentRatingValidator() {            
            RuleFor(contentRating => contentRating.ContentRatingName).NotEmpty().Length(1, 100).WithMessage("ContentRatingName is required");
            RuleFor(contentRating => contentRating.ContentRatingAge).NotNull().WithMessage("ContentRatingAge is required");
            RuleFor(contentRating => contentRating.ContentRatingStatus).NotNull().WithMessage("ContentRatingStatus is required");
            RuleFor(contentRating => contentRating.ContentRatingImage).Length(1, 150).WithMessage("ContentRatingImage is required");
            RuleFor(contentRating => contentRating.ContentRatingImage).Length(1, 150).WithMessage("ContentRatingImage is required");
            RuleFor(contentRating => contentRating.ContentRatingOrder).NotNull().WithMessage("ContentRatingOrder is required");
            RuleFor(contentRating => contentRating.ContentRatingLanguageCode).NotEmpty().Length(1, 10).WithMessage("ContentRatingLanguageCode is required");
        }
    }  
}   

