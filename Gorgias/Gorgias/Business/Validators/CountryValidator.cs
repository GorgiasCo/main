using Gorgias.Business.DataTransferObjects;
using FluentValidation;
namespace Gorgias.Business.Validators
{   
        
    public class CountryValidator : AbstractValidator<CountryDTO>
    {
        public CountryValidator() {            
            RuleFor(country => country.CountryName).NotEmpty().Length(1, 70).WithMessage("CountryName is required");
            RuleFor(country => country.CountryShortName).NotEmpty().Length(1, 10).WithMessage("CountryShortName is required");
            RuleFor(country => country.CountryShortName).NotEmpty().Length(1, 10).WithMessage("CountryShortName is required");
            RuleFor(country => country.CountryPhoneCode).NotEmpty().Length(1, 5).WithMessage("CountryPhoneCode is required");
            RuleFor(country => country.CountryImage).Length(1, 150).WithMessage("CountryImage is required");
            RuleFor(country => country.CountryImage).Length(1, 150).WithMessage("CountryImage is required");
        }
    }  
}   

