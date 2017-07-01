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
    public class AlbumFacade
    {
        public AlbumDTO GetAlbum(int AlbumID)
        {
            AlbumDTO result = Mapper.Map<AlbumDTO>(DataLayer.DataLayerFacade.AlbumRepository().GetAlbum(AlbumID));
            return result;
        }

        public bool UpdateAlbumView(int AlbumID)
        {
            bool result = DataLayer.DataLayerFacade.AlbumRepository().Update(AlbumID);
            return result;
        }

        public DTResult<AlbumDTO> FilterResult(string search, string sortOrder, int start, int length, List<string> columnFilters, DTParameters param)
        {
            var basequery = DataLayer.DataLayerFacade.AlbumRepository().GetAlbumsAllAsQueryable();

            if (search.Length > 0)
            {
                basequery = basequery.Where(p => (p.Profile.ProfileFullname.ToLower().Contains(search.ToLower()) || p.AlbumName.ToLower().Contains(search.ToLower())));
            }

            var queryList = RepositoryHelper.PaginationDatatables<Album>(start, length, basequery).Future();
            var queryTotal = basequery.FutureCount();

            var resultList = Mapper.Map<List<AlbumDTO>>(queryList.ToList());
            int intTotal = queryTotal.Value;

            DTResult<AlbumDTO> result = new DTResult<AlbumDTO>
            {
                draw = param.Draw,
                recordsFiltered = intTotal,
                recordsTotal = intTotal,
                data = resultList
            };
            return result;
        }

        public PaginationSet<AlbumDTO> GetAlbums(int page, int pagesize)
        {
            var basequery = DataLayer.DataLayerFacade.AlbumRepository().GetAlbumsAllAsQueryable();

            var queryList = DataLayer.Repository.RepositoryHelper.Pagination<Album>(page, pagesize, basequery).Future();
            var queryTotal = basequery.FutureCount();

            int intTotal = queryTotal.Value;

            PaginationSet<AlbumDTO> result = new PaginationSet<AlbumDTO>()
            {
                Page = page,
                TotalCount = intTotal,
                TotalPages = (int)Math.Ceiling((decimal)intTotal / pagesize),
                Items = Mapper.Map<List<AlbumDTO>>(queryList.ToList())
            };

            return result;
        }

        public List<AlbumDTO> GetAlbums()
        {
            var basequery = DataLayer.DataLayerFacade.AlbumRepository().GetAlbumsAllAsQueryable().Take(200);
            return Mapper.Map<List<AlbumDTO>>(basequery.ToList());
        }

        public List<AlbumContentDTO> GetAlbumsForContents()
        {
            var basequery = DataLayer.DataLayerFacade.AlbumRepository().GetAlbumsAllForInsertContentAsQueryable().Take(1000);
            return Mapper.Map<List<AlbumContentDTO>>(basequery.ToList());
        }


        public DTResult<AlbumDTO> FilterResultByCategoryID(string search, string sortOrder, int start, int length, List<string> columnFilters, DTParameters param, int CategoryID)
        {
            var basequery = DataLayer.DataLayerFacade.AlbumRepository().GetAlbumsAllAsQueryable().Where(m => m.CategoryID == CategoryID);

            if (search.Length > 0)
            {
                basequery = basequery.Where(p => (p.AlbumName.ToLower().Contains(search.ToLower())));
            }

            var queryList = RepositoryHelper.PaginationDatatables<Album>(start, length, basequery).Future();
            var queryTotal = basequery.FutureCount();

            var resultList = Mapper.Map<List<AlbumDTO>>(queryList.ToList());
            int intTotal = queryTotal.Value;

            DTResult<AlbumDTO> result = new DTResult<AlbumDTO>
            {
                draw = param.Draw,
                recordsFiltered = intTotal,
                recordsTotal = intTotal,
                data = resultList
            };
            return result;
        }


        public PaginationSet<AlbumDTO> GetAlbumsByCategoryID(int CategoryID, int page, int pagesize)
        {

            var basequery = DataLayer.DataLayerFacade.AlbumRepository().GetAlbumsByCategoryIDAsQueryable(CategoryID);

            var queryList = DataLayer.Repository.RepositoryHelper.Pagination<Album>(page, pagesize, basequery).Future();
            var queryTotal = basequery.FutureCount();

            int intTotal = queryTotal.Value;

            PaginationSet<AlbumDTO> result = new PaginationSet<AlbumDTO>()
            {
                Page = page,
                TotalCount = intTotal,
                TotalPages = (int)Math.Ceiling((decimal)intTotal / pagesize),
                Items = Mapper.Map<List<AlbumDTO>>(queryList.ToList())
            };

            return result;
        }
        public DTResult<AlbumDTO> FilterResultByProfileID(string search, string sortOrder, int start, int length, List<string> columnFilters, DTParameters param, int ProfileID)
        {
            var basequery = DataLayer.DataLayerFacade.AlbumRepository().GetAlbumsAllAsQueryable().Where(m => m.ProfileID == ProfileID);

            if (search.Length > 0)
            {
                basequery = basequery.Where(p => (p.AlbumName.ToLower().Contains(search.ToLower())));
            }

            var queryList = RepositoryHelper.PaginationDatatables<Album>(start, length, basequery).Future();
            var queryTotal = basequery.FutureCount();

            var resultList = Mapper.Map<List<AlbumDTO>>(queryList.ToList());
            int intTotal = queryTotal.Value;

            DTResult<AlbumDTO> result = new DTResult<AlbumDTO>
            {
                draw = param.Draw,
                recordsFiltered = intTotal,
                recordsTotal = intTotal,
                data = resultList
            };
            return result;
        }


        public PaginationSet<AlbumDTO> GetAlbumsByProfileID(int ProfileID, int page, int pagesize)
        {

            var basequery = DataLayer.DataLayerFacade.AlbumRepository().GetAlbumsByProfileIDAsQueryable(ProfileID);

            var queryList = DataLayer.Repository.RepositoryHelper.Pagination<Album>(page, pagesize, basequery).Future();
            var queryTotal = basequery.FutureCount();

            int intTotal = queryTotal.Value;

            PaginationSet<AlbumDTO> result = new PaginationSet<AlbumDTO>()
            {
                Page = page,
                TotalCount = intTotal,
                TotalPages = (int)Math.Ceiling((decimal)intTotal / pagesize),
                Items = Mapper.Map<List<AlbumDTO>>(queryList.ToList())
            };

            return result;
        }

        public AlbumDTO Insert(AlbumDTO objAlbum)
        {
            AlbumDTO result = Mapper.Map<AlbumDTO>(DataLayer.DataLayerFacade.AlbumRepository().Insert(objAlbum.AlbumName, objAlbum.AlbumDateCreated, objAlbum.AlbumStatus, objAlbum.AlbumCover, objAlbum.AlbumIsDeleted, objAlbum.CategoryID, objAlbum.ProfileID));

            if (result != null)
            {
                return result;
            }
            else
            {
                return new AlbumDTO();
            }
        }

        public AlbumDTO InsertForMobile(AlbumDTO objAlbum)
        {
            AlbumDTO result = Mapper.Map<AlbumDTO>(DataLayer.DataLayerFacade.AlbumRepository().Insert(objAlbum.AlbumName, objAlbum.AlbumDateCreated, objAlbum.AlbumDatePublish, objAlbum.AlbumAvailability, true, objAlbum.AlbumCover, objAlbum.AlbumIsDeleted, objAlbum.CategoryID, objAlbum.ProfileID, objAlbum.AlbumHasComment));

            if (result != null)
            {
                return result;
            }
            else
            {
                return new AlbumDTO();
            }
        }

        public AlbumDTO InsertHottest(AlbumDTO objAlbum)
        {
            AlbumDTO result = Mapper.Map<AlbumDTO>(DataLayer.DataLayerFacade.AlbumRepository().Insert(objAlbum.AlbumName, objAlbum.AlbumDateCreated, objAlbum.AlbumDatePublish, objAlbum.AlbumAvailability, objAlbum.AlbumStatus, objAlbum.AlbumCover, objAlbum.AlbumIsDeleted, objAlbum.CategoryID, objAlbum.ProfileID, objAlbum.AlbumHasComment));

            if (result != null)
            {
                ContentDTO obj = new ContentDTO { AlbumID = result.AlbumID, ContentCreatedDate = DateTime.UtcNow, ContentIsDeleted = false, ContentStatus = false, ContentTitle = result.AlbumName, ContentType = 1, ContentURL = result.AlbumCover };
                Facade.ContentFacade().Insert(obj);
                return result;
            }
            else
            {
                return new AlbumDTO();
            }
        }

        public bool Delete(int AlbumID)
        {
            bool result = DataLayer.DataLayerFacade.AlbumRepository().Delete(AlbumID);
            if (result)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool UpdateHottest(int AlbumID, AlbumDTO objAlbum)
        {
            bool result = DataLayer.DataLayerFacade.AlbumRepository().Update(objAlbum.AlbumID, objAlbum.AlbumName, objAlbum.AlbumDateCreated, objAlbum.AlbumDatePublish, objAlbum.AlbumAvailability, objAlbum.AlbumStatus, objAlbum.AlbumCover, objAlbum.AlbumIsDeleted, objAlbum.CategoryID, objAlbum.ProfileID, objAlbum.AlbumHasComment);
            if (result)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Update(int AlbumID, AlbumDTO objAlbum)
        {
            bool result = DataLayer.DataLayerFacade.AlbumRepository().Update(objAlbum.AlbumID, objAlbum.AlbumName, objAlbum.AlbumDateCreated, objAlbum.AlbumStatus, objAlbum.AlbumCover, objAlbum.AlbumIsDeleted, objAlbum.CategoryID, objAlbum.ProfileID);
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