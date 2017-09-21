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
    public class IndustryRepository : IIndustryRepository, IDisposable
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
        public Industry Insert(String IndustryName, Boolean IndustryStatus, int? IndustryParentID, String IndustryImage, String IndustryDescription)
        {
            try
            {
                Industry obj = new Industry();


                obj.IndustryName = IndustryName;
                obj.IndustryStatus = IndustryStatus;
                obj.IndustryParentID = IndustryParentID;
                obj.IndustryImage = IndustryImage;
                obj.IndustryDescription = IndustryDescription;
                context.Industries.Add(obj);
                context.SaveChanges();
                return obj;
            }
            catch (Exception ex)
            {
                return new Industry();
            }
        }

        public Industry Insert(Industry objIndustry)
        {
            Industry result = (from x in context.Industries where x.IndustryName == objIndustry.IndustryName select x).FirstOrDefault();
            if (result != null)
            {
                return result;
            }
            else
            {
                try
                {
                    context.Industries.Add(objIndustry);
                    context.SaveChanges();
                    return objIndustry;
                }
                catch (Exception ex)
                {
                    return new Industry();
                }
            }
        }


        public bool Update(int IndustryID, String IndustryName, Boolean IndustryStatus, int? IndustryParentID, String IndustryImage, String IndustryDescription)
        {
            Industry obj = new Industry();
            obj = (from w in context.Industries where w.IndustryID == IndustryID select w).FirstOrDefault();
            if (obj != null)
            {
                context.Industries.Attach(obj);

                obj.IndustryName = IndustryName;
                obj.IndustryStatus = IndustryStatus;
                obj.IndustryParentID = IndustryParentID;
                obj.IndustryImage = IndustryImage;
                obj.IndustryDescription = IndustryDescription;
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Delete(int IndustryID)
        {
            Industry obj = new Industry();
            obj = (from w in context.Industries where w.IndustryID == IndustryID select w).FirstOrDefault();
            if (obj != null)
            {
                context.Industries.Attach(obj);
                context.Industries.Remove(obj);
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public Industry GetIndustry(int IndustryID)
        {
            return (from w in context.Industries where w.IndustryID == IndustryID select w).FirstOrDefault();
        }

        //Lists
        public List<Industry> GetIndustriesAll()
        {
            return (from w in context.Industries orderby w.IndustryID descending select w).ToList();
        }
        public List<Industry> GetIndustriesAll(bool IndustryStatus)
        {
            return (from w in context.Industries where w.IndustryStatus == IndustryStatus orderby w.IndustryID descending select w).ToList();
        }
        //List Pagings
        public List<Industry> GetIndustriesAll(int page = 1, int pageSize = 7, string filter = null)
        {
            var xList = new List<Industry>();
            if (filter != null)
            {
                xList = (from w in context.Industries orderby w.IndustryID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
            }
            else
            {
                xList = (from w in context.Industries orderby w.IndustryID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
            }
            return xList;
        }
        public List<Industry> GetIndustriesAll(bool IndustryStatus = true, int page = 1, int pageSize = 7, string filter = null)
        {
            var xList = new List<Industry>();
            if (filter != null)
            {
                xList = (from w in context.Industries where w.IndustryStatus == IndustryStatus orderby w.IndustryID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
            }
            else
            {
                xList = (from w in context.Industries where w.IndustryStatus == IndustryStatus orderby w.IndustryID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
            }
            return xList;
        }
        //IQueryable
        public IQueryable<Industry> GetIndustriesAllAsQueryable()
        {
            return (from w in context.Industries orderby w.IndustryID descending select w).AsQueryable();
        }

        public IQueryable<Business.DataTransferObjects.IndustryDTO> GetIndustriesAllAsQueryable(string languageCode)
        {
            return (from w in context.Industries
                    where w.IndustryStatus == true && w.IndustryParentID == null
                    orderby w.IndustryName ascending
                    select new Business.DataTransferObjects.IndustryDTO
                    {
                        IndustryName = w.IndustryName,
                        IndustryID = w.IndustryID,
                        Multilanguage = w.IndustryChilds.Where(m => m.IndustryLanguageCode == languageCode).FirstOrDefault().IndustryName
                    }).AsQueryable();
        }

        public IQueryable<Business.DataTransferObjects.Mobile.V2.IndustryMobileModel> GetIndustriesAsQueryable(string languageCode)
        {
            return (from w in context.Industries
                    where w.IndustryStatus == true && w.IndustryParentID == null
                    orderby w.IndustryName ascending
                    select new Business.DataTransferObjects.Mobile.V2.IndustryMobileModel
                    {
                        IndustryName = w.IndustryName,
                        IndustryID = w.IndustryID,
                        Multilanguage = w.IndustryChilds.Where(m => m.IndustryLanguageCode == languageCode).FirstOrDefault().IndustryName
                    }).AsQueryable();
        }

        public IQueryable<Business.DataTransferObjects.Mobile.V2.KeyValueMobileModel> GetIndustriesAsKeyValueQueryable(string languageCode)
        {
            return (from w in context.Industries
                    where w.IndustryStatus == true && w.IndustryParentID == null
                    orderby w.IndustryName ascending
                    select new Business.DataTransferObjects.Mobile.V2.KeyValueMobileModel
                    {
                        KeyName = w.IndustryName,
                        KeyID = w.IndustryID,
                        Multilanguage = w.IndustryChilds.Where(m => m.IndustryLanguageCode == languageCode).FirstOrDefault().IndustryName
                    }).AsQueryable();
        }

        public IQueryable<Industry> GetIndustriesAllAsQueryable(bool IndustryStatus)
        {
            return (from w in context.Industries where w.IndustryStatus == IndustryStatus orderby w.IndustryID descending select w).AsQueryable();
        }
        //IQueryable Pagings
        public IQueryable<Industry> GetIndustriesAllAsQueryable(int page = 1, int pageSize = 7, string filter = null)
        {
            IQueryable<Industry> xList;
            if (filter != null)
            {
                //In Case of Performance, Use following method ;)
                //xList = (from w in context.Industrys orderby w.IndustryID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();                
                xList = context.Industries.ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
            }
            else
            {
                //In Case of Performance, Use following method ;)
                //xList = (from w in context.Industries orderby w.IndustryID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();                
                xList = context.Industries.Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
            }
            return xList;
        }
        public IQueryable<Industry> GetIndustriesAllAsQueryable(bool IndustryStatus = true, int page = 1, int pageSize = 7, string filter = null)
        {
            IQueryable<Industry> xList;
            if (filter != null)
            {
                xList = (from w in context.Industries where w.IndustryStatus == IndustryStatus orderby w.IndustryID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
            }
            else
            {
                xList = (from w in context.Industries where w.IndustryStatus == IndustryStatus orderby w.IndustryID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
            }
            return xList;
        }

    }
}