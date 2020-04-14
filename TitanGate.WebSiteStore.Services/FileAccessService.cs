using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TitanGate.WebSiteStore.Entities;

namespace TitanGate.WebSiteStore.Services
{
    public class FileAccessService : IFileAccessService
    {
        private readonly AppSettings _settings;

        public FileAccessService(IOptions<AppSettings> settings)
        {
            _settings = settings.Value;
        }

        public (string dir, string file) GetFileName(int id, FileCategoryEnum fileCategory, string extension)
        {
            const int totalLength = 6;
            const int chunkSize = 2;
            string paddedFileName = id.ToString().PadLeft(6, '0');
            IEnumerable<string> result = Enumerable.Range(0, totalLength / chunkSize)
                .Select(i => paddedFileName.Substring(i * chunkSize, chunkSize));
            var fileName = Path.Combine(result.ToArray()) + extension;

            var filePath = Path.Combine(
                _settings.BaseAppFolder, 
                _settings.BaseFilesFolder, 
                Enum.GetName(typeof(FileCategoryEnum), fileCategory), 
                fileName);
            return (Path.GetDirectoryName(filePath), Path.GetFileName(filePath));
        }
    }
}
