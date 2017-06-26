namespace Gorgias.Business.DataTransferObjects.Mobile
{
    public class AlbumAvailabilityModel
    {
        public int AlbumCounter { get; set; }
        public int? AlbumAvailability { get; set; }
        public string AlbumTypeName { get; set; }
        public int AlbumTypeLimitation { get; set; }
        public int AlbumReminder { get; set; }
        public bool AvailabilityStatus { get; set; }
    }
}