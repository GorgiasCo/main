using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using Gorgias;
using Gorgias.DataLayer.Interface;
using Gorgias.Infrastruture.EntityFramework;
using System.Linq;
using Gorgias.Business.DataTransferObjects;
using System.Threading.Tasks;

namespace Gorgias.DataLayer.Repository.SQL
{
    public class CategoryRepository : ICategoryRepository, IDisposable
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
        public Category Insert(String CategoryName, Boolean CategoryStatus, String CategoryImage, String CategoryDescription, int CategoryParentID)
        {
            try
            {
                Category obj = new Category();


                obj.CategoryName = CategoryName;
                obj.CategoryStatus = CategoryStatus;
                obj.CategoryImage = CategoryImage;
                obj.CategoryDescription = CategoryDescription;
                if (CategoryParentID > 0)
                {
                    obj.CategoryParentID = CategoryParentID;
                }
                else
                {
                    obj.CategoryParentID = null;
                }

                context.Categories.Add(obj);
                context.SaveChanges();
                return obj;
            }
            catch (Exception ex)
            {
                return new Category();
            }
        }

        public Category Insert(string CategoryName, int? ProfileID, string languageCode)
        {
            try
            {
                Category obj = new Category();
                obj.CategoryName = CategoryName;
                obj.CategoryStatus = true;
                obj.CategoryParentID = null;
                obj.CategoryDescription = languageCode;

                obj.ProfileID = ProfileID.HasValue ? ProfileID : null;

                context.Categories.Add(obj);
                context.SaveChanges();
                return obj;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool Update(int CategoryID, String CategoryName, Boolean CategoryStatus, String CategoryImage, String CategoryDescription, int CategoryParentID)
        {
            Category obj = new Category();
            obj = (from w in context.Categories where w.CategoryID == CategoryID select w).FirstOrDefault();
            if (obj != null)
            {
                context.Categories.Attach(obj);

                obj.CategoryName = CategoryName;
                obj.CategoryStatus = CategoryStatus;
                obj.CategoryImage = CategoryImage;
                obj.CategoryDescription = CategoryDescription;

                if (CategoryParentID > 0)
                {
                    obj.CategoryParentID = CategoryParentID;
                }
                else
                {
                    obj.CategoryParentID = null;
                }
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public Category Insert(String CategoryName, Boolean CategoryStatus, String CategoryImage, String CategoryDescription, int CategoryParentID, int? CategoryOrder, int? CategoryType)
        {
            try
            {
                Category obj = new Category();


                obj.CategoryName = CategoryName;
                obj.CategoryStatus = CategoryStatus;
                obj.CategoryImage = CategoryImage;
                obj.CategoryDescription = CategoryDescription;
                if (CategoryOrder.HasValue)
                {
                    obj.CategoryOrder = CategoryOrder;
                }
                else
                {
                    obj.CategoryOrder = null;
                }

                obj.CategoryType = CategoryType;
                if (CategoryParentID > 0)
                {
                    obj.CategoryParentID = CategoryParentID;
                }
                else
                {
                    obj.CategoryParentID = null;
                }

                context.Categories.Add(obj);
                context.SaveChanges();
                return obj;
            }
            catch (Exception ex)
            {
                return new Category();
            }
        }

        public bool Update(int CategoryID, String CategoryName, Boolean CategoryStatus, String CategoryImage, String CategoryDescription, int CategoryParentID, int? CategoryOrder, int? CategoryType)
        {
            Category obj = new Category();
            obj = (from w in context.Categories where w.CategoryID == CategoryID select w).FirstOrDefault();
            if (obj != null)
            {
                context.Categories.Attach(obj);

                obj.CategoryName = CategoryName;
                obj.CategoryStatus = CategoryStatus;
                obj.CategoryImage = CategoryImage;
                obj.CategoryDescription = CategoryDescription;
                if (CategoryOrder.HasValue)
                {
                    obj.CategoryOrder = CategoryOrder;
                }
                else
                {
                    obj.CategoryOrder = null;
                }

                obj.CategoryType = CategoryType;
                if (CategoryParentID > 0)
                {
                    obj.CategoryParentID = CategoryParentID;
                }
                else
                {
                    obj.CategoryParentID = null;
                }
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Delete(int CategoryID)
        {
            Category obj = new Category();
            obj = (from w in context.Categories where w.CategoryID == CategoryID select w).FirstOrDefault();
            if (obj != null)
            {
                context.Categories.Attach(obj);
                context.Categories.Remove(obj);
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public Category GetCategory(int CategoryID)
        {
            return (from w in context.Categories where w.CategoryID == CategoryID select w).FirstOrDefault();
        }

        //Lists
        public List<Category> GetCategoriesAll()
        {
            return (from w in context.Categories orderby w.CategoryID descending select w).ToList();
        }
        public List<Category> GetCategoriesAll(bool CategoryStatus)
        {
            return (from w in context.Categories where w.CategoryStatus == CategoryStatus orderby w.CategoryID descending select w).ToList();
        }
        //List Pagings
        public List<Category> GetCategoriesAll(int page = 1, int pageSize = 7, string filter = null)
        {
            var xList = new List<Category>();
            if (filter != null)
            {
                xList = (from w in context.Categories orderby w.CategoryID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
            }
            else
            {
                xList = (from w in context.Categories orderby w.CategoryID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
            }
            return xList;
        }
        public List<Category> GetCategoriesAll(bool CategoryStatus = true, int page = 1, int pageSize = 7, string filter = null)
        {
            var xList = new List<Category>();
            if (filter != null)
            {
                xList = (from w in context.Categories where w.CategoryStatus == CategoryStatus orderby w.CategoryID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
            }
            else
            {
                xList = (from w in context.Categories where w.CategoryStatus == CategoryStatus orderby w.CategoryID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
            }
            return xList;
        }
        //IQueryable
        public IQueryable<Category> GetCategoriesAllAsQueryable()
        {
            //var xTest = (from w in context.Categories.Include("ChildCategory") where w.CategoryParentID == null orderby w.CategoryID descending select w).AsQueryable();
            //return (from w in context.Categories orderby w.CategoryID descending select w).AsQueryable();
            return (from w in context.Categories where w.CategoryParentID == null orderby w.CategoryID descending select w).AsQueryable();
        }

        public IQueryable<CategoryDTO> GetCategoriesAllAsQueryable(string languageCode)
        {
            var xTest = (from w in context.Categories where w.CategoryParentID == null orderby w.CategoryID descending select new { ChildCategory = w.ChildCategory.Where(m => m.CategoryDescription == languageCode), CategoryID = w.CategoryID, CategoryName = w.CategoryName }).AsQueryable();
            //return (from w in context.Categories orderby w.CategoryID descending select w).AsQueryable();
            return (from w in context.Categories where w.CategoryParentID == null orderby w.CategoryID descending select new CategoryDTO { Multilanguage = w.ChildCategory.Where(m => m.CategoryDescription == languageCode).FirstOrDefault().CategoryName, CategoryName = w.CategoryName, CategoryID = w.CategoryID, CategoryImage = w.CategoryImage, CategoryDescription = w.CategoryDescription, CategoryStatus = w.CategoryStatus }).AsQueryable();
        }

        //V2
        public IQueryable<Business.DataTransferObjects.Mobile.V2.CategoryMobileModel> GetV2CategoriesAllAsQueryable(string languageCode)
        {
            return (from w in context.Categories where w.CategoryParentID != null orderby w.CategoryType descending, w.CategoryOrder ascending select new Business.DataTransferObjects.Mobile.V2.CategoryMobileModel { Multilanguage = w.ChildCategory.Where(m => m.CategoryDescription == languageCode).FirstOrDefault().CategoryName, CategoryName = w.CategoryName, CategoryID = w.CategoryID, CategoryType = w.CategoryType }).AsQueryable();
        }

        public IQueryable<Business.DataTransferObjects.Mobile.V2.KeyValueMobileModel> GetV2CategoriesAllAsQueryableKeyValue(int CategoryParentID, string languageCode)
        {
            return (from w in context.Categories where w.CategoryParentID == CategoryParentID orderby w.CategoryType descending, w.CategoryOrder ascending select new Business.DataTransferObjects.Mobile.V2.KeyValueMobileModel { Multilanguage = w.ChildCategory.Where(m => m.CategoryDescription == languageCode).FirstOrDefault().CategoryName, KeyName = w.CategoryName, KeyID = w.CategoryID }).AsQueryable();
        }

        public IQueryable<Business.DataTransferObjects.Mobile.V2.KeyValueMobileModel> GetV2CategoriesByProfileIDAsQueryableKeyValue(int ProfileID, string languageCode)
        {
            return (from w in context.Categories where w.ProfileID == ProfileID orderby w.CategoryType descending, w.CategoryOrder ascending select new Business.DataTransferObjects.Mobile.V2.KeyValueMobileModel { Multilanguage = w.ChildCategory.Where(m => m.CategoryDescription == languageCode).FirstOrDefault().CategoryName, KeyName = w.CategoryName, KeyID = w.CategoryID }).AsQueryable();
        }

        public IQueryable<Business.DataTransferObjects.Mobile.V2.CategoryMobileModel> GetV2CategoriesAvailableByProfileIDAsQueryable(int ProfileID, string languageCode)
        {
            return (from w in context.Categories where w.ProfileID == ProfileID && w.Albums.Count > 0 orderby w.CategoryType descending, w.CategoryOrder ascending select new Business.DataTransferObjects.Mobile.V2.CategoryMobileModel { Multilanguage = w.ChildCategory.Where(m => m.CategoryDescription == languageCode).FirstOrDefault().CategoryName, CategoryName = w.CategoryName, CategoryID = w.CategoryID }).AsQueryable();
        }

        public IQueryable<Business.DataTransferObjects.Mobile.V2.KeyValueMobileModel> GetV2CategoriesBySearchAsQueryableKeyValue(string CategorySearch)
        {
            return (from w in context.Categories where w.CategoryName.Contains(CategorySearch) && w.CategoryType == 1 orderby w.CategoryType descending, w.CategoryOrder ascending select new Business.DataTransferObjects.Mobile.V2.KeyValueMobileModel { Multilanguage = w.CategoryName, KeyName = w.CategoryName, KeyID = w.CategoryID }).AsQueryable();
        }

        //Business.DataTransferObjects.Mobile.V2.KeyValueMobileModel
        public IQueryable<Category> GetCategoriesAllAsQueryableX(string languageCode)
        {
            var xTest = (from w in context.Categories where w.CategoryParentID == null orderby w.CategoryID descending select new { ChildCategory = w.ChildCategory.Where(m => m.CategoryDescription == languageCode), CategoryID = w.CategoryID, CategoryName = w.CategoryName }).AsQueryable();
            //return (from w in context.Categories orderby w.CategoryID descending select w).AsQueryable();
            return (from w in context.Categories where w.CategoryParentID == null orderby w.CategoryID descending select new Category { ChildCategory = (ICollection<Category>)w.ChildCategory.Where(m => m.CategoryDescription == languageCode), CategoryID = w.CategoryID, CategoryName = w.CategoryName }).AsQueryable();// (from w in context.Categories where w.CategoryParentID == null orderby w.CategoryID descending select new CategoryDTO { Multilanguage = w.ChildCategory.Where(m => m.CategoryDescription == languageCode).FirstOrDefault().CategoryName, CategoryName = w.CategoryName, CategoryID = w.CategoryID, CategoryImage = w.CategoryImage, CategoryDescription = w.CategoryDescription, CategoryStatus = w.CategoryStatus }).AsQueryable();
        }

        public IQueryable<Business.DataTransferObjects.Mobile.CategoryMobileModel> GetCategoriesAllAsQueryable(int ProfileID)
        {
            return (from w in context.Categories where w.Albums.Any(m => m.ProfileID == ProfileID & m.AlbumStatus == true & m.AlbumIsDeleted == false) && w.CategoryStatus == true orderby w.CategoryID descending select new Business.DataTransferObjects.Mobile.CategoryMobileModel { CategoryID = w.CategoryID, CategoryName = w.CategoryName }).AsQueryable();
        }
        public IQueryable<Category> GetCategoriesAllAsQueryable(bool CategoryStatus)
        {
            return (from w in context.Categories where w.CategoryStatus == CategoryStatus orderby w.CategoryID descending select w).AsQueryable();
        }
        //IQueryable Pagings
        public IQueryable<Category> GetCategoriesAllAsQueryable(int page = 1, int pageSize = 7, string filter = null)
        {
            IQueryable<Category> xList;
            if (filter != null)
            {
                //In Case of Performance, Use following method ;)
                //xList = (from w in context.Categorys orderby w.CategoryID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();                
                xList = context.Categories.ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
            }
            else
            {
                //In Case of Performance, Use following method ;)
                //xList = (from w in context.Categories orderby w.CategoryID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();                
                xList = context.Categories.Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
            }
            return xList;
        }
        public IQueryable<Category> GetCategoriesAllAsQueryable(bool CategoryStatus = true, int page = 1, int pageSize = 7, string filter = null)
        {
            IQueryable<Category> xList;
            if (filter != null)
            {
                xList = (from w in context.Categories where w.CategoryStatus == CategoryStatus orderby w.CategoryID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
            }
            else
            {
                xList = (from w in context.Categories where w.CategoryStatus == CategoryStatus orderby w.CategoryID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
            }
            return xList;
        }

    }
}