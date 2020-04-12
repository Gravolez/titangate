using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TitanGate.WebSiteStore.Api.Mappers
{
    public interface IMapper<MODEL, ENTITY>
    {
        MODEL EntityToModel(ENTITY entity);
        ENTITY ModelToEntity(MODEL model);
    }
}
