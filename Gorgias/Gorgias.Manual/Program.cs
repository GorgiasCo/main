﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Auth;
using Gorgias;

namespace Gorgias.Manual
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
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

            setupContents();

            //var resultCountry = readCountriesJSON();

            //setupContents();
            //var main = readJSON();
            //var r = readJSON(@"E:\Yasser\main\Gorgias\Gorgias.Manual\country-hant.csv");
            //var w = readJSON(@"E:\Yasser\main\Gorgias\Gorgias.Manual\country-hans.csv");

            //foreach(Country mainCountry in main)
            //{
            //    Country relatedCountry = r.Where(m => m.CountryShortName == mainCountry.CountryShortName).First();
            //    relatedCountry.CountryLanguageCode = "zh-hant";
            //    mainCountry.CountryChilds.Add(relatedCountry);
            //}

            //foreach (Country mainCountry in main)
            //{
            //    Country relatedCountry = w.Where(m => m.CountryShortName == mainCountry.CountryShortName).First();
            //    relatedCountry.CountryLanguageCode = "zh-hans";
            //    mainCountry.CountryChilds.Add(relatedCountry);
            //}

            //foreach (Country mainCountry in main)
            //{
            //    Console.WriteLine(mainCountry.CountryShortName + ", " + mainCountry.CountryName + ", child: " + mainCountry.CountryChilds.Count);
            //}

            // Need for Country ;)
            //string csv = string.Concat(from re in resultCountry
            //                           select string.Format("{0},{1},{2},{3}\n", re.CountryShortName, re.CountryName, re.CountryChilds.Where(m => m.CountryLanguageCode == "zh-hans").First().CountryName, re.CountryChilds.Where(m => m.CountryLanguageCode == "zh-hant").First().CountryName));


            //writeOnFile(csv, "newCountries.csv");

            //try
            //{
            //   //ex.SaveChanges();
            //}
            //catch (Exception exception)
            //{
            //    Console.WriteLine(exception.InnerException);
            //}

            Console.ReadLine();
        }

        public static void writeOnFile(string note, string filename)
        {
            using (var sw = new StreamWriter(File.Open(@"E:\Yasser\main\Gorgias\Gorgias.Manual\" + filename, FileMode.CreateNew), Encoding.UTF8))
            {
                sw.WriteLine(note);
            }
        }

        public static List<Country> readCountriesJSON()
        {
            using (var reader = new StreamReader(@"E:\Yasser\main\Gorgias\Gorgias.Manual\newCountryiesFromCharls.csv", System.Text.Encoding.UTF8))
            {
                List<Country> output = new List<Country>();
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split('\n');

                    Country obj = new Country();
                    obj.CountryStatus = false;
                    obj.CountryPhoneCode = "0";
                    obj.CountryShortName = values[0].Split(',')[0];
                    obj.CountryName = values[0].Split(',')[1].Replace("\"", "");

                    obj.CountryChilds.Add(new Country { CountryStatus = false, CountryName = values[0].Split(',')[2].Replace("\"", ""), CountryLanguageCode = "zh-hans", CountryPhoneCode = "0"});
                    obj.CountryChilds.Add(new Country { CountryStatus = false, CountryName = values[0].Split(',')[3].Replace("\"", ""), CountryLanguageCode = "zh-hant", CountryPhoneCode = "0" });

                    output.Add(obj);
                    Console.WriteLine(obj.CountryShortName + " --- " + obj.CountryName + " --- " + obj.CountryChilds.Count);
                }
                return output;
            }
        }

        public static List<Country> readJSON()
        {
            using (var reader = new StreamReader(@"E:\Yasser\main\Gorgias\Gorgias.Manual\country-en.csv", System.Text.Encoding.UTF8))
            {
                List<Country> output = new List<Country>();
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split('\n');

                    Country obj = new Country();
                    obj.CountryStatus = false;
                    obj.CountryPhoneCode = "0";
                    obj.CountryShortName = values[0].Split(',')[0];
                    obj.CountryName = values[0].Split(',')[1].Replace("\"", "");

                    output.Add(obj);
                    Console.WriteLine(obj.CountryShortName + " --- " + obj.CountryName);                    
                }
                return output;
            }
        }

        public static List<Country> readJSON(string path)
        {
            using (var reader = new StreamReader(path, System.Text.Encoding.UTF8))
            {
                List<Country> output = new List<Country>();
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split('\n');

                    Country obj = new Country();
                    obj.CountryStatus = false;
                    obj.CountryPhoneCode = "0";
                    obj.CountryShortName = values[0].Split(',')[0];
                    obj.CountryName = values[0].Split(',')[1].Replace("\"", "");

                    output.Add(obj);
                    Console.WriteLine(obj.CountryShortName + " --- " + obj.CountryName);
                }
                return output;
            }
        }

        public static bool setupContents()
        {
            GorgiasEntities ex = new GorgiasEntities();

            //!x.AlbumCover.EndsWith(".jpg") 3291 x.Contents.Any(m=> m.ContentDimension == null && m.ContentType == 1)
            var list = (from x in ex.Albums.Include("Contents") where x.AlbumID > 3331 orderby x.AlbumID descending select x).ToList();

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
                        return false;
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
            Console.WriteLine("******************------------" + list.Count);
            return true;
        }
    }
}
