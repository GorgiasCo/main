using System;
using FluentValidation.Attributes;
using Gorgias.Business.Validators;
namespace Gorgias.Business.DataTransferObjects
{   

    [Validator(typeof(AttributeValidator))]
    public class AttributeDTO
    {
            public int AttributeID{get; set;}
            public String AttributeName{get; set;}
            public String AttributeCaption{get; set;}
            public Boolean AttributeStatus{get; set;}
            public int AttributeMode{get; set;}
            public String AttributeRule{get; set;}
            public String AttributeType{get; set;}
            public String AttributeImage{get; set;}
            public String AttributeDescription{get; set;}
        
        
    }
}   

