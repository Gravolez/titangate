using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace TitanGate.WebSiteStore.DapperRepository
{
    public interface ISqlConnectionStore : IDisposable
    {
        IDbConnection Connection { get; }
    }
}
