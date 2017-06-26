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
    public class SubscriptionTypeFacade
    {
        public SubscriptionTypeDTO GetSubscriptionType(int SubscriptionTypeID)
        {
            SubscriptionTypeDTO result = Mapper.Map<SubscriptionTypeDTO>(DataLayer.DataLayerFacade.SubscriptionTypeRepository().GetSubscriptionType(SubscriptionTypeID));
            return result;
        }

        public DTResult<SubscriptionTypeDTO> FilterResult(string search, string sortOrder, int start, int length, List<string> columnFilters, DTParameters param)
        {
            var basequery = DataLayer.DataLayerFacade.SubscriptionTypeRepository().GetSubscriptionTypesAllAsQueryable();

            if (search.Length > 0)
            {
                basequery = basequery.Where(p => (p.SubscriptionTypeName.ToLower().Contains(search.ToLower())));
            }

            var queryList = RepositoryHelper.PaginationDatatables<SubscriptionType>(start, length, basequery).Future();
            var queryTotal = basequery.FutureCount();

            var resultList = Mapper.Map<List<SubscriptionTypeDTO>>(queryList.ToList());
            int intTotal = queryTotal.Value;

            DTResult<SubscriptionTypeDTO> result = new DTResult<SubscriptionTypeDTO>
            {
                draw = param.Draw,
                recordsFiltered = intTotal,
                recordsTotal = intTotal,
                data = resultList
            };
            return result;
        }

        public PaginationSet<SubscriptionTypeDTO> GetSubscriptionTypes(int page, int pagesize)
        {
            var basequery = DataLayer.DataLayerFacade.SubscriptionTypeRepository().GetSubscriptionTypesAllAsQueryable();

            var queryList = DataLayer.Repository.RepositoryHelper.Pagination<SubscriptionType>(page, pagesize, basequery).Future();
            var queryTotal = basequery.FutureCount();

            int intTotal = queryTotal.Value;

            PaginationSet<SubscriptionTypeDTO> result = new PaginationSet<SubscriptionTypeDTO>()
            {
                Page = page,
                TotalCount = intTotal,
                TotalPages = (int)Math.Ceiling((decimal)intTotal / pagesize),
                Items = Mapper.Map<List<SubscriptionTypeDTO>>(queryList.ToList())
            };

            return result;
        }

        public List<SubscriptionTypeDTO> GetSubscriptionTypes()
        {
            var basequery = Mapper.Map<List<SubscriptionTypeDTO>>(DataLayer.DataLayerFacade.SubscriptionTypeRepository().GetSubscriptionTypesAllAsQueryable());
            return basequery.ToList();
        }



        public SubscriptionTypeDTO Insert(SubscriptionTypeDTO objSubscriptionType)
        {
            SubscriptionTypeDTO result = Mapper.Map<SubscriptionTypeDTO>(DataLayer.DataLayerFacade.SubscriptionTypeRepository().Insert(objSubscriptionType.SubscriptionTypeName, objSubscriptionType.SubscriptionTypeFee, objSubscriptionType.SubscriptionTypeStatus, objSubscriptionType.SubscriptionTypeImage, objSubscriptionType.SubscriptionTypeDescription));

            if (result != null)
            {
                return result;
            }
            else
            {
                return new SubscriptionTypeDTO();
            }
        }

        public bool Delete(int SubscriptionTypeID)
        {
            bool result = DataLayer.DataLayerFacade.SubscriptionTypeRepository().Delete(SubscriptionTypeID);
            if (result)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Update(int SubscriptionTypeID, SubscriptionTypeDTO objSubscriptionType)
        {
            bool result = DataLayer.DataLayerFacade.SubscriptionTypeRepository().Update(objSubscriptionType.SubscriptionTypeID, objSubscriptionType.SubscriptionTypeName, objSubscriptionType.SubscriptionTypeFee, objSubscriptionType.SubscriptionTypeStatus, objSubscriptionType.SubscriptionTypeImage, objSubscriptionType.SubscriptionTypeDescription);
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