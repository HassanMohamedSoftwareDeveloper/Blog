using Blog.Domain.Entities;
using Blog.Domain.Exceptions;
using Blog.Domain.ValueObjects;
using Shared.Abstractions.Domain;

namespace Blog.Domain.Aggregates;

public class Post : AggregateRoot<PostId>, IAggregateRoot<PostId>
{
    #region Fields :
    private Title _title;
    private Description _description;
    private Tag _tag;
    private Body _body;
    private Image _image;
    private UserId _userId;
    private CategoryId _categoryId;
    private DateTime _created;
    private DateTime? _updated;
    private List<Comment> _comments = new();
    #endregion

    #region CTORS :
    public Post(PostId postId, Title title, Description description, Tag tag, Body body, Image image, UserId userId, CategoryId categoryId)
        => SetPostData(postId, title, description, tag, body, image, userId, categoryId);
    #endregion

    #region Methods :
    public void AddComment(Comment comment) => _comments.Add(comment);
    public void AddReply(CommentId commentId, Reply reply)
    {
        var comment = _comments.Where(x => x.Id == commentId).SingleOrDefault();
        if (comment is null) throw new InvalidCommentId();
        comment.AddReply(reply);
    }
    #endregion

    #region Helpers :
    private void SetPostData(PostId postId, Title title, Description description, Tag tag, Body body, Image image, UserId userId, CategoryId categoryId)
    {
        this.Id = postId;
        this._title = title;
        this._description = description;
        this._tag = tag;
        this._body = body;
        this._image = image;
        this._userId = userId;
        this._categoryId = categoryId;
        this._created = DateTime.Now;
    }
    #endregion
}
