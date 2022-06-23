﻿using AutoMapper;
using Blog.Application.DTOS.Dashboard;
using Blog.Infrastructure.Persistence.Models.Read;

namespace Blog.Infrastructure.Mapping.Profiles.Dashboard;

public class CommentProfile : Profile
{
    public CommentProfile()
    {
        CreateMap<CommentReadModel, CommentDto>()
            .ForMember(dest => dest.CommentDate, opt => opt.MapFrom(src => src.Created.ToString("dd MMMMM yyyy")));
    }
}
