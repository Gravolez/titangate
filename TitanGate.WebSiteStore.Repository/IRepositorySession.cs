using System;
using System.Collections.Generic;
using System.Text;

namespace TitanGate.WebSiteStore.Repository
{
    public interface IRepositorySession
    {
        IUnitOfWork BeginWork();
    }
}
