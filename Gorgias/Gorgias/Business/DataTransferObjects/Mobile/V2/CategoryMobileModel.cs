using System;

namespace Gorgias.Business.DataTransferObjects.Mobile.V2
{
    public class CategoryMobileModel
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
        public int? CategoryType { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        public string Multilanguage { get; set; }
    }
}