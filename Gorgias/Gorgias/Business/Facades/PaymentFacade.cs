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
    public class PaymentFacade
    {
        public PaymentDTO GetPayment(int PaymentID)
        {
            PaymentDTO result = Mapper.Map<PaymentDTO>(DataLayer.DataLayerFacade.PaymentRepository().GetPayment(PaymentID));
            return result;
        }

        public DTResult<PaymentDTO> FilterResult(string search, string sortOrder, int start, int length, List<string> columnFilters, DTParameters param)
        {
            var basequery = DataLayer.DataLayerFacade.PaymentRepository().GetPaymentsAllAsQueryable();

            if (search.Length > 0)
            {
                basequery = basequery.Where(p => (p.PaymentDateCreated.Equals(search.ToLower())));
            }

            var queryList = RepositoryHelper.PaginationDatatables<Payment>(start, length, basequery).Future();
            var queryTotal = basequery.FutureCount();

            var resultList = Mapper.Map<List<PaymentDTO>>(queryList.ToList());
            int intTotal = queryTotal.Value;

            DTResult<PaymentDTO> result = new DTResult<PaymentDTO>
            {
                draw = param.Draw,
                recordsFiltered = intTotal,
                recordsTotal = intTotal,
                data = resultList
            };
            return result;
        }

        public PaginationSet<PaymentDTO> GetPayments(int page, int pagesize)
        {
            var basequery = DataLayer.DataLayerFacade.PaymentRepository().GetPaymentsAllAsQueryable();

            var queryList = DataLayer.Repository.RepositoryHelper.Pagination<Payment>(page, pagesize, basequery).Future();
            var queryTotal = basequery.FutureCount();

            int intTotal = queryTotal.Value;

            PaginationSet<PaymentDTO> result = new PaginationSet<PaymentDTO>()
            {
                Page = page,
                TotalCount = intTotal,
                TotalPages = (int)Math.Ceiling((decimal)intTotal / pagesize),
                Items = Mapper.Map<List<PaymentDTO>>(queryList.ToList())
            };

            return result;
        }

        public List<PaymentDTO> GetPayments()
        {
            var basequery = Mapper.Map<List<PaymentDTO>>(DataLayer.DataLayerFacade.PaymentRepository().GetPaymentsAllAsQueryable());
            return basequery.ToList();
        }


        public DTResult<PaymentDTO> FilterResultByProfileCommissionID(string search, string sortOrder, int start, int length, List<string> columnFilters, DTParameters param, int ProfileCommissionID)
        {
            var basequery = DataLayer.DataLayerFacade.PaymentRepository().GetPaymentsAllAsQueryable().Where(m => m.ProfileCommissionID == ProfileCommissionID);

            if (search.Length > 0)
            {
                basequery = basequery.Where(p => (p.PaymentDateCreated.Equals(search.ToLower())));
            }

            var queryList = RepositoryHelper.PaginationDatatables<Payment>(start, length, basequery).Future();
            var queryTotal = basequery.FutureCount();

            var resultList = Mapper.Map<List<PaymentDTO>>(queryList.ToList());
            int intTotal = queryTotal.Value;

            DTResult<PaymentDTO> result = new DTResult<PaymentDTO>
            {
                draw = param.Draw,
                recordsFiltered = intTotal,
                recordsTotal = intTotal,
                data = resultList
            };
            return result;
        }


        public PaginationSet<PaymentDTO> GetPaymentsByProfileCommissionID(int ProfileCommissionID, int page, int pagesize)
        {

            var basequery = DataLayer.DataLayerFacade.PaymentRepository().GetPaymentsByProfileCommissionIDAsQueryable(ProfileCommissionID);

            var queryList = DataLayer.Repository.RepositoryHelper.Pagination<Payment>(page, pagesize, basequery).Future();
            var queryTotal = basequery.FutureCount();

            int intTotal = queryTotal.Value;

            PaginationSet<PaymentDTO> result = new PaginationSet<PaymentDTO>()
            {
                Page = page,
                TotalCount = intTotal,
                TotalPages = (int)Math.Ceiling((decimal)intTotal / pagesize),
                Items = Mapper.Map<List<PaymentDTO>>(queryList.ToList())
            };

            return result;
        }

        public PaymentDTO Insert(PaymentDTO objPayment)
        {
            PaymentDTO result = Mapper.Map<PaymentDTO>(DataLayer.DataLayerFacade.PaymentRepository().Insert(objPayment.PaymentDateCreated, objPayment.PaymentDatePaid, objPayment.PaymentIsPaid, objPayment.PaymentAmount, objPayment.PaymentNote, objPayment.ProfileCommissionID));

            if (result != null)
            {
                return result;
            }
            else
            {
                return new PaymentDTO();
            }
        }

        public bool Delete(int PaymentID)
        {
            bool result = DataLayer.DataLayerFacade.PaymentRepository().Delete(PaymentID);
            if (result)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Update(int PaymentID, PaymentDTO objPayment)
        {
            bool result = DataLayer.DataLayerFacade.PaymentRepository().Update(objPayment.PaymentID, objPayment.PaymentDateCreated, objPayment.PaymentDatePaid, objPayment.PaymentIsPaid, objPayment.PaymentAmount, objPayment.PaymentNote, objPayment.ProfileCommissionID);
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