using FluentValidation;
using FluentValidation.Attributes;
using Gorgias.Infrastruture.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Gorgias.Models
{
    [Validator(typeof(GenreValidator))]
    public class Genre
    {
        public String Name { get; set; }
        //public int ID { get; set; }
        //public bool Status { get; set; }

        //public DateTime Time { get; set; }
    }
}