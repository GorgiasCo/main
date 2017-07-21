using System;

namespace Gorgias.Business.DataTransferObjects.Report
{
    public class ProfileVisitReport
    {
        public Int64 ProfileView { get; set; }
        public Int64? AlbumView { get; set; }
        public Int64 ProfileVisit
        {
            get
            {
                if (AlbumView.HasValue)
                {
                    return ProfileView + AlbumView.Value;
                } else
                {
                    return ProfileView;
                }
            }
        }
    }
}