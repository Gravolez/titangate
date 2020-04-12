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
        Task UpdateWebSite(WebSite webSite);
        Task DeleteWebSite(int webSiteId);
        Task<IList<WebSite>> GetAllWebsites();
        Task<IList<WebSite>> GetWebSites(WebSiteSearchObject searchObject);
    }
}