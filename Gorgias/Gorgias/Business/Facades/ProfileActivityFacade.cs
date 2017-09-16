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
using System.Data.Entity.Spatial;

namespace Gorgias.BusinessLayer.Facades
{
    public class ProfileActivityFacade
    {
        public ProfileActivityDTO GetProfileActivity(int ProfileID, int AlbumID)
        {
            ProfileActivityDTO result = Mapper.Map<ProfileActivityDTO>(DataLayer.DataLayerFacade.ProfileActivityRepository().GetProfileActivity(ProfileID, AlbumID));
            return result;
        }

        public DTResult<ProfileActivityDTO> FilterResult(string search, string sortOrder, int start, int length, List<string> columnFilters, DTParameters param)
        {
            var basequery = DataLayer.DataLayerFacade.ProfileActivityRepository().GetProfileActivitiesAllAsQueryable();

            //if (search.Length > 0)
            //{
            //    basequery = basequery.Where(p => (p.AlbumID.ToLower().Contains(search.ToLower())));
            //}

            var queryList = RepositoryHelper.PaginationDatatables<ProfileActivity>(start, length, basequery).Future();
            var queryTotal = basequery.FutureCount();

            var resultList = Mapper.Map<List<ProfileActivityDTO>>(queryList.ToList());
            int intTotal = queryTotal.Value;

            DTResult<ProfileActivityDTO> result = new DTResult<ProfileActivityDTO>
            {
                draw = param.Draw,
                recordsFiltered = intTotal,
                recordsTotal = intTotal,
                data = resultList
            };
            return result;
        }

        public PaginationSet<ProfileActivityDTO> GetProfileActivities(int page, int pagesize)
        {
            var basequery = DataLayer.DataLayerFacade.ProfileActivityRepository().GetProfileActivitiesAllAsQueryable();

            var queryList = DataLayer.Repository.RepositoryHelper.Pagination<ProfileActivity>(page, pagesize, basequery).Future();
            var queryTotal = basequery.FutureCount();

            int intTotal = queryTotal.Value;

            PaginationSet<ProfileActivityDTO> result = new PaginationSet<ProfileActivityDTO>()
            {
                Page = page,
                TotalCount = intTotal,
                TotalPages = (int)Math.Ceiling((decimal)intTotal / pagesize),
                Items = Mapper.Map<List<ProfileActivityDTO>>(queryList.ToList())
            };

            return result;
        }

        public List<ProfileActivityDTO> GetProfileActivities()
        {
            var basequery = Mapper.Map<List<ProfileActivityDTO>>(DataLayer.DataLayerFacade.ProfileActivityRepository().GetProfileActivitiesAllAsQueryable());
            return basequery.ToList();
        }


        public DTResult<ProfileActivityDTO> FilterResultByAlbumID(string search, string sortOrder, int start, int length, List<string> columnFilters, DTParameters param, int AlbumID)
        {
            var basequery = DataLayer.DataLayerFacade.ProfileActivityRepository().GetProfileActivitiesAllAsQueryable().Where(m => m.AlbumID == AlbumID);

            //if (search.Length > 0)
            //{
            //    basequery = basequery.Where(p => (p.AlbumID.ToLower().Contains(search.ToLower())));
            //}

            var queryList = RepositoryHelper.PaginationDatatables<ProfileActivity>(start, length, basequery).Future();
            var queryTotal = basequery.FutureCount();

            var resultList = Mapper.Map<List<ProfileActivityDTO>>(queryList.ToList());
            int intTotal = queryTotal.Value;

            DTResult<ProfileActivityDTO> result = new DTResult<ProfileActivityDTO>
            {
                draw = param.Draw,
                recordsFiltered = intTotal,
                recordsTotal = intTotal,
                data = resultList
            };
            return result;
        }


        public PaginationSet<ProfileActivityDTO> GetProfileActivitiesByAlbumID(int AlbumID, int page, int pagesize)
        {

            var basequery = DataLayer.DataLayerFacade.ProfileActivityRepository().GetProfileActivitiesByAlbumIDAsQueryable(AlbumID);

            var queryList = DataLayer.Repository.RepositoryHelper.Pagination<ProfileActivity>(page, pagesize, basequery).Future();
            var queryTotal = basequery.FutureCount();

            int intTotal = queryTotal.Value;

            PaginationSet<ProfileActivityDTO> result = new PaginationSet<ProfileActivityDTO>()
            {
                Page = page,
                TotalCount = intTotal,
                TotalPages = (int)Math.Ceiling((decimal)intTotal / pagesize),
                Items = Mapper.Map<List<ProfileActivityDTO>>(queryList.ToList())
            };

            return result;
        }
        public DTResult<ProfileActivityDTO> FilterResultByActivityTypeID(string search, string sortOrder, int start, int length, List<string> columnFilters, DTParameters param, int ActivityTypeID)
        {
            var basequery = DataLayer.DataLayerFacade.ProfileActivityRepository().GetProfileActivitiesAllAsQueryable().Where(m => m.ActivityTypeID == ActivityTypeID);

            //if (search.Length > 0)
            //{
            //    basequery = basequery.Where(p => (p.AlbumID.ToLower().Contains(search.ToLower())));
            //}

            var queryList = RepositoryHelper.PaginationDatatables<ProfileActivity>(start, length, basequery).Future();
            var queryTotal = basequery.FutureCount();

            var resultList = Mapper.Map<List<ProfileActivityDTO>>(queryList.ToList());
            int intTotal = queryTotal.Value;

            DTResult<ProfileActivityDTO> result = new DTResult<ProfileActivityDTO>
            {
                draw = param.Draw,
                recordsFiltered = intTotal,
                recordsTotal = intTotal,
                data = resultList
            };
            return result;
        }


