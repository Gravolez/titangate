using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using TitanGate.WebSiteStore.Entities;

namespace TitanGate.WebSiteStore.DapperRepository
{
    public class SqlConnectionStore : ISqlConnectionStore
    {
        private readonly AppSettings _appSettings;
        private IDbConnection _dbConnection;

        public SqlConnectionStore(IOptions<AppSettings> settings)
        {
            _appSettings = settings.Value;
        }

        public IDbConnection Connection
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

        public void Dispose()
        {
            _dbConnection.Dispose();
        }

        ~SqlConnectionStore()
        {
            _dbConnection.Dispose();
        }
    }
}
