using Blog.Domain.Exceptions;

namespace Blog.Domain.ValueObjects;

public record Image
{
    public string Value { get; }
    public Image(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) throw new EmptyImageException();
        if (value.Split('.').Length != 2) throw new InvalidImageException();
        Value = value;
    }

    public static implicit operator string(Image image) => image.Value;
    public static implicit operator Image(string value) => new(value);
}
