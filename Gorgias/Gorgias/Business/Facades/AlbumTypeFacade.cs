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
    public class AlbumTypeFacade
    {
        public AlbumTypeDTO GetAlbumType(int AlbumTypeID)
        {
            AlbumTypeDTO result = Mapper.Map<AlbumTypeDTO>(DataLayer.DataLayerFacade.AlbumTypeRepository().GetAlbumType(AlbumTypeID));
            return result;
        }

        public DTResult<AlbumTypeDTO> FilterResult(string search, string sortOrder, int start, int length, List<string> columnFilters, DTParameters param)
        {
            var basequery = DataLayer.DataLayerFacade.AlbumTypeRepository().GetAlbumTypesAllAsQueryable();

            if (search.Length > 0)
            {
                basequery = basequery.Where(p => (p.AlbumTypeName.ToLower().Contains(search.ToLower())));
            }

            var queryList = RepositoryHelper.PaginationDatatables<AlbumType>(start, length, basequery).Future();
            var queryTotal = basequery.FutureCount();

            var resultList = Mapper.Map<List<AlbumTypeDTO>>(queryList.ToList());
            int intTotal = queryTotal.Value;

            DTResult<AlbumTypeDTO> result = new DTResult<AlbumTypeDTO>
            {
                draw = param.Draw,
                recordsFiltered = intTotal,
                recordsTotal = intTotal,
                data = resultList
            };
            return result;
        }

        public PaginationSet<AlbumTypeDTO> GetAlbumTypes(int page, int pagesize)
        {
            var basequery = DataLayer.DataLayerFacade.AlbumTypeRepository().GetAlbumTypesAllAsQueryable();

            var queryList = DataLayer.Repository.RepositoryHelper.Pagination<AlbumType>(page, pagesize, basequery).Future();
            var queryTotal = basequery.FutureCount();

            int intTotal = queryTotal.Value;

            PaginationSet<AlbumTypeDTO> result = new PaginationSet<AlbumTypeDTO>()
            {
                Page = page,
                TotalCount = intTotal,
                TotalPages = (int)Math.Ceiling((decimal)intTotal / pagesize),
                Items = Mapper.Map<List<AlbumTypeDTO>>(queryList.ToList())
            };

            return result;
        }

        public List<AlbumTypeDTO> GetAlbumTypes()
        {
            var basequery = Mapper.Map<List<AlbumTypeDTO>>(DataLayer.DataLayerFacade.AlbumTypeRepository().GetAlbumTypesAllAsQueryable());
            return basequery.ToList();
        }



        public AlbumTypeDTO Insert(AlbumTypeDTO objAlbumType)
        {
            AlbumTypeDTO result = Mapper.Map<AlbumTypeDTO>(DataLayer.DataLayerFacade.AlbumTypeRepository().Insert(objAlbumType.AlbumTypeID,objAlbumType.AlbumTypeName, objAlbumType.AlbumTypeStatus, objAlbumType.AlbumTypeLimitation));

            if (result != null)
            {
                return result;
            }
            else
            {
                return new AlbumTypeDTO();
            }
        }

        public bool Delete(int AlbumTypeID)
        {
            bool result = DataLayer.DataLayerFacade.AlbumTypeRepository().Delete(AlbumTypeID);
            if (result)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Update(int AlbumTypeID, AlbumTypeDTO objAlbumType)
        {
            bool result = DataLayer.DataLayerFacade.AlbumTypeRepository().Update(objAlbumType.AlbumTypeID, objAlbumType.AlbumTypeName, objAlbumType.AlbumTypeStatus, objAlbumType.AlbumTypeLimitation);
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