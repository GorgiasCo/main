using System;
using FluentValidation.Attributes;
using Gorgias.Business.Validators;
namespace Gorgias.Business.DataTransferObjects
{

    [Validator(typeof(TagValidator))]
    public class TagDTO
    {
        public int TagID { get; set; }
        public string TagName { get; set; }
        public bool TagStatus { get; set; }
        public bool TagIsPrimary { get; set; }
        public int TagWeight { get; set; }
    }
}

