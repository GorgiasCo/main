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
        public Language Insert(String LanguageName, String LanguageCode, Boolean LanguageStatus)
        {
            try
            {
                Language obj = new Language();


                obj.LanguageName = LanguageName;
                obj.LanguageCode = LanguageCode;
                obj.LanguageStatus = LanguageStatus;
                context.Languages.Add(obj);
                context.SaveChanges();
                return obj;
            }
            catch (Exception ex)
            {
                return new Language();
            }
        }

        public bool Update(int LanguageID, String LanguageName, String LanguageCode, Boolean LanguageStatus)
        {
            Language obj = new Language();
            obj = (from w in context.Languages where w.LanguageID == LanguageID select w).FirstOrDefault();
            if (obj != null)
            {
                context.Languages.Attach(obj);

                obj.LanguageName = LanguageName;
                obj.LanguageCode = LanguageCode;
                obj.LanguageStatus = LanguageStatus;
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