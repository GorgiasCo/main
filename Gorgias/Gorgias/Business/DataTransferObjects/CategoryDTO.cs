using System;
using FluentValidation.Attributes;
using Gorgias.Business.Validators;
namespace Gorgias.Business.DataTransferObjects
{

    [Validator(typeof(CategoryValidator))]
    public class CategoryDTO
    {
        private string _categoryName;
        public int CategoryID { get; set; }
        public String CategoryName
        {
            get
            {
                if (Multilanguage == null)
                {
                    return Multilanguage;
                }
                return _categoryName;
            }
            set
            {
                _categoryName = value;
            }
        }
        public Boolean CategoryStatus { get; set; }
        public String CategoryImage { get; set; }
        public String CategoryDescription { get; set; }
        public int CategoryParentID { get; set; }

        public string Multilanguage
        {
            get
            {
                return "Hello World ;)";
            }
        }


    }
}

