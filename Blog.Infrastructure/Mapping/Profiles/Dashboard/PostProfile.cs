using AutoMapper;
using Blog.Application.DTOS.Dashboard;
using Blog.Infrastructure.Helpers;
using Blog.Infrastructure.Persistence.Models.Read;

namespace Blog.Infrastructure.Mapping.Profiles.Dashboard;

public class PostProfile : Profile
{
    public PostProfile()
    {
        CreateMap<PostReadModel, BlogPostDto>()
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.Name))
            .ForMember(dest => dest.PostDate, opt => opt.MapFrom(src => src.Created.ToString("dd MMMMM yyyy")))
            .ForMember(dest => dest.TimeAgo, opt => opt.MapFrom(src => TimeAgoHelper.Create(src.Created)))
            .ForMember(dest => dest.CommentsCount, opt => opt.MapFrom(src => src.Comments.Count));

        CreateMap<PostReadModel, LatestPostDto>()
            .ForMember(dest => dest.ViewersCount, opt => opt.MapFrom(src => src.ViewersCount))
            .ForMember(dest => dest.CommentsCount, opt => opt.MapFrom(src => src.Comments.Count))
            .ForMember(dest => dest.PostDate, opt => opt.MapFrom(src => src.Created.ToString("dd MMMMM yyyy")));

        CreateMap<PostReadModel, NextPrevPostDto>();

        CreateMap<PostReadModel, PostDto>()
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.Name))
            .ForMember(dest => dest.PostDate, opt => opt.MapFrom(src => src.Created.ToString("dd MMMMM yyyy")))
            .ForMember(dest => dest.TimeAgo, opt => opt.MapFrom(src => TimeAgoHelper.Create(src.Created)))
            .ForMember(dest => dest.CommentsCount, opt => opt.MapFrom(src => src.Comments.Count))
            .ForMember(dest => dest.TagsList, opt => opt.Ignore());

        CreateMap<PostReadModel, SearchDto>();
    }


}
