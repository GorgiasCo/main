﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
namespace Gorgias.DataLayer.Interface
{   
        public interface IProfileTypeRepository
    {
    
    
        ProfileType Insert(String ProfileTypeName, Boolean ProfileTypeStatus, String ProfileTypeImage, String ProfileTypeDescription, int ProfileTypeParentID);
        bool Update(int ProfileTypeID, String ProfileTypeName, Boolean ProfileTypeStatus, String ProfileTypeImage, String ProfileTypeDescription, int ProfileTypeParentID);        
        bool Delete(int ProfileTypeID);

        ProfileType GetProfileType(int ProfileTypeID);

        //List
        List<ProfileType> GetProfileTypesAll();
        List<ProfileType> GetProfileTypesAll(bool ProfileTypeStatus);
        List<ProfileType> GetProfileTypesAll(int page = 1, int pageSize = 7, string filter=null);
        List<ProfileType> GetProfileTypesAll(bool ProfileTypeStatus, int page = 1, int pageSize = 7, string filter=null);        
        
        
        //IQueryable
        IQueryable<ProfileType> GetProfileTypesAllAsQueryable();
        IQueryable<ProfileType> GetProfileTypesAllAsQueryable(bool ProfileTypeStatus);
        IQueryable<ProfileType> GetProfileTypesAllAsQueryable(int page = 1, int pageSize = 7, string filter=null);
        IQueryable<ProfileType> GetProfileTypesAllAsQueryable(bool ProfileTypeStatus, int page = 1, int pageSize = 7, string filter=null);   
    }
}

