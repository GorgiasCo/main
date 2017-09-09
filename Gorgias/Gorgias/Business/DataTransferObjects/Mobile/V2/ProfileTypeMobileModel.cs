namespace Gorgias.Business.DataTransferObjects.Mobile.V2
{
    public class ProfileTypeMobileModel
    {
        private string _profileTypeName;
        public int ProfileTypeID { get; set; }
        public string ProfileTypeName
        {
            get
            {
                if (Multilanguage != null)
                {
                    return Multilanguage;
                }
                return _profileTypeName;
            }
            set
            {
                _profileTypeName = value;
            }
        }
        
        [Newtonsoft.Json.JsonIgnore]
        public string Multilanguage { get; set; }
    }
}