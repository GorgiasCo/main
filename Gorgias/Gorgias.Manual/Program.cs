using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Auth;

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

            //!x.AlbumCover.EndsWith(".jpg") 3014
            var list = (from x in ex.Albums.Include("Contents") where x.AlbumID > 3155 orderby x.AlbumID descending select x).ToList();

            foreach (Album objAlbum in list)
            {
                ex.Albums.Attach(objAlbum);
                //objAlbum.AlbumCover = objAlbum.AlbumCover + ".jpg";
                foreach (Content content in objAlbum.Contents)
                {
                    try
                    {
                        string image = content.ContentURL; //@"https://gorgiasasia.blob.core.windows.net/albums/album-6e998edc-9601-46f7-ae98-db9eb39b634f.jpg?timestamp=522";
                        byte[] imageData = new WebClient().DownloadData(image);
                        MemoryStream imgStream = new MemoryStream(imageData);
                        Image img = Image.FromStream(imgStream);


                        //HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(@"http://img.khoahoc.tv/photos/image/2015/05/14/hang_13.jpg";);

                        //HttpWebResponse response = (HttpWebResponse)req.GetResponse();

                        //Stream stream = response.GetResponseStream();

                        //Image img = Image.FromStream(stream);

                        //stream.Close();
                        content.ContentDimension = img.Width + "-" + img.Height;
                        Console.WriteLine("******************----" + content.ContentID + "--------" + content.ContentDimension);
                    }
                    catch
                    {

                    }

                }

                //Task.Run(async () =>
                //{
                //    StorageCredentials cred = new StorageCredentials("gorgiasasia", "pSFKEXa2PhmHONBNwsad8qRisJhbrkBaWOITFfNwjb4T9w5gAIGHyEBNJb8AzCmy9QN0TgL9l3keDw9Pnfi3Gg==");
                //    CloudBlobContainer container = new CloudBlobContainer(new Uri("https://gorgiasasia.blob.core.windows.net/albums/"), cred);

                //    string fileName = objAlbum.AlbumCover.Replace("https://gorgiasasia.blob.core.windows.net/albums/", "");
                //    string newFileName = objAlbum.AlbumCover.Replace("https://gorgiasasia.blob.core.windows.net/albums/", "") + ".jpg";
                //    await container.CreateIfNotExistsAsync();

                //    CloudBlockBlob blobCopy = container.GetBlockBlobReference(newFileName);

                //    if (!await blobCopy.ExistsAsync())
                //    {
                //        CloudBlockBlob blob = container.GetBlockBlobReference(fileName);

                //        if (await blob.ExistsAsync())
                //        {
                //            await blobCopy.StartCopyAsync(blob);
                //            await blob.DeleteIfExistsAsync();
                //        }
                //    }

                //    Console.WriteLine(objAlbum.AlbumID + " - " + objAlbum.AlbumCover.Replace("https://gorgiasasia.blob.core.windows.net/albums/", ""));

                //}).GetAwaiter().GetResult();

                    ex.SaveChanges();
                }

                //try
                //{
                //   //ex.SaveChanges();
                //}
                //catch (Exception exception)
                //{
                //    Console.WriteLine(exception.InnerException);
                //}





                Console.WriteLine("******************------------" + list.Count);

            Console.ReadLine();

        }
    }
}
