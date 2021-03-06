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

namespace Gorgias.BusinessLayer.Facades
{
    public class ConnectionFacade
    {
        //V2 Begin ;)
        public IQueryable<Business.DataTransferObjects.Mobile.V2.ProfileSubscribeMobileModel> getProfileSubscribes(int ProfileID)
        {
            return DataLayer.DataLayerFacade.ConnectionRepository().GetConnectionsByProfileIDAllAsQueryable(ProfileID);
        }

        public PaginationSet<Business.DataTransferObjects.Web.V2.ProfileFollowerModel> getProfileSubscribesAsFollower(int ProfileID, int RequestTypeID, int pagesize, int page)
        {
            var basequery = DataLayer.DataLayerFacade.ConnectionRepository().GetConnectionsByProfileIDAsFollowerAsQueryable(ProfileID, RequestTypeID);

            var queryList = RepositoryHelper.Pagination<Business.DataTransferObjects.Web.V2.ProfileFollowerModel>(page, pagesize, basequery).Future();
            var queryTotal = basequery.FutureCount();

            int intTotal = queryTotal.Value;

            PaginationSet<Business.DataTransferObjects.Web.V2.ProfileFollowerModel> result = new PaginationSet<Business.DataTransferObjects.Web.V2.ProfileFollowerModel>()
            {
                Page = page,
                TotalCount = intTotal,
                TotalPages = (int)Math.Ceiling((decimal)intTotal / pagesize),
                Items = queryList.ToList()
            };

            return result;// DataLayer.DataLayerFacade.ConnectionRepository().GetConnectionsByProfileIDAsFollowerAsQueryable(ProfileID, RequestTypeID);
        }        
        //V2 Ends ;)

        public ConnectionDTO GetConnection(int ProfileID, int RequestedProfileID, int RequestTypeID)
        {
            ConnectionDTO result = Mapper.Map<ConnectionDTO>(DataLayer.DataLayerFacade.ConnectionRepository().GetConnection(ProfileID, RequestedProfileID, RequestTypeID));
            return result;
        }

        public DTResult<ConnectionDTO> FilterResult(string search, string sortOrder, int start, int length, List<string> columnFilters, DTParameters param)
        {
            var basequery = DataLayer.DataLayerFacade.ConnectionRepository().GetConnectionsAllAsQueryable();

            if (search.Length > 0)
            {
                basequery = basequery.Where(p => (p.Profile.ProfileFullname.ToLower().Contains(search.ToLower()) || p.Profile1.ProfileFullname.ToLower().Contains(search.ToLower())));
            }

            var queryList = RepositoryHelper.PaginationDatatables<Connection>(start, length, basequery).Future();
            var queryTotal = basequery.FutureCount();

            var resultList = Mapper.Map<List<ConnectionDTO>>(queryList.ToList());
            int intTotal = queryTotal.Value;

            DTResult<ConnectionDTO> result = new DTResult<ConnectionDTO>
            {
                draw = param.Draw,
                recordsFiltered = intTotal,
                recordsTotal = intTotal,
                data = resultList
            };
            return result;
        }

        public PaginationSet<ConnectionDTO> GetConnections(int page, int pagesize)
        {
            var basequery = DataLayer.DataLayerFacade.ConnectionRepository().GetConnectionsAllAsQueryable();

            var queryList = DataLayer.Repository.RepositoryHelper.Pagination<Connection>(page, pagesize, basequery).Future();
            var queryTotal = basequery.FutureCount();

            int intTotal = queryTotal.Value;

            PaginationSet<ConnectionDTO> result = new PaginationSet<ConnectionDTO>()
            {
                Page = page,
                TotalCount = intTotal,
                TotalPages = (int)Math.Ceiling((decimal)intTotal / pagesize),
                Items = Mapper.Map<List<ConnectionDTO>>(queryList.ToList())
            };

            return result;
        }

