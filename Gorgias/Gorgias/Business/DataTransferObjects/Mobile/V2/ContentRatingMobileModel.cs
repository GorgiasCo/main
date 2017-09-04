namespace Gorgias.Business.DataTransferObjects.Mobile.V2
{
    public class ContentRatingMobileModel
    {
        private string _contentRatingName;
        public int ContentRatingID { get; set; }
        public string ContentRatingName
        {
            get
            {
                if (Multilanguage != null)
                {
                    return Multilanguage;
                }
                return _contentRatingName;
            }
            set
            {
                _contentRatingName = value;
            }
        }

        [Newtonsoft.Json.JsonIgnore]
        public string Multilanguage { get; set; }
    }
}