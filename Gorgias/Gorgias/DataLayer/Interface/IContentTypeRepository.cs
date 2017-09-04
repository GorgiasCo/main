using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
namespace Gorgias.DataLayer.Interface
{
    public interface IContentTypeRepository
    {
        ContentType Insert(String ContentTypeName, int ContentTypeOrder, Boolean ContentTypeStatus, String ContentTypeLanguageCode, String ContentTypeExpression, int? ContentTypeParentID);
        bool Update(int ContentTypeID, String ContentTypeName, int ContentTypeOrder, Boolean ContentTypeStatus, String ContentTypeLanguageCode, String ContentTypeExpression, int? ContentTypeParentID);
        bool Delete(int ContentTypeID);

        ContentType GetContentType(int ContentTypeID);

        //List
        List<ContentType> GetContentTypesAll();
        List<ContentType> GetContentTypesAll(bool ContentTypeStatus);
        List<ContentType> GetContentTypesAll(int page = 1, int pageSize = 7, string filter = null);
        List<ContentType> GetContentTypesAll(bool ContentTypeStatus, int page = 1, int pageSize = 7, string filter = null);

        List<ContentType> GetContentTypesByContentTypeParentID(int ContentTypeParentID, bool ContentTypeStatus);
        List<ContentType> GetContentTypesByContentTypeParentID(int ContentTypeParentID, int page = 1, int pageSize = 7, string filter = null);
        List<ContentType> GetContentTypesByContentTypeParentID(int ContentTypeParentID, bool ContentTypeStatus, int page = 1, int pageSize = 7, string filter = null);

        //IQueryable
        IQueryable<Business.DataTransferObjects.Mobile.V2.ContentTypeMobileModel> GetContentTypesAsQueryable(int ContentTypeID, string languageCode);
        IQueryable<ContentType> GetContentTypesAllAsQueryable();
        IQueryable<ContentType> GetContentTypesAllAsQueryable(bool ContentTypeStatus);
        IQueryable<ContentType> GetContentTypesAllAsQueryable(int page = 1, int pageSize = 7, string filter = null);
        IQueryable<ContentType> GetContentTypesAllAsQueryable(bool ContentTypeStatus, int page = 1, int pageSize = 7, string filter = null);
        IQueryable<ContentType> GetContentTypesByContentTypeParentIDAsQueryable(int ContentTypeParentID);
        IQueryable<ContentType> GetContentTypesByContentTypeParentIDAsQueryable(int ContentTypeParentID, bool ContentTypeStatus);
        IQueryable<ContentType> GetContentTypesByContentTypeParentIDAsQueryable(int ContentTypeParentID, int page = 1, int pageSize = 7, string filter = null);
        IQueryable<ContentType> GetContentTypesByContentTypeParentIDAsQueryable(int ContentTypeParentID, bool ContentTypeStatus, int page = 1, int pageSize = 7, string filter = null);
    }
}