        public List<ConnectionDTO> GetConnections()
        {
            var basequery = Mapper.Map<List<ConnectionDTO>>(DataLayer.DataLayerFacade.ConnectionRepository().GetConnectionsAllAsQueryable());
            return basequery.ToList();
        }


        public DTResult<ConnectionDTO> FilterResultByRequestedProfileID(string search, string sortOrder, int start, int length, List<string> columnFilters, DTParameters param, int RequestedProfileID)
        {
            var basequery = DataLayer.DataLayerFacade.ConnectionRepository().GetConnectionsAllAsQueryable().Where(m => m.RequestedProfileID == RequestedProfileID);

            if (search.Length > 0)
            {
                basequery = basequery.Where(p => (p.Profile.ProfileFullname.ToLower().Contains(search.ToLower())));
            }

            var queryList = RepositoryHelper.PaginationDatatables<Connection>(start, length, basequery).Future();
            var queryTotal = basequery.FutureCount();

            var resultList = Mapper.Map<List<ConnectionDTO>>(queryList.ToList());
            int intTotal = queryTotal.Value;

            DTResult<ConnectionDTO> result = new DTResult<ConnectionDTO>
            {
                draw = param.Draw,
                recordsFiltered = intTotal,
                recordsTotal = intTotal,
                data = resultList
            };
            return result;
        }

        public PaginationSet<ConnectionDTO> GetConnectionsByRequestedProfileID(int RequestedProfileID, int page, int pagesize)
        {

            var basequery = DataLayer.DataLayerFacade.ConnectionRepository().GetConnectionsByRequestedProfileIDAsQueryable(RequestedProfileID);

            var queryList = DataLayer.Repository.RepositoryHelper.Pagination<Connection>(page, pagesize, basequery).Future();
            var queryTotal = basequery.FutureCount();

            int intTotal = queryTotal.Value;

            PaginationSet<ConnectionDTO> result = new PaginationSet<ConnectionDTO>()
            {
                Page = page,
                TotalCount = intTotal,
                TotalPages = (int)Math.Ceiling((decimal)intTotal / pagesize),
                Items = Mapper.Map<List<ConnectionDTO>>(queryList.ToList())
            };

            return result;
        }

        public PaginationSet<ConnectionDTO> GetConnectionsByRequestedProfileID(int RequestedProfileID, bool ConnectStatus, int page, int pagesize)
        {

            var basequery = DataLayer.DataLayerFacade.ConnectionRepository().GetConnectionsByRequestedProfileIDAsQueryable(RequestedProfileID, ConnectStatus);

            var queryList = RepositoryHelper.Pagination<Connection>(page, pagesize, basequery).Future();
            var queryTotal = basequery.FutureCount();

            int intTotal = queryTotal.Value;

            PaginationSet<ConnectionDTO> result = new PaginationSet<ConnectionDTO>()
            {
                Page = page,
                TotalCount = intTotal,
                TotalPages = (int)Math.Ceiling((decimal)intTotal / pagesize),
                Items = Mapper.Map<List<ConnectionDTO>>(queryList.ToList())
            };

            return result;
        }
        public DTResult<ConnectionDTO> FilterResultByRequestTypeID(string search, string sortOrder, int start, int length, List<string> columnFilters, DTParameters param, int RequestTypeID)
        {
            var basequery = DataLayer.DataLayerFacade.ConnectionRepository().GetConnectionsAllAsQueryable().Where(m => m.RequestTypeID == RequestTypeID);

            if (search.Length > 0)
            {
                basequery = basequery.Where(p => (p.Profile.ProfileFullname.ToLower().Contains(search.ToLower())));
            }

            var queryList = RepositoryHelper.PaginationDatatables<Connection>(start, length, basequery).Future();
            var queryTotal = basequery.FutureCount();

            var resultList = Mapper.Map<List<ConnectionDTO>>(queryList.ToList());
            int intTotal = queryTotal.Value;

            DTResult<ConnectionDTO> result = new DTResult<ConnectionDTO>
            {
                draw = param.Draw,
                recordsFiltered = intTotal,
                recordsTotal = intTotal,
                data = resultList
            };
            return result;
        }


