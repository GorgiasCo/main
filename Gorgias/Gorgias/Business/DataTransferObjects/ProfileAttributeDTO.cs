using System;
using FluentValidation.Attributes;
using Gorgias.Business.Validators;
namespace Gorgias.Business.DataTransferObjects
{

    [Validator(typeof(ProfileAttributeValidator))]
    public class ProfileAttributeDTO
    {
        public int AttributeID { get; set; }
        public int ProfileID { get; set; }
        public String ProfileAttributeNote { get; set; }
        public AttributeDTO Attribute { get; set; }
        public virtual ProfileDTO Profile { get; set; }

    }
}

