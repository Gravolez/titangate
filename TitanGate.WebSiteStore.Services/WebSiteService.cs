using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TitanGate.WebSiteStore.Entities;
using TitanGate.WebSiteStore.Entities.DB;
using TitanGate.WebSiteStore.Repository;

namespace TitanGate.WebSiteStore.Services
{
    public class WebSiteService : IWebSiteService
    {
        private readonly IWebSiteRepository _webSiteRepository;

        public WebSiteService(IWebSiteRepository webSiteRepository)
        {
            _webSiteRepository = webSiteRepository;
        }

        public async Task<int> CreateWebSite(WebSite webSite)
        {
            return await _webSiteRepository.Create(webSite);
        }

        public async Task DeleteWebSite(int webSiteId)
        {
            await _webSiteRepository.Delete(webSiteId);
        }

        public async Task<IEnumerable<WebSite>> GetAllWebsites()
        {
            return await _webSiteRepository.FindAll();
        }

        public async Task<WebSite> GetWebSite(int webSiteId)
        {
            return await _webSiteRepository.FindById(webSiteId);
        }

        public async Task<IEnumerable<WebSite>> GetWebSites(WebSiteSearchObject searchObject)
        {
            return await _webSiteRepository.FindByFilter(searchObject);
        }

        public async Task UpdateWebSite(WebSite webSite)
        {
            await _webSiteRepository.Update(webSite);
        }
    }
}
