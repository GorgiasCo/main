using System;
using FluentValidation.Attributes;
using Gorgias.Business.Validators;
namespace Gorgias.Business.DataTransferObjects
{

    [Validator(typeof(ConnectionValidator))]
    public class ConnectionDTO
    {
        public int ProfileID { get; set; }
        public int RequestedProfileID { get; set; }
        public int RequestTypeID { get; set; }
        public Boolean ConnectStatus { get; set; }
        public DateTime ConnectDateCreated { get; set; }

        public virtual ProfileDTO Profile { get; set; }
        public virtual ProfileDTO Profile1 { get; set; }
        public virtual RequestTypeDTO RequestType { get; set; }

    }
}

