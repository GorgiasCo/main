﻿using System;
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
    public class LanguageRepository : ILanguageRepository, IDisposable
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
        public Language Insert(String LanguageName, String LanguageCode, Boolean LanguageStatus, int LanguageOrder)
        {
            try
            {
                Language obj = new Language();
                obj.LanguageName = LanguageName;
                obj.LanguageCode = LanguageCode;
                obj.LanguageStatus = LanguageStatus;
                obj.LanguageOrder = LanguageOrder;
                context.Languages.Add(obj);
                context.SaveChanges();
                return obj;
            }
            catch (Exception ex)
            {
                return new Language();
            }
        }

        public bool Update(int LanguageID, String LanguageName, String LanguageCode, Boolean LanguageStatus, int LanguageOrder)
        {
            Language obj = new Language();
            obj = (from w in context.Languages where w.LanguageID == LanguageID select w).FirstOrDefault();
            if (obj != null)
            {
                context.Languages.Attach(obj);

                obj.LanguageName = LanguageName;
                obj.LanguageCode = LanguageCode;
                obj.LanguageStatus = LanguageStatus;
                obj.LanguageOrder = LanguageOrder;
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Delete(int LanguageID)
        {
            Language obj = new Language();
            obj = (from w in context.Languages where w.LanguageID == LanguageID select w).FirstOrDefault();
            if (obj != null)
            {
                context.Languages.Attach(obj);
                context.Languages.Remove(obj);
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public Language GetLanguage(int LanguageID)
        {
            return (from w in context.Languages where w.LanguageID == LanguageID select w).FirstOrDefault();
        }

        //Lists
        public List<Language> GetLanguagesAll()
        {
            return (from w in context.Languages orderby w.LanguageID descending select w).ToList();
        }
        public List<Language> GetLanguagesAll(bool LanguageStatus)
        {
            return (from w in context.Languages where w.LanguageStatus == LanguageStatus orderby w.LanguageID descending select w).ToList();
        }
        //List Pagings
        public List<Language> GetLanguagesAll(int page = 1, int pageSize = 7, string filter = null)
        {
            var xList = new List<Language>();
            if (filter != null)
            {
                xList = (from w in context.Languages orderby w.LanguageID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
            }
            else
            {
                xList = (from w in context.Languages orderby w.LanguageID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
            }
            return xList;
        }
        public List<Language> GetLanguagesAll(bool LanguageStatus = true, int page = 1, int pageSize = 7, string filter = null)
        {
            var xList = new List<Language>();
            if (filter != null)
            {
                xList = (from w in context.Languages where w.LanguageStatus == LanguageStatus orderby w.LanguageID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
            }
            else
            {
                xList = (from w in context.Languages where w.LanguageStatus == LanguageStatus orderby w.LanguageID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
            }
            return xList;
        }
        //IQueryable
        public IQueryable<Language> GetLanguagesAllAsQueryable()
        {
            return (from w in context.Languages orderby w.LanguageID descending select w).AsQueryable();
        }

        public IQueryable<Language> GetLanguagesAllAsQueryable(bool LanguageStatus)
        {
            return (from w in context.Languages where w.LanguageStatus == LanguageStatus orderby w.LanguageID descending select w).AsQueryable();
        }

        //V2
        public IQueryable<Business.DataTransferObjects.Mobile.V2.ProfileReadingLanguageMobileModel> GetLanguagesAsQueryable(int ProfileID)
        {
            IQueryable<Business.DataTransferObjects.Mobile.V2.ProfileReadingLanguageMobileModel> result = context.Database.SqlQuery<Business.DataTransferObjects.Mobile.V2.ProfileReadingLanguageMobileModel>("SELECT  dbo.ProfileReading.ProfileReadingID, dbo.Language.LanguageName, dbo.Language.LanguageCode, dbo.Language.LanguageOrder, dbo.ProfileReading.ProfileID, dbo.Language.LanguageID FROM dbo.ProfileReading RIGHT OUTER JOIN dbo.Language ON dbo.Language.LanguageCode = dbo.ProfileReading.ProfileReadingLanguageCode AND dbo.ProfileReading.ProfileID = " + ProfileID + "  OR dbo.ProfileReading.ProfileReadingID IS NULL ORDER BY dbo.ProfileReading.ProfileReadingID DESC, dbo.Language.LanguageOrder").Select(m => new Business.DataTransferObjects.Mobile.V2.ProfileReadingLanguageMobileModel
            {
                isSelected = m.ProfileReadingID != null,
                LanguageCode = m.LanguageCode,
                LanguageID = m.LanguageID,
                LanguageName = m.LanguageName,
                ProfileReadingID = m.ProfileReadingID
            }).AsQueryable();
            return result;            
        }

        public IQueryable<Business.DataTransferObjects.Mobile.V2.LanguageMobileModel> GetLanguagesAsQueryable()
        {
            return (from w in context.Languages where w.LanguageStatus == true orderby w.LanguageOrder ascending, w.LanguageName ascending select new Business.DataTransferObjects.Mobile.V2.LanguageMobileModel { LanguageCode = w.LanguageCode, LanguageID = w.LanguageID, LanguageName = w.LanguageName }).AsQueryable();
        }

        public IQueryable<Business.DataTransferObjects.Mobile.V2.KeyValueMobileModel> GetLanguagesAsQueryableByKeyValue()
        {
            return (from w in context.Languages where w.LanguageStatus == true orderby w.LanguageOrder ascending, w.LanguageName ascending select new Business.DataTransferObjects.Mobile.V2.KeyValueMobileModel { KeyName = w.LanguageName, KeyID = w.LanguageID, KeyExtra = w.LanguageCode }).AsQueryable();
        }
        //IQueryable Pagings
        public IQueryable<Language> GetLanguagesAllAsQueryable(int page = 1, int pageSize = 7, string filter = null)
        {
            IQueryable<Language> xList;
            if (filter != null)
            {
                //In Case of Performance, Use following method ;)
                //xList = (from w in context.Languages orderby w.LanguageID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();                
                xList = context.Languages.ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
            }
            else
            {
                //In Case of Performance, Use following method ;)
                //xList = (from w in context.Languages orderby w.LanguageID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();                
                xList = context.Languages.Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
            }
            return xList;
        }
        public IQueryable<Language> GetLanguagesAllAsQueryable(bool LanguageStatus = true, int page = 1, int pageSize = 7, string filter = null)
        {
            IQueryable<Language> xList;
            if (filter != null)
            {
                xList = (from w in context.Languages where w.LanguageStatus == LanguageStatus orderby w.LanguageID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
            }
            else
            {
                xList = (from w in context.Languages where w.LanguageStatus == LanguageStatus orderby w.LanguageID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
            }
            return xList;
        }

    }
}