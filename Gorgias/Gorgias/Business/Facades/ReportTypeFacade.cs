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
    public class ReportTypeFacade
    {
        public ReportTypeDTO GetReportType(int ReportTypeID)
        {
            ReportTypeDTO result = Mapper.Map<ReportTypeDTO>(DataLayer.DataLayerFacade.ReportTypeRepository().GetReportType(ReportTypeID));
            return result;
        }

        public DTResult<ReportTypeDTO> FilterResult(string search, string sortOrder, int start, int length, List<string> columnFilters, DTParameters param)
        {
            var basequery = DataLayer.DataLayerFacade.ReportTypeRepository().GetReportTypesAllAsQueryable();

            if (search.Length > 0)
            {
                basequery = basequery.Where(p => (p.ReportTypeName.ToLower().Contains(search.ToLower())));
            }

            var queryList = RepositoryHelper.PaginationDatatables<ReportType>(start, length, basequery).Future();
            var queryTotal = basequery.FutureCount();

            var resultList = Mapper.Map<List<ReportTypeDTO>>(queryList.ToList());
            int intTotal = queryTotal.Value;

            DTResult<ReportTypeDTO> result = new DTResult<ReportTypeDTO>
            {
                draw = param.Draw,
                recordsFiltered = intTotal,
                recordsTotal = intTotal,
                data = resultList
            };
            return result;
        }

        public PaginationSet<ReportTypeDTO> GetReportTypes(int page, int pagesize)
        {
            var basequery = DataLayer.DataLayerFacade.ReportTypeRepository().GetReportTypesAllAsQueryable();

            var queryList = DataLayer.Repository.RepositoryHelper.Pagination<ReportType>(page, pagesize, basequery).Future();
            var queryTotal = basequery.FutureCount();

            int intTotal = queryTotal.Value;

            PaginationSet<ReportTypeDTO> result = new PaginationSet<ReportTypeDTO>()
            {
                Page = page,
                TotalCount = intTotal,
                TotalPages = (int)Math.Ceiling((decimal)intTotal / pagesize),
                Items = Mapper.Map<List<ReportTypeDTO>>(queryList.ToList())
            };

            return result;
        }

        public List<ReportTypeDTO> GetReportTypes()
        {
            var basequery = Mapper.Map<List<ReportTypeDTO>>(DataLayer.DataLayerFacade.ReportTypeRepository().GetReportTypesAllAsQueryable());
            return basequery.ToList();
        }

        public ReportTypeDTO Insert(ReportTypeDTO objReportType)
        {
            ReportTypeDTO result = Mapper.Map<ReportTypeDTO>(DataLayer.DataLayerFacade.ReportTypeRepository().Insert(objReportType.ReportTypeID, objReportType.ReportTypeName, objReportType.ReportTypeIsCountable, objReportType.ReportTypeStatus));

            if (result != null)
            {
                return result;
            }
            else
            {
                return new ReportTypeDTO();
            }
        }

        public bool Delete(int ReportTypeID)
        {
            bool result = DataLayer.DataLayerFacade.ReportTypeRepository().Delete(ReportTypeID);
            if (result)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Update(int ReportTypeID, ReportTypeDTO objReportType)
        {
            bool result = DataLayer.DataLayerFacade.ReportTypeRepository().Update(objReportType.ReportTypeID, objReportType.ReportTypeName, objReportType.ReportTypeIsCountable, objReportType.ReportTypeStatus);
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