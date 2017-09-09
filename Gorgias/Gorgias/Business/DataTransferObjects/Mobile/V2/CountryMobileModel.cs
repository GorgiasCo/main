namespace Gorgias.Business.DataTransferObjects.Mobile.V2
{
    public class CountryMobileModel
    {
        private string _countryName;
        public int CountryID { get; set; }
        public string CountryName
        {
            get
            {
                if (Multilanguage != null)
                {
                    return Multilanguage;
                }
                return _countryName;
            }
            set
            {
                _countryName = value;
            }
        }        

        [Newtonsoft.Json.JsonIgnore]
        public string Multilanguage { get; set; }
    }
}