using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Gorgias;
using Gorgias.DataLayer.Interface;
using Gorgias.Infrastruture.EntityFramework;
using System.Linq;
namespace Gorgias.DataLayer.Repository.SQL
{
    public class ContentRatingRepository : IContentRatingRepository, IDisposable
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
        public ContentRating Insert(String ContentRatingName, int ContentRatingAge, Boolean ContentRatingStatus, String ContentRatingImage, String ContentRatingDescription, int ContentRatingOrder, String ContentRatingLanguageCode, int? ContentRatingParentID)
        {
            try
            {
                ContentRating obj = new ContentRating();


                obj.ContentRatingName = ContentRatingName;
                obj.ContentRatingAge = ContentRatingAge;
                obj.ContentRatingStatus = ContentRatingStatus;
                obj.ContentRatingImage = ContentRatingImage;
                obj.ContentRatingDescription = ContentRatingDescription;
                obj.ContentRatingOrder = ContentRatingOrder;
                obj.ContentRatingLanguageCode = ContentRatingLanguageCode;

                if (ContentRatingParentID.HasValue)
                {
                    obj.ContentRatingParentID = ContentRatingParentID;
                } else
                {
                    obj.ContentRatingParentID = null;
                }
                
                context.ContentRatings.Add(obj);
                context.SaveChanges();
                return obj;
            }
            catch (Exception ex)
            {
                return new ContentRating();
            }
        }

