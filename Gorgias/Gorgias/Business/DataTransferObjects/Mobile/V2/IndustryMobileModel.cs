namespace Gorgias.Business.DataTransferObjects.Mobile.V2
{
    public class IndustryMobileModel
    {
        private string _industryName;
        public int IndustryID { get; set; }
        public string IndustryName
        {
            get
            {
                if (Multilanguage != null)
                {
                    return Multilanguage;
                }
                return _industryName;
            }
            set
            {
                _industryName = value;
            }
        }        

        [Newtonsoft.Json.JsonIgnore]
        public string Multilanguage { get; set; }
    }
}