using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TitanGate.WebSiteStore.Entities;

namespace TitanGate.WebSiteStore.Services.Interface
{
    public interface IFileAccessService
    {
        string GetFileName(int id, FileCategoryEnum fileCategory, string extension);
    }
}
