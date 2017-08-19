using System;
using FluentValidation.Attributes;
using Gorgias.Business.Validators;
using System.Linq;
using System.Collections.Generic;

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
                if (Multilanguage != null)
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
        public string CategoryDescription { get; set; }
        public int CategoryParentID { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        public virtual ICollection<CategoryDTO> ChildCategory { get; set; }
        [Newtonsoft.Json.JsonIgnore]
        public virtual CategoryDTO ParentCategory { get; set; }

        //[Newtonsoft.Json.JsonIgnore]
        //public string Multilanguage
        //{
        //    get
        //    {
        //        if(ChildCategory.Count > 0)
        //        {
        //            return ChildCategory.First().CategoryName;
        //        }
        //        return null;
        //    }
        //}

        [Newtonsoft.Json.JsonIgnore]
        public string Multilanguage { get;set;}


    }
}

