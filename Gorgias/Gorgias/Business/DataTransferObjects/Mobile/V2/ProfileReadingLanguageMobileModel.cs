namespace Gorgias.Business.DataTransferObjects.Mobile.V2
{
    public class ProfileReadingLanguageMobileModel
    {
        public int? ProfileReadingID { get; set; }
        public int LanguageID { get; set; }
        public string LanguageName { get; set; }
        public string LanguageCode { get; set; }
        public bool isSelected { get; set; }
    }
}