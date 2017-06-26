using System;
using FluentValidation.Attributes;
using Gorgias.Business.Validators;
namespace Gorgias.Business.DataTransferObjects
{

    [Validator(typeof(ExternalLinkValidator))]
    public class ExternalLinkDTO
    {
        public int LinkTypeID { get; set; }
        public int ProfileID { get; set; }
        public String ExternalLinkURL { get; set; }
        public virtual LinkTypeDTO LinkType { get;set;}
        public virtual ProfileDTO Profile { get; set; }
    }
}

