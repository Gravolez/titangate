using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TitanGate.WebSiteStore.Entities;
using TitanGate.WebSiteStore.Entities.DB;
using TitanGate.WebSiteStore.Repository;

namespace TitanGate.WebSiteStore.DapperRepository
{
    public class WebSiteRepository : IWebSiteRepository
    {
        private readonly RepositorySession _repositorySession;

        public WebSiteRepository(IRepositorySession repositorySession)
        {
            _repositorySession = (RepositorySession)repositorySession;
        }

        public async Task<int> Create(WebSite webSite)
        {
            int newId = await _repositorySession.Connection.QuerySingleAsync<int>(
                @"INSERT INTO dbo.WebSite (Name, Url, CategoryId, Email, [Password]) 
                OUTPUT INSERTED.[Id]
                VALUES (@Name, @Url, @CategoryId, @Email, @Password)", 
                FlattenWebSite(webSite), 
                _repositorySession.CurrentTransaction);
            return newId;
        }

        public async Task<bool> Delete(int id)
        {
            int rows = await _repositorySession.Connection.ExecuteAsync(
                @"UPDATE dbo.WebSite SET IsDeleted = 1 WHERE Id = @Id", 
                new { Id = id },
                _repositorySession.CurrentTransaction);
            return rows > 0;
        }

        public async Task<IEnumerable<WebSite>> FindAll()
        {
            return await _repositorySession.Connection.QueryAsync<WebSite, WebSiteCategory, WebSite>(
                @"SELECT ws.*, c.* 
                FROM dbo.WebSite ws 
                    INNER JOIN dbo.Category c ON c.Id = ws.CategoryId
                WHERE ws.IsDeleted = 0",
                MapWebSite, 
                transaction: _repositorySession.CurrentTransaction);
        }

        public async Task<IEnumerable<WebSite>> FindByFilter(WebSiteSearchObject searchObject)
        {
            bool hasSort = searchObject.SortExpression.Count > 0;
            var sortOrder = "ORDER BY #.Id";
            if (hasSort)
            {
                var sorts = searchObject.SortExpression.Select(sort =>
                    "#." + Enum.GetName(typeof(SortColumn), sort.sortColumn) + (sort.sortOrder == SortOrder.Ascending ? " ASC" : " DESC")
                );
                sortOrder = "ORDER BY " + string.Join(',', sorts);
            } 

            return await _repositorySession.Connection.QueryAsync<WebSite, WebSiteCategory, WebSite>(
                $@"SELECT 
                    t.*, 
                    c.*
                FROM (
                    SELECT ws.*,
                        ROW_NUMBER() OVER (
                            {sortOrder.Replace("#", "ws")}
                        ) as RowNumber
                    FROM dbo.WebSite ws
                    WHERE ws.IsDeleted = 0
                ) AS t
                    INNER JOIN dbo.Category c ON c.id = t.CategoryId
                WHERE t.RowNumber BETWEEN @PageStart AND @PageEnd
                {sortOrder.Replace("#", "t")}", 
                MapWebSite,
                new { 
                    PageStart = 1 + ((searchObject.PageNumber - 1) * searchObject.PageSize),
                    PageEnd = searchObject.PageNumber * searchObject.PageSize
                },
                _repositorySession.CurrentTransaction);
        }

        public async Task<WebSite> FindById(int id)
        {
            IEnumerable<WebSite> result = await _repositorySession.Connection.QueryAsync<WebSite, WebSiteCategory, WebSite>(
                @"SELECT TOP 1 ws.*, c.* 
                FROM dbo.WebSite ws 
                    INNER JOIN dbo.Category c ON c.Id = ws.CategoryId
                WHERE ws.Id = @Id AND ws.IsDeleted = 0",
                MapWebSite,
                param: new { Id = id },
                transaction: _repositorySession.CurrentTransaction);
            return result.FirstOrDefault();
        }

        public async Task<bool> Update(WebSite webSite)
        {
            int rows = await _repositorySession.Connection.ExecuteAsync(
                @"UPDATE dbo.WebSite 
                SET Name = @Name, 
                    Url = @Url,
                    CategoryId = @CategoryId, 
                    Email = @Email, 
                    [Password] = @Password,
                    HasScreenshot = @HasScreenshot,
                    ScreenshotExt = @ScreenshotExt
                WHERE Id = @Id",
                FlattenWebSite(webSite),
                _repositorySession.CurrentTransaction);
            return rows > 0;
        }

        private WebSite MapWebSite(WebSite webSite, WebSiteCategory category)
        {
            webSite.Category = category;
            return webSite;
        }

        private object FlattenWebSite(WebSite website)
        {
            return new
            {
                Id = website.Id,
                Name = website.Name,
                Url = website.Url,
                CategoryId = website.Category.Id,
                Email = website.Email,
                Password = website.Password,
                HasScreenshot = website.HasScreenshot,
                ScreenshotExt = website.ScreenshotExt
            };
        }
    }
}
