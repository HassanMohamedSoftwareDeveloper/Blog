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

    public static implicit operator List<string>(Tag tag) => tag.Value.Split(',').ToList();
    public static implicit operator Tag(List<string> tags) => new(string.Join(",", tags));
}
