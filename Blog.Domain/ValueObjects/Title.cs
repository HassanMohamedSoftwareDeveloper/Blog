using Blog.Domain.Exceptions;

namespace Blog.Domain.ValueObjects;

public record Title
{
    public string Value { get; }
    public Title(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) throw new EmptyTitleException();
        Value = value;
    }

    public static implicit operator string(Title title) => title.Value;
    public static implicit operator Title(string value) => new(value);
}
