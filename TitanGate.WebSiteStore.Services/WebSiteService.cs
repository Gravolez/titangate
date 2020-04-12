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
            return await _webSiteRepository.
        }

        public async Task<IList<WebSite>> GetAllWebsites()
        {
            throw new NotImplementedException();
        }

        public async Task<WebSite> GetWebSite(int webSiteId)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<WebSite>> GetWebSites(WebSiteSearchObject searchObject)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateWebSite(WebSite webSite)
        {
            throw new NotImplementedException();
        }
    }
}
