namespace Gorgias.Business.DataTransferObjects.Mobile
{
    public class ContentMobileModel
    {
        public string ContentURL { get; set; }
        public int ContentID { get; set; }
        public string ContentTitle { get; set; }
        public int? ContentLike { get; set; }
        public int ContentComments { get; set; }
    }
}