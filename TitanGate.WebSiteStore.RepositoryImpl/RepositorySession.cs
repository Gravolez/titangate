using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using TitanGate.WebSiteStore.Entities;
using TitanGate.WebSiteStore.Repository;

namespace TitanGate.WebSiteStore.DapperRepository
{
    public class RepositorySession : IRepositorySession
    {
        private readonly AppSettings _appSettings;
        private IDbTransaction _dbTransaction;
        private IDbConnection _dbConnection;

        public RepositorySession(IOptions<AppSettings> settings)
        {
            _appSettings = settings.Value;
        }

        internal IDbConnection Connection
        {
            get
            {
                if (_dbConnection == null)
                {
                    _dbConnection = new SqlConnection(_appSettings.SqlConnectionString);
                    _dbConnection.Open();
                }

                return _dbConnection;
            }
        }

       internal IDbTransaction CurrentTransaction
        {
            get
            {
                if (_dbTransaction == null)
                {
                    _dbTransaction = Connection.BeginTransaction();
                }

                return _dbTransaction;
            }
        }

        public IUnitOfWork BeginWork()
        {
            return new UnitOfWork(CurrentTransaction);
        }

        public void Dispose()
        {
            _dbConnection.Close();
            _dbConnection.Dispose();
        }

        ~RepositorySession()
        {
            _dbConnection.Dispose();
        }
    }
}
