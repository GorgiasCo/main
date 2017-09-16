using System.Data.Entity.Spatial;

namespace Gorgias.Business.DataTransferObjects
{
    public class LocationDTO
    {
        public int ProfileActivityID { get; set; }
        public DbGeography ProfileActivityLocation { get; set; }

        public string Location { get; set; }
    }
}