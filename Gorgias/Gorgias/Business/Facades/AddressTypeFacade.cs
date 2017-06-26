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
    public class AddressTypeFacade
    {                
        public AddressTypeDTO GetAddressType(int AddressTypeID)
        {
            AddressTypeDTO result = Mapper.Map<AddressTypeDTO>(DataLayer.DataLayerFacade.AddressTypeRepository().GetAddressType(AddressTypeID));             
            return result;
        }

        public DTResult<AddressTypeDTO> FilterResult(string search, string sortOrder, int start, int length, List<string> columnFilters, DTParameters param)
        {
            var basequery = DataLayer.DataLayerFacade.AddressTypeRepository().GetAddressTypesAllAsQueryable();

            if (search.Length>0) {
                basequery = basequery.Where(p => (p.AddressTypeName.ToLower().Contains(search.ToLower())));
            }

            var queryList = RepositoryHelper.PaginationDatatables<AddressType>(start, length, basequery).Future();
            var queryTotal = basequery.FutureCount();

            var resultList = Mapper.Map<List<AddressTypeDTO>>(queryList.ToList());
            int intTotal = queryTotal.Value;

            DTResult<AddressTypeDTO> result = new DTResult<AddressTypeDTO>
            {
                draw = param.Draw,
                recordsFiltered = intTotal,
                recordsTotal = intTotal,
                data = resultList
            };
            return result;
        }       

        public PaginationSet<AddressTypeDTO> GetAddressTypes(int page, int pagesize)
        {           
            var basequery = DataLayer.DataLayerFacade.AddressTypeRepository().GetAddressTypesAllAsQueryable();

            var queryList = DataLayer.Repository.RepositoryHelper.Pagination<AddressType>(page, pagesize, basequery).Future();
            var queryTotal = basequery.FutureCount();

            int intTotal = queryTotal.Value;

            PaginationSet<AddressTypeDTO> result = new PaginationSet<AddressTypeDTO>()
            {
                Page = page,
                TotalCount = intTotal,
                TotalPages = (int)Math.Ceiling((decimal)intTotal / pagesize),
                Items = Mapper.Map<List<AddressTypeDTO>>(queryList.ToList())
            };

            return result;            
        }
        
        public List<AddressTypeDTO> GetAddressTypes()
        {           
            var basequery = Mapper.Map <List<AddressTypeDTO>>(DataLayer.DataLayerFacade.AddressTypeRepository().GetAddressTypesAllAsQueryable());
            return basequery.ToList();
        }

        

        public AddressTypeDTO Insert(AddressTypeDTO objAddressType)
        {            
            AddressTypeDTO result = Mapper.Map<AddressTypeDTO>(DataLayer.DataLayerFacade.AddressTypeRepository().Insert(objAddressType.AddressTypeName, objAddressType.AddressTypeImage, objAddressType.AddressTypeStatus));

            if (result!=null)
            {
                return result;            
            }
            else
            {
                return new AddressTypeDTO();
            }
        }
               
        public bool Delete(int AddressTypeID)
        {            
            bool result = DataLayer.DataLayerFacade.AddressTypeRepository().Delete(AddressTypeID);
            if (result)
            {
                return true;            
            }
            else
            {
                return false;
            }
        }

        public bool Update(int AddressTypeID, AddressTypeDTO objAddressType)
        {            
            bool result = DataLayer.DataLayerFacade.AddressTypeRepository().Update(objAddressType.AddressTypeID, objAddressType.AddressTypeName, objAddressType.AddressTypeImage, objAddressType.AddressTypeStatus);
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