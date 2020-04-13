using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace TitanGate.WebSiteStore.Repository
{
    public interface IUnitOfWork: IDisposable
    {
        IDbTransaction BeginTransaction();
    }
}
