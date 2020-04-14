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
        private readonly IRepositorySession _session;

        public WebSiteService(IWebSiteRepository webSiteRepository, IRepositorySession session)
        {
            _webSiteRepository = webSiteRepository;
            _session = session;
        }

        public async Task<int> CreateWebSite(WebSite webSite)
        {
            using var unitOfWork = _session.BeginWork();
            return await _webSiteRepository.Create(webSite);
        }

        public async Task DeleteWebSite(int webSiteId)
        {
            using var unitOfWork = _session.BeginWork();
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
            using var unitOfWork = _session.BeginWork();
            await _webSiteRepository.Update(webSite);
        }
    }
}
