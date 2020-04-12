using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TitanGate.WebSiteStore.Entities;
using TitanGate.WebSiteStore.Entities.DB;
using TitanGate.WebSiteStore.Repository;

namespace TitanGate.WebSiteStore.RepositoryImpl
{
    public class WebSiteRepository : IWebSiteRepository
    {
        public Task<int> Create(WebSite webSite)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IList<WebSite>> FindAll()
        {
            throw new NotImplementedException();
        }

        public Task<IList<WebSite>> FindByFilter(WebSiteSearchObject searchObject)
        {
            throw new NotImplementedException();
        }

        public Task<WebSite> FindById(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateWebsite(WebSite webSite)
        {
            throw new NotImplementedException();
        }
    }
}
