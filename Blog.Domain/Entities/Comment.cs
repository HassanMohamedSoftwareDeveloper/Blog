using Blog.Domain.ValueObjects;

namespace Blog.Domain.Entities;

public class Comment : CommentBase
{
    #region Fields :
    private List<Reply> _replies = new List<Reply>();
    #endregion

    #region CTORS :
    public Comment(Message message, UserId userId) : base(message, userId) { }
    #endregion

    #region Methods :
    public void AddReply(Reply reply) => _replies.Add(reply);
    #endregion
}
