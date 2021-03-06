using Blog.Domain.ValueObjects;

namespace Blog.Domain.Entities;

public class Reply : CommentBase
{
    #region CTORS :
    private Reply() : base()
    {

    }
    public Reply(CommentId replyId, Message message, UserId userId) : base(replyId, message, userId) { }
    #endregion
}
