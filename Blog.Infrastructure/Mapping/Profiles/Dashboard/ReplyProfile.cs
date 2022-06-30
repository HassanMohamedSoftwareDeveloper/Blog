using AutoMapper;
using Blog.Application.DTOS.Dashboard;
using Blog.Infrastructure.Helpers;
using Blog.Infrastructure.Persistence.Models.Read;

namespace Blog.Infrastructure.Mapping.Profiles.Dashboard;

public class ReplyProfile : Profile
{
    public ReplyProfile()
    {
        CreateMap<ReplyReadModel, ReplyDto>()
            .ForMember(dest => dest.ReplyDate, opt => opt.MapFrom(src => src.Created.ToString("dd MMMMM yyyy")))
             .ForMember(dest => dest.TimeAgo, opt => opt.MapFrom(src => TimeAgoHelper.Create(src.Created)));
    }
}
