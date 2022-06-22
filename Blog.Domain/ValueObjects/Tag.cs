using Blog.Domain.Exceptions;

namespace Blog.Domain.ValueObjects;

public record Tag
{
    public string Value { get; }
    public Tag(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) throw new EmptyTagException();
        Value = value;
    }

    public static implicit operator string(Tag tag) => tag.Value;
    public static implicit operator Tag(string tags) => new(tags);
}
