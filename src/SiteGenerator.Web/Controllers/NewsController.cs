using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SiteGenerator.Domain.Abstractions;
using SiteGenerator.Domain.Entities;
using SiteGenerator.Domain.Models;
using SiteGenerator.Web.Contracts.Requests;
using SiteGenerator.Web.Contracts.Responses;

namespace SiteGenerator.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly INewsService _news;
        private readonly IMapper _mapper;

        public NewsController(INewsService news, IMapper mapper)
        {
            _news = news;
            _mapper = mapper;
        }

        /// <summary>
        /// Метод получения списка всех существующих новостей
        /// </summary>
        /// <param name="pageNumber">номер страницы</param>
        /// <param name="pageSize">размер страницы</param>
        /// <param name="alias">alias сайта</param>
        [HttpGet("{alias}")]
        public async Task<ActionResult<IEnumerable<NewsResponseModel>>> GetAll(string alias, int? pageNumber = 0, int? pageSize = 5, CancellationToken cancellationToken = default)
        {
            var news = await _news.GetList(alias, pageNumber.Value, pageSize.Value, cancellationToken);
            return Ok(_mapper.Map<IEnumerable<NewsResponseModel>>(news));
        }

        /// <summary>
        /// Метод создания новости
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<NewsResponseModel>> CreateNews(CreateNewsRequest request, CancellationToken cancellationToken)
        {
            var model = _mapper.Map<CreateNewsModel>(request);
            var news = await _news.CreateNews(model, cancellationToken);

            return Ok(_mapper.Map<NewsResponseModel>(news));
        }
    }
}
