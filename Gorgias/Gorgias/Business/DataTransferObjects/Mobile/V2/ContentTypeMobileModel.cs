namespace Gorgias.Business.DataTransferObjects.Mobile.V2
{
    public class ContentTypeMobileModel
    {
        private string _contentTypeName;
        public int ContentTypeID { get; set; }
        public string ContentTypeName
        {
            get
            {
                if (Multilanguage != null)
                {
                    return Multilanguage;
                }
                return _contentTypeName;
            }
            set
            {
                _contentTypeName = value;
            }
        }

        public string ContentTypeExpression { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        public string Multilanguage { get; set; }
    }
}