        public PaginationSet<ConnectionDTO> GetConnectionsByRequestTypeID(int RequestTypeID, int page, int pagesize)
        {

            var basequery = DataLayer.DataLayerFacade.ConnectionRepository().GetConnectionsByRequestTypeIDAsQueryable(RequestTypeID);

            var queryList = DataLayer.Repository.RepositoryHelper.Pagination<Connection>(page, pagesize, basequery).Future();
            var queryTotal = basequery.FutureCount();

            int intTotal = queryTotal.Value;

            PaginationSet<ConnectionDTO> result = new PaginationSet<ConnectionDTO>()
            {
                Page = page,
                TotalCount = intTotal,
                TotalPages = (int)Math.Ceiling((decimal)intTotal / pagesize),
                Items = Mapper.Map<List<ConnectionDTO>>(queryList.ToList())
            };

            return result;
        }

        public string InsertBookmarkFromMicroApp(int ProfileID, int MicroAppProfileID)
        {
            Connection resultBookmark = DataLayer.DataLayerFacade.ConnectionRepository().GetConnection(ProfileID, MicroAppProfileID, 4);
            if(resultBookmark != null)
            {
                return "0";
            } else
            {
                return DataLayer.DataLayerFacade.ProfileRepository().GetProfileFullname(ProfileID);// DataLayer.DataLayerFacade.ConnectionRepository().Insert(ProfileID, MicroAppProfileID, 4, true);
            }
        }

        public bool InsertInAppForMobile(ConnectionDTO objConnection)
        {
            if (objConnection.RequestTypeID == 4)
            {
                return DataLayer.DataLayerFacade.ConnectionRepository().Insert(objConnection.ProfileID, objConnection.RequestedProfileID, objConnection.RequestTypeID, true);
            }
            else
            {
                return DataLayer.DataLayerFacade.ConnectionRepository().Insert(objConnection.ProfileID, objConnection.RequestedProfileID, objConnection.RequestTypeID);
            }
        }

        public bool InsertForMobile(ConnectionDTO objConnection)
        {
            if(objConnection.RequestTypeID == 4)
            {
                return DataLayer.DataLayerFacade.ConnectionRepository().Insert(objConnection.ProfileID, objConnection.RequestedProfileID, objConnection.RequestTypeID, false);
            } else
            {
                return DataLayer.DataLayerFacade.ConnectionRepository().Insert(objConnection.ProfileID, objConnection.RequestedProfileID, objConnection.RequestTypeID);
            }            
        }

        public ConnectionDTO Insert(ConnectionDTO objConnection)
        {
            ConnectionDTO result = Mapper.Map<ConnectionDTO>(DataLayer.DataLayerFacade.ConnectionRepository().Insert(objConnection.ProfileID, objConnection.RequestedProfileID, objConnection.RequestTypeID, objConnection.ConnectStatus, objConnection.ConnectDateCreated));

            if (result != null)
            {
                return result;
            }
            else
            {
                return new ConnectionDTO();
            }
        }

        public bool Delete(int ProfileID, int RequestedProfileID, int RequestTypeID)
        {
            bool result = DataLayer.DataLayerFacade.ConnectionRepository().Delete(ProfileID, RequestedProfileID, RequestTypeID);
            if (result)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Update(int ProfileID, int RequestedProfileID, int RequestTypeID, ConnectionDTO objConnection)
        {
            bool result = DataLayer.DataLayerFacade.ConnectionRepository().Update(ProfileID, RequestedProfileID, RequestTypeID, objConnection.ConnectStatus);
            if (result)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Update(int ProfileID, int RequestedProfileID)
        {
            bool result = DataLayer.DataLayerFacade.ConnectionRepository().Update(ProfileID, RequestedProfileID);
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