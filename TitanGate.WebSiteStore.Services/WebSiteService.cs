using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using TitanGate.WebSiteStore.Entities;
using TitanGate.WebSiteStore.Entities.DB;
using TitanGate.WebSiteStore.Entities.Exceptions;
using TitanGate.WebSiteStore.Repository;

namespace TitanGate.WebSiteStore.Services
{
    public class WebSiteService : IWebSiteService
    {
        private readonly IWebSiteRepository _webSiteRepository;
        private readonly IRepositorySession _session;
        private readonly IFileAccessService _fileAccessService;

        public WebSiteService(IWebSiteRepository webSiteRepository, IRepositorySession session, IFileAccessService fileAccessService)
        {
            _webSiteRepository = webSiteRepository;
            _session = session;
            _fileAccessService = fileAccessService;
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

        public async Task UploadFile(int webSiteId, byte[] file, string extension)
        {
            using var unitOFWork = _session.BeginWork();
            WebSite webSite = await _webSiteRepository.FindById(webSiteId);
            (string dir, string fileName) = _fileAccessService.GetFileName(webSiteId, FileCategoryEnum.WebsiteScreenshot, extension);
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            await File.WriteAllBytesAsync(Path.Combine(dir, fileName), file);
            webSite.ScreenshotExt = extension;
            webSite.HasScreenshot = true;
            await _webSiteRepository.Update(webSite);
            unitOFWork.Persist();
        }

        public async Task<byte[]> DownloadFile(int webSiteId)
        {
            using var unitOFWork = _session.BeginWork();
            WebSite webSite = await _webSiteRepository.FindById(webSiteId);
            if (!webSite.HasScreenshot)
            {
                throw new WebSiteStoreException("No screenshot for this site");
            }
            (string dir, string fileName) = _fileAccessService.GetFileName(webSiteId, FileCategoryEnum.WebsiteScreenshot, webSite.ScreenshotExt);
            return await File.ReadAllBytesAsync(Path.Combine(dir, fileName));
        }
    }
}
