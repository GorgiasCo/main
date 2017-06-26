using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
namespace Gorgias.DataLayer.Interface
{
    public interface IAlbumTypeRepository
    {
        AlbumType Insert(int AlbumTypeID, String AlbumTypeName, Boolean AlbumTypeStatus, int AlbumTypeLimitation);
        bool Update(int AlbumTypeID, String AlbumTypeName, Boolean AlbumTypeStatus, int AlbumTypeLimitation);
        bool Delete(int AlbumTypeID);

        AlbumType GetAlbumType(int AlbumTypeID);

        //List
        List<AlbumType> GetAlbumTypesAll();
        List<AlbumType> GetAlbumTypesAll(bool AlbumTypeStatus);
        List<AlbumType> GetAlbumTypesAll(int page = 1, int pageSize = 7, string filter = null);
        List<AlbumType> GetAlbumTypesAll(bool AlbumTypeStatus, int page = 1, int pageSize = 7, string filter = null);


        //IQueryable
        IQueryable<AlbumType> GetAlbumTypesAllAsQueryable();
        IQueryable<AlbumType> GetAlbumTypesAllAsQueryable(bool AlbumTypeStatus);
        IQueryable<AlbumType> GetAlbumTypesAllAsQueryable(int page = 1, int pageSize = 7, string filter = null);
        IQueryable<AlbumType> GetAlbumTypesAllAsQueryable(bool AlbumTypeStatus, int page = 1, int pageSize = 7, string filter = null);
    }
}


