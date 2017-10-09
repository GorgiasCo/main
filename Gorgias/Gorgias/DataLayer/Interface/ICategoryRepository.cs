using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
namespace Gorgias.DataLayer.Interface
{
    public interface ICategoryRepository
    {
        Category Insert(String CategoryName, Boolean CategoryStatus, String CategoryImage, String CategoryDescription, int CategoryParentID);
        Category Insert(string CategoryName, int? ProfileID, string languageCode);
        bool Update(int CategoryID, String CategoryName, Boolean CategoryStatus, String CategoryImage, String CategoryDescription, int CategoryParentID);

        Category Insert(String CategoryName, Boolean CategoryStatus, String CategoryImage, String CategoryDescription, int CategoryParentID, int? CategoryOrder, int? CategoryType);
        bool Update(int CategoryID, String CategoryName, Boolean CategoryStatus, String CategoryImage, String CategoryDescription, int CategoryParentID, int? CategoryOrder, int? CategoryType);

        bool Delete(int CategoryID);

        Category GetCategory(int CategoryID);

        //List
        List<Category> GetCategoriesAll();
        List<Category> GetCategoriesAll(bool CategoryStatus);
        List<Category> GetCategoriesAll(int page = 1, int pageSize = 7, string filter = null);
        List<Category> GetCategoriesAll(bool CategoryStatus, int page = 1, int pageSize = 7, string filter = null);


        //IQueryable
        IQueryable<Category> GetCategoriesAllAsQueryable();
        IQueryable<Business.DataTransferObjects.CategoryDTO> GetCategoriesAllAsQueryable(string languageCode);
        IQueryable<Business.DataTransferObjects.Mobile.V2.CategoryMobileModel> GetV2CategoriesAllAsQueryable(string languageCode);
        IQueryable<Business.DataTransferObjects.Mobile.V2.KeyValueMobileModel> GetV2CategoriesAllAsQueryableKeyValue(int CategoryParentID, string languageCode);
        IQueryable<Category> GetCategoriesAllAsQueryableX(string languageCode);
        IQueryable<Business.DataTransferObjects.Mobile.CategoryMobileModel> GetCategoriesAllAsQueryable(int ProfileID);
        IQueryable<Category> GetCategoriesAllAsQueryable(bool CategoryStatus);
        IQueryable<Category> GetCategoriesAllAsQueryable(int page = 1, int pageSize = 7, string filter = null);
        IQueryable<Category> GetCategoriesAllAsQueryable(bool CategoryStatus, int page = 1, int pageSize = 7, string filter = null);
    }
}


