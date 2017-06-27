using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gorgias.Manual
{
    class Program
    {
        static void Main(string[] args)
        {
            SqlServerTypes.Utilities.LoadNativeAssemblies(AppDomain.CurrentDomain.BaseDirectory);

            GorgiasEntities ex = new GorgiasEntities();

            var list = (from x in ex.Albums.Include("Contents") where x.Contents.Count == 0 && x.AlbumAvailability == 0 select x).ToList();

            foreach(Album objAlbum in list)
            {
                Content newContent = new Content();
                newContent.AlbumID = objAlbum.AlbumID;
                newContent.ContentCreatedDate = objAlbum.AlbumDateCreated;
                newContent.ContentIsDeleted = false;
                newContent.ContentLike = 0;
                newContent.ContentStatus = true;
                if(objAlbum.AlbumName != null)
                {
                    newContent.ContentTitle = objAlbum.AlbumName;
                }                
                newContent.ContentType = 1;
                newContent.ContentURL = objAlbum.AlbumCover;
                newContent.ContentGeoLocation = null;
                ex.Contents.Add(newContent);
                //if (objAlbum.AlbumID == 848)
                //{
                    
                //}

                Console.WriteLine(objAlbum.AlbumID);
            }

            try
            {
               // ex.SaveChanges();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.InnerException);
            }
            

            Console.WriteLine("******************------------"+list.Count);
            Console.ReadLine();
        }
    }
}
