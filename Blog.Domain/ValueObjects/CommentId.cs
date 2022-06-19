using Blog.Domain.Exceptions;

namespace Blog.Domain.ValueObjects;

public record CommentId
{
    public Guid Value { get; }
    public CommentId(Guid value)
    {
        if (value == default || value == Guid.Empty) throw new EmptyCommentIdException();
        Value = value;
    }

    public static implicit operator Guid(CommentId body) => body.Value;
    public static implicit operator CommentId(Guid value) => new(value);
}
