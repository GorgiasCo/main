using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Gorgias.Infrastruture.EntityFramework
{
    public static class MultipleResultSets
    {
        public static MultipleResultSetWrapper MultipleResults(this DbContext db, string storedProcedure, IList<SqlParameter> param = null)
        {
            return new MultipleResultSetWrapper(db, storedProcedure, param);
        }

        public class MultipleResultSetWrapper
        {
            private readonly DbContext _db;
            private readonly string _storedProcedure;
            public List<Func<IObjectContextAdapter, DbDataReader, IEnumerable>> _resultSets;
            private readonly IList<SqlParameter> _param;

            public MultipleResultSetWrapper(DbContext db, string storedProcedure)
            {
                _db = db;
                _storedProcedure = storedProcedure;
                _resultSets = new List<Func<IObjectContextAdapter, DbDataReader, IEnumerable>>();
            }

            public MultipleResultSetWrapper(DbContext db, string storedProcedure, IList<SqlParameter> param = null)
            {
                _db = db;
                _storedProcedure = storedProcedure;
                _resultSets = new List<Func<IObjectContextAdapter, DbDataReader, IEnumerable>>();
                _param = param;
            }

            public MultipleResultSetWrapper With<TResult>()
            {
                _resultSets.Add((adapter, reader) => adapter
                    .ObjectContext
                    .Translate<TResult>(reader)
                    .ToList());

                return this;
            }

            public List<IEnumerable> Execute()
            {
                var results = new List<IEnumerable>();

                using (var connection = _db.Database.Connection)
                {
                    connection.Open();

                    DbCommand command = new SqlCommand();
                    command.Connection = connection;
                    
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = _storedProcedure;
                    
                    //Add Parameters if any available ;)
                    if (_param != null) {
                        command.Parameters.AddRange(_param.ToArray());
                    }

                    using (var reader = command.ExecuteReader())
                    {
                        var adapter = ((IObjectContextAdapter)_db);

                        foreach (var resultSet in _resultSets)
                        {
                            results.Add(resultSet(adapter, reader));
                            reader.NextResult();
                        }
                    }
                    //_db.Database.Connection.Close();
                    return results;
                }
            }
        }
    }
}