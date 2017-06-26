using System;

namespace Gorgias.Business.DataTransferObjects.Web
{
    public class AlbumModel
    {
        public int CategoryID { get; set; }
        public int AlbumID { get; set; }
        public string AlbumName { get; set; }
        public DateTime AlbumDateCreated { get; set; }
        public DateTime AlbumDatePublish { get; set; }
        public DateTime AlbumDateExpire { get; set; }
        public int AlbumAvailability { get; set; }
        public string AlbumCover { get; set; }
        public bool isValidate {
            get {
                if (AlbumDateExpire > DateTime.UtcNow)
                {
                    return true;
                }else
                {
                    return false;
                }
            }
        }
    }
}