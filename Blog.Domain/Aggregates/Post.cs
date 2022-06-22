using Blog.Domain.Entities;
using Blog.Domain.Exceptions;
using Blog.Domain.ValueObjects;
using Shared.Abstractions.Domain;

namespace Blog.Domain.Aggregates;

public class Post : AggregateRoot<PostId>, IAggregateRoot<PostId>
{
    #region PROPS :
    public Image Image { get; set; }
    #endregion

    #region Fields :
    private Title _title;
    private Description _description;
    private Tag _tag;
    private Body _body;
    private UserId _userId;
    private CategoryId _categoryId;
    private DateTime _created;
    private DateTime? _updated;
    private int _viewersCount;
    private List<Comment> _comments = new();
    #endregion

    #region CTORS :
    public Post(PostId postId, Title title, Description description, Tag tag, Body body, Image image, UserId userId, CategoryId categoryId)
    {
        this.Id = postId;
        SetPostData(title, description, tag, body, image, userId, categoryId);
        this._created = DateTime.Now;
    }
    #endregion

    #region Methods :
    public void Update(Title title, Description description, Tag tag, Body body, Image image, UserId userId, CategoryId categoryId)
    {
        SetPostData(title, description, tag, body, image, userId, categoryId);
        this._updated = DateTime.Now;
    }
    public void AddComment(Comment comment) => _comments.Add(comment);
    public void AddReply(CommentId commentId, Reply reply)
    {
        var comment = _comments.Where(x => x.Id == commentId).SingleOrDefault();
        if (comment is null) throw new InvalidCommentId();
        comment.AddReply(reply);
    }
    public void AddViewer() => _viewersCount += 1;
    #endregion

    #region Helpers :
    private void SetPostData(Title title, Description description, Tag tag, Body body, Image image, UserId userId, CategoryId categoryId)
    {
        this._title = title;
        this._description = description;
        this._tag = tag;
        this._body = body;
        this.Image = image;
        this._userId = userId;
        this._categoryId = categoryId;
    }
    #endregion
}
