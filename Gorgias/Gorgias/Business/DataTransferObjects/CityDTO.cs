using System;
using FluentValidation.Attributes;
using Gorgias.Business.Validators;
using System.Collections.Generic;

namespace Gorgias.Business.DataTransferObjects
{

    [Validator(typeof(CityValidator))]
    public class CityDTO
    {
        private string _cityName;
        public int CityID { get; set; }
        public string CityName {
            get
            {
                if (Multilanguage != null)
                {
                    return Multilanguage;
                }
                return _cityName;
            }
            set
            {
                _cityName = value;
            }
        }
        public bool CityStatus { get; set; }
        public int CountryID { get; set; }
        public int? CityParentID { get; set; }
        public string CityLanguageCode { get; set; }
        public byte[] CityUpdateDate { get; set; }

        public virtual CountryDTO Country { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        public virtual CityDTO CityParent { get; set; }
        [Newtonsoft.Json.JsonIgnore]
        public virtual ICollection<CityDTO> CityChilds { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        public string Multilanguage { get; set; }
    }
}

