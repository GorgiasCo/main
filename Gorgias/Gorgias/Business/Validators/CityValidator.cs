using Gorgias.Business.DataTransferObjects;
using FluentValidation;
namespace Gorgias.Business.Validators
{   
        
    public class CityValidator : AbstractValidator<CityDTO>
    {
        public CityValidator() {            
            RuleFor(city => city.CityName).NotEmpty().Length(1, 70).WithMessage("CityName is required");
            RuleFor(city => city.CityStatus).NotNull().WithMessage("CityStatus is required");
            RuleFor(city => city.CountryID).NotNull().WithMessage("Country is required");
            //RuleFor(city => city.CityUpdateDate).NotNull().WithMessage("CityUpdateDate is required");
        }
    }  
}   

