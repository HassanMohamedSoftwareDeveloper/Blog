﻿using MediatR;

namespace Blog.Application.Commands;

public record UpdatePost(Guid Id, string Title, string Description, string Tags, string Body, string ImageSourcePath, string UserId, Guid CategoryId)
    : IRequest<bool>;
