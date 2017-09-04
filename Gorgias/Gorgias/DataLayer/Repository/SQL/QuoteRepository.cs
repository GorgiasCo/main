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
    public class QuoteRepository : IQuoteRepository, IDisposable
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
        public Quote Insert(String QuoteName, Boolean QuoteStatus, String QuoteLanguageCode, int QuoteProbability, int CategoryID)
        {
            try
            {
                Quote obj = new Quote();


                obj.QuoteName = QuoteName;
                obj.QuoteStatus = QuoteStatus;
                obj.QuoteLanguageCode = QuoteLanguageCode;
                obj.QuoteProbability = QuoteProbability;
                obj.CategoryID = CategoryID;
                context.Quotes.Add(obj);
                context.SaveChanges();
                return obj;
            }
            catch (Exception ex)
            {
                return new Quote();
            }
        }

        public bool Update(int QuoteID, String QuoteName, Boolean QuoteStatus, String QuoteLanguageCode, int QuoteProbability, int CategoryID)
        {
            Quote obj = new Quote();
            obj = (from w in context.Quotes where w.QuoteID == QuoteID select w).FirstOrDefault();
            if (obj != null)
            {
                context.Quotes.Attach(obj);

                obj.QuoteName = QuoteName;
                obj.QuoteStatus = QuoteStatus;
                obj.QuoteLanguageCode = QuoteLanguageCode;
                obj.QuoteProbability = QuoteProbability;
                obj.CategoryID = CategoryID;
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Delete(int QuoteID)
        {
            Quote obj = new Quote();
            obj = (from w in context.Quotes where w.QuoteID == QuoteID select w).FirstOrDefault();
            if (obj != null)
            {
                context.Quotes.Attach(obj);
                context.Quotes.Remove(obj);
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public Quote GetQuote(int QuoteID)
        {
            return (from w in context.Quotes where w.QuoteID == QuoteID select w).FirstOrDefault();
        }

        //Lists
        public List<Quote> GetQuotesAll()
        {
            return (from w in context.Quotes orderby w.QuoteID descending select w).ToList();
        }
        public List<Quote> GetQuotesAll(bool QuoteStatus)
        {
            return (from w in context.Quotes where w.QuoteStatus == QuoteStatus orderby w.QuoteID descending select w).ToList();
        }
        //List Pagings
        public List<Quote> GetQuotesAll(int page = 1, int pageSize = 7, string filter = null)
        {
            var xList = new List<Quote>();
            if (filter != null)
            {
                xList = (from w in context.Quotes orderby w.QuoteID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
            }
            else
            {
                xList = (from w in context.Quotes orderby w.QuoteID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
            }
            return xList;
        }
        public List<Quote> GetQuotesAll(bool QuoteStatus = true, int page = 1, int pageSize = 7, string filter = null)
        {
            var xList = new List<Quote>();
            if (filter != null)
            {
                xList = (from w in context.Quotes where w.QuoteStatus == QuoteStatus orderby w.QuoteID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
            }
            else
            {
                xList = (from w in context.Quotes where w.QuoteStatus == QuoteStatus orderby w.QuoteID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
            }
            return xList;
        }
        //IQueryable
        public IQueryable<Quote> GetQuotesAllAsQueryable()
        {
            return (from w in context.Quotes orderby w.QuoteID descending select w).AsQueryable();
        }
        public IQueryable<Quote> GetQuotesAllAsQueryable(bool QuoteStatus)
        {
            return (from w in context.Quotes where w.QuoteStatus == QuoteStatus orderby w.QuoteID descending select w).AsQueryable();
        }
        //V2
        public IQueryable<Business.DataTransferObjects.Mobile.V2.QuoteMobileModel> GetQuotesAsQueryable(string languageCode)
        {
            return (from w in context.Quotes where w.QuoteStatus == true && w.QuoteLanguageCode == languageCode orderby w.QuoteProbability descending select new Business.DataTransferObjects.Mobile.V2.QuoteMobileModel { QuoteName = w.QuoteName, QuoteProbability = w.QuoteProbability }).AsQueryable();
        }
        //IQueryable Pagings
        public IQueryable<Quote> GetQuotesAllAsQueryable(int page = 1, int pageSize = 7, string filter = null)
        {
            IQueryable<Quote> xList;
            if (filter != null)
            {
                //In Case of Performance, Use following method ;)
                //xList = (from w in context.Quotes orderby w.QuoteID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();                
                xList = context.Quotes.ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
            }
            else
            {
                //In Case of Performance, Use following method ;)
                //xList = (from w in context.Quotes orderby w.QuoteID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();                
                xList = context.Quotes.Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
            }
            return xList;
        }
        public IQueryable<Quote> GetQuotesAllAsQueryable(bool QuoteStatus = true, int page = 1, int pageSize = 7, string filter = null)
        {
            IQueryable<Quote> xList;
            if (filter != null)
            {
                xList = (from w in context.Quotes where w.QuoteStatus == QuoteStatus orderby w.QuoteID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
            }
            else
            {
                xList = (from w in context.Quotes where w.QuoteStatus == QuoteStatus orderby w.QuoteID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
            }
            return xList;
        }
        //Relationship Functions based on Foreign Key
        //Relationship List
        public List<Quote> GetQuotesByCategoryID(int CategoryID, bool QuoteStatus)
        {
            return (from w in context.Quotes where w.CategoryID == CategoryID && w.QuoteStatus == QuoteStatus orderby w.QuoteID descending select w).ToList();
        }
        public List<Quote> GetQuotesByCategoryID(int CategoryID, int page = 1, int pageSize = 7, string filter = null)
        {
            return (from w in context.Quotes where w.CategoryID == CategoryID orderby w.QuoteID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
        }
        public List<Quote> GetQuotesByCategoryID(int CategoryID, bool QuoteStatus, int page = 1, int pageSize = 7, string filter = null)
        {
            return (from w in context.Quotes where w.CategoryID == CategoryID && w.QuoteStatus == QuoteStatus orderby w.QuoteID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
        }
        public IQueryable<Quote> GetQuotesByCategoryIDAsQueryable(int CategoryID)
        {
            return (from w in context.Quotes where w.CategoryID == CategoryID orderby w.QuoteID descending select w).AsQueryable();
        }
        public IQueryable<Quote> GetQuotesByCategoryIDAsQueryable(int CategoryID, bool QuoteStatus)
        {
            return (from w in context.Quotes where w.CategoryID == CategoryID && w.QuoteStatus == QuoteStatus orderby w.QuoteID descending select w).AsQueryable();
        }
        public IQueryable<Quote> GetQuotesByCategoryIDAsQueryable(int CategoryID, int page = 1, int pageSize = 7, string filter = null)
        {
            return (from w in context.Quotes where w.CategoryID == CategoryID orderby w.QuoteID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
        }
        public IQueryable<Quote> GetQuotesByCategoryIDAsQueryable(int CategoryID, bool QuoteStatus, int page = 1, int pageSize = 7, string filter = null)
        {
            return (from w in context.Quotes where w.CategoryID == CategoryID && w.QuoteStatus == QuoteStatus orderby w.QuoteID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
        }

    }
}