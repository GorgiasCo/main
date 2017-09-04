using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
namespace Gorgias.DataLayer.Interface
{   
        public interface ILanguageRepository
    {
    
    
        Language Insert(String LanguageName, String LanguageCode, Boolean LanguageStatus);
        bool Update(int LanguageID, String LanguageName, String LanguageCode, Boolean LanguageStatus);        
        bool Delete(int LanguageID);

        Language GetLanguage(int LanguageID);

        //List
        List<Language> GetLanguagesAll();
        List<Language> GetLanguagesAll(bool LanguageStatus);
        List<Language> GetLanguagesAll(int page = 1, int pageSize = 7, string filter=null);
        List<Language> GetLanguagesAll(bool LanguageStatus, int page = 1, int pageSize = 7, string filter=null);        
        
        
        //IQueryable
        IQueryable<Language> GetLanguagesAllAsQueryable();
        IQueryable<Language> GetLanguagesAllAsQueryable(bool LanguageStatus);
        IQueryable<Language> GetLanguagesAllAsQueryable(int page = 1, int pageSize = 7, string filter=null);
        IQueryable<Language> GetLanguagesAllAsQueryable(bool LanguageStatus, int page = 1, int pageSize = 7, string filter=null);   
    }
}


