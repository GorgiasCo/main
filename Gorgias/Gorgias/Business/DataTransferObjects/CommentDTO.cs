using System;
using FluentValidation.Attributes;
using Gorgias.Business.Validators;
namespace Gorgias.Business.DataTransferObjects
{   

    [Validator(typeof(CommentValidator))]
    public class CommentDTO
    {
            public int CommentID{get; set;}
            public String CommentNote{get; set;}
            public int CommentLike{get; set;}
            public DateTime CommentDateTime{get; set;}
            public Boolean CommentStatus{get; set;}
            public int ProfileID{get; set;}
            public int ContentID{get; set;}
        
            public virtual ProfileDTO Profile {get; set;}
            public virtual ContentDTO Content {get; set;}
        
    }
}   

