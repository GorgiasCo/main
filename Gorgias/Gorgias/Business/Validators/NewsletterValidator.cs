using Gorgias.Business.DataTransferObjects;
using FluentValidation;
namespace Gorgias.Business.Validators
{   
        
    public class NewsletterValidator : AbstractValidator<NewsletterDTO>
    {
        public NewsletterValidator() {            
            RuleFor(newsletter => newsletter.NewsletterName).NotEmpty().Length(1, 150).WithMessage("NewsletterName is required");
            RuleFor(newsletter => newsletter.NewsletterNote).NotEmpty().WithMessage("NewsletterNote is required");
            RuleFor(newsletter => newsletter.NewsletterStatus).NotNull().WithMessage("NewsletterStatus is required");
        }
    }  
}   

