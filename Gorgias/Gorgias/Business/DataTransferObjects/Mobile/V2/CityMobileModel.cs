namespace Gorgias.Business.DataTransferObjects.Mobile.V2
{
    public class CityMobileModel
    {
        private string _cityName;
        public int CityID { get; set; }
        public string CityName
        {
            get
            {
                if (Multilanguage != null)
                {
                    return Multilanguage;
                }
                return _cityName;
            }
            set
            {
                _cityName = value;
            }
        }      

        [Newtonsoft.Json.JsonIgnore]
        public string Multilanguage { get; set; }
    }
}