using AutoMapper;
using SiteGenerator.Domain.Entities;
using SiteGenerator.Domain.Models;
using SiteGenerator.Web.Contracts.Requests;
using SiteGenerator.Web.Contracts.Responses;

namespace SiteGenerator.Web.MappingProfiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Website, WebsiteModel>()
            .ForMember(x => x.Data, opt => opt.MapFrom(x => x.Data.ToDictionary()));
        CreateMap<WebsiteModel, WebsiteResponseModel>();
        CreateMap<WebsiteModel, WebsiteDetailedResponseModel>();
        CreateMap<CreateWebsiteRequest, CreateWebsiteModel>();
        CreateMap<UpdateWebsiteRequest, UpdateWebsiteModel>();

        CreateMap<News, NewsModel>();
        CreateMap<NewsModel, NewsResponseModel>();
        CreateMap<CreateNewsRequest, CreateNewsModel>();
    }
}