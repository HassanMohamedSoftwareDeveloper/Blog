using Blog.Domain.ValueObjects;

namespace Blog.Domain.Entities;

public class Comment : CommentBase
{
    #region Fields :
    private List<Reply> _replies = new();
    #endregion

    #region CTORS :
    private Comment() : base()
    {

    }
    public Comment(CommentId commentId, Message message, UserId userId) : base(commentId, message, userId) { }
    #endregion

    #region Methods :
    public void AddReply(Reply reply) => _replies.Add(reply);
    #endregion
}
