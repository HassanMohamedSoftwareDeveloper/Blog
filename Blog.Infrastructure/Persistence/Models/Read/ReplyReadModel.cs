﻿namespace Blog.Infrastructure.Persistence.Models.Read;

internal class ReplyReadModel
{
    public Guid Id { get; set; }
    public string Message { get; set; }
    public string UserId { get; set; }
    public virtual UserReadModel User { get; set; }
}