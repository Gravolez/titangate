using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TitanGate.WebSiteStore.Api.Models
{
    [Serializable]
    public enum SortFieldModelEnum
    {
        Name = 1,
        Url,
        Category,
        Email,
        Password
    }
}
