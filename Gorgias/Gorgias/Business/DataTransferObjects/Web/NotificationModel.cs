namespace Gorgias.Business.DataTransferObjects.Web
{
    public class NotificationModel
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public string AlbumID { get; set; }
        public string ChannelID { get; set; }
        public int? ProfileID { get; set; }
        public string ProfileFullname { get; set; }
    }
}