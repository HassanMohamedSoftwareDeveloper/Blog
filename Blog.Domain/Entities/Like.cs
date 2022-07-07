using Blog.Domain.ValueObjects;
using Shared.Abstractions.Domain;

namespace Blog.Domain.Entities;

public class Like : IEntity<LikeId>
{
    #region PROPS :
    public LikeId Id { get; set; }
    #endregion

    #region Fields :
    private DateTime _created;
    internal UserId _userId;
    #endregion

    #region CTORS :
    private Like() { }
    public Like(LikeId likeId, UserId userId)
    {
        this.Id = likeId;
        this._userId = userId;
        this._created = DateTime.Now;
    }
    #endregion
}
