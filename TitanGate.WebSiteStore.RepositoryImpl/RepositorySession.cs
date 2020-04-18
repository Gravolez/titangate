using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using TitanGate.WebSiteStore.Entities;
using TitanGate.WebSiteStore.Entities.Exceptions;
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
                return _dbTransaction;
            }
        }

        public IUnitOfWork BeginWork()
        {
            if (_dbTransaction != null)
            {
                throw new WebSiteStoreException("UnitOfWork canno be in another UnitOfWork");
            }

            _dbTransaction = Connection.BeginTransaction();
            return new UnitOfWork(Commit, Rollback);
        }

        public void Dispose()
        {
            _dbConnection.Close();
            _dbConnection.Dispose();
        }

        private void Commit()
        {
            _dbTransaction.Commit();
            _dbTransaction = null;
        }

        private void Rollback()
        {
            _dbTransaction.Rollback();
            _dbTransaction = null;
        }

        ~RepositorySession()
        {
            _dbConnection.Dispose();
        }
    }
}
