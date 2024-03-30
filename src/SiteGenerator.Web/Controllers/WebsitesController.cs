using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SiteGenerator.Domain.Abstractions;
using SiteGenerator.Domain.Models;
using SiteGenerator.Web.Contracts.Requests;
using SiteGenerator.Web.Contracts.Responses;

namespace SiteGenerator.Web.Controllers
{
    /// <summary>
    /// Методы взаимодействия с вебсайтами
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class WebsitesController : ControllerBase
    {
        private readonly IWebsiteService _websites;
        private readonly IMapper _mapper;

        public WebsitesController(IWebsiteService websites, IMapper mapper)
        {
            _websites = websites;
            _mapper = mapper;
        }

        /// <summary>
        /// Метод получения списка всех существующих вебсайтов
        /// </summary>
        /// <param name="pageNumber">номер страницы</param>
        /// <param name="pageSize">размер страницы</param>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WebsiteResponseModel>>> GetAll(int? pageNumber = 0, int? pageSize = 5, CancellationToken cancellationToken = default)
        {
            var websites = await _websites.GetList(pageNumber.Value, pageSize.Value, cancellationToken);
            return Ok(_mapper.Map<IEnumerable<WebsiteResponseModel>>(websites));
        }

        /// <summary>
        /// Метод получения сайта по alias
        /// </summary>
        [HttpGet("{alias}")]
        public async Task<ActionResult<WebsiteDetailedResponseModel>> GetById(string alias, CancellationToken cancellationToken)
        {
            var website = await _websites.GetWebsiteByAlias(alias, cancellationToken);
            return Ok(_mapper.Map<WebsiteDetailedResponseModel>(website));
        }
    
        /// <summary>
        /// Метод создания нового вебсайта
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<string>> Create(CreateWebsiteRequest request, CancellationToken cancellationToken)
        {
            var model = _mapper.Map<CreateWebsiteModel>(request);
            var id = await _websites.CreateNewSite(model, cancellationToken);

            return Created(string.Empty, id);
        }
        
        /// <summary>
        /// Метод обновления вебсайта
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult> Update(UpdateWebsiteRequest request, CancellationToken cancellationToken)
        {
            var model = _mapper.Map<UpdateWebsiteModel>(request);
            model.Data = request.Data;
            var website = await _websites.UpdateWebsiteData(model, cancellationToken);
            return website ? Ok() : NotFound();
        }

        /// <summary>
        /// Метод удаления вебсайта
        /// </summary>
        [HttpDelete("{alias}")]
        public async Task<ActionResult> Delete(string alias, CancellationToken cancellationToken)
        {
            await _websites.DeleteWebsiteByAlias(alias, cancellationToken);
            return Ok();
        }
    }
}
