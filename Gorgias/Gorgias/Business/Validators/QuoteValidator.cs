using Gorgias.Business.DataTransferObjects;
using FluentValidation;
namespace Gorgias.Business.Validators
{   
        
    public class QuoteValidator : AbstractValidator<QuoteDTO>
    {
        public QuoteValidator() {            
            RuleFor(quote => quote.QuoteName).NotEmpty().Length(1, 250).WithMessage("QuoteName is required");
            RuleFor(quote => quote.QuoteStatus).NotNull().WithMessage("QuoteStatus is required");
            RuleFor(quote => quote.QuoteLanguageCode).NotEmpty().Length(1, 10).WithMessage("QuoteLanguageCode is required");
            RuleFor(quote => quote.QuoteProbability).NotNull().WithMessage("QuoteProbability is required");
            RuleFor(quote => quote.CategoryID).NotNull().WithMessage("Category is required");
        }
    }  
}   

