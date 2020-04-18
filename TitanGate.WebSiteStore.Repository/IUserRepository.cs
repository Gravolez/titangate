using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TitanGate.WebSiteStore.Entities.DB;

namespace TitanGate.WebSiteStore.Repository
{
    public interface IUserRepository
    {
        Task<User> FindByLogin(string login);
    }
}
