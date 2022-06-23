using AutoMapper;
using Blog.Application.DTOS.Dashboard;
using Blog.Infrastructure.Persistence.Models.Read;

namespace Blog.Infrastructure.Mapping.Profiles.Dashboard;

public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<CategoryReadModel, CategoryDto>()
            .ForMember(dest => dest.PostsCount, opt => opt.MapFrom(src => src.Posts.Count));
    }
}
