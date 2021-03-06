﻿using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using Gorgias.DataLayer.Repository;
using Gorgias.Business.DataTransferObjects;
using Gorgias.Infrastruture.Core;
using Gorgias.Business.DataTransferObjects.Helper;
using EntityFramework.Extensions;
using System.Threading.Tasks;

namespace Gorgias.BusinessLayer.Facades
{
    public class CategoryFacade
    {
        //V2 Begin ;)
        public IQueryable<Business.DataTransferObjects.Mobile.V2.CategoryMobileModel> getCategories(string languageCode)
        {
            return DataLayer.DataLayerFacade.CategoryRepository().GetV2CategoriesAllAsQueryable(languageCode);
        }

        public IQueryable<Business.DataTransferObjects.Mobile.V2.KeyValueMobileModel> getCategories(int CategoryParentID, string languageCode)
        {
            return DataLayer.DataLayerFacade.CategoryRepository().GetV2CategoriesAllAsQueryableKeyValue(CategoryParentID, languageCode);
        }

        public IQueryable<Business.DataTransferObjects.Mobile.V2.KeyValueMobileModel> getCategoriesByProfile(int ProfileID, string languageCode)
        {
            return DataLayer.DataLayerFacade.CategoryRepository().GetV2CategoriesByProfileIDAsQueryableKeyValue(ProfileID, languageCode);
        }

        public IQueryable<Business.DataTransferObjects.Mobile.V2.CategoryMobileModel> getCategoriesAvailableByProfile(int ProfileID, string languageCode)
        {
            IQueryable<Business.DataTransferObjects.Mobile.V2.CategoryMobileModel>  result = DataLayer.DataLayerFacade.CategoryRepository().GetV2CategoriesAvailableByProfileIDAsQueryable(ProfileID, languageCode);
            return result;
        }

        public IQueryable<Business.DataTransferObjects.Mobile.V2.KeyValueMobileModel> getCategoriesBySearch(string categorySearch)
        {
            return DataLayer.DataLayerFacade.CategoryRepository().GetV2CategoriesBySearchAsQueryableKeyValue(categorySearch);
        }

        //V2 End
        public CategoryDTO GetCategory(int CategoryID)
        {
            CategoryDTO result = Mapper.Map<CategoryDTO>(DataLayer.DataLayerFacade.CategoryRepository().GetCategory(CategoryID));
            return result;
        }

        public DTResult<CategoryDTO> FilterResult(string search, string sortOrder, int start, int length, List<string> columnFilters, DTParameters param)
        {
            var basequery = DataLayer.DataLayerFacade.CategoryRepository().GetCategoriesAllAsQueryable();

            if (search.Length > 0)
            {
                basequery = basequery.Where(p => (p.CategoryName.ToLower().Contains(search.ToLower())));
            }

            var queryList = RepositoryHelper.PaginationDatatables<Category>(start, length, basequery).Future();
            var queryTotal = basequery.FutureCount();

            var resultList = Mapper.Map<List<CategoryDTO>>(queryList.ToList());
            int intTotal = queryTotal.Value;

            DTResult<CategoryDTO> result = new DTResult<CategoryDTO>
            {
                draw = param.Draw,
                recordsFiltered = intTotal,
                recordsTotal = intTotal,
                data = resultList
            };
            return result;
        }

        public PaginationSet<CategoryDTO> GetCategories(int page, int pagesize)
        {
            var basequery = DataLayer.DataLayerFacade.CategoryRepository().GetCategoriesAllAsQueryable();

            var queryList = DataLayer.Repository.RepositoryHelper.Pagination<Category>(page, pagesize, basequery).Future();
            var queryTotal = basequery.FutureCount();

            int intTotal = queryTotal.Value;

            PaginationSet<CategoryDTO> result = new PaginationSet<CategoryDTO>()
            {
                Page = page,
                TotalCount = intTotal,
                TotalPages = (int)Math.Ceiling((decimal)intTotal / pagesize),
                Items = Mapper.Map<List<CategoryDTO>>(queryList.ToList())
            };

            return result;
        }

        public List<CategoryDTO> GetCategories()
        {
            var basequery = Mapper.Map<List<CategoryDTO>>(DataLayer.DataLayerFacade.CategoryRepository().GetCategoriesAllAsQueryable());
            return basequery.ToList();
        }

        public List<CategoryDTO> GetCategories(string languageCode)
        {
            //var basequery = Mapper.Map<IQueryable<CategoryDTO>>(DataLayer.DataLayerFacade.CategoryRepository().GetCategoriesAllAsQueryableX(languageCode));
            var basequery = DataLayer.DataLayerFacade.CategoryRepository().GetCategoriesAllAsQueryable(languageCode);
            return basequery.ToList();
        }

        public CategoryDTO Insert(CategoryDTO objCategory)
        {
            CategoryDTO result = Mapper.Map<CategoryDTO>(DataLayer.DataLayerFacade.CategoryRepository().Insert(objCategory.CategoryName, objCategory.CategoryStatus, objCategory.CategoryImage, objCategory.CategoryDescription, objCategory.CategoryParentID, objCategory.CategoryOrder, objCategory.CategoryType));

            if (result != null)
            {
                return result;
            }
            else
            {
                return new CategoryDTO();
            }
        }

        //V2
        public CategoryDTO Insert(string CategoryName, int? CategoryID, string languageCode)
        {
            CategoryDTO result = Mapper.Map<CategoryDTO>(DataLayer.DataLayerFacade.CategoryRepository().Insert(CategoryName, CategoryID, languageCode));

            if (result != null)
            {
                return result;
            }
            else
            {
                return new CategoryDTO();
            }
        }
        //End of V2

        public bool Delete(int CategoryID)
        {
            bool result = DataLayer.DataLayerFacade.CategoryRepository().Delete(CategoryID);
            if (result)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Update(int CategoryID, CategoryDTO objCategory)
        {
            bool result = DataLayer.DataLayerFacade.CategoryRepository().Update(objCategory.CategoryID, objCategory.CategoryName, objCategory.CategoryStatus, objCategory.CategoryImage, objCategory.CategoryDescription, objCategory.CategoryParentID, objCategory.CategoryOrder, objCategory.CategoryType);
            if (result)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}