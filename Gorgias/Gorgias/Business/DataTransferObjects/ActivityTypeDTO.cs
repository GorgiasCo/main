using System;
using FluentValidation.Attributes;
using Gorgias.Business.Validators;
namespace Gorgias.Business.DataTransferObjects
{

    [Validator(typeof(ActivityTypeValidator))]
    public class ActivityTypeDTO
    {
        private string _ActivityTypeName;

        public int ActivityTypeID { get; set; }

        public String ActivityTypeName
        {
            get
            {
                if (Multilanguage != null)
                {
                    return Multilanguage;
                }
                return _ActivityTypeName;
            }
            set
            {
                _ActivityTypeName = value;
            }
        }

        public String ActivityTypeLanguageCode { get; set; }
        public Boolean ActivityTypeStatus { get; set; }
        public int? ActivityTypeParentID { get; set; }

        public virtual ActivityTypeDTO ActivityTypeParent { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        public string Multilanguage { get; set; }
    }
}

