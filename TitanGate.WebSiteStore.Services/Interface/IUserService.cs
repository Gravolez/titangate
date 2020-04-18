using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TitanGate.WebSiteStore.Entities.DB;

namespace TitanGate.WebSiteStore.Services
{
    public interface IUserService
    {
        Task<User> Login(string username, string password);
        Task<User> GetByUsername(string username);
    }
}
