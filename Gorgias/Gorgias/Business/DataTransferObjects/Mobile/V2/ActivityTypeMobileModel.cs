namespace Gorgias.Business.DataTransferObjects.Mobile.V2
{
    public class ActivityTypeMobileModel
    {
        private string _activityTypeName;
        public int ActivityTypeID { get; set; }
        public string ActivityTypeName
        {
            get
            {
                if (Multilanguage != null)
                {
                    return Multilanguage;
                }
                return _activityTypeName;
            }
            set
            {
                _activityTypeName = value;
            }
        }

        [Newtonsoft.Json.JsonIgnore]
        public string Multilanguage { get; set; }
    }
}