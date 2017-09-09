using System;
using FluentValidation.Attributes;
using Gorgias.Business.Validators;
using System.Collections.Generic;

namespace Gorgias.Business.DataTransferObjects
{

    [Validator(typeof(IndustryValidator))]
    public class IndustryDTO
    {
        private string _industryName;
        public int IndustryID { get; set; }
        public string IndustryName {
            get
            {
                if (Multilanguage != null)
                {
                    return Multilanguage;
                }
                return _industryName;
            }
            set
            {
                _industryName = value;
            }
        }
        public bool IndustryStatus { get; set; }
        public int? IndustryParentID { get; set; }
        public string IndustryImage { get; set; }
        public string IndustryDescription { get; set; }

        //[Newtonsoft.Json.JsonIgnore]
        //public virtual IndustryDTO IndustryParent { get; set; }
        //[Newtonsoft.Json.JsonIgnore]
        //public virtual ICollection<IndustryDTO> IndustryChilds { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        public string Multilanguage { get; set; }

    }
}

