using System;
using FluentValidation.Attributes;
using Gorgias.Business.Validators;
namespace Gorgias.Business.DataTransferObjects
{

    [Validator(typeof(LanguageValidator))]
    public class LanguageDTO
    {
        public int LanguageID { get; set; }
        public string LanguageName { get; set; }
        public string LanguageCode { get; set; }
        public bool LanguageStatus { get; set; }
    }
}

