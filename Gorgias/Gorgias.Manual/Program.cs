using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;


namespace Gorgias.Manual
{
    class Program
    {
        static void Main(string[] args)
        {

            //string image = @"https://gorgiasasia.blob.core.windows.net/albums/album-6e998edc-9601-46f7-ae98-db9eb39b634f.jpg?timestamp=522";
            //byte[] imageData = new WebClient().DownloadData(image);
            //MemoryStream imgStream = new MemoryStream(imageData);
            //Image img = Image.FromStream(imgStream);

            //HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(@"http://img.khoahoc.tv/photos/image/2015/05/14/hang_13.jpg";);

            //HttpWebResponse response = (HttpWebResponse)req.GetResponse();

            //Stream stream = response.GetResponseStream();

            //Image img = Image.FromStream(stream);

            //stream.Close();

            SqlServerTypes.Utilities.LoadNativeAssemblies(AppDomain.CurrentDomain.BaseDirectory);

            GorgiasEntities ex = new GorgiasEntities();

            var list = (from x in ex.Albums.Include("Contents") where !x.AlbumCover.EndsWith(".jpg") orderby x.AlbumID descending select x).ToList();

            foreach(Album objAlbum in list)
            {
                //ex.Albums.Attach(objAlbum);
                //foreach(Content content in objAlbum.Contents)
                //{
                //    try
                //    {
                //        string image = content.ContentURL; //@"https://gorgiasasia.blob.core.windows.net/albums/album-6e998edc-9601-46f7-ae98-db9eb39b634f.jpg?timestamp=522";
                //        byte[] imageData = new WebClient().DownloadData(image);
                //        MemoryStream imgStream = new MemoryStream(imageData);
                //        Image img = Image.FromStream(imgStream);


                //        //HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(@"http://img.khoahoc.tv/photos/image/2015/05/14/hang_13.jpg";);

                //        //HttpWebResponse response = (HttpWebResponse)req.GetResponse();

                //        //Stream stream = response.GetResponseStream();

                //        //Image img = Image.FromStream(stream);

                //        //stream.Close();
                //        content.ContentDimension = img.Width + "-" + img.Height;
                //        Console.WriteLine("******************----" + content.ContentID + "--------" + content.ContentDimension);
                //    }
                //    catch
                //    {

                //    }

                //}
                //ex.SaveChanges();
                Console.WriteLine(objAlbum.AlbumID);
            }

            //try
            //{
            //   //ex.SaveChanges();
            //}
            //catch (Exception exception)
            //{
            //    Console.WriteLine(exception.InnerException);
            //}
            

            Console.WriteLine("******************------------"+list.Count);

            Console.ReadLine();

        }
    }
}
