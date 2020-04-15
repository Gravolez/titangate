using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task Delete(int id)
        {
            await _repositorySession.Connection.ExecuteAsync(
                @"UPDATE dbo.WebSite SET IsDeleted = 1 WHERE Id = @Id", 
                new { Id = id },
                _repositorySession.CurrentTransaction);
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
            return await _repositorySession.Connection.QueryAsync<WebSite>(
                @"SELECT t.* 
                FROM (
                    SELECT *, ROW_NUMBER() as RowNumber
                    FROM dbo.WebSite ws
                        INNER JOIN dbo.Category c ON c.id = ws.CategoryId
                     WHERE ws.IsDeleted = 0
                    ORDER BY @SortOrder
                ) AS t
                WHERE t.RowNumber BETWEEN @PageStart AND @PageEnd", 
                new { 
                    SortOrder = searchObject.SortExpression,
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

        public async Task Update(WebSite webSite)
        {
            await _repositorySession.Connection.ExecuteAsync(
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
