using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;
using TitanGate.WebSiteStore.Entities;
using TitanGate.WebSiteStore.Repository;

namespace TitanGate.WebSiteStore.DapperRepository
{
    public class UnitOfWork : IUnitOfWork
    {
        private static int _transactionCount = 0;
        private readonly ISqlConnectionStore _sqlConnectionStore;

        public IWebSiteRepository WebSiteRepository { get; private set; }

        public UnitOfWork(ISqlConnectionStore sqlConnectionStore)
        {
            _sqlConnectionStore = sqlConnectionStore;
        }

        public IDbTransaction BeginTransaction()
        {
            _transactionCount++;
            return _sqlConnectionStore.Connection.BeginTransaction();
        }

        public void Dispose()
        {
            _transactionCount--;
            if (_transactionCount == 0)
            {
                _sqlConnectionStore.Connection.Close();
            }
        }
    }
}
