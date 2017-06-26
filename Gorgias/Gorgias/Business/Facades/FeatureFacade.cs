using AutoMapper;
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

namespace Gorgias.BusinessLayer.Facades
{   
    public class FeatureFacade
    {                
        public FeatureDTO GetFeature(int FeatureID)
        {
            FeatureDTO result = Mapper.Map<FeatureDTO>(DataLayer.DataLayerFacade.FeatureRepository().GetFeature(FeatureID));             
            return result;
        }

        public DTResult<FeatureDTO> FilterResult(string search, string sortOrder, int start, int length, List<string> columnFilters, DTParameters param)
        {
            var basequery = DataLayer.DataLayerFacade.FeatureRepository().GetFeaturesAllAsQueryable();

            if (search.Length>0) {
                basequery = basequery.Where(p => (p.FeatureTitle.ToLower().Contains(search.ToLower())));
            }

            var queryList = RepositoryHelper.PaginationDatatables<Feature>(start, length, basequery).Future();
            var queryTotal = basequery.FutureCount();

            var resultList = Mapper.Map<List<FeatureDTO>>(queryList.ToList());
            int intTotal = queryTotal.Value;

            DTResult<FeatureDTO> result = new DTResult<FeatureDTO>
            {
                draw = param.Draw,
                recordsFiltered = intTotal,
                recordsTotal = intTotal,
                data = resultList
            };
            return result;
        }       

        public PaginationSet<FeatureDTO> GetFeatures(int page, int pagesize)
        {           
            var basequery = DataLayer.DataLayerFacade.FeatureRepository().GetFeaturesAllAsQueryable();

            var queryList = DataLayer.Repository.RepositoryHelper.Pagination<Feature>(page, pagesize, basequery).Future();
            var queryTotal = basequery.FutureCount();

            int intTotal = queryTotal.Value;

            PaginationSet<FeatureDTO> result = new PaginationSet<FeatureDTO>()
            {
                Page = page,
                TotalCount = intTotal,
                TotalPages = (int)Math.Ceiling((decimal)intTotal / pagesize),
                Items = Mapper.Map<List<FeatureDTO>>(queryList.ToList())
            };

            return result;            
        }
        
        public List<FeatureDTO> GetFeatures()
        {           
            var basequery = Mapper.Map <List<FeatureDTO>>(DataLayer.DataLayerFacade.FeatureRepository().GetFeaturesAllAsQueryable());
            return basequery.ToList();
        }

        

        public FeatureDTO Insert(FeatureDTO objFeature)
        {            
            FeatureDTO result = Mapper.Map<FeatureDTO>(DataLayer.DataLayerFacade.FeatureRepository().Insert(objFeature.FeatureTitle, objFeature.FeatureDateCreated, objFeature.FeatureDateExpired, objFeature.FeatureStatus, objFeature.FeatureIsDeleted, objFeature.FeatureImage, objFeature.FeatureDescription, objFeature.ProfileID));

            if (result!=null)
            {
                return result;            
            }
            else
            {
                return new FeatureDTO();
            }
        }
               
        public bool Delete(int FeatureID)
        {            
            bool result = DataLayer.DataLayerFacade.FeatureRepository().Delete(FeatureID);
            if (result)
            {
                return true;            
            }
            else
            {
                return false;
            }
        }

        public bool Update(int FeatureID, FeatureDTO objFeature)
        {            
            bool result = DataLayer.DataLayerFacade.FeatureRepository().Update(objFeature.FeatureID, objFeature.FeatureTitle, objFeature.FeatureDateCreated, objFeature.FeatureDateExpired, objFeature.FeatureStatus, objFeature.FeatureIsDeleted, objFeature.FeatureImage, objFeature.FeatureDescription, objFeature.ProfileID);
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