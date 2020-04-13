using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TitanGate.WebSiteStore.Api.Mappers;
using TitanGate.WebSiteStore.Api.Models;
using TitanGate.WebSiteStore.Entities.DB;
using TitanGate.WebSiteStore.Services;

namespace TitanGate.WebSiteStore.Api.Controllers
{
    [Route("api/website")]
    [ApiController]
    public class WebSiteController : ControllerBase
    {
        private readonly IWebSiteService _webSiteService;
        private readonly IMapper<WebSiteModel, WebSite> _mapper;

        public WebSiteController(IWebSiteService webSiteService, IMapper<WebSiteModel, WebSite> mapper)
        {
            _webSiteService = webSiteService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get()
        {
            IList<WebSite> result = await _webSiteService.GetAllWebsites();
            return Ok(result.Select(x => _mapper.EntityToModel(x)).ToArray());
        }

        [HttpGet("{webSiteId}", Name = "Get")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int webSiteId)
        {
            WebSite result = await _webSiteService.GetWebSite(webSiteId);
            return Ok(_mapper.EntityToModel(result));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] WebSiteModel value)
        {
            WebSite webSite = _mapper.ModelToEntity(value);
            int id = await _webSiteService.CreateWebSite(webSite);
            return Ok(id);
        }

        [HttpPut("{webSiteId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put(int webSiteId, [FromBody] WebSiteModel value)
        {
            WebSite webSite = _mapper.ModelToEntity(value);
            await _webSiteService.UpdateWebSite(webSite);
            return Ok();
        }

        [HttpDelete("{webSiteId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int webSiteId)
        {
            await _webSiteService.DeleteWebSite(webSiteId);
            return Ok();
        }

        [HttpPost]
        [Route("page")]
        public async Task<IActionResult> GetPagedResults()
        {

        }
    }
}
