using Blog.Domain.Exceptions;

namespace Blog.Domain.ValueObjects;

public record Body
{
    public string Value { get; }
    public Body(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) throw new EmptyBodyException();
        Value = value;
    }

    public static implicit operator string(Body body) => body.Value;
    public static implicit operator Body(string value) => new(value);
}
