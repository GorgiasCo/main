using System;

namespace Gorgias.Business.DataTransferObjects.Mobile
{
    public class CommentModel
    {
        public int CommentID { get; set; }
        public String CommentNote { get; set; }
        public int CommentLike { get; set; }
        public DateTime CommentDateTime { get; set; }
        public Boolean CommentStatus { get; set; }
        public int ProfileID { get; set; }
        public bool ProfileIsConfirmed { get; set; }
        public int ContentID { get; set; }

        public string ProfileFullname { get; set; }

        public string CommentDate
        {
            get
            {
                return Infrastruture.Core.DateToMoment.TimeAgo(CommentDateTime);
            }
        }

    }
}