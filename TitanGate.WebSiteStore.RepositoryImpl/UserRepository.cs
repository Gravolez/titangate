using Dapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TitanGate.WebSiteStore.Entities.DB;
using TitanGate.WebSiteStore.Repository;

namespace TitanGate.WebSiteStore.DapperRepository
{
    public class UserRepository : IUserRepository
    {
        private readonly RepositorySession _repositorySession;

        public UserRepository(IRepositorySession repositorySession)
        {
            _repositorySession = (RepositorySession)repositorySession;
        }

        public async Task<User> FindByLogin(string username)
        {
            return await _repositorySession.Connection.QueryFirstOrDefaultAsync<User>(
                @"SELECT TOP 1 u.* 
                            FROM dbo.[User] u 
                            WHERE u.Username = @Username",
                param: new { Username = username },
                transaction: _repositorySession.CurrentTransaction);
        }
    }
}
