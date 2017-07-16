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
    public class AddressFacade
    {   

        /// <summary>
        /// 
        /// </summary>
        /// <param name="AddressID"></param>
        /// <returns></returns>
                             
        public AddressDTO GetAddress(int AddressID)
        {
            AddressDTO result = Mapper.Map<AddressDTO>(DataLayer.DataLayerFacade.AddressRepository().GetAddress(AddressID));             
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="search"></param>
        /// <param name="sortOrder"></param>
        /// <param name="start"></param>
        /// <param name="length"></param>
        /// <param name="columnFilters"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public DTResult<AddressDTO> FilterResult(string search, string sortOrder, int start, int length, List<string> columnFilters, DTParameters param)
        {
            var basequery = DataLayer.DataLayerFacade.AddressRepository().GetAddressesAllAsQueryable();

            if (search.Length>0) {
                basequery = basequery.Where(p => (p.AddressName.ToLower().Contains(search.ToLower()) || p.Profile.ProfileFullname.ToLower().Contains(search.ToLower()) || p.AddressType.AddressTypeName.ToLower().Contains(search.ToLower())));
            }

            var queryList = RepositoryHelper.PaginationDatatables<Address>(start, length, basequery).Future();
            var queryTotal = basequery.FutureCount();

            var resultList = Mapper.Map<List<AddressDTO>>(queryList.ToList());
            int intTotal = queryTotal.Value;

            DTResult<AddressDTO> result = new DTResult<AddressDTO>
            {
                draw = param.Draw,
                recordsFiltered = intTotal,
                recordsTotal = intTotal,
                data = resultList
            };
            return result;
        }       

        public PaginationSet<AddressDTO> GetAddresses(int page, int pagesize)
        {           
            var basequery = DataLayer.DataLayerFacade.AddressRepository().GetAddressesAllAsQueryable();

            var queryList = DataLayer.Repository.RepositoryHelper.Pagination<Address>(page, pagesize, basequery).Future();
            var queryTotal = basequery.FutureCount();

            int intTotal = queryTotal.Value;

            PaginationSet<AddressDTO> result = new PaginationSet<AddressDTO>()
            {
                Page = page,
                TotalCount = intTotal,
                TotalPages = (int)Math.Ceiling((decimal)intTotal / pagesize),
                Items = Mapper.Map<List<AddressDTO>>(queryList.ToList())
            };

            return result;            
        }
        
        public List<AddressDTO> GetAddresses()
        {           
            var basequery = Mapper.Map <List<AddressDTO>>(DataLayer.DataLayerFacade.AddressRepository().GetAddressesAllAsQueryable());
            return basequery.ToList();
        }

        
        public DTResult<AddressDTO> FilterResultByCityID(string search, string sortOrder, int start, int length, List<string> columnFilters, DTParameters param, int CityID)
        {
            var basequery = DataLayer.DataLayerFacade.AddressRepository().GetAddressesAllAsQueryable().Where(m=> m.CityID==CityID);

            if (search.Length>0) {
                basequery = basequery.Where(p => (p.AddressName.ToLower().Contains(search.ToLower())));
            }

            var queryList = RepositoryHelper.PaginationDatatables<Address>(start, length, basequery).Future();
            var queryTotal = basequery.FutureCount();

            var resultList = Mapper.Map<List<AddressDTO>>(queryList.ToList());
            int intTotal = queryTotal.Value;

            DTResult<AddressDTO> result = new DTResult<AddressDTO>
            {
                draw = param.Draw,
                recordsFiltered = intTotal,
                recordsTotal = intTotal,
                data = resultList
            };
            return result;
        }               
        
        
        public PaginationSet<AddressDTO> GetAddressesByCityID(int CityID, int page, int pagesize)
        {
            
            var basequery = DataLayer.DataLayerFacade.AddressRepository().GetAddressesByCityIDAsQueryable(CityID);

            var queryList = DataLayer.Repository.RepositoryHelper.Pagination<Address>(page, pagesize, basequery).Future();
            var queryTotal = basequery.FutureCount();

            int intTotal = queryTotal.Value;

            PaginationSet<AddressDTO> result = new PaginationSet<AddressDTO>()
            {
                Page = page,
                TotalCount = intTotal,
                TotalPages = (int)Math.Ceiling((decimal)intTotal / pagesize),
                Items = Mapper.Map<List<AddressDTO>>(queryList.ToList())
            };

            return result;            
        }
        public DTResult<AddressDTO> FilterResultByProfileID(string search, string sortOrder, int start, int length, List<string> columnFilters, DTParameters param, int ProfileID)
        {
            var basequery = DataLayer.DataLayerFacade.AddressRepository().GetAddressesAllAsQueryable().Where(m=> m.ProfileID==ProfileID);

            if (search.Length>0) {
                basequery = basequery.Where(p => (p.AddressName.ToLower().Contains(search.ToLower())));
            }

            var queryList = RepositoryHelper.PaginationDatatables<Address>(start, length, basequery).Future();
            var queryTotal = basequery.FutureCount();

            var resultList = Mapper.Map<List<AddressDTO>>(queryList.ToList());
            int intTotal = queryTotal.Value;

            DTResult<AddressDTO> result = new DTResult<AddressDTO>
            {
                draw = param.Draw,
                recordsFiltered = intTotal,
                recordsTotal = intTotal,
                data = resultList
            };
            return result;
        }               
        
        
        public PaginationSet<AddressDTO> GetAddressesByProfileID(int ProfileID, int page, int pagesize)
        {
            
            var basequery = DataLayer.DataLayerFacade.AddressRepository().GetAddressesByProfileIDAsQueryable(ProfileID);

            var queryList = DataLayer.Repository.RepositoryHelper.Pagination<Address>(page, pagesize, basequery).Future();
            var queryTotal = basequery.FutureCount();

            int intTotal = queryTotal.Value;

            PaginationSet<AddressDTO> result = new PaginationSet<AddressDTO>()
            {
                Page = page,
                TotalCount = intTotal,
                TotalPages = (int)Math.Ceiling((decimal)intTotal / pagesize),
                Items = Mapper.Map<List<AddressDTO>>(queryList.ToList())
            };

            return result;            
        }
        public DTResult<AddressDTO> FilterResultByAddressTypeID(string search, string sortOrder, int start, int length, List<string> columnFilters, DTParameters param, int AddressTypeID)
        {
            var basequery = DataLayer.DataLayerFacade.AddressRepository().GetAddressesAllAsQueryable().Where(m=> m.AddressTypeID==AddressTypeID);

            if (search.Length>0) {
                basequery = basequery.Where(p => (p.AddressName.ToLower().Contains(search.ToLower())));
            }

            var queryList = RepositoryHelper.PaginationDatatables<Address>(start, length, basequery).Future();
            var queryTotal = basequery.FutureCount();

            var resultList = Mapper.Map<List<AddressDTO>>(queryList.ToList());
            int intTotal = queryTotal.Value;

            DTResult<AddressDTO> result = new DTResult<AddressDTO>
            {
                draw = param.Draw,
                recordsFiltered = intTotal,
                recordsTotal = intTotal,
                data = resultList
            };
            return result;
        }               
        
        
        public PaginationSet<AddressDTO> GetAddressesByAddressTypeID(int AddressTypeID, int page, int pagesize)
        {
            
            var basequery = DataLayer.DataLayerFacade.AddressRepository().GetAddressesByAddressTypeIDAsQueryable(AddressTypeID);

            var queryList = DataLayer.Repository.RepositoryHelper.Pagination<Address>(page, pagesize, basequery).Future();
            var queryTotal = basequery.FutureCount();

            int intTotal = queryTotal.Value;

            PaginationSet<AddressDTO> result = new PaginationSet<AddressDTO>()
            {
                Page = page,
                TotalCount = intTotal,
                TotalPages = (int)Math.Ceiling((decimal)intTotal / pagesize),
                Items = Mapper.Map<List<AddressDTO>>(queryList.ToList())
            };

            return result;            
        }

        public AddressDTO Insert(AddressDTO objAddress)
        {
            string[] resultLocation = objAddress.AddressStringLocation.Split('#');
            var point = string.Format("POINT({1} {0})", double.Parse(resultLocation[0]), double.Parse(resultLocation[1]));
            AddressDTO result = Mapper.Map<AddressDTO>(DataLayer.DataLayerFacade.AddressRepository().Insert(objAddress.AddressName, objAddress.AddressStatus, objAddress.AddressTel, objAddress.AddressFax, objAddress.AddressZipCode, objAddress.AddressAddress, objAddress.AddressEmail, objAddress.AddressImage, objAddress.CityID, (Int32) objAddress.ProfileID, objAddress.AddressTypeID, DbGeography.FromText(point)));
            if (result!=null)
            {
                return result;            
            }
            else
            {
                return new AddressDTO();
            }
        }
               
        public bool Delete(int AddressID)
        {            
            bool result = DataLayer.DataLayerFacade.AddressRepository().Delete(AddressID);
            if (result)
            {
                return true;            
            }
            else
            {
                return false;
            }
        }

        public bool Update(int AddressID, AddressDTO objAddress)
        {
            string[] resultLocation = objAddress.AddressStringLocation.Split('#');
            var point = string.Format("POINT({1} {0})", double.Parse(resultLocation[0]), double.Parse(resultLocation[1]));
            bool result = DataLayer.DataLayerFacade.AddressRepository().Update(objAddress.AddressID, objAddress.AddressName, objAddress.AddressStatus, objAddress.AddressTel, objAddress.AddressFax, objAddress.AddressZipCode, objAddress.AddressAddress, objAddress.AddressEmail, objAddress.AddressImage, objAddress.CityID, objAddress.ProfileID, objAddress.AddressTypeID, DbGeography.FromText(point));
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