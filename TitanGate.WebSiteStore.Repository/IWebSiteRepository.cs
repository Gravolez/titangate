using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TitanGate.WebSiteStore.Entities;
using TitanGate.WebSiteStore.Entities.DB;

namespace TitanGate.WebSiteStore.Repository
{
    public interface IWebSiteRepository
    {
        Task<WebSite> FindById(int id);
        Task<IList<WebSite>> FindAll();
        Task<int> Create(WebSite webSite);
        Task UpdateWebsite(WebSite webSite);
        Task<IList<WebSite>> FindByFilter(WebSiteSearchObject searchObject);
        Task Delete(int id);
    }
}
