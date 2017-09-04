using Gorgias.Business.DataTransferObjects;
using FluentValidation;
namespace Gorgias.Business.Validators
{   
        
    public class LanguageValidator : AbstractValidator<LanguageDTO>
    {
        public LanguageValidator() {            
            RuleFor(language => language.LanguageName).NotEmpty().Length(1, 100).WithMessage("LanguageName is required");
            RuleFor(language => language.LanguageCode).NotEmpty().Length(1, 10).WithMessage("LanguageCode is required");
            RuleFor(language => language.LanguageStatus).NotNull().WithMessage("LanguageStatus is required");
        }
    }  
}   

