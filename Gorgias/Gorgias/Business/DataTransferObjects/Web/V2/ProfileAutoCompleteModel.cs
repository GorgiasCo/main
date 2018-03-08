namespace Gorgias.Business.DataTransferObjects.Web.V2
{
    public class ProfileAutoCompleteModel
    {
        public string ProfileFullname { get; set; }
        public int UserID { get; set; }
        public string ProfileEmail { get; set; }

        public string ProfileInformation
        {
            get
            {
                return ProfileFullname + ", " + ProfileEmail;
            }
        }        
    }
}