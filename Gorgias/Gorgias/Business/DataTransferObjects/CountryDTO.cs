using System;
using FluentValidation.Attributes;
using Gorgias.Business.Validators;
using System.Collections.Generic;

namespace Gorgias.Business.DataTransferObjects
{

    [Validator(typeof(CountryValidator))]
    public class CountryDTO
    {
        private string _countryName;
        public int CountryID { get; set; }
        public string CountryName {
            get
            {
                if (Multilanguage != null)
                {
                    return Multilanguage;
                }
                return _countryName;
            }
            set
            {
                _countryName = value;
            }
        }
        public string CountryShortName { get; set; }
        public bool CountryStatus { get; set; }
        public string CountryPhoneCode { get; set; }
        public string CountryImage { get; set; }
        public string CountryDescription { get; set; }
        public int? CountryParentID { get; set; }
        public string CountryLanguageCode { get; set; }

        public virtual ICollection<CityDTO> Cities { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        public virtual CountryDTO CountryParent { get; set; }
        [Newtonsoft.Json.JsonIgnore]
        public virtual ICollection<CountryDTO> CountryChilds { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        public string Multilanguage { get; set; }
    }
}