        public bool Update(int ContentRatingID, String ContentRatingName, int ContentRatingAge, Boolean ContentRatingStatus, String ContentRatingImage, String ContentRatingDescription, int ContentRatingOrder, String ContentRatingLanguageCode, int? ContentRatingParentID)
        {
            ContentRating obj = new ContentRating();
            obj = (from w in context.ContentRatings where w.ContentRatingID == ContentRatingID select w).FirstOrDefault();
            if (obj != null)
            {
                context.ContentRatings.Attach(obj);

                obj.ContentRatingName = ContentRatingName;
                obj.ContentRatingAge = ContentRatingAge;
                obj.ContentRatingStatus = ContentRatingStatus;
                obj.ContentRatingImage = ContentRatingImage;
                obj.ContentRatingDescription = ContentRatingDescription;
                obj.ContentRatingOrder = ContentRatingOrder;
                obj.ContentRatingLanguageCode = ContentRatingLanguageCode;

                if (ContentRatingParentID.HasValue)
                {
                    obj.ContentRatingParentID = ContentRatingParentID;
                }
                else
                {
                    obj.ContentRatingParentID = null;
                }

                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Delete(int ContentRatingID)
        {
            ContentRating obj = new ContentRating();
            obj = (from w in context.ContentRatings where w.ContentRatingID == ContentRatingID select w).FirstOrDefault();
            if (obj != null)
            {
                context.ContentRatings.Attach(obj);
                context.ContentRatings.Remove(obj);
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public ContentRating GetContentRating(int ContentRatingID)
        {
            return (from w in context.ContentRatings where w.ContentRatingID == ContentRatingID select w).FirstOrDefault();
        }

        //Lists
        public List<ContentRating> GetContentRatingsAll()
        {
            return (from w in context.ContentRatings orderby w.ContentRatingID descending select w).ToList();
        }
        public List<ContentRating> GetContentRatingsAll(bool ContentRatingStatus)
        {
            return (from w in context.ContentRatings where w.ContentRatingStatus == ContentRatingStatus orderby w.ContentRatingID descending select w).ToList();
        }
        //List Pagings
        public List<ContentRating> GetContentRatingsAll(int page = 1, int pageSize = 7, string filter = null)
        {
            var xList = new List<ContentRating>();
            if (filter != null)
            {
                xList = (from w in context.ContentRatings orderby w.ContentRatingID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
            }
            else
            {
                xList = (from w in context.ContentRatings orderby w.ContentRatingID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
            }
            return xList;
        }
        public List<ContentRating> GetContentRatingsAll(bool ContentRatingStatus = true, int page = 1, int pageSize = 7, string filter = null)
        {
            var xList = new List<ContentRating>();
            if (filter != null)
            {
                xList = (from w in context.ContentRatings where w.ContentRatingStatus == ContentRatingStatus orderby w.ContentRatingID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
            }
            else
            {
                xList = (from w in context.ContentRatings where w.ContentRatingStatus == ContentRatingStatus orderby w.ContentRatingID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
            }
            return xList;
        }
        //IQueryable
        public IQueryable<ContentRating> GetContentRatingsAllAsQueryable()
        {
            return (from w in context.ContentRatings orderby w.ContentRatingID descending select w).AsQueryable();
        }
        public IQueryable<ContentRating> GetContentRatingsAllAsQueryable(bool ContentRatingStatus)
        {
            return (from w in context.ContentRatings where w.ContentRatingStatus == ContentRatingStatus orderby w.ContentRatingID descending select w).AsQueryable();
        }

        //V2
        public IQueryable<Business.DataTransferObjects.Mobile.V2.ContentRatingMobileModel> GetContentRatingsAllAsQueryable(string languageCode)
        {
            return (from w in context.ContentRatings where w.ContentRatingStatus == true && w.ContentRatingParentID == null orderby w.ContentRatingOrder ascending select new Business.DataTransferObjects.Mobile.V2.ContentRatingMobileModel { ContentRatingID = w.ContentRatingID, ContentRatingName = w.ContentRatingName, Multilanguage = w.ContentRatingChilds.Where(m=> m.ContentRatingLanguageCode == languageCode).FirstOrDefault().ContentRatingName }).AsQueryable();
        }
        //IQueryable Pagings
        public IQueryable<ContentRating> GetContentRatingsAllAsQueryable(int page = 1, int pageSize = 7, string filter = null)
        {
            IQueryable<ContentRating> xList;
            if (filter != null)
            {
                //In Case of Performance, Use following method ;)
                //xList = (from w in context.ContentRatings orderby w.ContentRatingID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();                
                xList = context.ContentRatings.ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
            }
            else
            {
                //In Case of Performance, Use following method ;)
                //xList = (from w in context.ContentRatings orderby w.ContentRatingID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();                
                xList = context.ContentRatings.Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
            }
            return xList;
        }
        public IQueryable<ContentRating> GetContentRatingsAllAsQueryable(bool ContentRatingStatus = true, int page = 1, int pageSize = 7, string filter = null)
        {
            IQueryable<ContentRating> xList;
            if (filter != null)
            {
                xList = (from w in context.ContentRatings where w.ContentRatingStatus == ContentRatingStatus orderby w.ContentRatingID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
            }
            else
            {
                xList = (from w in context.ContentRatings where w.ContentRatingStatus == ContentRatingStatus orderby w.ContentRatingID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
            }
            return xList;
        }
        //Relationship Functions based on Foreign Key
        //Relationship List
        public List<ContentRating> GetContentRatingsByContentRatingParentID(int ContentRatingParentID, bool ContentRatingStatus)
        {
            return (from w in context.ContentRatings where w.ContentRatingParentID == ContentRatingParentID && w.ContentRatingStatus == ContentRatingStatus orderby w.ContentRatingID descending select w).ToList();
        }
        public List<ContentRating> GetContentRatingsByContentRatingParentID(int ContentRatingParentID, int page = 1, int pageSize = 7, string filter = null)
        {
            return (from w in context.ContentRatings where w.ContentRatingParentID == ContentRatingParentID orderby w.ContentRatingID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
        }
        public List<ContentRating> GetContentRatingsByContentRatingParentID(int ContentRatingParentID, bool ContentRatingStatus, int page = 1, int pageSize = 7, string filter = null)
        {
            return (from w in context.ContentRatings where w.ContentRatingParentID == ContentRatingParentID && w.ContentRatingStatus == ContentRatingStatus orderby w.ContentRatingID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
        }
        public IQueryable<ContentRating> GetContentRatingsByContentRatingParentIDAsQueryable(int ContentRatingParentID)
        {
            return (from w in context.ContentRatings where w.ContentRatingParentID == ContentRatingParentID orderby w.ContentRatingID descending select w).AsQueryable();
        }
        public IQueryable<ContentRating> GetContentRatingsByContentRatingParentIDAsQueryable(int ContentRatingParentID, bool ContentRatingStatus)
        {
            return (from w in context.ContentRatings where w.ContentRatingParentID == ContentRatingParentID && w.ContentRatingStatus == ContentRatingStatus orderby w.ContentRatingID descending select w).AsQueryable();
        }
        public IQueryable<ContentRating> GetContentRatingsByContentRatingParentIDAsQueryable(int ContentRatingParentID, int page = 1, int pageSize = 7, string filter = null)
        {
            return (from w in context.ContentRatings where w.ContentRatingParentID == ContentRatingParentID orderby w.ContentRatingID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
        }
        public IQueryable<ContentRating> GetContentRatingsByContentRatingParentIDAsQueryable(int ContentRatingParentID, bool ContentRatingStatus, int page = 1, int pageSize = 7, string filter = null)
        {
            return (from w in context.ContentRatings where w.ContentRatingParentID == ContentRatingParentID && w.ContentRatingStatus == ContentRatingStatus orderby w.ContentRatingID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
        }

    }
}