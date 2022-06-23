using AutoMapper;
using Blog.Application.DTOS.Dashboard;
using Blog.Infrastructure.Persistence.Models.Read;

namespace Blog.Infrastructure.Mapping.Profiles.Dashboard;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserReadModel, UserDto>()
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => string.Join(" ", src.FirstName, src.LastName)));
    }
}
