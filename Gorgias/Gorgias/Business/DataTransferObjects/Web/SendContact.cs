namespace Gorgias.Business.DataTransferObjects.Web
{
    public class SendContact
    {
        public int ProfileID { get; set; }
        public int RequestedProfileID { get; set; }
        public string ContactSubject { get; set; }
        public string ContactNote { get; set; }
    }
}