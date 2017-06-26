using Gorgias.Business.DataTransferObjects;
using FluentValidation;
namespace Gorgias.Business.Validators
{   
        
    public class ThemeValidator : AbstractValidator<ThemeDTO>
    {
        public ThemeValidator() {            
            RuleFor(theme => theme.ThemeName).NotEmpty().Length(1, 70).WithMessage("ThemeName is required");
            RuleFor(theme => theme.ThemeClassCode).NotEmpty().Length(1, 70).WithMessage("ThemeClassCode is required");
            RuleFor(theme => theme.ThemeStatus).NotNull().WithMessage("ThemeStatus is required");
        }
    }  
}   

