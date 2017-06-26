using System;
using FluentValidation.Attributes;
using Gorgias.Business.Validators;
namespace Gorgias.Business.DataTransferObjects
{

    [Validator(typeof(ProfileTagValidator))]
    public class ProfileTagDTO
    {
        public int TagID { get; set; }
        public virtual string TagName { get; set; }
        public int ProfileID { get; set; }
        public Boolean ProfileTagStatus { get; set; }
        public virtual TagDTO Tag { get; set; }
        public virtual ProfileDTO Profile { get; set; }

    }
}