        public PaginationSet<ProfileActivityDTO> GetProfileActivitiesByActivityTypeID(int ActivityTypeID, int page, int pagesize)
        {

            var basequery = DataLayer.DataLayerFacade.ProfileActivityRepository().GetProfileActivitiesByActivityTypeIDAsQueryable(ActivityTypeID);

            var queryList = DataLayer.Repository.RepositoryHelper.Pagination<ProfileActivity>(page, pagesize, basequery).Future();
            var queryTotal = basequery.FutureCount();

            int intTotal = queryTotal.Value;

            PaginationSet<ProfileActivityDTO> result = new PaginationSet<ProfileActivityDTO>()
            {
                Page = page,
                TotalCount = intTotal,
                TotalPages = (int)Math.Ceiling((decimal)intTotal / pagesize),
                Items = Mapper.Map<List<ProfileActivityDTO>>(queryList.ToList())
            };

            return result;
        }

        public ProfileActivityDTO Insert(Business.DataTransferObjects.Mobile.V2.ProfileActivityUpdateMobileModel objProfileActivity)
        {
            ProfileActivityDTO result = null;
            if (objProfileActivity.Share != "")
            {
                result = Mapper.Map<ProfileActivityDTO>(DataLayer.DataLayerFacade.ProfileActivityRepository().Insert(objProfileActivity.ProfileID, objProfileActivity.AlbumID, objProfileActivity.ActivityTypeID, objProfileActivity.ProfileActivityCount, objProfileActivity.ProfileActivityIsFirst, objProfileActivity.Share));
            }
            else if (objProfileActivity.Location != "")
            {
                result = Mapper.Map<ProfileActivityDTO>(DataLayer.DataLayerFacade.ProfileActivityRepository().Insert(objProfileActivity.ProfileID, objProfileActivity.AlbumID, objProfileActivity.ActivityTypeID, objProfileActivity.ProfileActivityCount, objProfileActivity.ProfileActivityIsFirst, DbGeography.FromText(objProfileActivity.Location)));
            }
            else if (objProfileActivity.ContentLikes != null)
            {
                //Update Likes
                //Update Like Album
            }
            else
            {
                result = Mapper.Map<ProfileActivityDTO>(DataLayer.DataLayerFacade.ProfileActivityRepository().Insert(objProfileActivity.ProfileID, objProfileActivity.AlbumID, objProfileActivity.ActivityTypeID, objProfileActivity.ProfileActivityCount, objProfileActivity.ProfileActivityIsFirst));
            }
            
            if (result != null)
            {
                return result;
            }
            else
            {
                return new ProfileActivityDTO();
            }
        }

        public ProfileActivityDTO Insert(ProfileActivityDTO objProfileActivity)
        {
            ProfileActivityDTO result = null;
            if (objProfileActivity.Share != null)
            {
                result = Mapper.Map<ProfileActivityDTO>(DataLayer.DataLayerFacade.ProfileActivityRepository().Insert(objProfileActivity.ProfileID, objProfileActivity.AlbumID, objProfileActivity.ActivityTypeID, objProfileActivity.ProfileActivityCount, objProfileActivity.ProfileActivityIsFirst, objProfileActivity.Share.ProfileActivityShare));
            }
            else if (objProfileActivity.Location != null)
            {
                result = Mapper.Map<ProfileActivityDTO>(DataLayer.DataLayerFacade.ProfileActivityRepository().Insert(objProfileActivity.ProfileID, objProfileActivity.AlbumID, objProfileActivity.ActivityTypeID, objProfileActivity.ProfileActivityCount, objProfileActivity.ProfileActivityIsFirst, DbGeography.FromText(objProfileActivity.Location.Location)));
            }
            else
            {
                result = Mapper.Map<ProfileActivityDTO>(DataLayer.DataLayerFacade.ProfileActivityRepository().Insert(objProfileActivity.ProfileID, objProfileActivity.AlbumID, objProfileActivity.ActivityTypeID, objProfileActivity.ProfileActivityCount, objProfileActivity.ProfileActivityIsFirst));
            }

            if (result != null)
            {
                return result;
            }
            else
            {
                return new ProfileActivityDTO();
            }
        }

        public bool Delete(int ProfileID, int AlbumID)
        {
            bool result = DataLayer.DataLayerFacade.ProfileActivityRepository().Delete(ProfileID, AlbumID);
            if (result)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Update(int ProfileID, int AlbumID, ProfileActivityDTO objProfileActivity)
        {
            bool result = DataLayer.DataLayerFacade.ProfileActivityRepository().Update(objProfileActivity.ProfileID, objProfileActivity.AlbumID, objProfileActivity.ActivityTypeID, objProfileActivity.ProfileActivityCount, objProfileActivity.ProfileActivityIsFirst);
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