﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TitanGate.WebSiteStore.Api.Mappers;
using TitanGate.WebSiteStore.Api.Models;
using TitanGate.WebSiteStore.Entities;
using TitanGate.WebSiteStore.Entities.DB;
using TitanGate.WebSiteStore.Services;

namespace TitanGate.WebSiteStore.Api.Controllers
{
    [Route("api/website")]
    [ApiController]
    public class WebSiteController : ControllerBase
    {
        private readonly IWebSiteService _webSiteService;
        private readonly IMapper<WebSiteModel, WebSite> _webSiteMapper;
        private readonly IMapper<SearchObjectModel, WebSiteSearchObject> _searchObjectMapper;

        public WebSiteController(
            IWebSiteService webSiteService,
            IMapper<WebSiteModel, WebSite> webSiteMapper,
            IMapper<SearchObjectModel, WebSiteSearchObject> searchObjectMapper)
        {
            _webSiteService = webSiteService;
            _webSiteMapper = webSiteMapper;
            _searchObjectMapper = searchObjectMapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get()
        {
            IEnumerable<WebSite> result = await _webSiteService.GetAllWebsites();
            return Ok(result.Select(x => _webSiteMapper.EntityToModel(x)).ToArray());
        }

        [HttpGet("{webSiteId}", Name = "Get")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int webSiteId)
        {
            WebSite result = await _webSiteService.GetWebSite(webSiteId);
            return Ok(_webSiteMapper.EntityToModel(result));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] WebSiteModel value)
        {
            WebSite webSite = _webSiteMapper.ModelToEntity(value);
            int id = await _webSiteService.CreateWebSite(webSite);
            return Ok(id);
        }

        [HttpPut("{webSiteId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put(int webSiteId, [FromBody] WebSiteModel value)
        {
            WebSite webSite = _webSiteMapper.ModelToEntity(value);
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
        public async Task<IActionResult> GetPagedResults([FromBody] SearchObjectModel searchObjectModel)
        {
            IEnumerable<WebSite> result = await _webSiteService.GetWebSites(_searchObjectMapper.ModelToEntity(searchObjectModel));
            return Ok(result.Select(x => _webSiteMapper.EntityToModel(x)).ToArray());
        }

        [HttpPost()]
        [Route("{webSiteId}/screenshot")]
        public async Task<IActionResult> UploadScreenshot([FromRoute]int webSiteId, [FromForm] IFormFile screenshot)
        {
            using (var memoryStream = new MemoryStream())
            {
                await screenshot.CopyToAsync(memoryStream);
                byte[] fileContents = memoryStream.ToArray();
                await _webSiteService.UploadFile(webSiteId, fileContents, Path.GetExtension(screenshot.FileName));
            }
            return Ok();
        }

        [HttpGet()]
        [Route("{webSiteId}/screenshot")]
        public async Task<IActionResult> DownloadScreenshot([FromRoute]int webSiteId)
        {
            byte[] file = await _webSiteService.DownloadFile(webSiteId);
            return File(file, "application/octet-stream");
        }
    }
}
