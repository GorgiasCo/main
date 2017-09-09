using System;
using FluentValidation.Attributes;
using Gorgias.Business.Validators;
using System.Collections.Generic;

namespace Gorgias.Business.DataTransferObjects
{

    [Validator(typeof(ProfileTypeValidator))]
    public class ProfileTypeDTO
    {
        private string _profileTypeName;
        public int ProfileTypeID { get; set; }
        public string ProfileTypeName {
            get
            {
                if (Multilanguage != null)
                {
                    return Multilanguage;
                }
                return _profileTypeName;
            }
            set
            {
                _profileTypeName = value;
            }
        }
        public bool ProfileTypeStatus { get; set; }
        public string ProfileTypeImage { get; set; }
        public string ProfileTypeDescription { get; set; }
        public int? ProfileTypeParentID { get; set; }
        
        public string ProfileTypeLanguageCode { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        public virtual ProfileTypeDTO ProfileTypeParent { get; set; }
        [Newtonsoft.Json.JsonIgnore]
        public virtual ICollection<ProfileTypeDTO> ProfileTypeChilds { get; set; }
        [Newtonsoft.Json.JsonIgnore]
        public string Multilanguage { get; set; }
    }
}

