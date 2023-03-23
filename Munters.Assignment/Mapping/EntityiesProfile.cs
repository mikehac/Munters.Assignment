using AutoMapper;
using Munters.Assignment.DTOs;
using Munters.Assignment.Models;

namespace Munters.Assignment.Mapping
{
    public class EntityiesProfile : Profile
    {
        public EntityiesProfile()
        {
            CreateMap<GiphyResponse, GiphyResponseDTO>();

            CreateMap<SingleGif, SingleGifDTO>()
                .ForMember(c => c.Title, o => o.MapFrom(x => x.title))
                .ForMember(c => c.SiteUrl, o => o.MapFrom(x => x.url))
                .ForMember(c => c.ImageUrl, o => o.MapFrom(x => x.images.downsized.url));
        }
    }
}
