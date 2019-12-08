using AutoMapper;
using MagicShortener.API.Inputs;
using MagicShortener.Logic.Commands.Links.CreateLink;

namespace MagicShortener.API.Infrastructure
{
    /// <summary>
    /// Основной профайл маппинга в сущности входных запросов
    /// </summary>
    public class BaseMappingProfile : Profile
    {
        public BaseMappingProfile()
        {
            CreateMap<CreateLinkRequest, CreateLinkCommand>()
                .ForMember(c => c.FullLink, opt => opt.MapFrom(r => r.Url));
        }
    }
}
