using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Gorgias;
using Gorgias.DataLayer.Interface;
using Gorgias.Infrastruture.EntityFramework;
using System.Linq;
namespace Gorgias.DataLayer.Repository.SQL
{   
	public class PaymentRepository : IPaymentRepository, IDisposable
	{
     	// To detect redundant calls
		private bool disposedValue = false;

		private GorgiasEntities context = new GorgiasEntities();

		// IDisposable
		protected virtual void Dispose(bool disposing)
		{
			if (!this.disposedValue) {
				if (!this.disposedValue) {
					if (disposing) {
						context.Dispose();
					}
				}
				this.disposedValue = true;
			}
			this.disposedValue = true;
		}

		#region " IDisposable Support "
		// This code added by Visual Basic to correctly implement the disposable pattern.
		public void Dispose()
		{
			// Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
			Dispose(true);
			GC.SuppressFinalize(this);
		}
		#endregion
        
        //CRUD Functions
		public Payment Insert(DateTime PaymentDateCreated, DateTime PaymentDatePaid, Boolean PaymentIsPaid, double PaymentAmount, String PaymentNote, int ProfileCommissionID)
		{
            try{
                Payment obj = new Payment();	
                
                
            		obj.PaymentDateCreated = PaymentDateCreated;			
            		obj.PaymentDatePaid = PaymentDatePaid;			
            		obj.PaymentIsPaid = PaymentIsPaid;			
            		obj.PaymentAmount = PaymentAmount;			
            		obj.PaymentNote = PaymentNote;			
            		obj.ProfileCommissionID = ProfileCommissionID;			
    			context.Payments.Add(obj);
    			context.SaveChanges();
                return obj;
            }
            catch(Exception ex){
                return new Payment();
            }
		}

		public bool Update(int PaymentID, DateTime PaymentDateCreated, DateTime PaymentDatePaid, Boolean PaymentIsPaid, double PaymentAmount, String PaymentNote, int ProfileCommissionID)
		{
		    Payment obj = new Payment();
            obj = (from w in context.Payments where w.PaymentID == PaymentID  select w).FirstOrDefault();
			if (obj != null)
            {
                context.Payments.Attach(obj);
	
        		obj.PaymentDateCreated = PaymentDateCreated;			
        		obj.PaymentDatePaid = PaymentDatePaid;			
        		obj.PaymentIsPaid = PaymentIsPaid;			
        		obj.PaymentAmount = PaymentAmount;			
        		obj.PaymentNote = PaymentNote;			
        		obj.ProfileCommissionID = ProfileCommissionID;			
			context.SaveChanges();
                return true;
            } else {
                return false;
            }
		}

		public bool Delete(int PaymentID)
		{
			Payment obj = new Payment();
			obj = (from w in context.Payments where  w.PaymentID == PaymentID  select w).FirstOrDefault();
			if (obj != null)
            {
                context.Payments.Attach(obj);
			    context.Payments.Remove(obj);
			    context.SaveChanges();
                return true;
            } else {
                return false;
            }
		}

		public Payment GetPayment(int PaymentID)
		{
			return (from w in context.Payments where  w.PaymentID == PaymentID  select w).FirstOrDefault();
		}

        //Lists
		public List<Payment> GetPaymentsAll()
		{
			return (from w in context.Payments orderby w.PaymentID descending select w).ToList();
		}
        //List Pagings
        public List<Payment> GetPaymentsAll(int page = 1, int pageSize = 7, string filter=null)
		{
            var xList = new List<Payment>();
            if (filter != null)
            {
                xList = (from w in context.Payments orderby w.PaymentID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).ToList();                
            }
            else {
                xList = (from w in context.Payments orderby w.PaymentID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();                
            }
            return xList;     
		}
        //IQueryable
		public IQueryable<Payment> GetPaymentsAllAsQueryable()
		{
			return (from w in context.Payments orderby w.PaymentID descending select w).AsQueryable();
		}
        //IQueryable Pagings
        public IQueryable<Payment> GetPaymentsAllAsQueryable(int page = 1, int pageSize = 7, string filter=null)
		{
            IQueryable<Payment> xList;
            if (filter != null)
            {
                //In Case of Performance, Use following method ;)
                //xList = (from w in context.Payments orderby w.PaymentID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();                
                xList= context.Payments.ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
            }
            else {
                //In Case of Performance, Use following method ;)
                //xList = (from w in context.Payments orderby w.PaymentID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();                
                xList= context.Payments.Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
            }
            return xList;     
		}
        //Relationship Functions based on Foreign Key
        //Relationship List
        public List<Payment> GetPaymentsByProfileCommissionID(int ProfileCommissionID, int page = 1, int pageSize = 7, string filter=null)
        {
            return (from w in context.Payments where w.ProfileCommissionID == ProfileCommissionID orderby w.PaymentID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
        }
        public IQueryable<Payment> GetPaymentsByProfileCommissionIDAsQueryable(int ProfileCommissionID)
		{
			return (from w in context.Payments where w.ProfileCommissionID == ProfileCommissionID orderby w.PaymentID descending select w).AsQueryable();
		}
        public IQueryable<Payment> GetPaymentsByProfileCommissionIDAsQueryable(int ProfileCommissionID, int page = 1, int pageSize = 7, string filter=null)
        {
            return (from w in context.Payments where w.ProfileCommissionID == ProfileCommissionID orderby w.PaymentID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
        }

	}
}