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
        private readonly IDbTransaction _currentTransaction;
        private bool _finished = false;

        public UnitOfWork(IDbTransaction dbTransaction)
        {
            _currentTransaction = dbTransaction;
        }

        public void Persist()
        {
            _currentTransaction.Commit();
            _finished = true;
        }

        public void Rollback()
        {
            _currentTransaction.Rollback();
            _finished = true;
        }

        public void Dispose()
        {
        }
    }
}
