using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
namespace Gorgias.DataLayer.Interface
{   
        public interface IPaymentRepository
    {
    
    
        Payment Insert(DateTime PaymentDateCreated, DateTime PaymentDatePaid, bool PaymentIsPaid, double PaymentAmount, string PaymentNote, int ProfileCommissionID);
        bool Update(int PaymentID, DateTime PaymentDateCreated, DateTime PaymentDatePaid, bool PaymentIsPaid, double PaymentAmount, string PaymentNote, int ProfileCommissionID);        
        bool Delete(int PaymentID);

        Payment GetPayment(int PaymentID);

        //List
        List<Payment> GetPaymentsAll();
        List<Payment> GetPaymentsAll(int page = 1, int pageSize = 7, string filter=null);
        
        List<Payment> GetPaymentsByProfileCommissionID(int ProfileCommissionID, int page = 1, int pageSize = 7, string filter=null);       
        
        //IQueryable
        IQueryable<Payment> GetPaymentsAllAsQueryable();
        IQueryable<Payment> GetPaymentsAllAsQueryable(int page = 1, int pageSize = 7, string filter=null);
        IQueryable<Payment> GetPaymentsByProfileCommissionIDAsQueryable(int ProfileCommissionID);
        IQueryable<Payment> GetPaymentsByProfileCommissionIDAsQueryable(int ProfileCommissionID, int page = 1, int pageSize = 7, string filter=null);       
    }
}


