using Blog.Domain.Exceptions;

namespace Blog.Domain.ValueObjects;

public record LikeId
{
    public Guid Value { get; }
    public LikeId(Guid value)
    {
        if (value == default || value == Guid.Empty) throw new EmptyLikeIdException();
        Value = value;
    }

    public static implicit operator Guid(LikeId body) => body.Value;
    public static implicit operator LikeId(Guid value) => new(value);
}
