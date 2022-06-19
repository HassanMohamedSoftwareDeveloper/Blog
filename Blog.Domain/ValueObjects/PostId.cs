using Blog.Domain.Exceptions;

namespace Blog.Domain.ValueObjects;

public record PostId
{
    public Guid Value { get; }
    public PostId(Guid value)
    {
        if (value == default || value == Guid.Empty) throw new EmptyPostIdException();
        Value = value;
    }

    public static implicit operator Guid(PostId postId) => postId.Value;
    public static implicit operator PostId(Guid value) => new(value);
}
