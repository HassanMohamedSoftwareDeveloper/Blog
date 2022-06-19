using Blog.Domain.Exceptions;

namespace Blog.Domain.ValueObjects;

public record UserId
{
    public string Value { get; }
    public UserId(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) throw new EmptyUserIdException();
        Value = value;
    }

    public static implicit operator string(UserId userId) => userId.Value;
    public static implicit operator UserId(string value) => new(value);
}
