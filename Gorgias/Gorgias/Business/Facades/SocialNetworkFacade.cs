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
    public class SocialNetworkFacade
    {
        public SocialNetworkDTO GetSocialNetwork(int SocialNetworkID)
        {
            SocialNetworkDTO result = Mapper.Map<SocialNetworkDTO>(DataLayer.DataLayerFacade.SocialNetworkRepository().GetSocialNetwork(SocialNetworkID));
            return result;
        }

        public DTResult<SocialNetworkDTO> FilterResult(string search, string sortOrder, int start, int length, List<string> columnFilters, DTParameters param)
        {
            var basequery = DataLayer.DataLayerFacade.SocialNetworkRepository().GetSocialNetworksAllAsQueryable();

            if (search.Length > 0)
            {
                basequery = basequery.Where(p => (p.SocialNetworkName.ToLower().Contains(search.ToLower())));
            }

            var queryList = RepositoryHelper.PaginationDatatables<SocialNetwork>(start, length, basequery).Future();
            var queryTotal = basequery.FutureCount();

            var resultList = Mapper.Map<List<SocialNetworkDTO>>(queryList.ToList());
            int intTotal = queryTotal.Value;

            DTResult<SocialNetworkDTO> result = new DTResult<SocialNetworkDTO>
            {
                draw = param.Draw,
                recordsFiltered = intTotal,
                recordsTotal = intTotal,
                data = resultList
            };
            return result;
        }

        public PaginationSet<SocialNetworkDTO> GetSocialNetworks(int page, int pagesize)
        {
            var basequery = DataLayer.DataLayerFacade.SocialNetworkRepository().GetSocialNetworksAllAsQueryable();

            var queryList = DataLayer.Repository.RepositoryHelper.Pagination<SocialNetwork>(page, pagesize, basequery).Future();
            var queryTotal = basequery.FutureCount();

            int intTotal = queryTotal.Value;

            PaginationSet<SocialNetworkDTO> result = new PaginationSet<SocialNetworkDTO>()
            {
                Page = page,
                TotalCount = intTotal,
                TotalPages = (int)Math.Ceiling((decimal)intTotal / pagesize),
                Items = Mapper.Map<List<SocialNetworkDTO>>(queryList.ToList())
            };

            return result;
        }

        public List<SocialNetworkDTO> GetSocialNetworks()
        {
            var basequery = Mapper.Map<List<SocialNetworkDTO>>(DataLayer.DataLayerFacade.SocialNetworkRepository().GetSocialNetworksAllAsQueryable());
            return basequery.ToList();
        }



        public SocialNetworkDTO Insert(SocialNetworkDTO objSocialNetwork)
        {
            SocialNetworkDTO result = Mapper.Map<SocialNetworkDTO>(DataLayer.DataLayerFacade.SocialNetworkRepository().Insert(objSocialNetwork.SocialNetworkName, objSocialNetwork.SocialNetworkStatus, objSocialNetwork.SocialNetworkURL, objSocialNetwork.SocialNetworkImage, objSocialNetwork.SocialNetworkDescription));

            if (result != null)
            {
                return result;
            }
            else
            {
                return new SocialNetworkDTO();
            }
        }

        public bool Delete(int SocialNetworkID)
        {
            bool result = DataLayer.DataLayerFacade.SocialNetworkRepository().Delete(SocialNetworkID);
            if (result)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Update(int SocialNetworkID, SocialNetworkDTO objSocialNetwork)
        {
            bool result = DataLayer.DataLayerFacade.SocialNetworkRepository().Update(objSocialNetwork.SocialNetworkID, objSocialNetwork.SocialNetworkName, objSocialNetwork.SocialNetworkStatus, objSocialNetwork.SocialNetworkURL, objSocialNetwork.SocialNetworkImage, objSocialNetwork.SocialNetworkDescription);
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