namespace Gorgias.Business.DataTransferObjects.Mobile.V2
{
    public class ProfileMobileModel
    {
        public int ProfileID { get; set; }
        public string ProfileFullname { get; set; }
        public string ProfileFullnameEnglish { get; set; }
        public bool ProfileIsConfirmed { get; set; }
        public bool ProfileIsPeople { get; set; }
        public string ProfileImage { get; set; }        
    }
}