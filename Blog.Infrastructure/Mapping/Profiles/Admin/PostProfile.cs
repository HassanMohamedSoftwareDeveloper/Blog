using AutoMapper;
using Blog.Application.DTOS.Admin;
using Blog.Infrastructure.Persistence.Models.Read;

namespace Blog.Infrastructure.Mapping.Profiles.Admin
{
    public class PostProfile : Profile
    {
        public PostProfile()
        {
            CreateMap<PostReadModel, PostDto>()
                .ForMember(dest => dest.CurrentImagePath, opt => opt.MapFrom(src => src.Image));

            CreateMap<PostReadModel, UserPostDto>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.Name));
        }
    }
}
