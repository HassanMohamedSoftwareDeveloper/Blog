using Blog.Domain.ValueObjects;
using Shared.Abstractions.Domain;

namespace Blog.Domain.Entities;

public abstract class CommentBase : IEntity<CommentId>
{
    #region PROPS :
    public CommentId Id { get; set; }
    #endregion

    #region Fields :
    private Message _message;
    private DateTime _created;
    private UserId _userId;
    #endregion

    #region CTORS :
    protected CommentBase()
    {

    }
    public CommentBase(CommentId commentId, Message message, UserId userId)
    {
        this.Id = commentId;
        this._message = message;
        this._userId = userId;
        this._created = DateTime.Now;
    }
    #endregion
}
