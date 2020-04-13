using Dapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TitanGate.WebSiteStore.Entities;
using TitanGate.WebSiteStore.Entities.DB;
using TitanGate.WebSiteStore.Repository;

namespace TitanGate.WebSiteStore.DapperRepository
{
    public class WebSiteRepository : IWebSiteRepository
    {
        private readonly ISqlConnectionStore _sqlConnectionStore;

        public WebSiteRepository(ISqlConnectionStore sqlConnectionStore)
        {
            _sqlConnectionStore = sqlConnectionStore;
        }

        public async Task<int> Create(WebSite webSite)
        {
            int newId = await _sqlConnectionStore.Connection.QuerySingleAsync<int>(
                @"INSERT INTO dbo.WebSite (Name, Url, CategoryId,) 
                OUTPUT INSERTED.[Id]
                VALUES (@Name, @Url, @CategoryId, @Email, @)", webSite);
            return newId;
        }

        public async Task Delete(int id)
        {
            await _sqlConnectionStore.Connection.ExecuteAsync(@"DELETE FROM dbo.WebSite WHERE Id = @Id", id);
        }

        public async Task<IEnumerable<WebSite>> FindAll()
        {
            return await _sqlConnectionStore.Connection.QueryAsync<WebSite>(@"SELECT * FROM dbo.WebSite");
        }

        public async Task<IEnumerable<WebSite>> FindByFilter(WebSiteSearchObject searchObject)
        {
            return await _sqlConnectionStore.Connection.QueryAsync<WebSite>(
                @"SELECT t.* 
                FROM (
                    SELECT *, ROW_NUMBER() as RowNumber
                    FROM dbo.WebSite ws
                        INNER JOIN dbo.Category c ON c.id = ws.CategoryId
                    ORDER BY @SortOrder
                ) AS t
                WHERE t.RowNumber BETWEEN @PageStart AND @PageEnd", 
                new { 
                    SortOrder = searchObject.SortExpression,
                    PageStart = 1 + ((searchObject.PageNumber - 1) * searchObject.PageSize),
                    PageEnd = searchObject.PageNumber * searchObject.PageSize
                });
        }

        public async Task<WebSite> FindById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task Update(WebSite webSite)
        {
            throw new NotImplementedException();
        }
    }
}
