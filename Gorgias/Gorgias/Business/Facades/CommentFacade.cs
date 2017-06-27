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
    public class CommentFacade
    {
        public CommentDTO GetComment(int CommentID)
        {
            CommentDTO result = Mapper.Map<CommentDTO>(DataLayer.DataLayerFacade.CommentRepository().GetComment(CommentID));
            return result;
        }

        public DTResult<CommentDTO> FilterResult(string search, string sortOrder, int start, int length, List<string> columnFilters, DTParameters param)
        {
            var basequery = DataLayer.DataLayerFacade.CommentRepository().GetCommentsAllAsQueryable();

            if (search.Length > 0)
            {
                basequery = basequery.Where(p => (p.CommentNote.ToLower().Contains(search.ToLower())));
            }

            var queryList = RepositoryHelper.PaginationDatatables<Comment>(start, length, basequery).Future();
            var queryTotal = basequery.FutureCount();

            var resultList = Mapper.Map<List<CommentDTO>>(queryList.ToList());
            int intTotal = queryTotal.Value;

            DTResult<CommentDTO> result = new DTResult<CommentDTO>
            {
                draw = param.Draw,
                recordsFiltered = intTotal,
                recordsTotal = intTotal,
                data = resultList
            };
            return result;
        }

        public PaginationSet<CommentDTO> GetComments(int page, int pagesize)
        {
            var basequery = DataLayer.DataLayerFacade.CommentRepository().GetCommentsAllAsQueryable();

            var queryList = DataLayer.Repository.RepositoryHelper.Pagination<Comment>(page, pagesize, basequery).Future();
            var queryTotal = basequery.FutureCount();

            int intTotal = queryTotal.Value;

            PaginationSet<CommentDTO> result = new PaginationSet<CommentDTO>()
            {
                Page = page,
                TotalCount = intTotal,
                TotalPages = (int)Math.Ceiling((decimal)intTotal / pagesize),
                Items = Mapper.Map<List<CommentDTO>>(queryList.ToList())
            };

            return result;
        }

        public List<CommentDTO> GetComments()
        {
            var basequery = Mapper.Map<List<CommentDTO>>(DataLayer.DataLayerFacade.CommentRepository().GetCommentsAllAsQueryable());
            return basequery.ToList();
        }


        public DTResult<CommentDTO> FilterResultByProfileID(string search, string sortOrder, int start, int length, List<string> columnFilters, DTParameters param, int ProfileID)
        {
            var basequery = DataLayer.DataLayerFacade.CommentRepository().GetCommentsAllAsQueryable().Where(m => m.ProfileID == ProfileID);

            if (search.Length > 0)
            {
                basequery = basequery.Where(p => (p.CommentNote.ToLower().Contains(search.ToLower())));
            }

            var queryList = RepositoryHelper.PaginationDatatables<Comment>(start, length, basequery).Future();
            var queryTotal = basequery.FutureCount();

            var resultList = Mapper.Map<List<CommentDTO>>(queryList.ToList());
            int intTotal = queryTotal.Value;

            DTResult<CommentDTO> result = new DTResult<CommentDTO>
            {
                draw = param.Draw,
                recordsFiltered = intTotal,
                recordsTotal = intTotal,
                data = resultList
            };
            return result;
        }


        public PaginationSet<CommentDTO> GetCommentsByProfileID(int ProfileID, int page, int pagesize)
        {

            var basequery = DataLayer.DataLayerFacade.CommentRepository().GetCommentsByProfileIDAsQueryable(ProfileID);

            var queryList = DataLayer.Repository.RepositoryHelper.Pagination<Comment>(page, pagesize, basequery).Future();
            var queryTotal = basequery.FutureCount();

            int intTotal = queryTotal.Value;

            PaginationSet<CommentDTO> result = new PaginationSet<CommentDTO>()
            {
                Page = page,
                TotalCount = intTotal,
                TotalPages = (int)Math.Ceiling((decimal)intTotal / pagesize),
                Items = Mapper.Map<List<CommentDTO>>(queryList.ToList())
            };

            return result;
        }
        public DTResult<CommentDTO> FilterResultByContentID(string search, string sortOrder, int start, int length, List<string> columnFilters, DTParameters param, int ContentID)
        {
            var basequery = DataLayer.DataLayerFacade.CommentRepository().GetCommentsAllAsQueryable().Where(m => m.ContentID == ContentID);

            if (search.Length > 0)
            {
                basequery = basequery.Where(p => (p.CommentNote.ToLower().Contains(search.ToLower())));
            }

            var queryList = RepositoryHelper.PaginationDatatables<Comment>(start, length, basequery).Future();
            var queryTotal = basequery.FutureCount();

            var resultList = Mapper.Map<List<CommentDTO>>(queryList.ToList());
            int intTotal = queryTotal.Value;

            DTResult<CommentDTO> result = new DTResult<CommentDTO>
            {
                draw = param.Draw,
                recordsFiltered = intTotal,
                recordsTotal = intTotal,
                data = resultList
            };
            return result;
        }


        public PaginationSet<CommentDTO> GetCommentsByContentID(int ContentID, int page, int pagesize)
        {

            var basequery = DataLayer.DataLayerFacade.CommentRepository().GetCommentsByContentIDAsQueryable(ContentID);

            var queryList = DataLayer.Repository.RepositoryHelper.Pagination<Comment>(page, pagesize, basequery).Future();
            var queryTotal = basequery.FutureCount();

            int intTotal = queryTotal.Value;

            PaginationSet<CommentDTO> result = new PaginationSet<CommentDTO>()
            {
                Page = page,
                TotalCount = intTotal,
                TotalPages = (int)Math.Ceiling((decimal)intTotal / pagesize),
                Items = Mapper.Map<List<CommentDTO>>(queryList.ToList())
            };

            return result;
        }

        public PaginationSet<Business.DataTransferObjects.Mobile.CommentModel> GetMobileCommentsByContentID(int ContentID, int page, int pagesize)
        {

            var basequery = DataLayer.DataLayerFacade.CommentRepository().GetCommentsByContentIDForMobileAsQueryable(ContentID);

            var queryList = DataLayer.Repository.RepositoryHelper.Pagination<Business.DataTransferObjects.Mobile.CommentModel>(page, pagesize, basequery).Future();
            var queryTotal = basequery.FutureCount();

            int intTotal = queryTotal.Value;

            PaginationSet<Business.DataTransferObjects.Mobile.CommentModel> result = new PaginationSet<Business.DataTransferObjects.Mobile.CommentModel>()
            {
                Page = page,
                TotalCount = intTotal,
                TotalPages = (int)Math.Ceiling((decimal)intTotal / pagesize),
                Items = (List<Business.DataTransferObjects.Mobile.CommentModel>)(queryList.ToList())
            };

            return result;
        }

        public CommentDTO Insert(CommentDTO objComment)
        {
            CommentDTO result = Mapper.Map<CommentDTO>(DataLayer.DataLayerFacade.CommentRepository().Insert(objComment.CommentNote, objComment.CommentLike, objComment.CommentDateTime, objComment.CommentStatus, objComment.ProfileID, objComment.ContentID));

            if (result != null)
            {
                return result;
            }
            else
            {
                return new CommentDTO();
            }
        }

        public CommentDTO Insert(Business.DataTransferObjects.Mobile.CommentCustomModel objComment)
        {
            CommentDTO result = Mapper.Map<CommentDTO>(DataLayer.DataLayerFacade.CommentRepository().Insert(objComment.CommentNote, 0, DateTime.UtcNow, true, objComment.ProfileID, objComment.ContentID));

            if (result != null)
            {
                return result;
            }
            else
            {
                return new CommentDTO();
            }
        }

        public bool Delete(int CommentID)
        {
            bool result = DataLayer.DataLayerFacade.CommentRepository().Delete(CommentID);
            if (result)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Update(int CommentID, CommentDTO objComment)
        {
            bool result = DataLayer.DataLayerFacade.CommentRepository().Update(objComment.CommentID, objComment.CommentNote, objComment.CommentLike, objComment.CommentDateTime, objComment.CommentStatus, objComment.ProfileID, objComment.ContentID);
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