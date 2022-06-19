using Blog.Domain.ValueObjects;

namespace Blog.Domain.Entities;

public class Reply : CommentBase
{
    #region CTORS :
    public Reply(Message message, UserId userId) : base(message, userId) { }
    #endregion
}
