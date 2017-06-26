using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
namespace Gorgias.DataLayer.Interface
{   
        public interface IThemeRepository
    {
    
    
        Theme Insert(String ThemeName, String ThemeClassCode, Boolean ThemeStatus);
        bool Update(int ThemeID, String ThemeName, String ThemeClassCode, Boolean ThemeStatus);        
        bool Delete(int ThemeID);

        Theme GetTheme(int ThemeID);

        //List
        List<Theme> GetThemesAll();
        List<Theme> GetThemesAll(bool ThemeStatus);
        List<Theme> GetThemesAll(int page = 1, int pageSize = 7, string filter=null);
        List<Theme> GetThemesAll(bool ThemeStatus, int page = 1, int pageSize = 7, string filter=null);        
        
        
        //IQueryable
        IQueryable<Theme> GetThemesAllAsQueryable();
        IQueryable<Theme> GetThemesAllAsQueryable(bool ThemeStatus);
        IQueryable<Theme> GetThemesAllAsQueryable(int page = 1, int pageSize = 7, string filter=null);
        IQueryable<Theme> GetThemesAllAsQueryable(bool ThemeStatus, int page = 1, int pageSize = 7, string filter=null);   
    }
}


