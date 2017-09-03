using System;
using FluentValidation.Attributes;
using Gorgias.Business.Validators;
namespace Gorgias.Business.DataTransferObjects
{   

    [Validator(typeof(ContentTypeValidator))]
    public class ContentTypeDTO
    {
            public int ContentTypeID{get; set;}
            public String ContentTypeName{get; set;}
            public int ContentTypeOrder{get; set;}
            public Boolean ContentTypeStatus{get; set;}
            public String ContentTypeLanguageCode{get; set;}
            public String ContentTypeExpression{get; set;}
            public int? ContentTypeParentID{get; set;}
        
            public virtual ContentTypeDTO ContentTypeParent {get; set;}
        
    }
}   

