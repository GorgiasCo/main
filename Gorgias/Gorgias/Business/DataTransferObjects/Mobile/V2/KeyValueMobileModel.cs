namespace Gorgias.Business.DataTransferObjects.Mobile.V2
{
    public class KeyValueMobileModel
    {
        private string _keyName;
        public int KeyID { get; set; }
        public string KeyExtra { get; set; }
        public string KeyName
        {
            get
            {
                if (Multilanguage != null)
                {
                    return Multilanguage;
                }
                return _keyName;
            }
            set
            {
                _keyName = value;
            }
        }

        [Newtonsoft.Json.JsonIgnore]
        public string Multilanguage { get; set; }
    }
}