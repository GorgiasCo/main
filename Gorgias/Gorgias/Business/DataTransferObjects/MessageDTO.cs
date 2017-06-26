using System;
using FluentValidation.Attributes;
using Gorgias.Business.Validators;
namespace Gorgias.Business.DataTransferObjects
{

    [Validator(typeof(MessageValidator))]
    public class MessageDTO
    {
        public int MessageID { get; set; }
        public String MessageNote { get; set; }
        public DateTime MessageDateCreated { get; set; }
        public String MessageSubject { get; set; }
        public Boolean MessageStatus { get; set; }
        public Boolean MessageIsDeleted { get; set; }
        public int ProfileID { get; set; }
        public virtual ProfileDTO Profile { get; set; }
        public virtual ProfileDTO Profile1 { get; set; }
    }
}

