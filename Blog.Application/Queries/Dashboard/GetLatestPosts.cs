﻿using Blog.Application.DTOS.Dashboard;
using MediatR;

namespace Blog.Application.Queries.Dashboard;

public record GetLatestPosts() : IRequest<List<LatestPostDto>>;
