using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
namespace Gorgias.DataLayer.Interface
{   
        public interface INewsletterRepository
    {
    
    
        Newsletter Insert(String NewsletterName, String NewsletterNote, Boolean NewsletterStatus);
        bool Update(int NewsletterID, String NewsletterName, String NewsletterNote, Boolean NewsletterStatus);        
        bool Delete(int NewsletterID);

        Newsletter GetNewsletter(int NewsletterID);
        Newsletter GetNewsletter(string NewsletterName);

        //List
        List<Newsletter> GetNewslettersAll();
        List<Newsletter> GetNewslettersAll(bool NewsletterStatus);
        List<Newsletter> GetNewslettersAll(int page = 1, int pageSize = 7, string filter=null);
        List<Newsletter> GetNewslettersAll(bool NewsletterStatus, int page = 1, int pageSize = 7, string filter=null);        
        
        
        //IQueryable
        IQueryable<Newsletter> GetNewslettersAllAsQueryable();
        IQueryable<Newsletter> GetNewslettersAllAsQueryable(bool NewsletterStatus);
        IQueryable<Newsletter> GetNewslettersAllAsQueryable(int page = 1, int pageSize = 7, string filter=null);
        IQueryable<Newsletter> GetNewslettersAllAsQueryable(bool NewsletterStatus, int page = 1, int pageSize = 7, string filter=null);   
    }
}


