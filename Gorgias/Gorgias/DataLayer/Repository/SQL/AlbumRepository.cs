using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Gorgias;
using Gorgias.DataLayer.Interface;
using Gorgias.Infrastruture.EntityFramework;
using System.Linq;
using System.Data.Entity;
using Gorgias.Business.DataTransferObjects;

namespace Gorgias.DataLayer.Repository.SQL
{
    public class AlbumRepository : IAlbumRepository, IDisposable
    {
        // To detect redundant calls
        private bool disposedValue = false;

        private GorgiasEntities context = new GorgiasEntities();

        // IDisposable
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (!this.disposedValue)
                {
                    if (disposing)
                    {
                        context.Dispose();
                    }
                }
                this.disposedValue = true;
            }
            this.disposedValue = true;
        }

        #region " IDisposable Support "
        // This code added by Visual Basic to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

        //CRUD Functions
        public Album Insert(String AlbumName, DateTime AlbumDateCreated, Boolean AlbumStatus, String AlbumCover, Boolean AlbumIsDeleted, int CategoryID, int ProfileID)
        {
            try
            {
                Album obj = new Album();
                obj.AlbumName = AlbumName;
                obj.AlbumDateCreated = DateTime.UtcNow;
                obj.AlbumStatus = AlbumStatus;
                obj.AlbumCover = AlbumCover;
                obj.AlbumIsDeleted = AlbumIsDeleted;
                obj.CategoryID = CategoryID;
                obj.ProfileID = ProfileID;

                //Add for Hottest
                obj.AlbumDateExpire = DateTime.Now.AddYears(7);
                obj.AlbumDatePublish = DateTime.Now;
                obj.AlbumAvailability = 0;

                context.Albums.Add(obj);
                context.SaveChanges();
                return obj;
            }
            catch (Exception ex)
            {
                return new Album();
            }
        }

        public Album Insert(String AlbumName, DateTime AlbumDateCreated, DateTime AlbumDatePublish, int AlbumAvailability, Boolean AlbumStatus, String AlbumCover, Boolean AlbumIsDeleted, int CategoryID, int ProfileID, bool? AlbumHasComment)
        {
            try
            {
                Album obj = new Album();
                obj.AlbumName = AlbumName;
                obj.AlbumDateCreated = DateTime.UtcNow;
                obj.AlbumStatus = AlbumStatus;
                obj.AlbumCover = AlbumCover;
                obj.AlbumIsDeleted = AlbumIsDeleted;
                obj.CategoryID = CategoryID;
                obj.ProfileID = ProfileID;
                obj.AlbumDatePublish = obj.AlbumDateCreated;

                if (AlbumHasComment.HasValue)
                {
                    obj.AlbumHasComment = AlbumHasComment.Value;
                } else
                {
                    obj.AlbumHasComment = false;
                }

                //Add for Hottest
                if (AlbumAvailability > 0)
                {
                    obj.AlbumDateExpire = obj.AlbumDatePublish.AddMinutes(AlbumAvailability);
                }
                else
                {
                    //4 years to expire
                    obj.AlbumDateExpire = obj.AlbumDateCreated.AddMonths(48);
                }

                obj.AlbumDatePublish = obj.AlbumDateCreated;
                obj.AlbumAvailability = AlbumAvailability;

                context.Albums.Add(obj);
                context.SaveChanges();
                return obj;
            }
            catch (Exception ex)
            {
                return new Album();
            }
        }


        public bool Update(int AlbumID, String AlbumName, DateTime AlbumDateCreated, Boolean AlbumStatus, String AlbumCover, Boolean AlbumIsDeleted, int CategoryID, int ProfileID)
        {
            Album obj = new Album();
            obj = (from w in context.Albums where w.AlbumID == AlbumID select w).FirstOrDefault();
            if (obj != null)
            {
                context.Albums.Attach(obj);

                obj.AlbumName = AlbumName;
                obj.AlbumDateCreated = DateTime.UtcNow;
                obj.AlbumStatus = AlbumStatus;
                obj.AlbumCover = AlbumCover;
                obj.AlbumIsDeleted = AlbumIsDeleted;
                obj.CategoryID = CategoryID;
                obj.ProfileID = ProfileID;
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Update(int AlbumID)
        {
            Album obj = new Album();
            obj = (from w in context.Albums where w.AlbumID == AlbumID select w).FirstOrDefault();
            if (obj != null)
            {
                context.Albums.Attach(obj);

                obj.AlbumView = obj.AlbumView + 1;                
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Update(int AlbumID, String AlbumName, DateTime AlbumDateCreated, DateTime AlbumDatePublish, int AlbumAvailability, Boolean AlbumStatus, String AlbumCover, Boolean AlbumIsDeleted, int CategoryID, int ProfileID, bool? AlbumHasComment)
        {
            Album obj = new Album();
            obj = (from w in context.Albums where w.AlbumID == AlbumID select w).FirstOrDefault();
            if (obj != null)
            {
                context.Albums.Attach(obj);

                obj.AlbumName = AlbumName;
                obj.AlbumDateCreated = DateTime.UtcNow;
                obj.AlbumStatus = AlbumStatus;
                obj.AlbumCover = AlbumCover;
                obj.AlbumIsDeleted = AlbumIsDeleted;
                obj.CategoryID = CategoryID;
                obj.ProfileID = ProfileID;

                if (AlbumHasComment.HasValue)
                {
                    obj.AlbumHasComment = AlbumHasComment.Value;
                }
                else
                {
                    obj.AlbumHasComment = false;
                }

                //Add for Hottest
                if (AlbumAvailability > 0)
                {
                    obj.AlbumDateExpire = AlbumDatePublish.AddMinutes(AlbumAvailability);
                }
                else
                {
                    //4 years to expire
                    obj.AlbumDateExpire = AlbumDatePublish.AddMonths(48);
                }
                obj.AlbumDatePublish = AlbumDatePublish;
                obj.AlbumAvailability = AlbumAvailability;

                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Delete(int AlbumID)
        {
            Album obj = new Album();
            obj = (from w in context.Albums where w.AlbumID == AlbumID select w).FirstOrDefault();
            if (obj != null)
            {
                context.Albums.Attach(obj);
                context.Albums.Remove(obj);
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public Album GetAlbum(int AlbumID)
        {
            return (from w in context.Albums where w.AlbumID == AlbumID select w).FirstOrDefault();
        }

        //Lists
        public List<Album> GetAlbumsAll()
        {
            return (from w in context.Albums orderby w.AlbumID descending select w).ToList();
        }
        public List<Album> GetAlbumsAll(bool AlbumStatus)
        {
            return (from w in context.Albums where w.AlbumStatus == AlbumStatus orderby w.AlbumID descending select w).ToList();
        }
        //List Pagings
        public List<Album> GetAlbumsAll(int page = 1, int pageSize = 7, string filter = null)
        {
            var xList = new List<Album>();
            if (filter != null)
            {
                xList = (from w in context.Albums orderby w.AlbumID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
            }
            else
            {
                xList = (from w in context.Albums orderby w.AlbumID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
            }
            return xList;
        }
        public List<Album> GetAlbumsAll(bool AlbumStatus = true, int page = 1, int pageSize = 7, string filter = null)
        {
            var xList = new List<Album>();
            if (filter != null)
            {
                xList = (from w in context.Albums where w.AlbumStatus == AlbumStatus orderby w.AlbumID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
            }
            else
            {
                xList = (from w in context.Albums where w.AlbumStatus == AlbumStatus orderby w.AlbumID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
            }
            return xList;
        }

        public object getAlbumSet(int ProfileID)
        {
            return (from w in context.Albums.Include("Contents")
                    where w.ProfileID == ProfileID && w.Contents.Count > 2
                    orderby w.AlbumID descending
                    select new
                    {
                        w.AlbumName,
                        w.Profile.ProfileFullname,
                        Contents = w.Contents.Select(cc => cc.ContentURL).Take(4)
                    }).Take(5);
        }

        //Business.DataTransferObjects.Mobile.AlbumMobileModel
        public Business.DataTransferObjects.Mobile.AlbumMobileModel GetAlbumContent(int AlbumID)
        {
            bool resultAlbumCount = Update(AlbumID);
            if (resultAlbumCount)
            {
                var result = (from w in context.Albums.Include("Contents") where w.AlbumID == AlbumID && w.AlbumStatus == true && w.AlbumIsDeleted == false select new Business.DataTransferObjects.Mobile.AlbumMobileModel { AlbumCover = w.AlbumCover, AlbumDateCreated = w.AlbumDateCreated, AlbumName = w.AlbumName, AlbumAvailability = w.AlbumAvailability, AlbumDateExpire = w.AlbumDateExpire, AlbumDatePublish = w.AlbumDatePublish, AlbumAvailabilityName = w.AlbumType.AlbumTypeName, AlbumLike = w.Contents.Sum(m => m.ContentLike), AlbumContents = w.Contents.Count, Contents = w.Contents.Select(c => new Business.DataTransferObjects.Mobile.ContentMobileModel() { ContentURL = c.ContentURL, ContentID = c.ContentID, ContentTitle = c.ContentTitle, ContentComments = c.Comments.Count }).ToList() }).FirstOrDefault();
                return result;
            }
            return null;            
        }

        public IList<Business.DataTransferObjects.Mobile.AlbumMobileModel> GetAlbumContentsAsQueryable(int ProfileID, int page = 1, int pageSize = 7, int contentSize = 6)
        {
            var result =  (from w in context.Albums.Include("Contents") where w.AlbumStatus == true && w.ProfileID == ProfileID && w.AlbumIsDeleted == false orderby w.AlbumDateCreated descending select new Business.DataTransferObjects.Mobile.AlbumMobileModel { AlbumID = w.AlbumID , AlbumCover = w.AlbumCover, AlbumDateCreated = w.AlbumDateCreated, AlbumName = w.AlbumName, AlbumAvailability= w.AlbumAvailability, AlbumDateExpire=w.AlbumDateExpire, AlbumDatePublish=w.AlbumDatePublish, AlbumAvailabilityName = w.AlbumType.AlbumTypeName, AlbumLike = w.Contents.Sum(m=> m.ContentLike), AlbumContents = w.Contents.Count, Contents = w.Contents.OrderByDescending(c => c.ContentCreatedDate).Select(c=> new Business.DataTransferObjects.Mobile.ContentMobileModel(){ ContentLike = c.ContentLike, ContentURL = c.ContentURL, ContentID = c.ContentID, ContentTitle = c.ContentTitle, ContentComments = c.Comments.Count}).Take(contentSize).ToList()}).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();            
            return result.ToList();
        }

        public IList<Business.DataTransferObjects.Mobile.AlbumMobileAdminModel> GetAdminAlbumContentsAsQueryable(int ProfileID, int page = 1, int pageSize = 7, int contentSize = 6)
        {
            var result = (from w in context.Albums.Include("Contents") where w.AlbumStatus == true && w.ProfileID == ProfileID && w.AlbumIsDeleted == false orderby w.AlbumDateCreated descending select new Business.DataTransferObjects.Mobile.AlbumMobileAdminModel { AlbumID = w.AlbumID, AlbumCover = w.AlbumCover, AlbumDateCreated = w.AlbumDateCreated, AlbumName = w.AlbumName, AlbumAvailability = w.AlbumAvailability, AlbumDateExpire = w.AlbumDateExpire, AlbumDatePublish = w.AlbumDatePublish, AlbumCategoryName = w.Category.CategoryName, AlbumAvailabilityName = w.AlbumType.AlbumTypeName, AlbumLike = w.Contents.Sum(m => m.ContentLike), AlbumContents = w.Contents.Count, Contents = w.Contents.OrderByDescending(c => c.ContentCreatedDate).Select(c => new Business.DataTransferObjects.Mobile.ContentMobileModel() { ContentLike = c.ContentLike, ContentURL = c.ContentURL, ContentID = c.ContentID, ContentTitle = c.ContentTitle, ContentComments = c.Comments.Count }).Take(contentSize).ToList() }).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
            return result.ToList();
        }

        public async System.Threading.Tasks.Task<IList<Business.DataTransferObjects.Mobile.AlbumMobileAdminModel>> GetAdminAlbumContentsAsQueryableAsync(int ProfileID, int page = 1, int pageSize = 7, int contentSize = 6)
        {
            var result = (from w in context.Albums.Include("Contents") where w.AlbumStatus == true && w.ProfileID == ProfileID && w.AlbumIsDeleted == false orderby w.AlbumDateCreated descending select new Business.DataTransferObjects.Mobile.AlbumMobileAdminModel { AlbumID = w.AlbumID, AlbumCover = w.AlbumCover, AlbumDateCreated = w.AlbumDateCreated, AlbumName = w.AlbumName, AlbumAvailability = w.AlbumAvailability, AlbumDateExpire = w.AlbumDateExpire, AlbumDatePublish = w.AlbumDatePublish, AlbumCategoryName = w.Category.CategoryName, AlbumAvailabilityName = w.AlbumType.AlbumTypeName, AlbumLike = w.Contents.Sum(m => m.ContentLike), AlbumContents = w.Contents.Count, Contents = w.Contents.OrderByDescending(c => c.ContentCreatedDate).Select(c => new Business.DataTransferObjects.Mobile.ContentMobileModel() { ContentLike = c.ContentLike, ContentURL = c.ContentURL, ContentID = c.ContentID, ContentTitle = c.ContentTitle, ContentComments = c.Comments.Count }).Take(contentSize).ToList() }).Skip(pageSize * (page - 1)).Take(pageSize).ToListAsync();
            return await result;
        }

        public IList<Business.DataTransferObjects.Mobile.AlbumMobileModel> GetAlbumHottestContentsAsQueryable(int ProfileID, int CategoryID, int page = 1, int pageSize = 7)
        {
            var result = (from w in context.Albums.Include("Contents") where w.CategoryID == CategoryID && w.AlbumStatus == true && w.ProfileID == ProfileID && w.AlbumIsDeleted == false && w.AlbumAvailability != 0 orderby w.AlbumID descending select new Business.DataTransferObjects.Mobile.AlbumMobileModel { AlbumCover = w.AlbumCover, AlbumDateCreated = w.AlbumDateCreated, AlbumName = w.AlbumName, AlbumAvailability = w.AlbumAvailability, AlbumDateExpire = w.AlbumDateExpire, AlbumDatePublish = w.AlbumDatePublish, AlbumAvailabilityName = w.AlbumType.AlbumTypeName, AlbumLike = w.Contents.Sum(m => m.ContentLike), AlbumContents = w.Contents.Count, Contents = w.Contents.Select(c => new Business.DataTransferObjects.Mobile.ContentMobileModel() { ContentLike = c.ContentLike, ContentURL = c.ContentURL, ContentID = c.ContentID, ContentTitle = c.ContentTitle, ContentComments = c.Comments.Count }).ToList() }).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
            return result.ToList();
        }

        public IList<Business.DataTransferObjects.Mobile.AlbumMobileModel> GetAlbumGalleryContentsAsQueryable(int ProfileID, int CategoryID, int page = 1, int pageSize = 7)
        {
            var result = (from w in context.Albums.Include("Contents") where w.CategoryID == CategoryID && w.Category.CategoryStatus == true && w.AlbumStatus == true && w.ProfileID == ProfileID && w.AlbumIsDeleted == false && w.AlbumAvailability == 0 orderby w.AlbumDateCreated descending select new Business.DataTransferObjects.Mobile.AlbumMobileModel { AlbumID = w.AlbumID, AlbumCover = w.AlbumCover, AlbumDateCreated = w.AlbumDateCreated, AlbumName = w.AlbumName, AlbumAvailability = w.AlbumAvailability, AlbumDateExpire = w.AlbumDateExpire, AlbumDatePublish = w.AlbumDatePublish, AlbumAvailabilityName = w.AlbumType.AlbumTypeName, AlbumLike = w.Contents.Sum(m => m.ContentLike), AlbumContents = w.Contents.Count, Contents = w.Contents.OrderByDescending(c => c.ContentCreatedDate).Select(c => new Business.DataTransferObjects.Mobile.ContentMobileModel() { ContentLike = c.ContentLike, ContentURL = c.ContentURL, ContentID = c.ContentID, ContentTitle = c.ContentTitle, ContentComments = c.Comments.Count }).ToList() }).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
            return result.ToList();
        }

        public IList<Business.DataTransferObjects.Mobile.AlbumMobileModel> GetAlbumGalleryContentsAsQueryable(int ProfileID, int page = 1, int pageSize = 7)
        {
            var result = (from w in context.Albums.Include("Contents") where w.Category.CategoryStatus == true && w.AlbumStatus == true && w.ProfileID == ProfileID && w.AlbumIsDeleted == false && w.AlbumAvailability == 0 orderby w.AlbumDateCreated descending select new Business.DataTransferObjects.Mobile.AlbumMobileModel { AlbumID = w.AlbumID, AlbumCover = w.AlbumCover, AlbumDateCreated = w.AlbumDateCreated, AlbumName = w.AlbumName, AlbumAvailability = w.AlbumAvailability, AlbumDateExpire = w.AlbumDateExpire, AlbumDatePublish = w.AlbumDatePublish, AlbumAvailabilityName = w.AlbumType.AlbumTypeName, AlbumContents = w.Contents.Count, AlbumLike = w.Contents.Sum(m => m.ContentLike), Contents = w.Contents.OrderByDescending(c => c.ContentCreatedDate).Select(c => new Business.DataTransferObjects.Mobile.ContentMobileModel() { ContentLike = c.ContentLike, ContentURL = c.ContentURL, ContentID = c.ContentID, ContentTitle = c.ContentTitle, ContentComments = c.Comments.Count }).ToList() }).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
            return result.ToList();
        }

        //IQueryable 
        public IQueryable<Business.DataTransferObjects.AlbumContentDTO> GetAlbumsAllForInsertContentAsQueryable()
        {
            return (from w in context.Albums.Include("Profile") orderby w.AlbumID descending select new AlbumContentDTO { AlbumID = w.AlbumID, AlbumName = w.AlbumName + " - " + w.Profile.ProfileFullname }).AsQueryable();
        }
        public IQueryable<Album> GetAlbumsAllAsQueryable()
        {
            return (from w in context.Albums.Include("Profile").Include("Contents") orderby w.AlbumID descending select w).AsQueryable();
        }
        public IQueryable<Album> GetAlbumsAllAsQueryable(bool AlbumStatus)
        {
            return (from w in context.Albums where w.AlbumStatus == AlbumStatus orderby w.AlbumID descending select w).AsQueryable();
        }
        //IQueryable Pagings
        public IQueryable<Album> GetAlbumsAllAsQueryable(int page = 1, int pageSize = 7, string filter = null)
        {
            IQueryable<Album> xList;
            if (filter != null)
            {
                //In Case of Performance, Use following method ;)
                //xList = (from w in context.Albums orderby w.AlbumID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();                
                xList = context.Albums.ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
            }
            else
            {
                //In Case of Performance, Use following method ;)
                //xList = (from w in context.Albums orderby w.AlbumID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();                
                xList = context.Albums.Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
            }
            return xList;
        }
        public IQueryable<Album> GetAlbumsAllAsQueryable(bool AlbumStatus = true, int page = 1, int pageSize = 7, string filter = null)
        {
            IQueryable<Album> xList;
            if (filter != null)
            {
                xList = (from w in context.Albums where w.AlbumStatus == AlbumStatus orderby w.AlbumID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
            }
            else
            {
                xList = (from w in context.Albums where w.AlbumStatus == AlbumStatus orderby w.AlbumID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
            }
            return xList;
        }
        //Relationship Functions based on Foreign Key
        //Relationship List
        public List<Album> GetAlbumsByCategoryID(int CategoryID, bool AlbumStatus)
        {
            return (from w in context.Albums where w.CategoryID == CategoryID && w.AlbumStatus == AlbumStatus orderby w.AlbumID descending select w).ToList();
        }
        public List<Album> GetAlbumsByCategoryID(int CategoryID, int page = 1, int pageSize = 7, string filter = null)
        {
            return (from w in context.Albums where w.CategoryID == CategoryID orderby w.AlbumID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
        }
        public List<Album> GetAlbumsByCategoryID(int CategoryID, bool AlbumStatus, int page = 1, int pageSize = 7, string filter = null)
        {
            return (from w in context.Albums where w.CategoryID == CategoryID && w.AlbumStatus == AlbumStatus orderby w.AlbumID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
        }
        public List<Album> GetAlbumsByProfileID(int ProfileID, bool AlbumStatus)
        {
            return (from w in context.Albums where w.ProfileID == ProfileID && w.AlbumStatus == AlbumStatus orderby w.AlbumID descending select w).ToList();
        }
        public List<Album> GetAlbumsByProfileID(int ProfileID, int page = 1, int pageSize = 7, string filter = null)
        {
            return (from w in context.Albums where w.ProfileID == ProfileID orderby w.AlbumID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
        }
        public List<Album> GetAlbumsByProfileID(int ProfileID, bool AlbumStatus, int page = 1, int pageSize = 7, string filter = null)
        {
            return (from w in context.Albums where w.ProfileID == ProfileID && w.AlbumStatus == AlbumStatus orderby w.AlbumID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
        }
        public IQueryable<Album> GetAlbumsByCategoryIDAsQueryable(int CategoryID)
        {
            return (from w in context.Albums where w.CategoryID == CategoryID orderby w.AlbumID descending select w).AsQueryable();
        }
        public IQueryable<Album> GetAlbumsByCategoryIDAsQueryable(int CategoryID, bool AlbumStatus)
        {
            return (from w in context.Albums where w.CategoryID == CategoryID && w.AlbumStatus == AlbumStatus orderby w.AlbumID descending select w).AsQueryable();
        }
        public IQueryable<Album> GetAlbumsByCategoryIDAsQueryable(int CategoryID, int page = 1, int pageSize = 7, string filter = null)
        {
            return (from w in context.Albums where w.CategoryID == CategoryID orderby w.AlbumID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
        }
        public IQueryable<Album> GetAlbumsByCategoryIDAsQueryable(int CategoryID, bool AlbumStatus, int page = 1, int pageSize = 7, string filter = null)
        {
            return (from w in context.Albums where w.CategoryID == CategoryID && w.AlbumStatus == AlbumStatus orderby w.AlbumID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
        }
        public IQueryable<Album> GetAlbumsByProfileIDAsQueryable(int ProfileID)
        {
            return (from w in context.Albums.Include("Category") where w.ProfileID == ProfileID orderby w.AlbumID descending select w).AsQueryable();
        }
        public IQueryable<Album> GetAlbumsByProfileIDAsQueryable(int ProfileID, bool AlbumStatus)
        {
            return (from w in context.Albums where w.ProfileID == ProfileID && w.AlbumStatus == AlbumStatus orderby w.AlbumID descending select w).AsQueryable();
        }
        public IQueryable<Album> GetAlbumsByProfileIDAsQueryable(int ProfileID, int page = 1, int pageSize = 7, string filter = null)
        {
            return (from w in context.Albums where w.ProfileID == ProfileID orderby w.AlbumID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
        }
        public IQueryable<Album> GetAlbumsByProfileIDAsQueryable(int ProfileID, bool AlbumStatus, int page = 1, int pageSize = 7, string filter = null)
        {
            return (from w in context.Albums where w.ProfileID == ProfileID && w.AlbumStatus == AlbumStatus orderby w.AlbumID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
        }

    }
}