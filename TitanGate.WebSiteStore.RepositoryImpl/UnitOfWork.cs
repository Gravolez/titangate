using System;
using System.Data;
using TitanGate.WebSiteStore.Entities.Exceptions;
using TitanGate.WebSiteStore.Repository;

namespace TitanGate.WebSiteStore.DapperRepository
{
    public class UnitOfWork : IUnitOfWork
    {
        private Action _commitCallback;
        private Action _rollbackCallback;
        private bool _finished = false;

        public UnitOfWork(Action commitCallback, Action rollbackCallback)
        {
            _commitCallback = commitCallback;
            _rollbackCallback = rollbackCallback;
        }

        public void Persist()
        {
            _finished = true;
            _commitCallback();
        }

        public void Rollback()
        {
            _finished = true;
            _rollbackCallback();
        }

        public void Dispose()
        {
            if (!_finished)
            {
                _rollbackCallback();
            }
        }
    }
}
