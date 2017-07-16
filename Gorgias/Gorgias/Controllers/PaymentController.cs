using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Gorgias.Business.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using Gorgias.Infrastruture.Core;
using Gorgias.Business.DataTransferObjects.Helper;


namespace Gorgias.Controllers
{   
    [RoutePrefix("api")]
    public class PaymentController : ApiControllerBase
    {
        [Route("Payment/PaymentID/{PaymentID}", Name = "GetPaymentByID")]
        [HttpGet]
        public HttpResponseMessage GetPayment(HttpRequestMessage request, int PaymentID)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                PaymentDTO result = BusinessLayer.Facades.Facade.PaymentFacade().GetPayment(PaymentID); 
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<PaymentDTO>(HttpStatusCode.OK, result);
                }
                return response;
            });           
        }

        [Route("Payments/data", Name = "GetPaymentsDataTables")]
        [HttpPost]
        public DTResult<PaymentDTO> GetPayments(HttpRequestMessage request, DTParameters param)
        {
            List<string> columnSearch = new List<string>();

            foreach (var col in param.Columns)
            {
                columnSearch.Add(col.Search.Value);
            }

            DTResult<PaymentDTO> result = BusinessLayer.Facades.Facade.PaymentFacade().FilterResult(param.Search.Value, param.SortOrder, param.Start, param.Length, columnSearch, param);
            return result;
        }

        [Route("Payments", Name = "GetPaymentsAll")]
        [HttpGet]
        public HttpResponseMessage GetPayments(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<PaymentDTO> result = BusinessLayer.Facades.Facade.PaymentFacade().GetPayments();
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<List<PaymentDTO>>(HttpStatusCode.OK, result);
                }
                return response;
            });                        
        }
        
        [Route("Payments/{page}/{pagesize}", Name = "GetPayments")]
        [HttpGet]
        public HttpResponseMessage GetPayments(HttpRequestMessage request, int page, int pagesize)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                PaginationSet<PaymentDTO> result = BusinessLayer.Facades.Facade.PaymentFacade().GetPayments(page,pagesize);
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<PaginationSet<PaymentDTO>>(HttpStatusCode.OK, result);
                }
                return response;
            });                        
        }
        
        
        
        [Route("Payments/ProfileCommission/{ProfileCommissionID}/data", Name = "GetPaymentsDataTablesByProfileCommissionID}")]
        [HttpPost]
        public DTResult<PaymentDTO> GetPaymentsByProfileCommissionID(HttpRequestMessage request, int ProfileCommissionID, DTParameters param)
        {
            List<string> columnSearch = new List<string>();

            foreach (var col in param.Columns)
            {
                columnSearch.Add(col.Search.Value);
            }

            DTResult<PaymentDTO> result = BusinessLayer.Facades.Facade.PaymentFacade().FilterResultByProfileCommissionID(param.Search.Value, param.SortOrder, param.Start, param.Length, columnSearch, param, ProfileCommissionID);
            return result;
        }
            
        [Route("Payments/ProfileCommission/{ProfileCommissionID}/{page}/{pagesize}", Name = "GetPaymentsByProfileCommission")]
        [HttpGet]
        public HttpResponseMessage GetPaymentsByProfileCommissionID(HttpRequestMessage request, int ProfileCommissionID, int page, int pagesize)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                PaginationSet<PaymentDTO> result = BusinessLayer.Facades.Facade.PaymentFacade().GetPaymentsByProfileCommissionID(ProfileCommissionID,page,pagesize);
                
                if (result == null)
                {
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);                
                }
                else {
                    response = request.CreateResponse<PaginationSet<PaymentDTO>>(HttpStatusCode.OK, result);
                }
                return response;
            });                                     
        }
        
        [Route("Payment", Name = "PaymentInsert")]        
        [HttpPost]
        public HttpResponseMessage Post(HttpRequestMessage request, Business.DataTransferObjects.PaymentDTO objPayment)
        {
             return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                if (!ModelState.IsValid)
                {
                    response = request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    PaymentDTO result = BusinessLayer.Facades.Facade.PaymentFacade().Insert(objPayment);
                    if (result != null)
                    {
                        response = request.CreateResponse<Business.DataTransferObjects.PaymentDTO>(HttpStatusCode.Created, result);
                    }
                    else
                    {
                        response = request.CreateResponse<String>(HttpStatusCode.Found, null);
                    }
                }
                return response;
            });           
        }
        

        [Route("Payment/PaymentID/{PaymentID}", Name = "DeletePayment")]        
        public HttpResponseMessage Delete(HttpRequestMessage request, int PaymentID)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                if (!ModelState.IsValid)
                {
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, ModelState);
                }
                else
                {
                    bool result = BusinessLayer.Facades.Facade.PaymentFacade().Delete(PaymentID);
                    if (result)
                    {
                        response = request.CreateResponse<String>(HttpStatusCode.OK, "Done");
                    }
                    else
                    {
                        response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);
                    }                    
                }
                return response;
            });            
        }


        [Route("Payment/PaymentID/{PaymentID}", Name = "UpdatePayment")]
        [HttpPost]
        public HttpResponseMessage Update(HttpRequestMessage request, int PaymentID, PaymentDTO objPayment)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                if (!ModelState.IsValid)
                {
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, ModelState);
                }
                else
                {
                    bool result = BusinessLayer.Facades.Facade.PaymentFacade().Update(PaymentID,objPayment);
                    if (result)
                    {
                        response = request.CreateResponse<Business.DataTransferObjects.PaymentDTO>(HttpStatusCode.OK, objPayment);
                    }
                    else
                    {
                        response = request.CreateResponse<String>(HttpStatusCode.NotFound, null);
                    }                             
                }
                return response;
            });                        
        }
    }
}