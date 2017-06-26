
namespace Gorgias.Business.DataTransferObjects.Web
{
    public class ProfileSemiModel
    {
        public int ProfileID { get; set; }
        public string ProfileFullname { get; set; }
        public string ProfileImage { get; set; }
        public bool ProfileIsPeople { get; set; }
        public int ProfileTypeID { get; set; }
        public int ThemeID { get; set; }
        public int? RequestedProfileID { get; set; }
    }
}