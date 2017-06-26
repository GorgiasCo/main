using Gorgias.Infrastruture.EntityFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Gorgias.DataLayer.Repository.SQL.Web
{
    public class WebRepository : IDisposable
    {
        // To detect redundant calls
        private bool disposedValue = false;

        private GorgiasEntities context = new GorgiasEntities();

        // IDisposable
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (!this.disposedValue)
                {
                    if (disposing)
                    {
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

        public void updateProfileLike(int ProfileID)
        {
            context.updateProfileLike(ProfileID);            
        }

        public List<IEnumerable> getStoredProcedure<T, Y, Z, A, B, C>(string name, IList<SqlParameter> param = null) where T : class
        {
            var results = context.MultipleResults(name, param).With<T>().With<Y>().With<Z>().With<A>().With<B>().With<C>().Execute();
            return results;
        }

        public List<IEnumerable> getStoredProcedure<T, Y, Z, A, B>(string name, IList<SqlParameter> param = null) where T : class
        {
            var results = context.MultipleResults(name, param).With<T>().With<Y>().With<Z>().With<A>().With<B>().Execute();
            return results;
        }

        public List<IEnumerable> getStoredProcedure<T, Y, Z>(string name, IList<SqlParameter> param = null) where T : class
        {
            var results = context.MultipleResults(name, param).With<T>().With<Y>().With<Z>().Execute();
            return results;
        }

        public List<IEnumerable> getStoredProcedure<T, Y, Z,L>(string name, IList<SqlParameter> param = null) where T : class
        {
            var results = context.MultipleResults(name, param).With<T>().With<Y>().With<Z>().With<L>().Execute();
            return results;
        }

        public List<IEnumerable> getStoredProcedure<T, Y, Z, A, B, C, D>(string name, IList<SqlParameter> param = null) where T : class
        {
            var results = context.MultipleResults(name, param).With<T>().With<Y>().With<Z>().With<A>().With<B>().With<C>().With<D>().Execute();
            return results;
        }

        public List<IEnumerable> getStoredProcedure<T, Y, Z, A, B, C, D, E>(string name, IList<SqlParameter> param = null) where T : class
        {
            var results = context.MultipleResults(name, param).With<T>().With<Y>().With<Z>().With<A>().With<B>().With<C>().With<D>().With<E>().Execute();
            return results;
        }

        public List<IEnumerable> getStoredProcedure<T, Y>(string name, IList<SqlParameter> param = null) where T : class
        {
            var results = context.MultipleResults(name, param).With<T>().With<Y>().Execute();
            return results;
        }

        public List<IEnumerable> getStoredProcedure<T>(string name, IList<SqlParameter> param = null) where T : class
        {
            var results = context.MultipleResults(name, param).With<T>().Execute();
            return results;
        }

        public List<IEnumerable> getStoredProcedure<T>(string name) where T : class
        {
            var results = context.MultipleResults(name).With<T>().Execute();
            return results;
        }

    }
}