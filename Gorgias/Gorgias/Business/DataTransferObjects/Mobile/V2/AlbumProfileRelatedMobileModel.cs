using System.Linq;

namespace Gorgias.Business.DataTransferObjects.Mobile.V2
{
    public class AlbumProfileRelatedMobileModel
    {
        public int ProfileID { get; set; }        
        public string AlbumTitle { get; set; }
        public int AlbumID { get; set; }
        public string AlbumCover { get; set; }
        public string ProfileFullname { get; set; }
        public System.DateTime AlbumPublishDate { get; set; }

        public string cdnAlbumCover
        {
            get
            {
                //return "ddd";
                return AlbumCover.Replace("https://gorgiasasia.blob.core.windows.net/", "https://gorgiascdn.azureedge.net/");
                //return AlbumCover.Replace("https://gorgiasasia.blob.core.windows.net/", "https://gorgiasresizer.azurewebsites.net/");
            }
        }

        //Content Dimension Info
        public string ContentDimension { get; set; }

        public string[] Dimension
        {
            get
            {
                if (ContentDimension != null)
                {
                    return ContentDimension.Split('-');
                }
                else
                {
                    return null;
                }
            }
        }
        public int ContentWidth
        {
            get
            {
                if (Dimension != null)
                {
                    if (Dimension.Count() > 0)
                    {
                        return int.Parse(Dimension[0]);
                    }
                    else
                    {
                        return 0;
                    }
                }
                else
                {
                    return 0;
                }
            }
        }

        public int ContentHeight
        {
            get
            {
                if (Dimension != null)
                {
                    if (Dimension.Count() > 0)
                    {
                        return int.Parse(Dimension[1]);
                    }
                    else
                    {
                        return 0;
                    }
                }
                else
                {
                    return 0;
                }
            }
        }

        public int ContentDeviceHeight
        {
            get
            {
                if (ContentHeight > 0 && DeviceWidth > 0 && ContentWidth > 0)
                {
                    return ((DeviceWidth) * ContentHeight) / (ContentWidth);
                }
                return 0;
            }
        }
        //Content Type Info
        public int DeviceWidth { get; set; }

    }
}