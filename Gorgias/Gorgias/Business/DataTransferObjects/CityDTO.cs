using System;
using FluentValidation.Attributes;
using Gorgias.Business.Validators;
namespace Gorgias.Business.DataTransferObjects
{

    [Validator(typeof(CityValidator))]
    public class CityDTO
    {
        public int CityID { get; set; }
        public String CityName { get; set; }
        public Boolean CityStatus { get; set; }
        public int CountryID { get; set; }
        public byte[] CityUpdateDate { get; set; }

        public virtual CountryDTO Country { get; set; }

    }
}

