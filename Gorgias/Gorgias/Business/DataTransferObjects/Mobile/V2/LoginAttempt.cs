namespace Gorgias.Business.DataTransferObjects.Mobile.V2
{
    public class LoginAttempt
    {
        public int? ProfileID { get; set; }
        public string ProfileEmail { get; set; }
        public bool isValid { get; set; }
        public bool alreadyRegistered { get; set; }
    }
}