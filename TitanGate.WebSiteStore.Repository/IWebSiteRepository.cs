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
        Task<IEnumerable<WebSite>> FindAll();
        Task<int> Create(WebSite webSite);
        Task<bool> Update(WebSite webSite);
        Task<(IEnumerable<WebSite> sites, int count)> FindByFilter(WebSiteSearchObject searchObject);
        Task<bool> Delete(int id);
    }
}
