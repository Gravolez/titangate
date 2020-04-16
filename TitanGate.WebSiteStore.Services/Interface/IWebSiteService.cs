using System.Collections.Generic;
using System.Threading.Tasks;
using TitanGate.WebSiteStore.Entities;
using TitanGate.WebSiteStore.Entities.DB;

namespace TitanGate.WebSiteStore.Services
{
    public interface IWebSiteService
    {
        Task<WebSite> GetWebSite(int webSiteId);
        Task<int> CreateWebSite(WebSite webSite);
        Task<bool> UpdateWebSite(WebSite webSite);
        Task<bool> DeleteWebSite(int webSiteId);
        Task<IEnumerable<WebSite>> GetAllWebsites();
        Task<(IEnumerable<WebSite> webSites, int count)> GetWebSites(WebSiteSearchObject searchObject);
        Task UploadFile(int webSiteId, byte[] file, string extension);
        Task<(byte[] contents, string extension)> DownloadFile(int webSiteId);
    }
}