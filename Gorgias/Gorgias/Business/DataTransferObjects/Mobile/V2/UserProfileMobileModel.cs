namespace Gorgias.Business.DataTransferObjects.Mobile.V2
{
    public class UserProfileMobileModel
    {
        public int UserID { get; set; }
        public int ProfileID { get; set; }
        public string ProfileFullname { get; set; }
        public bool ProfileIsConfirmed { get; set; }
        public bool ProfileIsPeople { get; set; }
        public string ProfileImage { get; set; }
        public int UserRoleID { get; set; }
    }
}