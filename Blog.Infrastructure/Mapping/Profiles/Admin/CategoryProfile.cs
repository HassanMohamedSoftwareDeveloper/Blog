using AutoMapper;
using Blog.Application.DTOS.Admin;
using Blog.Infrastructure.Persistence.Models.Read;

namespace Blog.Infrastructure.Mapping.Profiles.Admin;

public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<CategoryReadModel, CategoryDto>();
    }
}
