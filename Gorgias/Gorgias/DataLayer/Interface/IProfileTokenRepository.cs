using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
namespace Gorgias.DataLayer.Interface
{
    public interface IProfileTokenRepository
    {
        ProfileToken Insert(String ProfileTokenString, DateTime ProfileTokenRegistration, int ProfileID);
        bool Update(int ProfileTokenID, String ProfileTokenString, DateTime ProfileTokenRegistration, int ProfileID);
        bool Delete(int ProfileTokenID);

        ProfileToken GetProfileToken(int ProfileTokenID);

        //List
        List<ProfileToken> GetProfileTokensAll();
        List<ProfileToken> GetProfileTokensAll(int page = 1, int pageSize = 7, string filter = null);

        List<ProfileToken> GetProfileTokensByProfileID(int ProfileID, int page = 1, int pageSize = 7, string filter = null);

        //IQueryable
        IQueryable<ProfileToken> GetProfileTokensAllAsQueryable();
        IQueryable<ProfileToken> GetProfileTokensAllAsQueryable(int page = 1, int pageSize = 7, string filter = null);
        IQueryable<ProfileToken> GetProfileTokensByProfileIDAsQueryable(int ProfileID);
        IQueryable<ProfileToken> GetProfileTokensByProfileIDAsQueryable(int ProfileID, int page = 1, int pageSize = 7, string filter = null);
    }
}


