using System;
using FluentValidation.Attributes;
using Gorgias.Business.Validators;
namespace Gorgias.Business.DataTransferObjects
{

    [Validator(typeof(ReportTypeValidator))]
    public class ReportTypeDTO
    {
        public int ReportTypeID { get; set; }
        public string ReportTypeName { get; set; }
        public bool ReportTypeIsCountable { get; set; }
        public bool ReportTypeStatus { get; set; }


    }
}

